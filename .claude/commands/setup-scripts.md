# Setup de Scripts

Crie ou atualize a pasta `scripts/` para que o projeto possa ser executado localmente com comandos previsíveis.

## Objetivo

Gerar scripts de inicialização, testes e orquestração adequados à stack real do repositório.

## Procedimento

1. Leia a raiz do projeto para identificar a stack, ponto de entrada, portas, dependências externas e projetos relacionados.
2. Gere scripts shell em `scripts/` com:
   - `set -euo pipefail`
   - resolução de `SCRIPT_DIR` e `PROJECT_ROOT`
   - helpers de cor para `info`, `success`, `warn` e `error`
   - carregamento de `.env.development` com prioridade sobre `.env`
   - validação de pré-requisitos
   - gerenciamento de portas em scripts de orquestração
   - signal handling e cleanup de processos em background
   - health check com retry
   - banner com URLs, portas e logs
3. Adicione `scripts/.logs/` e `scripts/.pids/` ao `.gitignore`.
4. Gere `scripts/README.md` com:
   - tabela dos scripts
   - portas padrão
   - variáveis de ambiente suportadas
   - pré-requisitos
   - exemplos de uso

## Regras

- Não invente serviços que o repositório não possui.
- Preserve os entrypoints reais do projeto.
- Use valores padrão seguros, mas permita override por variáveis de ambiente.
- Prefira health checks HTTP simples quando não houver endpoint dedicado.
- Se o projeto for frontend-only, `full-stack/start-all.sh` pode orquestrar apenas o frontend.
