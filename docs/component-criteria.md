# Critérios para Criação de Componentes — Ganesha Design System

## Visão Geral

Este documento define quando criar um componente novo, quando evoluir um existente e onde cada artefato deve viver no codebase atual.

O critério principal não é “ficou bonito” ou “seria legal abstrair”, e sim reuso real com contrato previsível.

## Quando Estender um Componente Existente

Prefira estender quando:

- a diferença for de variante, tamanho ou estado
- a diferença for apenas conteúdo, rótulo ou composição simples
- o componente existente já cobrir o comportamento principal
- a necessidade puder ser resolvida com `RenderFragment`, enum ou parâmetro adicional sem perder coesão

## Quando Criar um Novo Componente

Crie novo componente apenas quando:

- o padrão aparecer em dois ou mais contextos reais
- houver responsabilidade própria clara
- a solução não couber de forma saudável em um contrato existente
- o artefato melhorar previsibilidade de uso, não apenas esconder markup

## Quando NÃO Criar

Não crie novo componente quando:

- o caso for específico de uma única página-lab
- for só um wrapper cosmético de um componente existente
- ainda não estiver claro se existe reuso real
- a abstração servir mais ao entusiasmo de refatoração do que ao projeto

## Níveis de Placement

### Base do Design System

Use `Components/DesignSystem/` quando o artefato for um building block reutilizável de baixo ou médio nível.

Exemplos reais:

- `GnsButton`
- `GnsInputText`
- `GnsTable`
- `GnsAlert`
- `GnsGrid`
- `GnsModal`

### Composite

Use `Components/Composites/` quando a solução encapsular uma composição recorrente de múltiplos componentes base.

Exemplos reais:

- `GnsAppShell`
- `GnsPageHeader`
- `GnsSearchFilter`
- `GnsSidebar`
- `GnsTopBar`

### Showcase / Página

Mantenha na página quando o caso ainda for específico do lab ou de uma demonstração isolada.

Exemplos:

- blocos específicos de `LabHome`
- exemplos ricos de dashboard
- composições ainda não repetidas em outro contexto

## Checklist de Prontidão

### Estrutura

- [ ] A responsabilidade do componente é clara
- [ ] O componente ficou na camada correta: base, composite ou showcase
- [ ] O nome segue o padrão `Gns` + PascalCase
- [ ] O namespace acompanha a pasta

### Contrato

- [ ] A API é pequena e previsível
- [ ] Eventos usam `EventCallback`
- [ ] Slots usam `RenderFragment` quando necessário
- [ ] Variantes e tamanhos usam enums quando fizer sentido
- [ ] `AdditionalCssClass` existe quando o componente precisa aceitar extensão controlada

### Estilo

- [ ] O estilo usa tokens `--gns-*` quando houver valor estrutural
- [ ] As classes seguem convenção `gns-` com elementos e modifiers consistentes
- [ ] O componente funciona em `light` e `dark`
- [ ] O arquivo `.razor.css` existe quando o componente precisa de estilo próprio

### Estados

- [ ] Estado default foi considerado
- [ ] Hover e focus foram considerados quando aplicável
- [ ] Active foi tratado em elementos interativos quando fizer sentido
- [ ] Disabled foi tratado quando aplicável
- [ ] Loading, error, success ou empty foram tratados quando aplicável

### Acessibilidade

- [ ] Labels e atributos ARIA necessários foram tratados
- [ ] Navegação por teclado foi considerada
- [ ] O papel semântico está correto
- [ ] Feedback visual de foco existe

### Integração com o Lab

- [ ] Existe demonstração suficiente em `Components/Lab/Pages/`
- [ ] O exemplo mostra os estados e variantes relevantes

## Árvore de Decisão Rápida

```text
É reutilizável fora da página atual?
├── Não -> manter na página/showcase
└── Sim
    É building block base?
    ├── Sim -> Components/DesignSystem/
    └── Não
        É composição recorrente de múltiplos componentes?
        ├── Sim -> Components/Composites/
        └── Não -> revisar escopo antes de criar
```

## Anti-padrões

| Anti-padrão | Problema | Melhor alternativa |
|---|---|---|
| Criar wrapper novo para trocar label/cor | Duplica contrato | Evoluir variante ou props |
| Extrair cedo demais algo visto uma vez | Abstração prematura | Manter na página até repetir |
| Colocar contrato novo em `Lab/Pages` | Mistura showcase com DS | Criar em `DesignSystem` ou `Composites` |
| Hardcode visual em vez de token | Quebra consistência | Usar tokens `--gns-*` |
| API enorme e vaga | Reduz previsibilidade | Dividir responsabilidade |

## Referências

- `docs/analysis/frontend/03-design-system.md`
- `docs/analysis/frontend/04-mapa-reaproveitamento.md`
- `.claude/rules/frontend-design-system.md`
- `.claude/rules/frontend-reaproveitamento.md`
