# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy Shared projects first
COPY Shared/Shared.Domain/Shared.Domain.csproj Shared/Shared.Domain/
COPY Shared/Shared.Application/Shared.Application.csproj Shared/Shared.Application/
COPY Shared/Shared.Infrastructure/Shared.Infrastructure.csproj Shared/Shared.Infrastructure/

# Copy Shop project files
COPY Shop/Shop.Domain/Shop.Domain.csproj Shop/Shop.Domain/
COPY Shop/Shop.Contracts/Shop.Contracts.csproj Shop/Shop.Contracts/
COPY Shop/Shop.Application/Shop.Application.csproj Shop/Shop.Application/
COPY Shop/Shop.Infrastructure/Shop.Infrastructure.csproj Shop/Shop.Infrastructure/
COPY Shop/Shop.Api/Shop.Api.csproj Shop/Shop.Api/

# Restore dependencies
WORKDIR /src/Shop/Shop.Api
RUN dotnet restore

# Copy all source code
WORKDIR /src
COPY Shop/ Shop/
COPY Shared/ Shared/

# Publish the application
WORKDIR /src/Shop/Shop.Api
RUN dotnet publish \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS runtime
WORKDIR /app

# Optional: security best practice
RUN addgroup -S appgroup && adduser -S appuser -G appgroup
USER appuser

COPY --from=build /app/publish .

# ASP.NET Core default port
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Shop.Api.dll"]
