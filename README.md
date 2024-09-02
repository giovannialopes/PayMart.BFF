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

No futuro, planejo incluir as seguintes funcionalidades no projeto:

- **Docker**: Suporte para execução do projeto em containers Docker, facilitando o deploy e a escalabilidade.
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
- [Git](https://git-scm.com/)
- [Docker](https://www.docker.com/) (opcional)

## Configuração

1. Clone este repositório:

    ```bash
    git clone https://github.com/seuusuario/PayMart.BFF.git
    ```

2. Navegue até a pasta do projeto:

    ```bash
    cd PayMart.BFF
    ```

3. Restaure as dependências e compile o projeto:

    ```bash
    dotnet restore
    dotnet build
    ```

4. Execute o projeto:

    ```bash
    dotnet run
    ```

## Contribuições

Contribuições são bem-vindas! Se você tiver sugestões ou encontrar algum problema, sinta-se à vontade para abrir uma issue ou enviar um pull request.

