# Utilizar a imagem do SDK do .NET 6 para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copiar o arquivo csproj e restaurar as dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante dos arquivos e construir a aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Utilizar a imagem do runtime do .NET 6 para a execução da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "SmartSchool.WebAPI.dll" ]
