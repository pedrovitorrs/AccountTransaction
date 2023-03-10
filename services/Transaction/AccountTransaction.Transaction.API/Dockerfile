#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AccountTransaction.Transaction.API/AccountTransaction.Transaction.API.csproj", "AccountTransaction.Transaction.API/"]
COPY ["AccountTransaction.Commom.Core/AccountTransaction.Commom.Core.csproj", "AccountTransaction.Commom.Core/"]
COPY ["AccountTransaction.MessageBus/AccountTransaction.MessageBus.csproj", "AccountTransaction.MessageBus/"]
RUN dotnet restore "AccountTransaction.Transaction.API/AccountTransaction.Transaction.API.csproj"
COPY . .
WORKDIR "/src/AccountTransaction.Transaction.API"
RUN dotnet build "AccountTransaction.Transaction.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountTransaction.Transaction.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountTransaction.Transaction.API.dll"]