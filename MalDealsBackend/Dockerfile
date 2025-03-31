# Build-Stage mit .NET 9.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY . .  
RUN dotnet restore "./MalDealsBackend.csproj"
RUN dotnet publish -c Release -o /out  

# Runtime-Stage mit .NET 9.0 Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "MalDealsBackend.dll"]
EXPOSE 5051