#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 2001
ENV ASPNETCORE_URLS="http://*:2001"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FakeNewsFilter.AdminApp/FakeNewsFilter.AdminApp.csproj", "FakeNewsFilter.AdminApp/"]
COPY ["FakeNewsFilter.ViewModel/FakeNewsFilter.ViewModel.csproj", "FakeNewsFilter.ViewModel/"]
COPY ["FakeNewsFilter.Data/FakeNewsFilter.Data.csproj", "FakeNewsFilter.Data/"]
COPY ["FakeNewsFilter.Utilities/FakeNewsFilter.Utilities.csproj", "FakeNewsFilter.Utilities/"]
COPY ["FakeNewsFilter.ClientService/FakeNewsFilter.ClientService.csproj", "FakeNewsFilter.ClientService/"]
COPY ["FakeNewsFilter.API/FakeNewsFilter.API.csproj", "FakeNewsFilter.API/"]
COPY ["FakeNewsFilter.Application/FakeNewsFilter.Application.csproj", "FakeNewsFilter.Application/"]
RUN dotnet restore "FakeNewsFilter.AdminApp/FakeNewsFilter.AdminApp.csproj"
COPY . .
WORKDIR "/src/FakeNewsFilter.AdminApp"
RUN dotnet build "FakeNewsFilter.AdminApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FakeNewsFilter.AdminApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FakeNewsFilter.AdminApp.dll"]
