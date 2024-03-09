#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MICROSERVICE.AZ.Transaction/MICROSERVICE.AZ.Transaction.csproj", "MICROSERVICE.AZ.Transaction/"]
RUN dotnet restore "MICROSERVICE.AZ.Transaction/MICROSERVICE.AZ.Transaction.csproj"
COPY . .
WORKDIR "/src/MICROSERVICE.AZ.Transaction"
RUN dotnet build "MICROSERVICE.AZ.Transaction.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MICROSERVICE.AZ.Transaction.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MICROSERVICE.AZ.Transaction.dll"]
