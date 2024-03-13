FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /src
COPY Store.sln ./
COPY Store.API/*.csproj ./Store.API/
COPY Store.Infrastructure/*.csproj ./Store.Infrastructure/
COPY Store.UnitTests/*.csproj ./Store.UnitTests/
COPY Store.SpecflowBDD.AutomationTest/*.csproj ./Store.SpecflowBDD.AutomationTest/
COPY Store.Ordering.Service/*.csproj ./Store.Ordering.Service/

RUN dotnet restore
COPY . .
WORKDIR /src/Store.API
RUN dotnet build -c Release -o /app

WORKDIR /src/Store.Infrastructure
RUN dotnet build -c Release -o /app

WORKDIR /src/Store.Ordering.Service
RUN dotnet build -c Release -o /app

FROM build-env AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Store.API.dll"]