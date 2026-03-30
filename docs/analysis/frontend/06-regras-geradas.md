# Justificativa das Regras Geradas

As regras em `.claude/rules/` foram derivadas do diagnóstico real do repositório: host web fino, biblioteca compartilhada forte, foco em Design System, ausência de suíte de testes e necessidade de reaproveitamento antes de criação nova.

## `frontend-arquitetura.md`

Conecta-se principalmente aos achados das etapas 1 e 2:

- host fino em `src/Ganesha.DesignLab.Web`
- maior parte da UI em `src/Ganesha.DesignLab.Shared`
- separação física já existente entre `DesignSystem`, `Composites`, `Infrastructure`, `Lab`, `Services` e `Models`

Objetivo:
- preservar a estrutura boa que já existe
- evitar que páginas-lab virem lugar de contratos permanentes

## `frontend-design-system.md`

Conecta-se à etapa 3:

- tokens e temas explícitos em `wwwroot/css/settings` e `wwwroot/css/theme`
- componentes base amplos em `Components/DesignSystem`
- necessidade de padronizar contratos, estados e acessibilidade mínima

Objetivo:
- consolidar o DS como produto real da biblioteca

## `frontend-reaproveitamento.md`

Conecta-se à etapa 4:

- reuso já comprovado de `GnsButton`, `GnsCard`, `GnsSection`, `GnsGrid`, `GnsPageHeader`, `GnsTable`
- risco de criar novos wrappers desnecessários ou abstração prematura

Objetivo:
- obrigar a checagem de equivalente existente antes de criar novo componente

## `frontend-testes.md`

Conecta-se à etapa 5:

- ausência completa de suíte
- necessidade de introduzir testes úteis por comportamento e contrato

Objetivo:
- orientar como adicionar testes com valor real para os componentes e serviços do DS

## `frontend-confiabilidade-testes.md`

Conecta-se à etapa 5:

- risco futuro de timers, animações, JS de charts e re-render assíncrono do Blazor
- necessidade de prevenir falso positivo, falso negativo e flakiness desde o início

Objetivo:
- evitar que a futura suíte nasça numerosa, mas pouco confiável
