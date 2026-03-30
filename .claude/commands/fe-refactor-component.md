# fe-refactor-component

## Quando usar

Use para refatorar componente base, composite ou infraestrutura de UI sem quebrar contrato existente.

## Pré-condições

- Ler o componente atual e seus consumidores
- Levantar usos em `Lab/Pages` e demais arquivos `.razor`
- Identificar estados e props públicas impactadas

## Passos

1. Mapear o contrato atual do componente.
2. Localizar consumidores principais e exemplos de uso.
3. Separar mudança interna de mudança de contrato.
4. Preservar comportamento observável ou explicitar a quebra.
5. Validar estados críticos e acessibilidade.
6. Atualizar demonstrações e documentação operacional.

## Cuidados

- Sem suíte robusta, toda refatoração deve ser mais conservadora.
- Mudanças em `CssClassBuilder`, tokens e CSS scoped merecem validação extra.
- Componentes com timers, overlays ou charts pedem atenção redobrada.

## Anti-padrões

- Refatorar apenas olhando o arquivo do componente.
- Mudar nomes de props ou slots sem verificar consumidores.
- Misturar limpeza interna com redesign sem delimitação clara.

## Checklist de qualidade

- [ ] Os consumidores relevantes foram revisados
- [ ] O contrato público permaneceu claro
- [ ] Estados e a11y continuam corretos
- [ ] A refatoração não empurrou contrato do DS para uma página-lab
