# SchoolOfNet_api-rest-asp-net-core-hateoas
Projeto do curso API REST com ASP NET Core - HATEOAS


Criar o projeto:
`dotnet new webapi` 

Copia os fontes do projeto antigo:
https://github.com/carlosmarian/SchoolOfNet_API_Rest_com_ASPNET_Core_2

Adicionar as bibliotecas:

`dotnet add package Pomelo.EntityFrameworkCore.MySql --version 2.2.0` 

`dotnet add package Swashbuckle.AspNetCore --version 5.0.0-rc2`


Restore das bibliotecas:
`dotnet restore`

Build do projeto:
`dotnet build`

Subir o doqcker:
`docker-compose up`

Criar o banco de dados:
`CREATE DATABASE apirest /*!40100 COLLATE 'latin1_general_cs' */;`

Efetuar as migrações:
`dotnet ef database update`

Subir a aplicação:
`dotnet watch run`

Após essas configurações a aplciação deve subir e ao acessar http://localhost:5000 deve apresentar a documentação SWAGGER.

