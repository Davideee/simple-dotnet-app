# Build-Phase für das Frontend
FROM node:18 AS frontend-build

WORKDIR /app/frontend

COPY simple-app.client/package*.json ./
RUN npm install
COPY simple-app.client/. ./
RUN npm run build

# Build-Phase für das Backend
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS backend-build

WORKDIR /app/backend

COPY simple-app.Server/simple-app.Server.csproj ./
RUN dotnet restore
COPY simple-app.Server/. ./
RUN dotnet publish -c Release -o /app/publish

# Endgültiges Image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=backend-build /app/publish ./
COPY --from=frontend-build /app/frontend/dist /app/wwwroot

EXPOSE 80

ENTRYPOINT ["dotnet", "simple-app.Server.dll"]