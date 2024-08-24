FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./Slayden.Api", "./Slayden.Api/"]
COPY ["./Slayden.Core", "./Slayden.Core/"]

RUN dotnet restore './Slayden.Api/Slayden.Api.csproj'
WORKDIR "/src/Slayden.Api"
RUN dotnet build "Slayden.Api.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "Slayden.Api.csproj" -c Release -o /app/publish

# Final Stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Slayden.Api.dll"]
