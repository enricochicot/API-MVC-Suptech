# Script PowerShell para testar a API localmente em modo Production

Write-Host "🚀 Testando API em modo Production..." -ForegroundColor Green

# Definir variáveis de ambiente de produção
$env:ASPNETCORE_ENVIRONMENT = "Production"
$env:DATABASE_URL = "Server=DESKTOP-LC7VCEC;Database=Suptech;Integrated Security=True;TrustServerCertificate=True;"

# Navegar para o diretório do projeto
Set-Location "API MVC Suptech"

Write-Host "📦 Restaurando pacotes..." -ForegroundColor Yellow
dotnet restore

Write-Host "🔨 Compilando projeto..." -ForegroundColor Yellow
dotnet build --configuration Release

Write-Host "🌐 Iniciando aplicação em modo Production..." -ForegroundColor Green
Write-Host "A API estará disponível em: http://localhost:5000" -ForegroundColor Cyan
Write-Host "Swagger disponível em: http://localhost:5000/swagger" -ForegroundColor Cyan
Write-Host ""
Write-Host "Pressione Ctrl+C para parar" -ForegroundColor Red

dotnet run --configuration Release --urls "http://localhost:5000"