FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release

#Build runtime image 
FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY --from=build-env /app/bin/Release/net5.0/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "NetCore.Docker.FulentMigration.dll"]
