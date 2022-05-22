#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM node:14-alpine as node
WORKDIR /src
COPY src/WebApi/ClientApp .
RUN npm install && npm run build

FROM base AS finalnode
WORKDIR /app
COPY --from=node /wwwroot ../wwwroot

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebApi/WebApi.csproj", "src/WebApi/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
RUN dotnet restore src/WebApi/WebApi.csproj
COPY . .
WORKDIR /src/src/WebApi
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=node /wwwroot ./wwwroot
COPY --from=node /wwwroot ../wwwroot
ENTRYPOINT ["dotnet", "WebApi.dll"]