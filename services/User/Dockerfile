# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy Shared projects first


# Copy User project file
COPY User/User.csproj User/

# Restore dependencies
WORKDIR /src/User
RUN dotnet restore

# Copy all source code
WORKDIR /src
COPY User/ User/
COPY Shared/ Shared/

# Publish the application
WORKDIR /src/User
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

ENTRYPOINT ["dotnet", "User.dll"]
