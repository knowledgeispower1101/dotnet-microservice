# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file and all project files
COPY User.Domain/User.Domain.csproj User.Domain/
COPY User.Constracts/User.Constracts.csproj User.Constracts/
COPY User.Application/User.Application.csproj User.Application/
COPY User.Infrastructure/User.Infrastructure.csproj User.Infrastructure/
COPY User.Api/User.Api.csproj User.Api/

# Restore dependencies
WORKDIR /src/User.Api
RUN dotnet restore

# Copy all source code
WORKDIR /src
COPY . .

# Publish the application
WORKDIR /src/User.Api
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
