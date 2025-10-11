# API MVC Suptech

API desenvolvida em ASP.NET Core (.NET 9) para gerenciamento de administradores, gerentes, t�cnicos e usu�rios.
Tamb�m incluir� o gerenciamento de chamados e uso de IA chatbot em vers�es futuras

## Funcionalidades
- Cadastro, edi��o, listagem e exclus�o de administradores, gerentes, t�cnicos e usu�rios
- Autentica��o via TokenService
- Tratamento de exce��es com logs

## Estrutura do Projeto
- Controllers: L�gica das rotas e endpoints
- Entitys: Entidades do dom�nio
- Entitys/Dtos: Objetos de transfer�ncia de dados
- Data: Contexto do Entity Framework (CrudData)
- Services: Servi�os auxiliares (ex: TokenService)

## Endpoints Principais
- `POST /api/Administrador/Adicionar` � Adiciona um administrador
- `GET /api/Administrador/Listar` � Lista todos os administradores
- `PUT /api/Administrador/Editar/{id}` � Edita um administrador
- `DELETE /api/Administrador/Excluir` � Exclui um administrador

(Repete para Gerente, Tecnico e Usuario)

## Como Executar
1. Instale o .NET 9 SDK
2. Restaure os pacotes:
   ```bash
   dotnet restore
   ```
3. Execute a aplica��o:
   ```bash
   dotnet run --project "API MVC Suptech/API MVC Suptech.csproj"
   ```
4. Acesse o Swagger em `https://localhost:{porta}/swagger`

## Observa��es
- O projeto utiliza Entity Framework Core para acesso a dados.
- O tratamento de erros � feito via try/catch e logs.
- Para editar entidades, utilize o endpoint de busca para preencher o body no Swagger.

## Licen�a
MIT
