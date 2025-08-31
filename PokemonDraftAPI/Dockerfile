# Imagen base para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj y restaurar dependencias
COPY PokemonDraftAPI/PokemonDraftAPI.csproj ./PokemonDraftAPI/
RUN dotnet restore ./PokemonDraftAPI/PokemonDraftAPI.csproj

# Copiar el resto del código y compilar
COPY . .
WORKDIR /src/PokemonDraftAPI
RUN dotnet publish -c Release -o /app

# Imagen final para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

EXPOSE 8080
ENTRYPOINT ["dotnet", "PokemonDraftAPI.dll"]
