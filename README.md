# API MVC Suptech

API desenvolvida em ASP.NET Core (.NET 9) para gerenciamento de administradores, gerentes, técnicos e usuários.
Também incluirá o gerenciamento de chamados e uso de IA chatbot em versões futuras

## Funcionalidades
- Cadastro, edição, listagem e exclusão de administradores, gerentes, técnicos e usuários
- Autenticação via TokenService
- Tratamento de exceções com logs

## Estrutura do Projeto
- Controllers: Lógica das rotas e endpoints
- Entitys: Entidades do domínio
- Entitys/Dtos: Objetos de transferência de dados
- Data: Contexto do Entity Framework (CrudData)
- Services: Serviços auxiliares (ex: TokenService)

## Endpoints Principais
- `POST /api/Administrador/Adicionar` — Adiciona um administrador
- `GET /api/Administrador/Listar` — Lista todos os administradores
- `PUT /api/Administrador/Editar/{id}` — Edita um administrador
- `DELETE /api/Administrador/Excluir` — Exclui um administrador

(Repete para Gerente, Tecnico e Usuario)

## Como Executar
1. Instale o .NET 9 SDK
2. Restaure os pacotes:
   ```bash
   dotnet restore
   ```
3. Execute a aplicação:
   ```bash
   dotnet run --project "API MVC Suptech/API MVC Suptech.csproj"
   ```
4. Acesse o Swagger em `https://localhost:{porta}/swagger`

## Observações
- O projeto utiliza Entity Framework Core para acesso a dados.
- O tratamento de erros é feito via try/catch e logs.
- Para editar entidades, utilize o endpoint de busca para preencher o body no Swagger.

## Licença
MIT
