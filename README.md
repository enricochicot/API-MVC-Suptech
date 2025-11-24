# API-MVC-Suptech: Backend Central do Sistema TechSupport (PIM-4 e SuptechDESKTOP)

<!-- BADGES SECTION -->
[![Status do Projeto](https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow)](https://github.com/enricochicot/API-MVC-Suptech)
[![Licença](https://img.shields.io/github/license/enricochicot/API-MVC-Suptech)](LICENSE)
[![Tecnologia Principal](https://img.shields.io/badge/Backend-ASP.NET%20Core%20(.NET%209)-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
[![Frontend Mobile/Web](https://img.shields.io/badge/Frontend%20(Usu%C3%A1rios%2FT%C3%A9cnicos)-PIM--4%20(React%20Native)-61DAFB?logo=react&logoColor=white)](https://github.com/heitorrsdev/PIM-4)
[![Frontend Desktop](https://img.shields.io/badge/Frontend%20(Gerentes)-SuptechDESKTOP%20(JavaFX)-007396?logo=java&logoColor=white)](https://github.com/LeonardoZanchi/SuptechDESKTOP)

## 🎓 Contexto do Projeto (Trabalho de Conclusão de Curso - TCC)

Este repositório contém o código-fonte do **Backend Central** do projeto **TechSupport App**, desenvolvido como parte do Trabalho de Conclusão de Curso (TCC). A **API-MVC-Suptech** é a camada de dados e lógica de negócios, responsável por gerenciar a autenticação, os dados de usuários e o ciclo de vida dos chamados técnicos para todas as interfaces do sistema.

### Interfaces do Sistema

Esta API serve como backend para duas aplicações frontend distintas, cada uma focada em um público-alvo específico:

| Aplicação Frontend | Tecnologia | Público-Alvo | Repositório |
| :--- | :--- | :--- | :--- |
| **PIM-4** | React Native Web + Expo | Usuários e Técnicos | [heitorrsdev/PIM-4](https://github.com/heitorrsdev/PIM-4) |
| **SuptechDESKTOP** | JavaFX 21 | Gerentes | [LeonardoZanchi/SuptechDESKTOP](https://github.com/LeonardoZanchi/SuptechDESKTOP) |

## ✨ Funcionalidades da API

A API foi projetada para suportar todas as operações de gerenciamento de usuários e chamados do **TechSupport App**, incluindo:

*   **Autenticação e Autorização:** Login seguro via **JWT (JSON Web Tokens)**.
*   **Gerenciamento de Usuários (CRUD):** Cadastro, listagem, edição e exclusão de:
    *   Administradores
    *   Gerentes
    *   Técnicos
    *   Usuários
*   **Gerenciamento de Chamados (CRUD):** Criação, consulta, edição e exclusão de chamados técnicos, com controle de prioridades e status.
*   **Tratamento de Exceções:** Logs e tratamento de erros robustos.

## 💻 Tecnologias Utilizadas

| Categoria | Tecnologia | Versão | Descrição |
| :--- | :--- | :--- | :--- |
| **Framework** | ASP.NET Core Web API | .NET 9.0 | Framework para construção da API RESTful. |
| **Linguagem** | C# | 13.0 | Linguagem de programação principal. |
| **ORM** | Entity Framework Core | 9.0.10 | Mapeamento Objeto-Relacional para acesso a dados. |
| **Banco de Dados** | SQL Server | - | Sistema de gerenciamento de banco de dados. |
| **Segurança** | JWT | 8.14.0 | Implementação de tokens para autenticação. |
| **Criptografia** | BCrypt.Net-Next | 4.0.3 | Utilizado para o hash seguro de senhas. |
| **Documentação** | Swagger/OpenAPI | - | Interface interativa para documentação e teste dos endpoints. |

## 📁 Estrutura do Projeto

O projeto segue o padrão **Model-View-Controller (MVC)** adaptado para uma API, com uma arquitetura limpa e modular:

```
API-MVC-Suptech/
├── Controllers/      # Lógica das rotas e endpoints (Auth, Entidades)
├── Entitys/          # Modelos de domínio (Usuário, Chamado, etc.)
│   └── Dtos/         # Objetos de Transferência de Dados (DTOs)
├── Data/             # Contexto do Entity Framework (CrudData.cs)
├── Services/         # Serviços auxiliares (Ex: TokenService.cs)
├── Program.cs        # Configuração e inicialização da aplicação
└── appsettings.json  # Configurações
```

## 🔑 Endpoints Principais da API

A API expõe endpoints para autenticação e operações CRUD nas entidades principais.

| Funcionalidade | Método | Endpoint | Descrição |
| :--- | :--- | :--- | :--- |
| **Login** | `POST` | `/api/Auth/Login` | Autentica o usuário e retorna um JWT. |
| **Usuários** | `POST` | `/api/Usuario/Adicionar` | Cria um novo usuário. |
| | `GET` | `/api/Usuario/Listar` | Lista todos os usuários. |
| **Chamados** | `POST` | `/api/Chamado/Adicionar` | Abre um novo chamado. |
| | `GET` | `/api/Chamado/Listar` | Lista todos os chamados. |
| | `PUT` | `/api/Chamado/Editar/{id}` | Edita um chamado existente. |
| **Gerentes** | `POST` | `/api/Gerente/Adicionar` | Cria um novo gerente. |
| | `GET` | `/api/Gerente/Listar` | Lista todos os gerentes. |
| **Técnicos** | `POST` | `/api/Tecnico/Adicionar` | Cria um novo técnico. |
| | `GET` | `/api/Tecnico/Listar` | Lista todos os técnicos. |

*   **Nota:** Todos os endpoints de listagem, obtenção por ID, edição e exclusão estão disponíveis para as entidades **Usuário**, **Técnico**, **Gerente** e **Chamado**.

## 🛠️ Instalação e Execução Local

Para rodar a API em seu ambiente de desenvolvimento, siga os passos abaixo:

### Pré-requisitos

*   [**SDK do .NET 9.0**](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) (ou superior).
*   Um servidor de banco de dados **SQL Server** (local ou remoto).

### Configuração

1.  **Clone o repositório:**
    ```shell
    git clone https://github.com/enricochicot/API-MVC-Suptech.git
    cd API-MVC-Suptech
    ```
2.  **Configure a Connection String e JWT:**
    *   Edite o arquivo `appsettings.json` e configure a string de conexão do SQL Server e a chave secreta do JWT.
3.  **Restaure os pacotes e dependências:**
    ```shell
    dotnet restore
    ```
4.  **Execute as Migrations:**
    *   Crie o banco de dados e as tabelas usando as migrações do Entity Framework Core:
    ```shell
    dotnet ef database update
    ```

### Execução

1.  **Inicie a aplicação:**
    ```shell
    dotnet run
    ```
2.  **Acesse a documentação Swagger:**
    *   A API será iniciada e o **Swagger** estará disponível em `https://localhost:{porta}/swagger` (a porta será exibida no console). Use o Swagger para testar os endpoints e visualizar os modelos de dados.

## 🤝 Contribuição

Contribuições são bem-vindas! Por favor, abra uma *Issue* para discutir a funcionalidade que você gostaria de adicionar ou o bug que deseja corrigir, e então submeta um *Pull Request*.

## 📄 Licença

Este projeto está licenciado sob a [Licença MIT](LICENSE).
