FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS build
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0@sha256:b4bea3a52a0a77317fa93c5bbdb076623f81e3e2f201078d89914da71318b5d8
WORKDIR /App
COPY --from=build /App/out .
ENTRYPOINT ["dotnet", "MalDealsBackend.dll"]
EXPOSE 5051

# Build-Stage mit .NET 9.0 SDK
#FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
#WORKDIR /app
#COPY . .  
#RUN dotnet restore "./MalDealsBackend.csproj"
#RUN dotnet publish -c Release -o /out  

# Runtime-Stage mit .NET 9.0 Runtime
#FROM mcr.microsoft.com/dotnet/aspnet:9.0
#WORKDIR /app
#COPY --from=build /out .
#ENTRYPOINT ["dotnet", "MalDealsBackend.dll"]
#EXPOSE 5051