FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["PropostaService/Seguro.Hexagonal.Proposta.Api/Seguro.Hexagonal.Proposta.Api.csproj", "PropostaService/Seguro.Hexagonal.Proposta.Api/"]
COPY ["PropostaService/Seguro.Hexagonal.Proposta.Application/Seguro.Hexagonal.Proposta.Application.csproj", "PropostaService/Seguro.Hexagonal.Proposta.Application/"]
COPY ["PropostaService/Seguro.Hexagonal.Proposta.Domain/Seguro.Hexagonal.Proposta.Domain.csproj", "PropostaService/Seguro.Hexagonal.Proposta.Domain/"]
COPY ["PropostaService/Seguro.Hexagonal.Proposta.Data/Seguro.Hexagonal.Proposta.Data.csproj", "PropostaService/Seguro.Hexagonal.Proposta.Data/"]
COPY ["PropostaService/Seguro.Hexagonal.Proposta.IoC/Seguro.Hexagonal.Proposta.IoC.csproj", "PropostaService/Seguro.Hexagonal.Proposta.IoC/"]

RUN dotnet restore "./PropostaService/Seguro.Hexagonal.Proposta.Api/Seguro.Hexagonal.Proposta.Api.csproj"

COPY . .
WORKDIR "/src/PropostaService/Seguro.Hexagonal.Proposta.Api"

RUN dotnet build "./Seguro.Hexagonal.Proposta.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "./Seguro.Hexagonal.Proposta.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Seguro.Hexagonal.Proposta.Api.dll"]
