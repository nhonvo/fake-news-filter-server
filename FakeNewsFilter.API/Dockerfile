#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 2000
ENV ASPNETCORE_URLS="http://*:2000"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FakeNewsFilter.API/FakeNewsFilter.API.csproj", "FakeNewsFilter.API/"]
COPY ["FakeNewsFilter.Data/FakeNewsFilter.Data.csproj", "FakeNewsFilter.Data/"]
COPY ["FakeNewsFilter.Application/FakeNewsFilter.Application.csproj", "FakeNewsFilter.Application/"]
COPY ["FakeNewsFilter.Utilities/FakeNewsFilter.Utilities.csproj", "FakeNewsFilter.Utilities/"]
COPY ["FakeNewsFilter.ViewModel/FakeNewsFilter.ViewModel.csproj", "FakeNewsFilter.ViewModel/"]
RUN dotnet restore "FakeNewsFilter.API/FakeNewsFilter.API.csproj"
COPY . .
WORKDIR "/src/FakeNewsFilter.API"
RUN dotnet build "FakeNewsFilter.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FakeNewsFilter.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FakeNewsFilter.API.dll"]
