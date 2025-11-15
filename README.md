# API MVC Suptech

API RESTful desenvolvida em .NET 9 para gerenciamento de chamados de suporte técnico, com sistema completo de autenticação e controle de usuários.

## 📋 Sobre o Projeto

A **API MVC Suptech** é um sistema de gerenciamento de help desk que permite o controle de chamados técnicos, usuários, técnicos e gerentes. A aplicação oferece endpoints para criação, listagem, edição e exclusão de entidades, além de sistema de autenticação com JWT.

### Principais Funcionalidades

- ✅ Gerenciamento completo de **Usuários**
- ✅ Gerenciamento completo de **Técnicos**
- ✅ Gerenciamento completo de **Gerentes**
- ✅ Sistema de **Chamados** com prioridades e status
- ✅ Autenticação com **JWT (JSON Web Token)**
- ✅ Criptografia de senhas com **BCrypt**
- ✅ Validação de dados com **Data Annotations**
- ✅ Documentação automática com **Swagger**
- ✅ Integração com **SQL Server**

## 🚀 Tecnologias Utilizadas

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 9.0.10**
- **SQL Server**
- **BCrypt.Net-Next 4.0.3** - Criptografia de senhas
- **JWT (System.IdentityModel.Tokens.Jwt 8.14.0)** - Autenticação
- **Swagger/OpenAPI** - Documentação da API
- **C# 13.0**

## 📦 Pacotes NuGet

```xml
<PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.10" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.10" />
<PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="8.14.0" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="9.0.6" />
```

## 🏗️ Estrutura do Projeto

```
API-MVC-Suptech/
│
├── Controllers/
│   ├── Autenticação/
│   │   ├── AuthController.cs      # Autenticação e login
│   │ └── AuthDesktop.cs
│   │
│   └── Entidades Controller/
│   ├── UsuarioController.cs   # CRUD de Usuários
│       ├── TecnicoController.cs   # CRUD de Técnicos
│       ├── GerenteController.cs   # CRUD de Gerentes
│    └── ChamadoController.cs   # CRUD de Chamados
│
├── Entitys/
│   ├── Usuario.cs           # Entidade Usuario
│   ├── Tecnico.cs               # Entidade Tecnico
│   ├── Gerente.cs       # Entidade Gerente
│   ├── Chamado.cs     # Entidade Chamado
│   │
│   └── Dtos/
│   ├── NovoUsuarioDto.cs
│       ├── NovoTecnicoDto.cs
│       ├── NovoGerenteDto.cs
│       ├── NovoChamadoDto.cs
│       ├── EditarDto.cs
│       ├── EditarChamadoDto.cs
│  ├── LoginDto.cs
│       └── ExcluirDto.cs
│
├── Data/
│   └── CrudData.cs      # DbContext do EF Core
│
├── Services/
│   └── TokenService.cs       # Geração de tokens JWT
│
├── Program.cs              # Configuração da aplicação
└── appsettings.json      # Configurações
```

## ⚙️ Configuração e Instalação

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads)
- IDE (Visual Studio 2022, VS Code ou Rider)

### Passos para Instalação

1. **Clone o repositório**
```bash
git clone https://github.com/enricochicot/API-MVC-Suptech.git
cd API-MVC-Suptech
```

2. **Configure a Connection String**

Edite o arquivo `appsettings.json` e configure sua connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=SuptechDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "SUA_CHAVE_SECRETA_AQUI_MIN_32_CARACTERES",
    "Issuer": "SuptechAPI",
    "Audience": "SuptechClients",
    "ExpireHours": 24
  }
}
```

3. **Restaure os pacotes**
```bash
dotnet restore
```

4. **Execute as Migrations**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

5. **Execute a aplicação**
```bash
dotnet run
```

6. **Acesse a documentação Swagger**
```
https://localhost:5001/swagger
```

## 📚 Endpoints da API

### 🔐 Autenticação

#### Login
```http
POST /api/Auth/Login
Content-Type: application/json

{
  "email": "usuario@exemplo.com",
  "senha": "senha123"
}
```

**Resposta:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

---

### 👥 Usuários

#### Adicionar Usuário
```http
POST /api/Usuario/Adicionar
Content-Type: application/json

{
  "nome": "João Silva",
  "email": "joao@exemplo.com",
  "senha": "senha123",
  "setor": "TI",
  "telefone": "(11) 98765-4321"
}
```

#### Listar Usuários
```http
GET /api/Usuario/Listar
```

#### Obter Usuário por ID
```http
GET /api/Usuario/Obter/{id}
```

#### Editar Usuário
```http
PUT /api/Usuario/Editar/{id}
Content-Type: application/json

{
  "nome": "João Silva Atualizado",
  "email": "joao.novo@exemplo.com",
  "senha": "novaSenha123",
  "setor": "Desenvolvimento",
  "telefone": "(11) 98765-4321"
}
```

#### Deletar Usuário
```http
DELETE /api/Usuario/Excluir/{id}
```

---

### 🔧 Técnicos

#### Adicionar Técnico
```http
POST /api/Tecnico/Adicionar
Content-Type: application/json

{
  "nome": "Maria Santos",
  "email": "maria@exemplo.com",
  "senha": "senha123",
  "especialidade": "Redes",
  "telefone": "(11) 91234-5678"
}
```

#### Listar Técnicos
```http
GET /api/Tecnico/Listar
```

#### Obter Técnico por ID
```http
GET /api/Tecnico/Obter/{id}
```

#### Editar Técnico
```http
PUT /api/Tecnico/Editar/{id}
```

#### Deletar Técnico
```http
DELETE /api/Tecnico/Excluir/{id}
```

---

### 👨‍💼 Gerentes

#### Adicionar Gerente
```http
POST /api/Gerente/Adicionar
Content-Type: application/json

{
  "nome": "Carlos Oliveira",
  "email": "carlos@exemplo.com",
  "senha": "senha123",
  "setor": "Suporte Técnico",
  "telefone": "(11) 99999-8888"
}
```

#### Listar Gerentes
```http
GET /api/Gerente/Listar
```

#### Obter Gerente por ID
```http
GET /api/Gerente/Obter/{id}
```

#### Editar Gerente
```http
PUT /api/Gerente/Editar/{id}
```

#### Deletar Gerente
```http
DELETE /api/Gerente/Excluir/{id}
```

---

### 🎫 Chamados

#### Adicionar Chamado
```http
POST /api/Chamado/Adicionar
Content-Type: application/json

{
  "nomeDoUsuario": "João Silva",
  "emailDoUsuario": "joao@exemplo.com",
  "setorDoUsuario": "TI",
  "titulo": "Problema no computador",
  "descricao": "O computador não liga",
  "prioridade": "Alta",
  "status": "Aberto"
}
```

**Valores aceitos:**
- **Prioridade**: `Baixa`, `Média`, `Alta`
- **Status**: `Aberto`, `Pendente`, `Fechado`

#### Listar Chamados
```http
GET /api/Chamado/Listar
```

#### Obter Chamado por ID
```http
GET /api/Chamado/Obter/{id}
```

#### Editar Chamado
```http
PUT /api/Chamado/Editar/{id}
```

#### Deletar Chamado
```http
DELETE /api/Chamado/Excluir/{id}
```

## 🗄️ Modelo de Dados

### Usuario
- `UsuarioID` (Guid, PK)
- `Nome` (string, obrigatório)
- `Email` (string, obrigatório, único, validado)
- `Senha` (string, obrigatório, min 6 caracteres, criptografada)
- `Setor` (string, obrigatório)
- `Telefone` (string, obrigatório)

### Tecnico
- `TecnicoID` (Guid, PK)
- `Nome` (string, obrigatório)
- `Email` (string, obrigatório, único, validado)
- `Senha` (string, obrigatório, min 6 caracteres, criptografada)
- `Especialidade` (string, obrigatório)
- `Telefone` (string, obrigatório)

### Gerente
- `GerenteID` (Guid, PK)
- `Nome` (string, obrigatório)
- `Email` (string, obrigatório, único, validado)
- `Senha` (string, obrigatório, min 6 caracteres, criptografada)
- `Setor` (string, obrigatório)
- `Telefone` (string, obrigatório)

### Chamado
- `ChamadoID` (Guid, PK)
- `NomeDoUsuario` (string, obrigatório)
- `EmailDoUsuario` (string, obrigatório, validado)
- `SetorDoUsuario` (string, obrigatório)
- `Titulo` (string, obrigatório)
- `Descricao` (string, obrigatório)
- `Prioridade` (string, obrigatório: Baixa/Média/Alta)
- `Status` (string, obrigatório: Aberto/Pendente/Fechado)
- `DataAbertura` (DateTime, gerado automaticamente)

## 🔒 Segurança

- **Criptografia de Senhas**: Todas as senhas são criptografadas usando BCrypt antes de serem armazenadas no banco de dados
- **JWT Authentication**: Sistema de autenticação baseado em tokens JWT com expiração configurável
- **Validação de Dados**: Validação robusta usando Data Annotations em todos os DTOs
- **CORS**: Configurado para aceitar requisições de diferentes origens (ajuste conforme necessário em produção)

## 🛠️ Tratamento de Erros

A API implementa tratamento de erros consistente:

- **200 OK**: Operação realizada com sucesso
- **400 Bad Request**: Dados inválidos ou violação de regras de negócio
- **401 Unauthorized**: Credenciais inválidas
- **404 Not Found**: Recurso não encontrado
- **500 Internal Server Error**: Erro interno do servidor (com logs detalhados)

Exemplo de resposta de erro:
```json
{
  "message": "Email já está em uso."
}
```

## 📝 Logs

A aplicação utiliza o sistema de logging do ASP.NET Core com ILogger:

- Logs de erro são registrados com detalhes completos
- Em desenvolvimento, erros retornam stack traces completos
- Em produção, apenas mensagens genéricas são retornadas

## 🧪 Testando a API

### Com Swagger
1. Execute a aplicação
2. Acesse `https://localhost:5001/swagger`
3. Teste os endpoints diretamente pela interface

### Com cURL

**Criar um usuário:**
```bash
curl -X POST "https://localhost:5001/api/Usuario/Adicionar" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "João Silva",
    "email": "joao@exemplo.com",
    "senha": "senha123",
    "setor": "TI",
    "telefone": "(11) 98765-4321"
  }'
```

**Fazer login:**
```bash
curl -X POST "https://localhost:5001/api/Auth/Login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "joao@exemplo.com",
    "senha": "senha123"
  }'
```

## 🌐 CORS

A API está configurada para aceitar requisições de qualquer origem. Para ambientes de produção, recomenda-se configurar origens específicas em `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", policy => 
    { 
        policy.WithOrigins("https://seudominio.com")
              .AllowAnyHeader()
              .AllowAnyMethod(); 
    });
});
```

## 📄 Licença

Este projeto é de código aberto e está disponível sob a licença MIT.

## 👨‍💻 Autor

**Enrico Chicot**
- GitHub: [@enricochicot](https://github.com/enricochicot)
- Repositório: [API-MVC-Suptech](https://github.com/enricochicot/API-MVC-Suptech)

## 🤝 Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para:

1. Fazer um Fork do projeto
2. Criar uma branch para sua feature (`git checkout -b feature/NovaFuncionalidade`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova funcionalidade'`)
4. Push para a branch (`git push origin feature/NovaFuncionalidade`)
5. Abrir um Pull Request

## 📞 Suporte

Para questões e suporte, abra uma [issue](https://github.com/enricochicot/API-MVC-Suptech/issues) no GitHub.

---

⭐ Se este projeto foi útil para você, considere dar uma estrela no repositório!
