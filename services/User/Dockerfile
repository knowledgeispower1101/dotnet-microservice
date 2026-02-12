# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy Shared projects first
COPY Shared/Shared.Domain/Shared.Domain.csproj Shared/Shared.Domain/
COPY Shared/Shared.Application/Shared.Application.csproj Shared/Shared.Application/
COPY Shared/Shared.Infrastructure/Shared.Infrastructure.csproj Shared/Shared.Infrastructure/

# Copy User project files
COPY User/User.Domain/User.Domain.csproj User/User.Domain/
COPY User/User.Constracts/User.Constracts.csproj User/User.Constracts/
COPY User/User.Application/User.Application.csproj User/User.Application/
COPY User/User.Infrastructure/User.Infrastructure.csproj User/User.Infrastructure/
COPY User/User.Api/User.Api.csproj User/User.Api/

# Restore dependencies
WORKDIR /src/User/User.Api
RUN dotnet restore

# Copy all source code
WORKDIR /src
COPY User/ User/
COPY Shared/ Shared/

# Publish the application
WORKDIR /src/User/User.Api
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

ENTRYPOINT ["dotnet", "User.Api.dll"]
