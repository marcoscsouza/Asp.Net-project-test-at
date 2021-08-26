# Migrations

## Atenção! rodar esses comandos dentro do projeto Data
Add
```powershell
dotnet ef migrations add Initial --startup-project ..\Asp.NetAT\Asp.NetAT.csproj 
```
rodar esse comando dentro da pasta data na camada de infra

Update
```powershell

dotnet ef database update --startup-project ..\Asp.NetAT\Asp.NetAT.csproj 
```
1° Commit - 
Feito o básico para o funcionamento das duas Models e seus controllers gerados com view usando entity
e depois feita a migração do Context para a pasta Data dentro do projeto Data 
e por ultimo feita a migrations add e update.

