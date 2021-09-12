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

8° Commit - começando o desacoplamento da aula 10
	removida as referencias do MVC
	removido as converções de ToModel e From
	IndexViewModel recebendo diretamente o ViewModel e não mais Model
	criado os Services e suas implementações e coloca-las nas controllers para substituir todos os metodos feitos antes 
	corrigir a dependencia do startup
	dica: a ideia principal ate o momento é fazer o MVC funcionar sem usar as outras camadas com o fake
	criar a webApi e colocar as referencias que estavam no MVC e as connectionString do appSettings.json
	consertar os migrations para receberem na webApi
	Criar as ApiController para servir de endPoints dos controllers do MVC
	gerar os verbos http para seguir o padrão restful
	criar os parametros para o apiController
	basicamente o ApiController vai fazer tudo que o Controller do MVC ja fazia 
	até a aula 10 a BandaApiController ja funciona porem sem muita ligação com o MVC

## GRANDE DICA
#### a comunicação entre WebApi e o presentation(MVC) se dá por string num jogo de descerializar de um lado e serealizar de outro,
#### Cada lado precisa de um modelo para saber onde cada informação ta indo, por exemplo Nome dos dois lados para quando chegar no outro lado(WebApi) saber que é um nome
 Browser(View onde o usuario preenche o formulario) 
		=> AutorController(do presentation usando o CRUD básico Index, Details e etc...) 
			⇒ AutorHttpService (tambem do presentation onde converte o CRUD básico em verbos HTTP GET, POST e etc...) 
				⇒ AutorApiController (do WebApi onde recebe os verbos HTTP e faz as operações de backEnd que seguiram para as camadas de negocio e persistencia)

Add
```powershell
dotnet ef migrations add Initial --startup-project ..\WebApi\WebApi.csproj 
```
rodar esse comando dentro da pasta data na camada de infra

Update
```powershell

dotnet ef database update --startup-project ..\WebApi\WebApi.csproj 
```

9° Commit - Continuação do WebApi
	criando os metodos no httpService usando os verbos http
	primeiro foi criada o GetAll e corrigida a problematica de loop na serialização 
		esse problema foi resolvido com a biblioteca do newtonsoft dentro de startup
	fazer o GetById é muito parecido com GetAll, vai passar o id na rota e não sera mais coleção
	malandragem! colocar o httpClient no construtor para não repetir código
	fazer o Create utilizando o verbo POST, ele precisa ser processado seguindo uma formula de bolo...
		dica tbm de iniciar o serializador como uma variavel estatica de campo
		create só funcionou depois que coloquei a validação remote para funcionar add ela no apicontroller
	edit é parecido com create, mas deu error 405 pq tem que mandar o id na rota tbm escolher mudar a assinatura
		tem que serializar do outro lado(ApiController) por causa do vai e volta
	Delete não tem forma nova de fazer com JsonAsync, e add o ensureSuccessStatusCode para evitar problemas 
		getFrom já faz isso
	A validação do tipo remote recebe um GET diferente 
	basta replicar tudo para a outra entidade(musico)
	Faltando agora terminar o index para retornar valores para o search e order ascendant

10° Commit - 