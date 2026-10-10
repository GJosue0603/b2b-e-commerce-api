
# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /source

# Copy the solution and project files
COPY b2b-e-commerce-backend.sln ./

COPY src/b2b-e-commerce-api/*.csproj src/b2b-e-commerce-api/
COPY src/b2b-e-commerce-bll/*.csproj src/b2b-e-commerce-bll/
COPY src/b2b-e-commerce-repository/*.csproj src/b2b-e-commerce-repository/
COPY src/b2b-e-commerce-models/*.csproj src/b2b-e-commerce-models/

# Restore dependencies
RUN dotnet restore src/b2b-e-commerce-api/b2b-e-commerce-api.csproj

# Copy source code
COPY src/ ./src/

# Publish the API
RUN dotnet publish src/b2b-e-commerce-api/b2b-e-commerce-api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "b2b-e-commerce-api.dll"]
