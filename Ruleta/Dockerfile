#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Ruleta/Ruleta.csproj", "Ruleta/"]
COPY ["Ruleta.Application/Ruleta.Application.csproj", "Ruleta.Application/"]
COPY ["Ruleta.Repository/Ruleta.Repository.csproj", "Ruleta.Repository/"]
COPY ["Ruleta.Services/Ruleta.Services.csproj", "Ruleta.Services/"]
COPY ["Ruleta.Abstractions/Ruleta.Abstractions.csproj", "Ruleta.Abstractions/"]
COPY ["Ruleta.Entities/Ruleta.Entities.csproj", "Ruleta.Entities/"]
COPY ["Ruleta.DataAccess/Ruleta.DataAccess.csproj", "Ruleta.DataAccess/"]
RUN dotnet restore "Ruleta/Ruleta.csproj"
COPY . .
WORKDIR "/src/Ruleta"
RUN dotnet build "Ruleta.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ruleta.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ruleta.dll"]