# ==========================================
# STAGE 1: RUNTIME BASE ENGINE
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
ARG APP_UID=app
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ==========================================
# STAGE 2: COMPILED BUILD ENVIRONMENT (SDK)
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Copy project files individually to leverage Docker layer caching
COPY ["App.API/App.API.csproj", "App.API/"]
COPY ["App.Application/App.Application.csproj", "App.Application/"]
COPY ["App.Infrastructure/App.Infrastructure.csproj", "App.Infrastructure/"]
COPY ["App.Domain/App.Domain.csproj", "App.Domain/"]

# 2. Run NuGet restore across the dependency tree
RUN dotnet restore "App.API/App.API.csproj"

# 3. Copy the remaining source files and compile
COPY . .
WORKDIR "/src/App.API"
RUN dotnet build "App.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ==========================================
# STAGE 3: PUBLISH TO PRODUCTION ASSETS
# ==========================================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "App.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ==========================================
# STAGE 4: COMBINE RUNTIME & COMPILED ASSETS
# ==========================================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.API.dll"]
