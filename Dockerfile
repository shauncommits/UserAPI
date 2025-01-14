FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8085

ENV ASPNETCORE_SQL_ENV="sqlserver"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["User/User.csproj", "User/"]
RUN dotnet restore "User/User.csproj"
COPY . .
WORKDIR "/src/User"
RUN dotnet build "User.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS development
WORKDIR /src
COPY ["User/User.csproj", "User/"]
WORKDIR "/src/User"
CMD dotnet run --no-launch-profile

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "User.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.dll", "--urls", "http://[::]:8085"]
