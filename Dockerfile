#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM node:16-alpine as node
WORKDIR /src
COPY src/WebApi/ClientApp .
RUN npm install  && npm run build --prod

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
ENV default=Server=ec2-3-217-219-146.compute-1.amazonaws.com;Port=5432;Database=daqt4ghivqrea3;Username=tyutubixvpegee;Password=420000d402bb54e446c4a08351b4221310520de112aba9e4ddde478d6d6319fb

COPY --from=publish /app/publish .
COPY --from=node /wwwroot .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet WebApi.dll