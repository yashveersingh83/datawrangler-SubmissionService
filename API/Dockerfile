# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# Copy solution and projects
COPY *.sln . 
COPY SubmissionService/API/*.csproj ./API/
COPY SubmissionService/Application/*.csproj ./Application/
COPY SubmissionService/Domain/*.csproj ./Domain/
COPY SubmissionService/Infrastructure/*.csproj ./Infrastructure/

# Restore dependencies
RUN dotnet restore ./API/SubmissionService.API.csproj

# Copy the entire source and publish the app
COPY . . 
WORKDIR /source/SubmissionService/API
RUN dotnet publish SubmissionService.API.csproj -c Release -o /app

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app . 
EXPOSE 8080
ENTRYPOINT ["dotnet", "SubmissionService.API.dll"]
