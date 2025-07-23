# Check-ListAPI

API RESTful para gerenciamento de tarefas com cadastro e autenticação de usuários.

---

## Sobre

Este projeto é uma API backend que permite criar, editar, listar e deletar tarefas associadas a usuários. Ideal para um sistema de checklist ou to-do list. 

Construída com .NET 9 e Entity Framework Core, com banco de dados SQL Server Express local.

---

## Funcionalidades atuais

- Cadastro de usuários (UserController)
- CRUD completo de tarefas (TaskController)
- Relacionamento entre usuários e tarefas
- Controle básico de dados via Entity Framework Core

---

## Tecnologias usadas

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server Express LocalDB

---

## Como rodar o projeto

1. Clone o repositório:
   git clone https://github.com/andrecapoano/check-listAPI.git
   cd TaskManagerAPI

2. Configure sua string de conexão no appsettings.json (para SQL Server Express LocalDB):

    "ConnectionStrings": {
        
        "DefaultConnection": "Server=(localdb)\\\\mssqllocaldb;Database=TaskManagerDB;Trusted_Connection=True;"
}

3. Rode as migrações para criar o banco:

    dotnet ef database update

4. Execute a API:

    dotnet run

5. Acesse a API via http://localhost:5213 ou https://localhost:7028.

## Próximos passos

* Implementar autenticação JWT;
* Melhorar validação e tratamento de erros;
* Criar frontend para consumir a API.