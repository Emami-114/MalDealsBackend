#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5051

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["MalDealsBackend/MalDealsBackend.csproj", "MalDealsBackend/"]
RUN dotnet restore "MalDealsBackend/MalDealsBackend.csproj"
COPY . .
WORKDIR "/src/MalDealsBackend"
RUN dotnet build "MalDealsBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MalDealsBackend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MalDealsBackend.dll"]