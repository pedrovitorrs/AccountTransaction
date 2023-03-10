#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AccountTransaction.WebUI/AccountTransaction.WebUI.csproj", "AccountTransaction.WebUI/"]
COPY ["AccountTransaction.Commom.Core/AccountTransaction.Commom.Core.csproj", "AccountTransaction.Commom.Core/"]
RUN dotnet restore "AccountTransaction.WebUI/AccountTransaction.WebUI.csproj"
COPY . .
WORKDIR "/src/AccountTransaction.WebUI"
RUN dotnet build "AccountTransaction.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountTransaction.WebUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountTransaction.WebUI.dll"]