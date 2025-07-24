# CustomerService.API

## Introdução

Um microsserviço desenvolvido com .NET 8 seguindo princípios da Clean Architecture e DDD.
Este projeto está em andamento, e o seu objetivo é servir como prova de conceito para aplicação de boas práticas de
arquitetura utilizando ASP.NET Core.

## Tecnologias

- ASP.NET Core
- Entity Framework Core
- MSSQL Server (via container)
- Mensageria assíncrona com RabbitMQ (via container)
- Clean Architecture
- DDD estratégico
- Docker Compose (para orquestração)
- Testes automatizados (planejados)

## Destaques

### Aplicação da Clean Architecture

Neste projeto, adotei Clean Architecture para garantir uma separação clara de responsabilidades, facilitar a manutenção,
testabilidade e evolução.

#### Estrutura por camadas

O CustomerService.API está dividido em camadas distintas:

**- Domain**

Contém regras de negócio e entidades centrais. Não depende de nenhuma outra camada.

**- Application**

Responsável pelos casos de uso, orquestrando regras de negócio do domínio. Esta camada depende do domínio, mas é
independente da infraestrutura.

**- Infrastructure**

Implementa detalhes técnicos de infraestrutura, como acesso ao banco de dados (via Entity Framework Core), mensageria (
MassTransit com RabbitMQ), etc.

**- API (Presentation)**

Exposição da aplicação ASP.NET Core Web API. Trata requisições e faz o mapeamento para os casos de uso da camada de
aplicação.

### Mensageria com RabbitMQ

Neste projeto, implementei um sistema de mensageria utilizando RabbitMQ como broker de mensagens e MassTransit como lib
de integração para publicação e consumo de eventos assíncronos entre microsserviços.

### Padronização de Respostas com Result<T>

Para garantir consistência, clareza e facilidade no tratamento de erros em toda a aplicação, foi implementado um sistema
de resposta padronizada baseado no padrão Result<T>. Esse padrão encapsula os resultados das operações em objetos que
indicam claramente sucesso ou falha, com mensagens e tipos de erro bem definidos.

### Observabilidade e logs

Implementei **Serilog** para logging estruturado, permitindo a rastreabilidade completa das requisições, eventos de
domínio e operações das regras de negócio.

**Características**

- Logs estruturados no formato JSON, ideais para ferramentas como Seq, Grafana Loki, ELK, etc.
- Captura automática de:
    - Caminho da requisição (RequestPath)
    - Verbo HTTP (RequestMethod)
    - Tempo de execução (Elapsed)
    - Status code
    - Exceções não tratadas
- Enriquecimento com contexto da aplicação:
    - Application, MachineName, SourceContext, etc.
- Logs do tipo Information, Warning, Error, e Debug (apenas em Development)
- Padrão seguro: sem logs sensíveis ou dados pessoais expostos

**Exemplo de log**

```json
{
  "@t": "2025-07-23T10:34:28.1883215-03:00",
  "@mt": "Novo cliente com nome {RequestName} e e-mail {RequestEmail} criado com Id {CustomerId}",
  "@l": "Information",
  "RequestName": "Darth Vadder",
  "RequestEmail": "darth@sith.com",
  "CustomerId": "28e4ad2d-77fc-47a9-84ff-f32c859c5e8d",
  "RequestPath": "/api/v1/customers",
  "RequestMethod": "POST",
  "StatusCode": 200,
  "Elapsed": 143.2,
  "SourceContext": "Application.UseCases.Customers.Create.CreateCustomerInteractor"
}
```

## Autor

[Anderson Vieira](https://linkedin.com/in/vieira-a)
