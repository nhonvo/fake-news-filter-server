#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS="http://*:8080"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FakeNewsFilter.UserApp/FakeNewsFilter.UserApp.csproj", "FakeNewsFilter.UserApp/"]
COPY ["FakeNewsFilter.ViewModel/FakeNewsFilter.ViewModel.csproj", "FakeNewsFilter.ViewModel/"]
COPY ["FakeNewsFilter.Data/FakeNewsFilter.Data.csproj", "FakeNewsFilter.Data/"]
COPY ["FakeNewsFilter.Utilities/FakeNewsFilter.Utilities.csproj", "FakeNewsFilter.Utilities/"]
COPY ["FakeNewsFilter.ClientService/FakeNewsFilter.ClientService.csproj", "FakeNewsFilter.ClientService/"]
COPY ["FakeNewsFilter.API/FakeNewsFilter.API.csproj", "FakeNewsFilter.API/"]
COPY ["FakeNewsFilter.Application/FakeNewsFilter.Application.csproj", "FakeNewsFilter.Application/"]
RUN dotnet restore "FakeNewsFilter.UserApp/FakeNewsFilter.UserApp.csproj"
COPY . .
WORKDIR "/src/FakeNewsFilter.UserApp"
RUN dotnet build "FakeNewsFilter.UserApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FakeNewsFilter.UserApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FakeNewsFilter.UserApp.dll"]
