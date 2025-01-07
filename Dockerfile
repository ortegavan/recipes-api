# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar o arquivo de projeto e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todos os arquivos e realizar o build da aplicação
COPY . ./
RUN dotnet publish -c Release -o /out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar os arquivos publicados da etapa de build
COPY --from=build /out .

# Expor a porta 3000
EXPOSE 3000

# Configurar o runtime para escutar na porta 3000
ENV ASPNETCORE_URLS=http://+:3000

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "recipes-api.dll"]
