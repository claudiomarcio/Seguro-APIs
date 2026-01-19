📌 Visão Geral

Este projeto foi desenvolvido como parte de um teste técnico, com o objetivo de demonstrar
a aplicação de boas práticas em um cenário realista:

	Arquitetura Hexagonal (Interfaces & Adapters)

	Princípios de DDD (Domain-Driven Design)

	SOLID

	Separação clara de responsabilidades

	Testes automatizados em múltiplas camadas

	Comunicação entre microserviços

	Segurança via JWT

	Observabilidade (Health Checks)

	Execução local e via Docker

	O sistema é composto por dois microserviços independentes:

		PropostaService — responsável pelo ciclo de vida de propostas

		ContratacaoService — responsável pela contratação de propostas aprovadas

🧱 Arquitetura

	O projeto segue Arquitetura Hexagonal, com a seguinte separação por serviço:

	Api            → Adapters de entrada (HTTP)
	Application    → Casos de uso (UseCases)
	Domain         → Regras de negócio puras
	Data           → Infraestrutura (EF Core, Repositories)
	IoC            → Injeção de dependência
	Tests          → Testes automatizados

	Cada microserviço possui:

		API própria
		Banco de dados próprio
		Migrations independentes
	
🔄 Comunicação entre serviços

	O ContratacaoService se comunica com o PropostaService via HTTP

	A comunicação é feita através de Interfaces, mantendo desacoplamento

	Implementação via HttpClient configurado no projeto IoC
	
	Autenticação service-to-service via JWT Bearer Token

🔐 Segurança (JWT)

	As APIs são protegidas via JWT Bearer Authentication.

	Autenticação aplicada nos controllers

	Geração de token via endpoint de autenticação (apenas para fins de teste)

	Comunicação entre microserviços também autenticada via JWT
	
	▶️ Gerar Token (Swagger)
		POST /api/auth/token
	Copie o token retornado e, no Swagger, clique em Authorize:
		Bearer {SEU_TOKEN_AQUI}
		
🩺 Health Checks

	Cada API expõe o endpoint:

	GET /health
	
	O health check cobre:

		Disponibilidade da API

		Conectividade com o banco de dados

		Dependências externas (no caso da Contratação → Proposta)

		Os health checks também são utilizados pelo Docker Compose para controlar a ordem de inicialização dos serviços.
		
		
🚀 Tecnologias Utilizadas

	.NET 8

	ASP.NET Core

	Entity Framework Core

	SQL Server

	xUnit

	Moq

	EF Core InMemory (testes)

	Swagger / OpenAPI

	Docker & Docker Compose

	JWT Bearer Authentication

	Polly (Retry / Timeout)
	

🧪 Estratégia de Testes

	Foram implementados testes automatizados em todas as camadas relevantes:

	✔ Domain Tests

	Regras de negócio puras

	Validação de invariantes

	✔ Application Tests

	Casos de uso isolados

	Mock de repositórios e serviços externos

	✔ Controller Tests

	Validação de contratos HTTP

	Status codes

	Fluxos de sucesso e erro

	✔ Repository Tests

	Testes de persistência com EF Core InMemory

▶️ Como Executar Localmente (Visual Studio)

	Pré-requisitos

		.NET SDK 8+

		SQL Server LocalDB

		Visual Studio 2022+

	Passos

		Clone o repositório

		Abra o arquivo Seguro.Hexagonal.sln

		Defina os projetos de API como Startup (Proposta e Contratação)

		Execute (F5)

	As migrations são aplicadas automaticamente no startup.


🐳 Executando com Docker (Recomendado)

	Na raiz do projeto:
		docker compose down -v
		docker compose up --build
		
	Acessos

	PropostaService
		http://localhost:5001/swagger/index.html

	ContratacaoService
		http://localhost:5002/swagger/index.html

🌐 Endpoints Principais
PropostaService

	POST /api/propostas

	GET /api/propostas

	GET /api/propostas/{id}

	PATCH /api/propostas/{id}/status

ContratacaoService

	POST /api/contratacoes


🔄 Fluxo Funcional do Sistema

	O sistema possui um fluxo de negócio bem definido, dividido entre os dois microserviços.

	🧩 Visão Geral do Fluxo
	
		🔐 Autenticação no Fluxo

			Todos os endpoints (exceto /health) exigem autenticação via JWT.
            Endpoint: POST /api/auth/token

		Criar uma Proposta (PropostaService)
							
			Serviço: PropostaService
			Endpoint: POST /api/propostas

            Cria uma nova proposta com status inicial 'Criada'.
			
			Resultado:

				Retorna 201 Created

				Retorna o id da proposta

				⚠️ Esse id será utilizado nas próximas etapas.
			
		Consultar a Proposta criada (PropostaService)
		
			Serviço: PropostaService
			Endpoint: GET /api/propostas/{id}

            Permite verificar os dados da proposta e seu status atual.
			

		Aprovar a Proposta (PropostaService)
			
			Serviço: PropostaService
			Endpoint: PATCH /api/propostas/{id}/status
			
			Body:
			    {
				  "status": "Aprovada"
				}

            Altera o status da proposta para Aprovada.
			
			Regras importantes:

				Apenas propostas em estado válido podem ser aprovadas.
				Retorna 204 No Content em caso de sucesso.

		Contratar uma Proposta aprovada (ContratacaoService)
		
			Serviço: ContratacaoService
			Endpoint: POST /api/contratacoes
			
			Cria uma contratação somente se a proposta estiver aprovada.
			
			Regras de negócio:

				O serviço consulta o PropostaService.
				Se a proposta não estiver aprovada, a contratação é recusada.
				Garante consistência entre serviços.


        🧠 Resumo Visual do Fluxo
			[POST /propostas]
					↓
			[GET /propostas/{id}]
					↓
			[PATCH /propostas/{id}/status → Aprovada]
					↓
			[POST /contratacoes]

🧠 Decisões Arquiteturais Importantes

	Controllers são thin adapters

	Nenhuma regra de negócio está na API

	UseCases são acessados via interfaces

	Repositories são acessados via Interfaces

	Domain não depende de framework

	Infraestrutura é completamente isolada

📂 Organização do Repositório

	Seguro.Hexagonal
	│
	├── PropostaService
	│   ├── Api
	│   ├── Application
	│   ├── Domain
	│   ├── Data
	│   ├── IoC
	│   └── Tests
	│
	├── ContratacaoService
	│   ├── Api
	│   ├── Application
	│   ├── Domain
	│   ├── Data
	│   ├── IoC
	│   └── Tests
	│
	├── docker-compose.yml
	└── README.md

📐 Diagrama Simples da Arquitetura (Bonus)

    ┌──────────────────────────────┐
	│          Cliente             │
	│  (Swagger / HTTP Client)     │
	└──────────────┬───────────────┘
	               │ JWT Bearer
	               ▼
	┌──────────────────────────────┐
	│        PropostaService       │
	│──────────────────────────────│
	│ API (Controllers)            │
	│  └── AuthController          │
	│  └── PropostasController     │
	│  └── HealthController        │
	│                              │
	│ Application (UseCases)       │
	│  └── CriarProposta           │
	│  └── ListarPropostas         │
	│  └── AlterarStatus           │
	│                              │
	│ Domain                       |    
	│  └── Entities                │
	│  └── Enums                   │
	│  └── Exceptions              │
	│  └── Interfaces              │
	│                              │
	│ Data (EF Core)               │
	│  └── Repositories            │
	│                              │
	│ Database                     │
	│  └── PropostaDb              │
	└──────────────┬───────────────┘
	               │ HTTP + JWT
	               ▼
	┌──────────────────────────────┐
	│      ContratacaoService      │
	│──────────────────────────────│
	│ API (Controllers)            │
	│  └── AuthController          │
	│  └── ContratacoesController  │
	│  └── HealthController        │
	│                              │
	│ Application (UseCases)       │
	│  └── ContratarProposta       │
	│                              │
	│ Domain                       |	
	│  └── Entities                │
	│  └── Enums                   │
	│  └── Exceptions              │
	│  └── Interfaces              │
	│                              │
	│ IoC / Infra                  │
	│  └── HttpClient Proposta     │
	│  └── Polly (Retry/Timeout)   │
	│                              │
	│ Data (EF Core)               │
	│  └── Repositories            │
	│                              │
	│ Database                     │
	│  └── ContratacaoDb           │
	└──────────────────────────────┘

👤 Autor

Cláudio Márcio
Desenvolvedor .NET
