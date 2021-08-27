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

ideia inicial do 2º commit
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

3° Commit - implementar o Search e OrderAscendant
	Criar as IndexViewModels dentro do presentation para fazer a search e orderAscendant
	mudar o metodo de Index dentro de controller para receber IndexViewModels
	Mudar a view Index de Banda e Musico para receberem seus IndexViewModel.cs

4° Commit - Migração dos Models para ViewModels
	Criar as ViewModel para as duas entidades dentro de presentation
	Atentar para propety que herda o model para herdar agora o ViewModel nas duas entidades
	Atentar para a referencia ciclica que pode ocasionar em erros no arquivo Json

		copiar todas as proprerties para o viewModel e criar 2 metodos
		metodo FROM() : sempre recebe a classe, ela pega o model e transforma em viewModel
			Usado no GET: para pegar o model que fica salvo no banco
		metodo ToModel() : sempre recebe a variravel, ele pega a viewModel e converte em model
			Usado no POST: para pegar a viewModel que o usuario transformou e salvar no banco novamente como Model
	Fazer as mudanças em todas as views menos no Index que tem seu proprio
	Modificar o controller para fazer as converções dos Models usando From e dos ViewModels usando ToModel()

5° Commit - Validações básicas e Remote
	Validações basicas basta alterar coloca-las no viewModel que já será usadas
	Remote
		usara uma action para servir de metodo e será direcionada na controller da banda
		o AdditionalFields serve para comparar pelo Id e evitar erros de comparação
		remote acessa diretamente o banco então precisa ser feita a ponte
			o ViewModel => Controller => IService(Service) => IRepository(Repository) => Context

### Assim conclui o necessario para o TP1 (Vai até a aula 6)

aulas 7 e 8 sobre identity

6° Commit  - Identity
	Fazer um scaffolding padrão de identity na camada de presentation só do login por enquanto...
		as paginas que escolhemos gerar servem para personalizar as paginas
	fazer as adaptações nos codigos para funcionar o identity
	Fazer o migrations para ter acesso a um banco de dados

	
## Migrations do Identity dentro do presentation
Add 
```powershell
dotnet ef migrations add AddInitIdentityMigration --context IdentityContext --output-dir Areas\Identity\Data\Migrations
```
Update
```powershell
dotnet ef database update --context IdentityContext
```
obs: não foi declarado o startup-project nem o project por teste mesmo, ocorreu alguns erros dificeis de identificar

## Assim conclui o necessario para o TP2 (aulas 7 e 8) bastava incluir o identity

7° Commit -	User secrets e Login pelo GOOGLE
	seguir o passo a passo dessa doc: https://www.notion.so/Aula-07-e-08-9d9ed3566c874057aee8c646a5ddf230#f120519f85464d448e3e1b832cfc29b3
	My Client-Id
		33517397278-h4kfl9q21ep434kko39llprl7aci0g41.apps.googleusercontent.com
	My Client-secret
		pb9Opcx62nD3t81jXh5StW5v

	usando User Secrets no CLI
	```powershell
	dotnet user-secrets init
	dotnet user-secrets set "Authentication:Google:ClientId" "<client-id>"
	dotnet user-secrets set "Authentication:Google:ClientId" "33517397278-h4kfl9q21ep434kko39llprl7aci0g41.apps.googleusercontent.com"
	dotnet user-secrets set "Authentication:Google:ClientSecret" "<client-secret>"	
	dotnet user-secrets set "Authentication:Google:ClientSecret" "pb9Opcx62nD3t81jXh5StW5v"
	```
	definidos os secrets agora neste pc esta habilitado para usar
	modificar a startup.cs para aceitar elas