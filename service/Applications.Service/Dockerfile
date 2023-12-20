FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV SWAGGER_ENABLED=$SWAGGER_ENABLED
ENV ASPNETCORE_HTTP_PORTS=80
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["service/Applications.Service/Applications.Service.csproj", "Applications.Service/"]
COPY ["service/Libraries.Web/Libraries.Web.csproj", "Libraries.Web/"]
RUN dotnet restore "Applications.Service/Applications.Service.csproj"
COPY ./service .
WORKDIR "/src/Applications.Service"
RUN dotnet build "Applications.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Applications.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Applications.Service.dll"]
