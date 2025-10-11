#!/bin/bash

# Script para testar a API localmente em modo Production

echo "🚀 Testando API em modo Production..."

# Definir variáveis de ambiente de produção
export ASPNETCORE_ENVIRONMENT=Production
export DATABASE_URL="Server=DESKTOP-LC7VCEC;Database=Suptech;Integrated Security=True;TrustServerCertificate=True;"

# Navegar para o diretório do projeto
cd "API MVC Suptech"

echo "📦 Restaurando pacotes..."
dotnet restore

echo "🔨 Compilando projeto..."
dotnet build --configuration Release

echo "🌐 Iniciando aplicação em modo Production..."
echo "A API estará disponível em: http://localhost:5000"
echo "Swagger disponível em: http://localhost:5000/swagger"
echo ""
echo "Pressione Ctrl+C para parar"

dotnet run --configuration Release --urls "http://localhost:5000"