# Frontend

## Visão geral operacional do projeto

O frontend principal roda em `src/Ganesha.DesignLab.Web` e consome a biblioteca compartilhada `src/Ganesha.DesignLab.Shared`, onde estão o Design System, os componentes compostos, os serviços de UI e as páginas-lab.

## Arquitetura identificada do frontend

- `Web`: bootstrap, assets e host
- `Shared/Components/DesignSystem`: componentes base
- `Shared/Components/Composites`: composições reutilizáveis
- `Shared/Components/Lab`: showcase e padrões
- `Shared/Services`: tema, toast, modal e drawer

## Convenções do projeto

- Prefixo de componentes: `Gns*`
- Prefixo de tokens/classes: `--gns-` e `.gns-`
- Reaproveitar antes de criar novo
- Página-lab não é o lugar para consolidar contrato permanente

## Onde ler primeiro

- `docs/architecture/frontend/00-overview.md`
- `docs/analysis/frontend/01-mapeamento-inicial.md`
- `docs/analysis/frontend/03-design-system.md`
- `.claude/rules/frontend-*.md`

## Riscos de alteração

- Não há suíte de testes frontend hoje.
- Mudanças em componentes base podem afetar várias páginas-lab silenciosamente.
- Charts, timers e tema global merecem validação extra.

## Como criar novas features

Use `/project:fe-implement-feature` quando disponível, ou siga `.claude/commands/fe-implement-feature.md`.

## Como criar ou evoluir componentes

Use `/project:fe-create-component` quando disponível, ou siga `.claude/commands/fe-create-component.md`.

## Como reaproveitar antes de criar novo

Verifique primeiro `src/Ganesha.DesignLab.Shared/Components/DesignSystem/` e `src/Ganesha.DesignLab.Shared/Components/Composites/`.

## Como escrever testes úteis

Use `/project:fe-add-tests` quando disponível, ou siga `.claude/commands/fe-add-tests.md`.

## Como validar confiabilidade dos testes

Use `/project:fe-validate-tests` quando disponível, ou siga `.claude/commands/fe-validate-tests.md`.

## Rules e commands relevantes

- `.claude/rules/frontend-arquitetura.md`
- `.claude/rules/frontend-design-system.md`
- `.claude/rules/frontend-reaproveitamento.md`
- `.claude/rules/frontend-testes.md`
- `.claude/rules/frontend-confiabilidade-testes.md`
- `.claude/commands/fe-*.md`

## Checklist antes de propor mudança

- [ ] Entendi se a mudança é host, DS base, composite ou showcase
- [ ] Verifiquei o que já existe
- [ ] Considerei estados, acessibilidade e tokens
- [ ] Avaliei impacto em tema, feedback e charts
- [ ] Planejei validação mínima mesmo sem suíte robusta
