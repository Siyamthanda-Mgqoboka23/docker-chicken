FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["ChickenAPI/ChickenAPI.csproj", "ChickenAPI/"]
RUN dotnet restore "ChickenAPI/ChickenAPI.csproj"
COPY . .
WORKDIR "/src/ChickenAPI"
RUN dotnet publish "ChickenAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ChickenAPI.dll"]
