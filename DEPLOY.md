# Deploy da API MVC Suptech no Vercel

## Pré-requisitos

1. Conta no [Vercel](https://vercel.com)
2. Repositório no GitHub/GitLab/Bitbucket
3. Banco de dados em nuvem (recomendado: Azure SQL Database, Amazon RDS, ou PlanetScale)

## Passos para Deploy

### 1. Configurar Banco de Dados em Nuvem

Você precisará migrar seu banco de dados local para um banco em nuvem. Opções recomendadas:

- **Azure SQL Database** (mais compatível com SQL Server)
- **Amazon RDS SQL Server**
- **PlanetScale** (MySQL - requer pequenas mudanças no código)

### 2. Configurar Variáveis de Ambiente no Vercel

No painel do Vercel, vá em Settings > Environment Variables e adicione:

```
DATABASE_URL=sua_connection_string_do_banco_em_nuvem
ASPNETCORE_ENVIRONMENT=Production
JWT_KEY=Suptech2025ProductionKey2024SecureToken
```

### 3. Deploy via GitHub

1. Faça push do seu código para um repositório GitHub
2. Conecte o repositório ao Vercel
3. O Vercel detectará automaticamente que é um projeto .NET
4. O deploy será feito automaticamente

### 4. Deploy via CLI do Vercel

Alternativamente, você pode fazer deploy via CLI:

```bash
npm i -g vercel
vercel login
vercel
```

## Estrutura de Arquivos Criados

- `vercel.json` - Configuração do Vercel para .NET
- `.vercelignore` - Arquivos a serem ignorados no deploy
- `appsettings.Production.json` - Configurações de produção

## Configurações Importantes

### CORS
O CORS já está configurado para aceitar qualquer origem. Em produção, considere restringir para domínios específicos.

### HTTPS
O HTTPS redirect foi desabilitado em produção para compatibilidade com Vercel.

### Swagger
O Swagger estará disponível em produção no endpoint `/swagger`

## Troubleshooting

### Erro de Connection String
- Verifique se a variável `DATABASE_URL` está configurada corretamente no Vercel
- Teste a connection string localmente primeiro

### Timeout de Deploy
- Verifique se todas as dependências estão no `.csproj`
- O Vercel tem limite de tempo para builds

### Erro de Routing
- Certifique-se que o `vercel.json` está na raiz do projeto
- Verifique se os caminhos no `vercel.json` estão corretos

## URLs Importantes

Após o deploy:
- API Base: `https://seu-projeto.vercel.app/`
- Swagger: `https://seu-projeto.vercel.app/swagger`
- Health Check: `https://seu-projeto.vercel.app/api/usuario` (ou outro endpoint)

## Como configurar a connection string segura no Vercel (passo-a-passo)

Preferência: usar `ConnectionStrings__DefaultConnection` como variável de ambiente. O ASP.NET Core lê `ConnectionStrings:DefaultConnection` a partir de uma variável chamada `ConnectionStrings__DefaultConnection`.

1) Via painel (UI) do Vercel:
	- Abra seu projeto no Vercel e vá em Settings -> Environment Variables.
	- Clique em "Add".
	- Em Name, coloque: `ConnectionStrings__DefaultConnection`
	- Em Value, cole a sua ADO.NET connection string (por exemplo:
	  `Server=tcp:suptech.database.windows.net,1433;Initial Catalog=Suptech;Persist Security Info=False;User ID=administradorsuptech;Password=<SUA_SENHA>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;`)
	- Em Environment escolha `Production` (ou `Preview` se preferir testar antes).
	- Salve.

2) Via CLI do Vercel (com `vercel env add`):
	- Instale/entre no CLI do vercel e autentique:

```powershell
npm i -g vercel
vercel login
```

	- Para adicionar a variável em Production:

```powershell
vercel env add "ConnectionStrings__DefaultConnection" production
# cole a connection string quando for solicitado
```

3) Remover segredos do repositório:
	- Depois de adicionar a variável no Vercel, remova a connection string sensível dos arquivos `appsettings.json` e `appsettings.Production.json` (já atualizei para usar um placeholder).
	- Commit e faça push.

4) Teste:
	- Faça um novo deploy (push no GitHub ou `vercel --prod`).
	- Verifique logs no Vercel e acesse `/swagger` ou um endpoint health-check.

Observação sobre firewall: se o deploy falhar por timeout/erro de conexão, revise a configuração de firewall do Azure SQL (veja o README sobre opções: permitir IPs temporariamente, usar Private Link ou hospedar a API no Azure para produção).