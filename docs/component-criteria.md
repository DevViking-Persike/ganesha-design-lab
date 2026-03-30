# Critérios para Criação de Componentes — Ganesha Design System

## Visão Geral

Este documento define os critérios para decidir quando criar um novo componente, como classificá-lo e quais requisitos ele deve atender antes de ser considerado parte do Design System.

---

## Quando Criar um Novo Componente vs. Estender um Existente

### Estender um componente existente quando:

- A variação é apenas visual (nova cor, tamanho ou espaçamento) — adicione um **modifier CSS**
- O comportamento é idêntico mas com conteúdo diferente — use **slots** (`RenderFragment`) no componente existente
- A diferença é de composição — construa uma **composição** usando componentes existentes sem criar um novo
- O cenário é específico de uma única tela — implemente na página, não no Design System

### Criar um novo componente quando:

- Representa um padrão de UI que se repete em dois ou mais contextos distintos
- Possui responsabilidade única e bem definida que não pertence a nenhum componente existente
- Introduz comportamento interativo novo (não coberto por extensão de parâmetros)
- É resultado de uma composição recorrente que merece ser encapsulada com API própria
- Existe uma correspondência direta com um padrão reconhecido de acessibilidade (WAI-ARIA pattern)

### Sinais de alerta para NÃO criar um novo componente:

- O componente seria usado em apenas um lugar
- É um wrapper cosmético com menos de 3 parâmetros sobre um componente existente
- Sua API duplica funcionalidade que outro componente já oferece
- Não é claro quem seria o consumidor fora do contexto atual

---

## Níveis de Complexidade de Componentes

### Atom (Átomo)

Componente com responsabilidade única e sem dependência de outros componentes do DS (exceto primitivos).

**Características:**
- Resolve exatamente um problema de UI
- Sem estado interno complexo (no máximo estado de hover/focus gerenciado por CSS)
- API pequena (tipicamente 3–8 parâmetros)
- Pode ser usado de forma isolada

**Exemplos:** `HlxButton`, `HlxBadge`, `HlxAvatar`, `HlxIcon`, `HlxSpinner`, `HlxDivider`

**Localização:** `Components/DesignSystem/{Categoria}/`

---

### Molecule (Molécula / Composite)

Composição de dois ou mais atoms que resolve um padrão de UI recorrente com lógica coordenada.

**Características:**
- Composto por atoms do Design System
- Pode ter estado interno (ex.: aberto/fechado, valor selecionado)
- API moderada (8–15 parâmetros), ou expõe slots ricos
- Encapsula lógica de interação entre seus sub-componentes

**Exemplos:** `HlxDropdown`, `HlxSearchBar`, `HlxModal`, `HlxToast`, `HlxAccordion`, `HlxTabs`

**Localização:** `Components/DesignSystem/{Categoria}/` (se pertencer claramente a uma categoria) ou `Components/Composites/`

---

### Composite (Composto de Nível Superior)

Estruturas de página que coordenam múltiplos componentes e definem o layout macro da aplicação.

**Características:**
- Define a estrutura de uma região completa da UI
- Integra-se diretamente com serviços (DI, rotas, tema)
- API orientada a configuração estrutural, não a detalhes visuais
- Raramente criado — cada adição é uma decisão arquitetural

**Exemplos:** `HlxAppShell`, `HlxTopBar`, `HlxSidebar`, `HlxPageHeader`

**Localização:** `Components/Composites/{NomeDoComposite}/`

---

## Checklist de Requisitos

Use esta lista antes de considerar um componente pronto para o Design System. Todos os itens devem estar marcados.

### Estrutura e Organização

- [ ] Possui **responsabilidade única e claramente definida** — faz uma coisa e faz bem
- [ ] Nome segue o padrão `Hlx` + PascalCase (ex.: `HlxTagInput`)
- [ ] Arquivo `.razor` está na pasta correta conforme sua categoria
- [ ] Namespace reflete a estrutura de pastas: `Ganesha.DesignLab.Shared.Components.DesignSystem.{Categoria}`
- [ ] Um componente por arquivo (sem múltiplos componentes no mesmo `.razor`)

### CSS e Tokens

- [ ] Arquivo `.razor.css` co-localizado com o componente
- [ ] **Nenhum valor hardcoded** de cor, tamanho, espaçamento ou fonte — apenas tokens CSS (`var(--hlx-*)`)
- [ ] Classes CSS seguem BEM com prefixo `hlx-` (ex.: `hlx-tag-input`, `hlx-tag-input__tag`, `hlx-tag-input--focused`)
- [ ] Funciona corretamente no tema **light**
- [ ] Funciona corretamente no tema **dark**

### Parâmetros e API

- [ ] Parâmetro `AdditionalCssClass` (tipo `string?`) presente e aplicado ao elemento raiz
- [ ] `CssClassBuilder` utilizado para composição de classes (sem concatenação manual)
- [ ] `EventCallback` (ou `EventCallback<T>`) utilizado para todos os eventos expostos — nunca `Action` ou `Func`
- [ ] Parâmetros com nomes claros, em inglês, seguindo as convenções do projeto
- [ ] Enums co-localizados com o componente (ou em `Models/Enums/` se compartilhados)

### Estados Visuais

- [ ] Estado **default** implementado
- [ ] Estado **hover** implementado (`:hover`)
- [ ] Estado **focus** implementado via `:focus-visible` com `HlxFocusRing` ou equivalente
- [ ] Estado **active** implementado (`:active`) para elementos clicáveis
- [ ] Estado **disabled** implementado: aparência desativada + `pointer-events: none` + `aria-disabled`
- [ ] Estado **loading** implementado (quando aplicável): spinner + `aria-busy="true"`
- [ ] Estado **error** implementado (quando aplicável): borda e texto em cor de danger
- [ ] Estado **success** implementado (quando aplicável)

### Acessibilidade

- [ ] Atributos ARIA adequados ao padrão WAI-ARIA do componente
- [ ] Navegação por teclado funcional (`Tab`, `Enter`, `Space`, setas conforme aplicável)
- [ ] `role` correto quando elemento HTML semântico não é suficiente
- [ ] `tabindex` gerenciado corretamente (positivo apenas em casos excepcionais justificados)
- [ ] Contraste de cor verificado em ambos os temas

### Responsividade

- [ ] Comportamento em telas pequenas definido e implementado
- [ ] Sem overflow ou quebra de layout em viewports estreitos
- [ ] Breakpoints utilizam os tokens do Design System quando aplicável

### Documentação e Lab

- [ ] Página de Lab criada ou atualizada com exemplos do componente
- [ ] Exemplos cobrem todas as variantes e estados relevantes
- [ ] Parâmetros documentados com comentários XML (summary) no code-behind

---

## Árvore de Decisão para Placement

Use esta árvore para decidir onde um novo componente deve ser colocado:

```
O componente é parte do Design System (não específico de negócio)?
│
├── Não → Pertence ao projeto da aplicação, não ao DS
│
└── Sim → É uma estrutura de página (AppShell, TopBar, Sidebar)?
    │
    ├── Sim → Components/Composites/{Nome}/
    │
    └── Não → Depende de 2+ atoms do DS para funcionar?
        │
        ├── Sim → É recorrente o suficiente para ser molecule?
        │   ├── Sim  → Components/DesignSystem/{Categoria}/ ou Components/Composites/
        │   └── Não  → Implemente na página por enquanto
        │
        └── Não → É um atom com responsabilidade única?
            ├── Sim → Components/DesignSystem/{Categoria}/
            └── Não → Revisar escopo do componente
```

### Como escolher a Categoria para um Atom/Molecule

| Pergunta | Categoria |
|----------|-----------|
| Inicia uma ação do usuário? | Actions |
| Coleta entrada de dados? | Form |
| Exibe dados ou informações? | DataDisplay |
| Comunica estado do sistema? | Feedback |
| Orienta ou desloca o usuário? | Navigation |
| Sobrepõe o conteúdo? | Overlay |
| Contém ou agrupa conteúdo? | Surfaces |
| Organiza elementos espacialmente? | Layout |

---

## Processo para Adição de Novo Componente

1. **Proposta**: Abrir issue descrevendo o componente, casos de uso, variantes esperadas e onde seria usado
2. **Validação**: Confirmar que não existe componente existente que resolva o problema com extensão
3. **Design**: Definir API (parâmetros, eventos, slots) antes de implementar
4. **Implementação**: Seguir o checklist acima durante o desenvolvimento
5. **Review**: Submeter PR com o componente + página de Lab + todos os itens do checklist marcados
6. **Merge**: Aprovação de pelo menos um outro membro do time

---

## Anti-padrões a Evitar

| Anti-padrão | Problema | Alternativa |
|-------------|---------|-------------|
| Componente "God" com 20+ parâmetros | Viola responsabilidade única, difícil de manter | Divida em componentes menores ou use composição |
| Prop drilling excessivo | Acoplamento desnecessário entre níveis | Use `CascadingValue` ou serviço scoped |
| Lógica de negócio no componente | Quebra separação de responsabilidades | Mova para serviço ou para a página consumidora |
| CSS hardcoded | Quebra o sistema de temas e tokens | Use apenas `var(--hlx-*)` |
| Componente específico de uma tela | Não é um padrão reutilizável | Implemente na página, não no DS |
| Duplicar funcionalidade existente | Fragmenta o DS, dificulta manutenção | Estenda o componente existente |
