FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development 
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
COPY src/DemoBookApp.Api/DemoBookApp.Api.csproj DemoBookApp.Api/
COPY src/DemoBookApp.Application/DemoBookApp.Application.csproj DemoBookApp.Application/
COPY src/DemoBookApp.Core/DemoBookApp.Core.csproj DemoBookApp.Core/
COPY src/DemoBookApp.Contracts/DemoBookApp.Contracts.csproj DemoBookApp.Contracts/
COPY src/DemoBookApp.Infrastructure/DemoBookApp.Infrastructure.csproj DemoBookApp.Infrastructure/
RUN dotnet restore "DemoBookApp.Api/DemoBookApp.Api.csproj"

COPY src/ ./
WORKDIR /app/DemoBookApp.Api
RUN dotnet build "DemoBookApp.Api.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
EXPOSE 7070
EXPOSE 7071
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoBookApp.Api.dll"]