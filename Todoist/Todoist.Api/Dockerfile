#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Todoist.Api/Todoist.Api.csproj", "Todoist.Api/"]
COPY ["Todoist.Application/Todoist.Application.csproj", "Todoist.Application/"]
COPY ["Todoist.Domain/Todoist.Domain.csproj", "Todoist.Domain/"]
COPY ["Todoist.Infrastructure/Todoist.Infrastructure.csproj", "Todoist.Infrastructure/"]
RUN dotnet restore "./Todoist.Api/./Todoist.Api.csproj"
COPY . .
WORKDIR "/src/Todoist.Api"
RUN dotnet build "./Todoist.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Todoist.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todoist.Api.dll"]