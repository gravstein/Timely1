FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish Timely1/Timely1.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
COPY Timely1/DatabaseApi.db .
COPY Timely1/DatabaseApi.db-shm .
COPY Timely1/DatabaseApi.db-wal .
ENTRYPOINT ["dotnet", "Timely1.dll"]