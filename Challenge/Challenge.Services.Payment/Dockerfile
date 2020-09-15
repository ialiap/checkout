FROM mcr.microsoft.com/dotnet/core/sdk:3.1  AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY Challenge.Services.Payment.csproj Challenge.Services.Payment/
RUN dotnet restore Challenge.Services.Payment/Challenge.Services.Payment.csproj
COPY . .
RUN dotnet build Challenge.Services.Payment.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Challenge.Services.Payment.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Challenge.Services.Payment.dll"]
