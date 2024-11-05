FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY AIR_Wheelly_Common/*.csproj AIR_Wheelly_Common/
COPY AIR_Wheelly_API/*.csproj AIR_Wheelly_API/
COPY AIR_Wheelly_BLL/*.csproj AIR_Wheelly_BLL/
COPY AIR_Wheelly_DAL/*.csproj AIR_Wheelly_DAL/

RUN dotnet restore

COPY . .
WORKDIR /app/AIR_Wheelly_API
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "AIR_Wheelly_API.dll"]
