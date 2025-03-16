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

COPY Model/Model.csproj ./Model/
COPY Web/Web.csproj ./Web/

RUN dotnet restore ./Web/Web.csproj

COPY Model/. ./Model/
COPY Web/. ./Web/

RUN dotnet publish ./Web/Web.csproj -c Release -o /app/publish

# Endgültiges Image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=backend-build /app/publish ./
COPY --from=frontend-build /app/frontend/dist /app/wwwroot

EXPOSE 80

ENTRYPOINT ["dotnet", "Web.dll"] 
