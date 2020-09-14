FROM mcr.microsoft.com/dotnet/core/sdk:3.1  AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY LUM.Services.Material.csproj LUM.Services.Material/
RUN dotnet restore LUM.Services.Material/LUM.Services.Material.csproj
COPY . .
RUN dotnet build LUM.Services.Material.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish LUM.Services.Material.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LUM.Services.Material.dll"]
