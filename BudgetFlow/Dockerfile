FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY BudgetFlow/BudgetFlow.csproj BudgetFlow/
COPY Db/Db.csproj Db/

WORKDIR /src/BudgetFlow
RUN dotnet restore "BudgetFlow.csproj"

WORKDIR /src
COPY . .

WORKDIR /src/BudgetFlow
RUN dotnet publish "BudgetFlow.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BudgetFlow.dll"]
