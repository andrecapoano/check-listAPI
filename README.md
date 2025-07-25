# Check-ListAPI

API RESTful para gerenciamento de tarefas com cadastro e autenticação de usuários.

---

## Sobre

Este projeto é uma API backend que permite criar, editar, listar e deletar tarefas associadas a usuários. Ideal para um sistema de checklist ou to-do list. 

Construída com .NET 9 e Entity Framework Core, com banco de dados SQL Server Express local.

---

## Funcionalidades atuais

- Cadastro, alteração e exclusão de usuários;
- Login para usuários cadastrados;
- CRUD completo de tarefas;
- Relacionamento entre usuários e tarefas;
- Controle básico de dados via Entity Framework Core.

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
        cd check-listAPI

2. Configure sua string de conexão no appsettings.json (para SQL Server Express LocalDB):

        "ConnectionStrings": {          
            "DefaultConnection": "Server=(localdb)\\\\mssqllocaldb;Database=TaskManagerDB;Trusted_Connection=True;"
}

3. Rode as migrações para criar o banco:

        dotnet ef database update

4. Execute a API:

        dotnet run

5. Acesse a API via https://localhost:7028.

---

## Endpoints

- Em construção...

---

## Próximos passos

### Concluídos
- ✅ Editar o código nos moldes de Repository Pattern.
- ✅ Adicionar endpoint de login.

### Em andamento
- ⏳ Implementar autenticação JWT;
- ⏳ Implementar o Guid para geração de ID de usuários;
- ⏳ Melhorar validação e tratamento de erros;
- ⏳ Criar frontend para consumir a API.