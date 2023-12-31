FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cohorts Assigment Week 2/Cohorts Assigment Week 2.csproj", "Cohorts Assigment Week 2/"]
RUN dotnet restore "Cohorts Assigment Week 2/Cohorts Assigment Week 2.csproj"
COPY . .
WORKDIR "/src/Cohorts Assigment Week 2"
RUN dotnet build "Cohorts Assigment Week 2.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Cohorts Assigment Week 2.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cohorts Assigment Week 2.dll"]
