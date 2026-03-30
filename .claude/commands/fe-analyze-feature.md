# fe-analyze-feature

## Quando usar

Use antes de implementar qualquer feature, tela, página-lab, composite ou componente base novo.

## Pré-condições

- Ler `docs/analysis/frontend/01-mapeamento-inicial.md`
- Ler `docs/analysis/frontend/02-diagnostico-arquitetural.md`
- Ler `docs/analysis/frontend/03-design-system.md`
- Verificar `Components/DesignSystem/`, `Components/Composites/`, `Components/Lab/Pages/` e `Services/`

## Passos

1. Identificar se a mudança é host, infraestrutura, DS base, composite ou showcase.
2. Localizar componentes e contratos já existentes que resolvem parte do problema.
3. Verificar se o caso pede variante, composição ou artefato novo.
4. Mapear estados obrigatórios e requisitos de acessibilidade.
5. Identificar impacto em tema, feedback global, charts ou páginas-lab.

## Cuidados

- O host web deve continuar fino.
- Páginas-lab não devem virar fonte de contratos permanentes.
- Reaproveitamento vem antes de criação nova.

## Anti-padrões

- Começar implementando sem pesquisar equivalente existente.
- Criar componente novo só porque o exemplo de tela é diferente.
- Resolver estrutura com estilos inline quando o padrão é recorrente.

## Checklist de qualidade

- [ ] Sei exatamente em qual camada a mudança pertence
- [ ] Verifiquei equivalentes em `DesignSystem` e `Composites`
- [ ] Listei estados, slots e requisitos de acessibilidade
- [ ] Entendi se a mudança é contrato permanente ou apenas showcase
