# PayMart.BFF

**Atenção:** Este projeto faz parte de uma arquitetura de microserviços e depende de outras APIs para funcionar corretamente. Por favor, certifique-se de baixar e configurar todas as APIs listadas abaixo antes de executar este projeto.

## Descrição do Projeto

O `PayMart.BFF` é o ponto central de comunicação para as APIs do sistema PayMart. Este projeto foi desenvolvido em .NET Core 8 e segue boas práticas de desenvolvimento, como SOLID, Design Patterns, e uso de frameworks específicos. Ele faz parte de um conjunto de microserviços projetados para fornecer uma solução escalável e robusta para gerenciamento de pagamentos, clientes, pedidos, e produtos.

### APIs Necessárias

Para que o `PayMart.BFF` funcione corretamente, é necessário clonar e configurar as seguintes APIs:

- [PayMart.Login](https://github.com/giovannialopes/PayMart.Login)
- [PayMart.Clients](https://github.com/giovannialopes/PayMart.Clients)
- [PayMart.Products](https://github.com/giovannialopes/PayMart.Products)
- [PayMart.Orders](https://github.com/giovannialopes/PayMart.Orders)
- [PayMart.Payments](https://github.com/giovannialopes/PayMart.Payments)

Certifique-se de que todas essas APIs estejam rodando corretamente antes de iniciar o `PayMart.BFF`.

## Futuras Funcionalidades

O projeto está em desenvolvimento contínuo, e as seguintes funcionalidades estão planejadas para futuras implementações:

- **Criação de Testes Unitários**: Implementação de testes unitários para garantir a qualidade e a estabilidade do código.
- **RabbitMQ**: Integração com RabbitMQ para gerenciamento de filas e comunicação assíncrona entre microserviços.

## Estrutura do Projeto

O projeto `PayMart.BFF` segue uma estrutura de pastas organizada para garantir a escalabilidade e manutenção do código. As principais camadas incluem:

- **API's-Project**: Responsável por receber requisições e retornar respostas.
- **Domain-Project**: Contém a regra de negócio do produto, models e interfaces.
- **Infraestrutura-Project**: Responsável pela comunicação externa, como banco de dados e serviços de terceiros.

## Pré-requisitos

Antes de iniciar o projeto, certifique-se de que você tem as seguintes ferramentas instaladas:

- [.NET Core 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/)
- [Git](https://git-scm.com/)

## Configuração do Projeto

### 0. Após fazer o download de todos os projetos, jogue o arquivo docker-compose.yml para a pasta PayMart que contempla todos os outros microserviços.

### 1. Clonar o Repositório

```bash
git clone https://github.com/seuusuario/PayMart.BFF.git
```

### 2. Navegar até a Pasta do Projeto onde está TODAS as aplicações

```bash
cd PayMart
```

### 3. Configuração com Docker

O projeto já está configurado para rodar com Docker. Siga os passos abaixo para garantir que o ambiente esteja pronto:
    1- Certifique-se de que o Docker está em execução.
    2- Utilize o comando abaixo para construir e rodar os containers:

```bash
docker-compose up --build
```
Este comando criará os containers e iniciará a aplicação junto com os serviços necessários.

### 5. Configuração do Banco de Dados

Lembre-se de alterar o caminho do banco de dados no arquivo appsettings.json de cada aplicação antes de iniciar o sistema. Certifique-se de que as configurações de conexão estejam corretas para o seu ambiente.

## Contribuições

Contribuições são bem-vindas! Se você tiver sugestões ou encontrar algum problema, sinta-se à vontade para abrir uma issue ou enviar um pull request.

