FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Api/Api.csproj Api/
COPY Db/Db.csproj Db/

WORKDIR /src/Api
RUN dotnet restore "Api.csproj"

WORKDIR /src
COPY . .

WORKDIR /src/Api
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5010
ENTRYPOINT ["dotnet", "Api.dll"]
