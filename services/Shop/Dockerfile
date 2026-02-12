# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file and all project files
COPY Shop.Domain/Shop.Domain.csproj Shop.Domain/
COPY Shop.Contracts/Shop.Contracts.csproj Shop.Contracts/
COPY Shop.Application/Shop.Application.csproj Shop.Application/
COPY Shop.Infrastructure/Shop.Infrastructure.csproj Shop.Infrastructure/
COPY Shop.Api/Shop.Api.csproj Shop.Api/

# Restore dependencies
WORKDIR /src/Shop.Api
RUN dotnet restore

# Copy all source code
WORKDIR /src
COPY . .

# Publish the application
WORKDIR /src/Shop.Api
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
