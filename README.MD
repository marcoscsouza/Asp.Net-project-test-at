# Migrations
1� Commit e suas etapas
 criar uma solu��o
 criar as solutions folders
 criar o project MVC e as CLASS LIBRARY dentro das solutions folders (DDD)
 Fazer referencias aos projetos
 criar os modelos dentro de Domain.Model para as entidades
 criar os controllers para gerar com view usando entity
 fazer a migra��o do Context para a pasta Data dentro do projeto Data

## Aten��o! rodar esses comandos dentro do projeto Data
Add
```powershell
dotnet ef migrations add Initial --startup-project ..\Asp.NetAT\Asp.NetAT.csproj 
```
rodar esse comando dentro da pasta data na camada de infra

Update
```powershell

dotnet ef database update --startup-project ..\Asp.NetAT\Asp.NetAT.csproj 
```
1� Commit - Cria��o do projeto e seus modelos
	Feito o b�sico para o funcionamento das duas Models e seus controllers gerados com view usando entity
	e depois feita a migra��o do Context para a pasta Data dentro do projeto Data 
	e por ultimo feita a migrations add e update.

Como � o acesso quando criado:
	A controller acessar diretamente o Context e faz os comandos
Como tem que ficar:
	A controller vai acessar o Service, que vai acessar o Repository, que s� ent�o vai fazer as opera��es no Context.
	(sempre abstraindo por meio das Interfaces)

2� Commit - migra��o para o modelo e infra para o DDD
	Criar as interfaces dentro do Domain.Model para repository e service dos models. ex: IBandaRepository e IServiceRepository
	criar as classes de Service no projeto Domain.Service e Repository no projeto Data com suas implementa��es
	fazer as modifica��es no controller onde acessava o context diretamente agora vai acessar o service
	dentro de Crosscutting.IoC tem que criar a inje��o de dependencia para funcionar e add o seu metodo dentro da startup.cs 
	e tirar a stringConnect da startup para bootstrapper
	mudar as view de musico onde estiver ViewBag.bandaId para Bandas ou de id para nome.


	