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

ideia inicial do 2� commit
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

3� Commit - implementar o Search e OrderAscendant
	Criar as IndexViewModels dentro do presentation para fazer a search e orderAscendant
	mudar o metodo de Index dentro de controller para receber IndexViewModels
	Mudar a view Index de Banda e Musico para receberem seus IndexViewModel.cs

4� Commit - Migra��o dos Models para ViewModels
	Criar as ViewModel para as duas entidades dentro de presentation
	Atentar para propety que herda o model para herdar agora o ViewModel nas duas entidades
	Atentar para a referencia ciclica que pode ocasionar em erros no arquivo Json

		copiar todas as proprerties para o viewModel e criar 2 metodos
		metodo FROM() : sempre recebe a classe, ela pega o model e transforma em viewModel
			Usado no GET: para pegar o model que fica salvo no banco
		metodo ToModel() : sempre recebe a variravel, ele pega a viewModel e converte em model
			Usado no POST: para pegar a viewModel que o usuario transformou e salvar no banco novamente como Model
	Fazer as mudan�as em todas as views menos no Index que tem seu proprio
	Modificar o controller para fazer as conver��es dos Models usando From e dos ViewModels usando ToModel()

5� Commit - Valida��es b�sicas e Remote
	Valida��es basicas basta alterar coloca-las no viewModel que j� ser� usadas
	Remote
		usara uma action para servir de metodo e ser� direcionada na controller da banda
		o AdditionalFields serve para comparar pelo Id e evitar erros de compara��o
		remote acessa diretamente o banco ent�o precisa ser feita a ponte
			o ViewModel => Controller => IService(Service) => IRepository(Repository) => Context

### Assim conclui o necessario para o TP1 (Vai at� a aula 6)