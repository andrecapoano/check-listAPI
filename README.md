# Check-ListAPI

API RESTful para gerenciamento de tarefas com cadastro e autenticação de usuários.

---

## Sobre

Projeto de API backend com frontend simples para gerenciamento de tarefas, utilizando autenticação JWT.
Frontend em HTML, CSS e JavaScript puro, e backend em C# e ASP.NET Core com Entity Framework Core.
Permite cadastro e login de usuários, com acesso exclusivo às tarefas vinculadas ao usuário autenticado.

---

## Funcionalidades atuais

- Autenticação com JWT para proteger rotas e garantir acesso individualizado aos dados;
- Cadastro e login de usuários com validações e mensagens dinâmicas no frontend;
- Painel web completo para gerenciar tarefas (HTML, CSS e JS puro);
- CRUD completo de tarefas com atualização em tempo real após cada operação;
- Relacionamento entre usuários e suas tarefas, com controle de acesso por token;
- Endpoints para edição e exclusão de usuários (ainda não expostos no frontend);
- Persistência de dados com Entity Framework Core e SQL Server Express;
- Armazenamento seguro do token JWT no localStorage após login.

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

Para acessar os todos os Endpoints, rode o projeto e acesse: https://localhost:7028/swagger

---

## Próximos passos

### Concluídos
- ✅ Editar o código nos moldes de Repository Pattern;
- ✅ Adicionar endpoint de login;
- ✅ Implementar autenticação JWT;
- ✅ Criar frontend para consumir a API;
- ✅ Filtrar tasks por id de usuário.

### Em andamento
- ⏳ Implementar o Guid para geração de ID de usuários;
- ⏳ Melhorar validação e tratamento de erros.
