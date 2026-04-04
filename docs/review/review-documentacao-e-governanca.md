# Contexto da Revisão

Revisão atualizada em 2026-04-04 após a consolidação dos charts do design system, a padronização de execução local por `scripts/full-stack/` e a limpeza dos artefatos locais de referência usados durante a exploração visual.

## Escopo Revisado

- `CLAUDE.md`
- `AGENTS.md`
- `docs/review/`
- `docs/architecture.md`
- `docs/design-system.md`
- `docs/conventions.md`
- `.claude/commands/`
- `scripts/README.md`
- código de referência:
  `src/Ganesha.DesignLab.Web/**`
  `src/Ganesha.DesignLab.Shared/**`

## Inconsistências Críticas

Nenhuma encontrada na revisão atual.

## Inconsistências Moderadas

## [MODERADA] Relatórios de review precisam ser mantidos como artefatos vivos

**Arquivos envolvidos:** `docs/review/quality-gate.md`, `docs/review/review-documentacao-e-governanca.md`  
**Tipo:** Drift de governança  
**Impacto:** Quando o repositório evolui, o review antigo pode continuar correto como histórico, mas incorreto como retrato do estado atual. Isso gera leitura errada de pendências já resolvidas.  
**Evidência:** A versão anterior destes relatórios ainda apontava drift crítico em `docs/architecture.md`, `docs/design-system.md` e `docs/conventions.md`, mas os documentos centrais já estavam alinhados ao código atual.  
**Correção sugerida:** Atualizar os relatórios em rodadas estruturais relevantes e deixar claro quando um review é snapshot histórico versus estado corrente.  
**Prioridade:** Próximo ciclo contínuo

## Melhorias de Clareza

## [BAIXA] Falta uma trilha curta e explícita de operação eficiente com Codex em um único bloco

**Arquivos envolvidos:** `CLAUDE.md`, `AGENTS.md`, `scripts/README.md`, `.claude/commands/`  
**Tipo:** Clareza operacional  
**Impacto:** Baixo, mas reduz eficiência quando alguém novo no repositório tenta descobrir como analisar, validar e subir a stack sem cair em fluxo manual.  
**Evidência:** As peças já existem, mas estavam distribuídas entre docs diferentes.  
**Correção sugerida:** Consolidar no onboarding operacional um fluxo mínimo:

1. Ler `AGENTS.md` e `CLAUDE.md`
2. Subir a stack com `./scripts/full-stack/start-all.sh`
3. Usar `.claude/commands/review-documentacao-e-governanca.md` para drift documental
4. Usar `.claude/commands/quality-gate.md` antes de merge
5. Priorizar scripts oficiais em vez de `dotnet run` ad-hoc

**Prioridade:** Agora

## Aderência Atual ao Código

Os documentos centrais revisados estão coerentes com o repositório atual:

- `docs/architecture.md`
- `docs/design-system.md`
- `docs/conventions.md`
- `CLAUDE.md`
- `AGENTS.md`
- `.claude/commands/quality-gate.md`
- `.claude/commands/review-documentacao-e-governanca.md`
- `.claude/commands/setup-scripts.md`

## Comandos e Governança Operacional

Os seguintes comandos já existem e reduzem a necessidade de fluxo manual disperso:

- `quality-gate`
- `review-documentacao-e-governanca`
- `setup-scripts`
- `propose-improvements`
- `fe-*`

Isso fecha parcialmente a lacuna reportada na revisão anterior, que citava commands ausentes já materializados no repositório.

## Fluxo Recomendado Para Uso Mais Eficiente do Codex

### Antes de editar

- Ler `AGENTS.md` e `CLAUDE.md`
- Ler os docs-alvo em `docs/architecture/frontend/` e `docs/analysis/frontend/`
- Verificar `.claude/rules/frontend-*.md`

### Para subir a aplicação

- Usar `./scripts/full-stack/start-all.sh`
- Evitar `dotnet run` manual como fluxo padrão de validação local

### Para revisar documentação e governança

- Usar o command `review-documentacao-e-governanca`
- Atualizar `docs/review/` quando a rodada alterar estrutura, scripts ou fluxo operacional

### Para encerrar rodada

- Usar o command `quality-gate`
- Registrar o que foi validado, o que não foi validado e o risco residual

## Documentação Que Continua Correta

- `docs/analysis/frontend/*`
- `docs/architecture/frontend/*`
- `docs/architecture.md`
- `docs/design-system.md`
- `docs/conventions.md`
- `CLAUDE.md`
- `AGENTS.md`
- `.claude/rules/frontend-*.md`
- `.claude/commands/fe-*.md`
- `.claude/commands/quality-gate.md`
- `.claude/commands/review-documentacao-e-governanca.md`
- `.claude/commands/setup-scripts.md`

## Riscos Residuais

- Ainda não há suíte de testes frontend para sustentar futuras rodadas com segurança.
- O host MAUI segue com menor cobertura documental e operacional do que o host web.
- Parte da eficiência com Codex ainda depende de disciplina do time para usar o fluxo oficial.

## Ações Recomendadas

1. Tratar `docs/review/` como artefato corrente e revalidar em rodadas estruturais.
2. Consolidar no onboarding do projeto o fluxo mínimo de operação com Codex.
3. Criar a primeira suíte de testes para contratos visuais e de comportamento críticos.
