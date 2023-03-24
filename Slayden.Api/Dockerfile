FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Slayden.Api/Slayden.Api.csproj", "Slayden.Api/"]
RUN dotnet restore "Slayden.Api/Slayden.Api.csproj"
COPY . .
WORKDIR "/src/Slayden.Api"
RUN dotnet build "Slayden.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Slayden.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Slayden.Api.dll"]
