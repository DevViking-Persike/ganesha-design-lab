# Frontend Reaproveitamento

## Objetivo

Forçar reaproveitamento saudável antes de criação nova e reduzir duplicação estrutural no catálogo e na biblioteca compartilhada.

## Regras obrigatórias

- Antes de criar novo componente, pesquisar em `Components/DesignSystem/` e `Components/Composites/`.
- Antes de criar novo header, toolbar, shell ou card especializado, validar se `GnsPageHeader`, `GnsSearchFilter`, `GnsAppShell`, `GnsCard`, `GnsSection` e `GnsGrid` já resolvem.
- Extrair novo composite apenas quando houver repetição real, não por antecipação.
- Evoluções pequenas devem preferir extensão do contrato existente a criação de componente paralelo.

## Permitido

- Manter implementação local na página quando o caso for claramente único e demonstrativo.
- Criar variante nova em componente existente quando isso preservar coesão do contrato.
- Extrair helper leve ou composite quando a repetição já estiver comprovada em dois ou mais contextos.

## Proibido

- Criar wrapper novo sem verificar equivalente existente.
- Duplicar componentes só para mudar texto, ícone, cor ou ordem de slots.
- Consolidar cedo demais uma composição que ainda apareceu uma única vez.

## Sinais de alerta

- Dois componentes com mesmo esqueleto e nomes diferentes.
- Página-lab copiando markup de outra página com pequenas alterações.
- Novos componentes que apenas repassam props para um componente base já existente.

## Checklist de revisão

- [ ] Pesquisei equivalentes em `DesignSystem` e `Composites`?
- [ ] O caso exige componente novo ou apenas variante/composição?
- [ ] Há repetição comprovada suficiente para extrair?
- [ ] A abstração reduz duplicação sem esconder demais o comportamento?
