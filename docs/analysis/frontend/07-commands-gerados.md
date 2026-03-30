# Commands Gerados e Relação com o Diagnóstico

## `fe-analyze-feature.md`

Criado porque o projeto precisa começar por leitura de `DesignSystem`, `Composites`, `Lab` e serviços antes de qualquer implementação. Evita que novas features ignorem a arquitetura host fino + library compartilhada.

## `fe-implement-feature.md`

Criado para traduzir os achados de arquitetura e reaproveitamento em execução prática. Endereça o risco de implementar direto em páginas-lab ou duplicar contratos.

## `fe-create-component.md`

Criado para governar a decisão entre base, variante, composite e showcase. Endereça o risco de expansão desordenada do DS.

## `fe-refactor-component.md`

Criado porque hoje não há suíte robusta; refatorações precisam de um playbook mais rigoroso para não quebrar contratos silenciosamente.

## `fe-add-tests.md`

Criado para atacar a lacuna mais importante do estado atual: ausência de testes úteis.

## `fe-validate-tests.md`

Criado para evitar que a futura suíte nasça com falso positivo, falso negativo e flakiness, especialmente em serviços com timer e charts com interatividade.
