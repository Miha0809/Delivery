FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Debug -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Delivery.dll"]
