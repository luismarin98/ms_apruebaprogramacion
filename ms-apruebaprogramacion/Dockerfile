#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ms-apruebaprogramacion/nuget.config", "."]
COPY ["ms-apruebaprogramacion/ms-apruebaprogramacion.csproj", "ms-apruebaprogramacion/"]
RUN dotnet restore "ms-apruebaprogramacion/ms-apruebaprogramacion.csproj"
COPY . .
WORKDIR "/src/ms-apruebaprogramacion"
RUN dotnet build "ms-apruebaprogramacion.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "ms-apruebaprogramacion.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LANG=es_ES.UTF-8
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ms-apruebaprogramacion.dll"]