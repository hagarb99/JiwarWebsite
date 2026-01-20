# Step 1: Build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution and restore dependencies
COPY *.sln .
COPY Jiwar/*.csproj ./Jiwar/
RUN dotnet restore

# Copy all project files and publish
COPY Jiwar/. ./Jiwar/
WORKDIR /app/Jiwar
RUN dotnet publish -c Release -o /app/publish

# Step 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Open port for Render
EXPOSE 5000

# Start the application
ENTRYPOINT ["dotnet", "Jiwar.dll"]
