# Structure

    - MovieStore.Api => WebApi com o serviço REST (via Swagger)
    - MovieStore.Core => Class Library para a camada do Domínio da Aplicação
    - MovieStore.Data => Class Library para a camada de Dados da Aplicação (EF, Dapper)
    - MovieStore.Services => Class Library para a camada de Services da Aplicação (regras de negócio)
    - MovieStore.Tests => Projeto de Testes de Integração da Aplicação (parcialmente implementado)

# Get Started:

1 - Faça o restore dos projetos (dotnet restore)

2 - Acesse a pasta principal dos projetos (backend) via terminal e execute o comando abaixo para criar a base de dados:

dotnet ef --startup-project MovieStore.Api/MovieStore.Api.csproj database update

OBS.: Para executar o comando acima, é preciso instalar o pacote de ferramentas do Entity Framework Core CLI. Caso não tenha instalado, utilize o comando abaixo para instalar:

dotnet tool install --global dotnet-ef

3 - Execute o projeto MovieStore.Api com o comando abaixo:

dotnet run -p MovieStore.Api/MovieStore.Api.csproj

# Tests

1 - Acesse a pasta principal dos projetos e execute o comando abaixo para executar os testes:
dotnet test MovieService.Tests/MovieService.Tests.csproj

# Dapper

1 - Há uma consulta com Dapper em MovieStore.Data/Repositories/GenreRepository.cs no método GetAllAsync()

2 - Também há uma segunda consulta com Dapper em MovieStore.Data/Repositories/MovieStore.cs no método GetAllWithGenreAsync(), porém está comentado devido ao Dapper não suportar (pelo que pesquisie) a configuração In Memory utilizada no projeto de Testes, e como este repositório é utilizado nos testes, tive que comentar. Caso rodar a aplicação e não os testes é possível "descomentar" este código que funcionará corretamente (e comentar o código padrã odo EF).
