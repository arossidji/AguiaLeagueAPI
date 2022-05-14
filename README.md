# Rodando o projeto no Visual Studio 2022:
1. Abra o Package Manager Console, selecione o projeto `Data` e rode o comando `Update-Database`. Isso irá criar o banco de dados Postgres  
<img src="https://i.imgur.com/DQL6Yun.png"/>

2. Execute o projeto `AguiaLeague.API`

# Tecnologias utilizadas:
- ASP.NET 6.0
- ASP.NET WebApi Core com autenticação JWT Bearer
- Entity Framework Core 6
- Injeção de dependências nativa do .NET Core
- AutoMapper
- FluentValidator
- Swagger UI com suporte ao JWT

# Arquitetura:
- Domain Driven Design
- Onion architecture
