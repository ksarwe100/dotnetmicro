#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MICROSERVICE.AZ.Security/MICROSERVICE.AZ.Security.csproj", "MICROSERVICE.AZ.Security/"]
RUN dotnet restore "MICROSERVICE.AZ.Security/MICROSERVICE.AZ.Security.csproj"
COPY . .
WORKDIR "/src/MICROSERVICE.AZ.Security"
RUN dotnet build "MICROSERVICE.AZ.Security.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MICROSERVICE.AZ.Security.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MICROSERVICE.AZ.Security.dll"]
