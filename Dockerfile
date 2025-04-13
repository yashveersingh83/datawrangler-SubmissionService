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

# Copy the rest of the source
COPY . .

# Publish the application
WORKDIR /src/API
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Match port to Kestrel's default (80)
EXPOSE 8080

ENTRYPOINT ["dotnet", "SubmissionService.API.dll"]
