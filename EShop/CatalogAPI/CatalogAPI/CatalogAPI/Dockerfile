#Stage 1

FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY ./ ./
RUN dotnet build
RUN dotnet publish -c Release -o dist

#Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS publish
WORKDIR /app
COPY --from=build /src/dist ./
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT = Production
ENV ConnectionStrings:CatalogCosmosConnection="mongodb://eshopcosmosdb:lSptoQ9FuRcTuq2oJDYpcqsUBNBK5csoGWVpJ5Xc4GBRQTYRTh2Ix6Iv5GHMVlns7z6WADwoGMdb718GwkQHbA==@eshopcosmosdb.documents.azure.com:10255/?ssl=true&replicaSet=globaldb"
ENTRYPOINT [ "dotnet","CatalogAPI.dll" ]
