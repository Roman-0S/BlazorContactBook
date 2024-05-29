#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlazorContactBook/BlazorContactBook.csproj", "BlazorContactBook/"]
COPY ["BlazorContactBook.Client/BlazorContactBook.Client.csproj", "BlazorContactBook.Client/"]
RUN dotnet restore "./BlazorContactBook/BlazorContactBook.csproj"
COPY . .
WORKDIR "/src/BlazorContactBook"
RUN dotnet build "./BlazorContactBook.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BlazorContactBook.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorContactBook.dll"]