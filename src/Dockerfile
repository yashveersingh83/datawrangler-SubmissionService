# Stage 1: Build the .NET Core application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the solution file and restore dependencies
COPY ./*.sln ./
COPY  /API/*.csproj ./API/
COPY  Domain/*.csproj ./Domain/
COPY  Infrastructure/*.csproj ./Infrastructure/
COPY  Application/*.csproj ./Application/
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR /src/API
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Expose the port your API will run on
EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "SubmissionService.API.dll"]