FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ContratacaoService/Seguro.Hexagonal.Contratacao.Api/Seguro.Hexagonal.Contratacao.Api.csproj", "ContratacaoService/Seguro.Hexagonal.Contratacao.Api/"]
COPY ["ContratacaoService/Seguro.Hexagonal.Contratacao.Application/Seguro.Hexagonal.Contratacao.Application.csproj", "ContratacaoService/Seguro.Hexagonal.Contratacao.Application/"]
COPY ["ContratacaoService/Seguro.Hexagonal.Contratacao.Domain/Seguro.Hexagonal.Contratacao.Domain.csproj", "ContratacaoService/Seguro.Hexagonal.Contratacao.Domain/"]
COPY ["ContratacaoService/Seguro.Hexagonal.Contratacao.Data/Seguro.Hexagonal.Contratacao.Data.csproj", "ContratacaoService/Seguro.Hexagonal.Contratacao.Data/"]
COPY ["ContratacaoService/Seguro.Hexagonal.Contratacao.IoC/Seguro.Hexagonal.Contratacao.IoC.csproj", "ContratacaoService/Seguro.Hexagonal.Contratacao.IoC/"]

RUN dotnet restore "./ContratacaoService/Seguro.Hexagonal.Contratacao.Api/Seguro.Hexagonal.Contratacao.Api.csproj"

COPY . .
WORKDIR "/src/ContratacaoService/Seguro.Hexagonal.Contratacao.Api"

RUN dotnet build "./Seguro.Hexagonal.Contratacao.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "./Seguro.Hexagonal.Contratacao.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Seguro.Hexagonal.Contratacao.Api.dll"]
