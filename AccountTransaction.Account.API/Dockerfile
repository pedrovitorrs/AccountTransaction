#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AccountTransaction.Account.API/AccountTransaction.Account.API.csproj", "AccountTransaction.Account.API/"]
RUN dotnet restore "AccountTransaction.Account.API/AccountTransaction.Account.API.csproj"
COPY . .
WORKDIR "/src/AccountTransaction.Account.API"
RUN dotnet build "AccountTransaction.Account.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountTransaction.Account.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountTransaction.Account.API.dll"]