FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AutoHub.API/AutoHub.API.csproj", "AutoHub.API/"]
COPY ["AutoHub.BusinessLogic/AutoHub.BusinessLogic.csproj", "AutoHub.BusinessLogic/"]
COPY ["AutoHub.DataAccess/AutoHub.DataAccess.csproj", "AutoHub.DataAccess/"]
COPY ["AutoHub.Domain/AutoHub.Domain.csproj", "AutoHub.Domain/"]
RUN dotnet restore "AutoHub.API/AutoHub.API.csproj"
COPY . .
WORKDIR "/src/AutoHub.API"
RUN dotnet build "AutoHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AutoHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoHub.API.dll"]
