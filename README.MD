# Migrations
1° Commit e suas etapas
 criar uma solução
 criar as solutions folders
 criar o project MVC e as CLASS LIBRARY dentro das solutions folders (DDD)
 Fazer referencias aos projetos
 criar os modelos dentro de Domain.Model para as entidades
 criar os controllers para gerar com view usando entity
 fazer a migração do Context para a pasta Data dentro do projeto Data

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
1° Commit - Criação do projeto e seus modelos
	Feito o básico para o funcionamento das duas Models e seus controllers gerados com view usando entity
	e depois feita a migração do Context para a pasta Data dentro do projeto Data 
	e por ultimo feita a migrations add e update.

Como é o acesso quando criado:
	A controller acessar diretamente o Context e faz os comandos
Como tem que ficar:
	A controller vai acessar o Service, que vai acessar o Repository, que só então vai fazer as operações no Context.
	(sempre abstraindo por meio das Interfaces)

2° Commit - migração para o modelo e infra para o DDD
	Criar as interfaces dentro do Domain.Model para repository e service dos models. ex: IBandaRepository e IServiceRepository
	criar as classes de Service no projeto Domain.Service e Repository no projeto Data com suas implementações
	fazer as modificações no controller onde acessava o context diretamente agora vai acessar o service
	dentro de Crosscutting.IoC tem que criar a injeção de dependencia para funcionar e add o seu metodo dentro da startup.cs 
	e tirar a stringConnect da startup para bootstrapper
	mudar as view de musico onde estiver ViewBag.bandaId para Bandas ou de id para nome.


	
