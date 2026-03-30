# Design System — Ganesha Design Lab

## Visão Geral

O Ganesha Design System é a fundação visual e de interação das aplicações Ganesha. Ele fornece um vocabulário consistente de componentes, tokens e padrões que garantem coerência estética e funcional independentemente da plataforma de execução (Web ou MAUI).

---

## Princípios de Design

### Consistência

Cada componente compartilha os mesmos tokens, comportamentos e APIs. Um `HlxButton` se comporta e aparece da mesma forma em qualquer contexto em que for utilizado, sem surpresas.

### Acessibilidade

Acessibilidade não é uma camada adicional — é um requisito de implementação. Todos os componentes implementam atributos ARIA adequados, suporte a navegação por teclado e contraste de cor em conformidade com WCAG 2.1 nível AA.

### Composabilidade

Componentes simples se combinam para formar composições mais complexas. A hierarquia Primitives → Atoms → Composites → Pages garante que cada nível utilize os blocos do nível anterior.

### Escalabilidade

O sistema de tokens desacopla os valores visuais dos componentes. Alterar um token reflete imediatamente em todos os componentes que o consomem, permitindo evoluções de marca sem refatoração de componentes.

---

## Identidade Visual

### Paleta Principal

O Ganesha Design System adota **Indigo** como cor primária, posicionando a marca em um estilo corporativo-tecnológico limpo e moderno:

| Role | Referência | Uso |
|------|-----------|-----|
| Primary | Indigo 500–600 | Ações principais, links, foco |
| Neutral | Cool Gray 50–900 | Textos, bordas, fundos |
| Success | Green 500–600 | Confirmações, estados de sucesso |
| Warning | Amber 500–600 | Alertas, atenção |
| Danger | Red 500–600 | Erros, ações destrutivas |
| Info | Sky 500–600 | Informações, dicas |

### Tipografia

Hierarquia tipográfica clara com escala modular:

| Token | Tamanho | Uso típico |
|-------|---------|-----------|
| `--hlx-font-size-xs` | 0.75rem | Labels, captions, helpers |
| `--hlx-font-size-sm` | 0.875rem | Textos secundários, metadados |
| `--hlx-font-size-base` | 1rem | Corpo de texto padrão |
| `--hlx-font-size-lg` | 1.125rem | Subtítulos, destaques |
| `--hlx-font-size-xl` | 1.25rem | Títulos de seção |
| `--hlx-font-size-2xl` | 1.5rem | Títulos de página |
| `--hlx-font-size-3xl` | 1.875rem | Títulos principais |

### Estética Geral

- Visual clean com espaços brancos generosos
- Bordas sutis (sem sombras pesadas como padrão)
- Border radius moderado (não arredondado demais, não quadrado)
- Sombras para elevação de overlays e cards interativos
- Transições suaves (150–200ms) para feedback de interação

---

## Categorias de Tokens

### Color

```css
/* Fundos */
--hlx-color-bg-page
--hlx-color-bg-surface
--hlx-color-bg-subtle
--hlx-color-bg-primary
--hlx-color-bg-primary-hover
--hlx-color-bg-danger

/* Textos */
--hlx-color-text-default
--hlx-color-text-muted
--hlx-color-text-disabled
--hlx-color-text-on-primary
--hlx-color-text-link
--hlx-color-text-danger

/* Bordas */
--hlx-color-border-default
--hlx-color-border-subtle
--hlx-color-border-focus
--hlx-color-border-danger

/* Estados semânticos */
--hlx-color-success
--hlx-color-warning
--hlx-color-danger
--hlx-color-info
```

### Typography

```css
--hlx-font-family-base
--hlx-font-family-mono
--hlx-font-size-{xs|sm|base|lg|xl|2xl|3xl}
--hlx-font-weight-normal
--hlx-font-weight-medium
--hlx-font-weight-semibold
--hlx-font-weight-bold
--hlx-line-height-tight
--hlx-line-height-base
--hlx-line-height-relaxed
```

### Spacing

Baseado em escala de 4px (0.25rem = 1 unidade):

```css
--hlx-space-1    /* 0.25rem  =  4px */
--hlx-space-2    /* 0.5rem   =  8px */
--hlx-space-3    /* 0.75rem  = 12px */
--hlx-space-4    /* 1rem     = 16px */
--hlx-space-5    /* 1.25rem  = 20px */
--hlx-space-6    /* 1.5rem   = 24px */
--hlx-space-8    /* 2rem     = 32px */
--hlx-space-10   /* 2.5rem   = 40px */
--hlx-space-12   /* 3rem     = 48px */
--hlx-space-16   /* 4rem     = 64px */
```

### Shadows

```css
--hlx-shadow-xs    /* elevação mínima, cards de fundo */
--hlx-shadow-sm    /* cards interativos, dropdowns */
--hlx-shadow-md    /* popovers, tooltips */
--hlx-shadow-lg    /* modais, drawers */
--hlx-shadow-xl    /* overlays de tela cheia */
```

### Borders e Radius

```css
--hlx-border-width-default   /* 1px */
--hlx-border-width-medium    /* 2px */

--hlx-radius-sm     /* 0.25rem */
--hlx-radius-md     /* 0.375rem */
--hlx-radius-lg     /* 0.5rem */
--hlx-radius-xl     /* 0.75rem */
--hlx-radius-2xl    /* 1rem */
--hlx-radius-full   /* 9999px */
```

### Z-Index

```css
--hlx-z-base        /* 0 */
--hlx-z-raised      /* 10 */
--hlx-z-dropdown    /* 100 */
--hlx-z-sticky      /* 200 */
--hlx-z-overlay     /* 300 */
--hlx-z-modal       /* 400 */
--hlx-z-toast       /* 500 */
--hlx-z-tooltip     /* 600 */
```

---

## Arquitetura de Temas

### Temas Disponíveis

| Tema | data-theme | Descrição |
|------|-----------|-----------|
| Light | `light` | Fundo claro, tema padrão |
| Dark | `dark` | Fundo escuro, modo noturno |

### Como os Temas Funcionam

Os temas são implementados como conjuntos de valores para tokens semânticos. Cada tema redefine os tokens no seletor `[data-theme="nome"]`:

```css
/* Tema claro (padrão) */
[data-theme="light"],
:root {
  --hlx-color-bg-page:     #f9fafb;
  --hlx-color-bg-surface:  #ffffff;
  --hlx-color-text-default: #111827;
}

/* Tema escuro */
[data-theme="dark"] {
  --hlx-color-bg-page:     #0f172a;
  --hlx-color-bg-surface:  #1e293b;
  --hlx-color-text-default: #f1f5f9;
}
```

### Extensibilidade

Novos temas podem ser adicionados criando um novo bloco `[data-theme="nome-do-tema"]` que redefine todos os tokens semânticos obrigatórios.

---

## Categorias de Componentes

### Actions (Ações)

Componentes que iniciam ações do usuário.

| Componente | Descrição |
|-----------|-----------|
| `HlxButton` | Botão principal com variantes Primary, Secondary, Ghost, Danger, Link |
| `HlxIconButton` | Botão compacto com apenas ícone |
| `HlxLinkButton` | Botão com comportamento de link de navegação |

### Form (Formulários)

Componentes de entrada de dados.

| Componente | Descrição |
|-----------|-----------|
| `HlxInput` | Campo de texto com suporte a validação |
| `HlxSelect` | Seleção de opções |
| `HlxCheckbox` | Seleção binária |
| `HlxRadio` / `HlxRadioGroup` | Seleção exclusiva em grupo |
| `HlxTextarea` | Campo de texto multilinha |
| `HlxSwitch` | Toggle on/off |
| `HlxFormField` | Wrapper com label, helper e mensagem de erro |

### DataDisplay (Exibição de Dados)

Componentes para apresentar informações.

| Componente | Descrição |
|-----------|-----------|
| `HlxBadge` | Rótulo pequeno para status e categorias |
| `HlxAvatar` | Representação visual de usuário |
| `HlxCard` | Container com elevação para conteúdo agrupado |
| `HlxTable` | Tabela de dados com ordenação opcional |
| `HlxList` | Lista estruturada de itens |
| `HlxStatCard` | Card de métrica/indicador |
| `HlxCodeBlock` | Bloco de código formatado |

### Feedback

Componentes que comunicam estado do sistema ao usuário.

| Componente | Descrição |
|-----------|-----------|
| `HlxAlert` | Mensagem de alerta contextual (sucesso, aviso, erro, info) |
| `HlxToast` | Notificação temporária |
| `HlxSpinner` | Indicador de carregamento circular |
| `HlxSkeleton` | Placeholder de conteúdo em carregamento |
| `HlxProgressBar` | Indicador de progresso linear |
| `HlxEmptyState` | Estado vazio com mensagem e ação |

### Navigation (Navegação)

Componentes para orientação e deslocamento dentro da aplicação.

| Componente | Descrição |
|-----------|-----------|
| `HlxTabs` | Abas de navegação horizontal |
| `HlxBreadcrumb` | Rastro de navegação hierárquica |
| `HlxPagination` | Controle de paginação |
| `HlxNavItem` | Item individual de menu de navegação |
| `HlxStepper` | Indicador de progresso em etapas |

### Surfaces (Superfícies)

Contêineres estruturais com semântica visual.

| Componente | Descrição |
|-----------|-----------|
| `HlxPanel` | Área delimitada sem elevação |
| `HlxSection` | Seção semântica de conteúdo |
| `HlxAccordion` | Conteúdo expansível/colapsável |

### Overlay

Componentes que se sobrepõem ao conteúdo principal.

| Componente | Descrição |
|-----------|-----------|
| `HlxModal` | Diálogo modal com backdrop |
| `HlxDrawer` | Painel lateral deslizante |
| `HlxTooltip` | Dica de contexto ao hover/focus |
| `HlxPopover` | Painel flutuante com conteúdo rico |
| `HlxDropdown` | Menu suspenso de opções |

### Layout

Primitivos de composição visual.

| Componente | Descrição |
|-----------|-----------|
| `HlxStack` | Empilhamento vertical ou horizontal com gap |
| `HlxGrid` | Grade de layout flexível |
| `HlxDivider` | Separador visual horizontal ou vertical |
| `HlxSpacer` | Espaçamento flexível |

---

## Componentes Composites

Os composites são composições de alto nível que formam a estrutura das páginas:

| Composite | Responsabilidade |
|-----------|-----------------|
| `HlxAppShell` | Estrutura raiz da aplicação (sidebar + topbar + conteúdo) |
| `HlxTopBar` | Barra de topo com logo, navegação e ações globais |
| `HlxSidebar` | Menu lateral com grupos de navegação |
| `HlxPageHeader` | Cabeçalho de página com título, subtítulo e ações |
| `HlxContentArea` | Área de conteúdo principal com padding consistente |
| `HlxSearchBar` | Barra de busca com sugestões |
| `HlxUserMenu` | Menu de perfil do usuário |
| `HlxNotificationCenter` | Centro de notificações |

---

## Estratégia Responsiva

O Design System utiliza uma abordagem **mobile-first** com breakpoints definidos como tokens:

```css
--hlx-breakpoint-sm:   640px;
--hlx-breakpoint-md:   768px;
--hlx-breakpoint-lg:   1024px;
--hlx-breakpoint-xl:   1280px;
--hlx-breakpoint-2xl:  1536px;
```

### Comportamentos Responsivos Padrão

- `HlxAppShell`: Sidebar colapsa em drawer em telas menores que `md`
- `HlxGrid`: Colunas se reduzem automaticamente por breakpoint
- `HlxTable`: Scroll horizontal em mobile, ocultação de colunas secundárias
- `HlxTopBar`: Ações secundárias se movem para menu overflow em mobile
- Tipografia escala de forma proporcional usando `clamp()` quando aplicável

---

## Acessibilidade

### Padrões ARIA

Cada componente implementa os padrões ARIA adequados conforme o WAI-ARIA Authoring Practices:

| Componente | Padrão ARIA |
|-----------|------------|
| `HlxButton` | `role="button"`, `aria-disabled`, `aria-pressed` |
| `HlxModal` | `role="dialog"`, `aria-modal`, `aria-labelledby` |
| `HlxTabs` | `role="tablist"`, `role="tab"`, `role="tabpanel"` |
| `HlxAlert` | `role="alert"` ou `role="status"` |
| `HlxToast` | `aria-live="polite"` ou `assertive` |
| `HlxSpinner` | `role="status"`, `aria-label` |
| `HlxSelect` | `aria-expanded`, `aria-haspopup`, `aria-activedescendant` |

### Navegação por Teclado

- Todos os elementos interativos são alcançáveis via `Tab`
- Ações são ativadas via `Enter` e/ou `Space`
- Componentes de lista usam teclas de seta para navegação interna
- `Escape` fecha overlays (modais, drawers, dropdowns)
- Trap de foco implementado em modais e drawers

### Contraste de Cor

- Texto normal: mínimo 4.5:1 (WCAG AA)
- Texto grande: mínimo 3:1 (WCAG AA)
- Elementos de interface: mínimo 3:1
- Tokens de cor garantem conformidade em ambos os temas (light e dark)

### Focus Management

- `HlxFocusRing` aplica outline visível e consistente em todos os elementos focáveis
- O estilo de foco não é removido (`outline: none` é proibido sem alternativa visual)
- Modais restauram o foco ao elemento que os abriu ao fechar

---

## Padrões de Estado em Componentes

Todo componente interativo implementa os seguintes estados visuais:

| Estado | Descrição | Implementação |
|--------|-----------|--------------|
| **Default** | Estado de repouso | Estilo base do componente |
| **Hover** | Cursor sobre o elemento | `:hover` + modifier `--hover` quando necessário |
| **Focus** | Elemento com foco de teclado | `:focus-visible` + `HlxFocusRing` |
| **Active** | Clique/pressionamento | `:active` |
| **Disabled** | Elemento desabilitado | `aria-disabled`, `pointer-events: none`, opacidade reduzida |
| **Loading** | Aguardando resposta | `HlxSpinner` inline, `aria-busy="true"` |
| **Error** | Validação falhou | Borda e texto em cor de danger |
| **Success** | Operação concluída | Borda e ícone em cor de success |
| **ReadOnly** | Somente leitura | Visual diferenciado, sem interação |
