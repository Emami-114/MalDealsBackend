FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS build
WORKDIR /App

# Copy everything
COPY . .
# Debug: List directory contents to see what's available
RUN ls -la
RUN ls -la MalDealsBackend || echo "MalDealsBackend directory not found"
# Restore as distinct layers
RUN dotnet restore "MalDealsBackend/MalDealsBackend.csproj"
# Rest of your Dockerfile...