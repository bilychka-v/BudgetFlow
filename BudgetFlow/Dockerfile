# Етап 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BudgetFlow.csproj", "."]
RUN dotnet restore "BudgetFlow.csproj"
COPY . .
WORKDIR /src
RUN dotnet publish "BudgetFlow.csproj" -c Release -o /app/publish

# Етап 2: final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BudgetFlow.dll"]

