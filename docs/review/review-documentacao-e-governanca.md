# Contexto da Revisão

Revisão executada em 2026-03-29 após a geração da nova documentação operacional e arquitetural de frontend, rules, commands e ADRs. O objetivo foi verificar aderência entre código real, documentação antiga do repositório e os novos artefatos criados nesta rodada.

## Escopo Revisado

- `CLAUDE.md`
- `AGENTS.md`
- `docs/adrs/`
- `docs/analysis/frontend/`
- `docs/architecture/frontend/`
- `.claude/commands/`
- `.claude/rules/`
- documentação antiga:
  `docs/architecture.md`
  `docs/design-system.md`
  `docs/conventions.md`
  `docs/component-criteria.md`
- código de referência:
  `src/Ganesha.DesignLab.Web/**`
  `src/Ganesha.DesignLab.Shared/**`

## Inconsistências Críticas

## [CRÍTICA] Documentação antiga ainda descreve prefixo `hlx-` e contratos que não correspondem ao código atual

**Arquivos envolvidos:** `docs/architecture.md`, `docs/design-system.md`, `docs/conventions.md`, `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-color.css`, `src/Ganesha.DesignLab.Shared/Components/DesignSystem/Actions/GnsButton.razor`
**Tipo:** Drift documental
**Impacto:** Quem seguir a documentação antiga pode implementar classes, tokens e contratos incompatíveis com o codebase real.
**Evidência:** A documentação antiga usa `hlx-` e `--hlx-*`, enquanto o código usa `gns-` e `--gns-*`. Exemplos concretos em `docs/architecture.md` e `docs/conventions.md`, contrastando com `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-color.css`.
**Correção sugerida:** Atualizar ou aposentar a documentação antiga, deixando claro que os documentos em `docs/architecture/frontend/` e `docs/analysis/frontend/` são a referência atual.
**Prioridade:** Agora

## [CRÍTICA] Documentação antiga enumera componentes e contratos não implementados

**Arquivos envolvidos:** `docs/design-system.md`, `docs/conventions.md`, `src/Ganesha.DesignLab.Shared/Components/DesignSystem/`, `src/Ganesha.DesignLab.Shared/Components/Composites/`
**Tipo:** Drift documental
**Impacto:** Pode induzir criação de código contra componentes inexistentes, revisão errada de PR e expectativa falsa sobre cobertura do Design System.
**Evidência:** `docs/design-system.md` cita `GnsIconButton`, `GnsLinkButton`, `GnsFormField`, `GnsList`, `GnsCodeBlock`, `GnsStepper`, `GnsAccordion`, `GnsTooltip`, `GnsPopover`, `GnsDropdown`, `GnsDivider`, `GnsSpacer`, `GnsContentArea`, `GnsUserMenu` e `GnsNotificationCenter`, mas esses artefatos não aparecem no inventário real de `src/Ganesha.DesignLab.Shared/Components/DesignSystem/` e `src/Ganesha.DesignLab.Shared/Components/Composites/`.
**Correção sugerida:** Remover da documentação o que não existe ou marcar explicitamente como roadmap, não como estado atual.
**Prioridade:** Agora

## Inconsistências Moderadas

## [MODERADA] Documentação antiga descreve `IThemeService` assíncrono e persistido, mas o código atual usa serviço síncrono em memória

**Arquivos envolvidos:** `docs/architecture.md`, `src/Ganesha.DesignLab.Shared/Abstractions/Services/IThemeService.cs`, `src/Ganesha.DesignLab.Shared/Services/ThemeService.cs`
**Tipo:** Drift documental
**Impacto:** Pode induzir decisões erradas sobre persistência, testes e responsabilidades dos hosts.
**Evidência:** `docs/architecture.md` descreve `SetThemeAsync(Theme theme)` e persistência via `localStorage`/`Preferences`, mas o código atual expõe `SetTheme(string theme)` e `ToggleTheme()` sem persistência.
**Correção sugerida:** Atualizar a seção de tema para refletir o serviço realmente implementado.
**Prioridade:** Próximo ciclo

## [MODERADA] A nova governança existe, mas ainda não está referenciada pelos documentos antigos centrais

**Arquivos envolvidos:** `CLAUDE.md`, `AGENTS.md`, `docs/architecture.md`, `docs/design-system.md`, `.claude/commands/`, `.claude/rules/`
**Tipo:** Governança duplicada
**Impacto:** O repositório fica com duas fontes concorrentes de orientação: documentação antiga ampla e nova governança operacional mais aderente.
**Evidência:** `CLAUDE.md` e `AGENTS.md` apontam para a nova estrutura, mas `docs/architecture.md` e `docs/design-system.md` seguem como documentos centrais sem aviso de desatualização.
**Correção sugerida:** Adicionar aviso de superseded/atualização nos documentos antigos ou alinhar seu conteúdo.
**Prioridade:** Próximo ciclo

## Melhorias de Clareza

## [BAIXA] Falta trilha explícita para os prompts/commands de qualidade ainda não materializados

**Arquivos envolvidos:** `CLAUDE.md`, `AGENTS.md`, `.claude/commands/`
**Tipo:** Artefato faltante
**Impacto:** Baixo agora, mas reduz previsibilidade de revisão futura.
**Evidência:** Nesta rodada foram criados `quality-gate`, `review-documentacao-e-governanca` e `propose-improvements`, mas ainda não existem commands equivalentes para `code-review`, `review-testes`, `review-loop`, `review-performance`, `review-seguranca` e `setup-scripts`.
**Correção sugerida:** Completar esses commands conforme necessidade operacional real.
**Prioridade:** Quando tocar a área

## Aderência e Lacunas de ADR

- Os ADRs recém-criados em `docs/adrs/ADR-0001-*` até `ADR-0005-*` estão coerentes com a arquitetura identificada e com a nova governança.
- Não foi encontrado conflito direto entre os novos ADRs e o código atual.
- Há uma lacuna potencial futura para a decisão sobre o papel do host MAUI, caso ele passe a divergir intencionalmente do host web.

## Documentação Que Continua Correta

- `docs/analysis/frontend/*`
- `docs/architecture/frontend/*`
- `CLAUDE.md`
- `AGENTS.md`
- `.claude/rules/frontend-*.md`
- `.claude/commands/fe-*.md`
- os ADRs recém-criados em `docs/adrs/`

## Riscos de Drift Residual

- Documentação antiga ainda pode ser lida antes da nova, por estar em caminhos mais curtos na raiz de `docs/`.
- Como ainda não há suíte de testes, parte da governança continua dependente de disciplina manual.
- O working tree já tinha mudanças estruturais em andamento; isso aumenta a chance de os documentos antigos ficarem mais desatualizados ainda.

## Ações Recomendadas

1. Corrigir primeiro `docs/architecture.md`, `docs/design-system.md` e `docs/conventions.md`.
2. Marcar explicitamente documentos antigos que ainda não foram harmonizados.
3. Completar os commands de qualidade restantes se eles forem entrar no fluxo normal do time.
