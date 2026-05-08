FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Assessment.Api/Assessment.Api.csproj Assessment.Api/
COPY Assessment.Application/Assessment.Application.csproj Assessment.Application/
COPY Assessment.Domain/Assessment.Domain.csproj Assessment.Domain/
COPY Assessment.Infrastructure/Assessment.Infrastructure.csproj Assessment.Infrastructure/

RUN dotnet restore Assessment.Api/Assessment.Api.csproj

COPY . .

RUN dotnet publish Assessment.Api/Assessment.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Assessment.Api.dll"]