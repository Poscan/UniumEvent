using Application.Contracts;
using Application.Dto;
using Application.Wrappers;
using Domain.Common;
using Domain.Common.Repositories;
using Domain.Entities;
using MapsterMapper;

namespace Application.Services;

public class ClientService : IClientService
{
    public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork, IMapper mapper,
        ICurrentUser currentUser)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public async Task<Response<IEnumerable<ClientDto>>> FindUserAsync(string searchString)
    {
        var clients = await _clientRepository.FindClientsAsync(searchString);

        var clientsDto = _mapper.Map<IEnumerable<ClientDto>>(clients);
        return Response.Success(clientsDto);
    }

    public async Task<Response<ClientDto>> GetByUserIdAsync(CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        if (userId == null)
            return Response.Fail<ClientDto>(new ResponseError("Не удалось найти текущего пользователя"));

        var client = await _clientRepository.GetByUserIdAsync(userId, cancellationToken);
        var clientDto = _mapper.Map<ClientDto>(client);
        return Response.Success(clientDto);
    }

    public async Task<Response<ClientDto>> GetAsync(int clientId, CancellationToken cancellationToken)
    {
        try
        {
            var foundClient = await _clientRepository.GetAsync(clientId, cancellationToken);
            if (foundClient == null)
                throw new Exception("НЕ удалось найти пользователя");
            
            var clientDto = _mapper.Map<ClientDto>(foundClient);

            return Response.Success(clientDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Response<IEnumerable<ClientDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            var foundClients = await _clientRepository.GetAllAsync(cancellationToken);
            var clientsDto = _mapper.Map<IEnumerable<ClientDto>>(foundClients);

            return Response.Success(clientsDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<int>> SaveAsync(SaveClientRequest request, CancellationToken cancellationToken)
    {
        var clientForSave = _mapper.Map<Client>(request);
        if (clientForSave.Id == 0)
        {
            await _clientRepository.SaveAsync(clientForSave, cancellationToken);    
        }
        else
        {
            _clientRepository.Update(clientForSave);
        }
        await _unitOfWork.CommitAsync(cancellationToken);

        return Response.Success(clientForSave.Id);
    }

    public async Task<Response<int>> CreateNew(ClientDto clientDto)
    {
        try
        {
            var clientForSave = _mapper.Map<Client>(clientDto);
            var client = await _clientRepository.CreateNew(clientForSave);
            await _unitOfWork.CommitAsync();

            return Response.Success(client.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Response.Fail<int>(new ResponseError(e.Message));
        }
    }
}