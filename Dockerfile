# Stage 1: Build the .NET Core application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy sln and csproj files
COPY ["SubmissionService.sln", 
      "API/SubmissionService.API.csproj", 
      "Application/SubmissionService.Application.csproj", 
      "Domain/SubmissionService.Domain.csproj", 
      "Infrastructure/SubmissionService.Infrastructure.csproj", 
      "./"]

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Replace appsettings.json with Docker-specific one
WORKDIR /src/API
RUN cp appsettings.Docker.json appsettings.json

RUN cat appsettings.json

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "SubmissionService.API.dll"]
