# Scripts locais

Scripts de apoio para subir e validar o projeto localmente.

## Pré-requisitos

- `dotnet` com suporte ao target do projeto
- `curl`
- `lsof`

## Scripts

| Script | Finalidade | Porta padrão |
| --- | --- | --- |
| `scripts/start-frontend.sh` | Sobe o frontend Blazor localmente em foreground | `5169` |
| `scripts/start-tests.sh` | Executa a suíte de testes do projeto shared | n/a |
| `scripts/full-stack/start-all.sh` | Orquestra a stack local, grava logs e mantém cleanup de processo | `5169` |

## Variáveis de ambiente

| Variável | Default | Uso |
| --- | --- | --- |
| `FRONTEND_PORT` | `5169` | Porta HTTP do frontend |
| `FRONTEND_HOST` | `localhost` | Host do frontend |
| `FRONTEND_SCHEME` | `http` | Scheme base do frontend |
| `ASPNETCORE_ENVIRONMENT` | `Development` | Ambiente do app |
| `HEALTHCHECK_URL` | `${FRONTEND_SCHEME}://${FRONTEND_HOST}:${FRONTEND_PORT}/` | URL usada no retry de health check |
| `WEB_PROJECT` | `src/Ganesha.DesignLab.Web/Ganesha.DesignLab.Web.csproj` | Projeto web a ser executado |
| `TEST_PROJECT` | `tests/Ganesha.DesignLab.Shared.Tests/Ganesha.DesignLab.Shared.Tests.csproj` | Projeto de testes |
| `CONFIGURATION` | `Debug` | Configuração do `dotnet test` |
| `KILL_EXISTING_PORT` | `0` | Se `1`, encerra o processo atual na porta do frontend em `start-frontend.sh` |

## Prioridade de `.env`

Os scripts carregam:

1. `.env`
2. `.env.development`

Se a mesma variável existir nos dois arquivos, o valor de `.env.development` prevalece.

## Uso

Use os scripts de `scripts/full-stack/` como caminho padrão para subir a aplicação localmente. O entrypoint padrão do repositório é:

```bash
./scripts/full-stack/start-all.sh
```

Evite subir o frontend manualmente com `dotnet run` quando a intenção for validar a stack local.

## Exemplos

```bash
./scripts/full-stack/start-all.sh
./scripts/start-frontend.sh
./scripts/start-tests.sh
```

## Logs e PIDs

- Logs: `scripts/.logs/`
- PID files: `scripts/.pids/`

Esses diretórios estão ignorados no Git.
