# Diagnóstico de Maturidade do Design System

O repositório já possui um Design System explícito, com identidade nominal própria (`Gns*`), tokens CSS consistentes, temas light/dark e catálogo dedicado. A maturidade atual é boa em fundações e amplitude de componentes, mas intermediária em governança de contratos, acessibilidade aprofundada e redução de estilos inline nas páginas de demonstração.

Há evidência forte de DS real em `src/Ganesha.DesignLab.Shared/Components/DesignSystem/` e `src/Ganesha.DesignLab.Shared/wwwroot/css/`. As páginas em `Components/Lab/Pages` não devem ser tratadas como DS em si; elas são consumo e demonstração do DS.

## Inventário de Tokens e Fundações

### Cores

- Tokens primitivos:
  `gray`, `primary`, `violet`, `rosa`, `ouro`, `bronze`, `success`, `warning`, `danger`, `info`
- Aliases semânticos:
  background, surface, text, border, interactive, status
- Evidência:
  `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-color.css`

### Tipografia

- Famílias:
  `--gns-font-sans`, `--gns-font-mono`, `--gns-font-display`
- Escalas:
  `--gns-text-xs` até `--gns-text-6xl`
- Pesos, leading e tracking semânticos
- Evidência:
  `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-typography.css`

### Spacing, radius, bordas, sombras, breakpoints, z-index e tamanhos

- Spacing:
  `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-spacing.css`
- Radius:
  `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-radius.css`
- Borders:
  `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-borders.css`
- Shadows:
  `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-shadows.css`
- Breakpoints:
  `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-breakpoints.css`
- Z-index:
  `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-z-index.css`
- Tamanhos de UI:
  `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-sizes.css`

### Temas

- Light explícito e dark explícito por `[data-theme]`
- Evidência:
  `src/Ganesha.DesignLab.Shared/wwwroot/css/theme/_theme-light.css`
  `src/Ganesha.DesignLab.Shared/wwwroot/css/theme/_theme-dark.css`

## Inventário de Componentes Base

### Ações

- `GnsButton`
  Contratos de variante, tamanho, loading, disabled, ícones, click
  Evidência: `src/Ganesha.DesignLab.Shared/Components/DesignSystem/Actions/GnsButton.razor`
- `GnsSwitch`

### Formulário

- `GnsInputText`
- `GnsInputPassword`
- `GnsSelect`
- `GnsTextarea`
- `GnsCheckbox`
- `GnsRadioGroup`
- `GnsRadioOption`

Evidência: `src/Ganesha.DesignLab.Shared/Components/DesignSystem/Form/`

### Data display

- `GnsAvatar`
- `GnsBadge`
- `GnsSectionHeader`
- `GnsStatCard`
- `GnsTable`
- `GnsTag`

### Feedback

- `GnsAlert`
- `GnsEmptyState`
- `GnsLoader`
- `GnsSvgLoader`
- `GnsToastContainer`
- `GnsToastItem`

### Layout

- `GnsContainer`
- `GnsGrid`
- `GnsSection`
- `GnsStack`

### Navegação

- `GnsBreadcrumb`
- `GnsBreadcrumbItem`
- `GnsNavItem`
- `GnsPagination`
- `GnsTab`
- `GnsTabs`

### Overlay

- `GnsDrawer`
- `GnsModal`

### Surfaces

- `GnsCard`
- `GnsPanel`

### Charts

- `GnsBarChart`
- `GnsDonutChart`
- `GnsHorizontalBarChart`
- `GnsLineChart`
- `GnsProgressBar`
- `GnsRadialProgress`
- `GnsSparkline`

## Inventário de Componentes Compostos

- `GnsAppShell`
  Estrutura sidebar + topbar + main
  Evidência: `src/Ganesha.DesignLab.Shared/Components/Composites/AppShell/GnsAppShell.razor`
- `GnsPageHeader`
- `GnsSearchFilter`
- `GnsSidebar`
- `GnsTopBar`
- `GnsMetricGrid`
- `GnsActionList`
- `GnsActionListItem`

Os compostos indicam que o projeto já reconhece um nível intermediário entre átomo/molécula e tela completa.

## Contratos Visuais e Comportamentais

### Sinais positivos

- Prefixo consistente `Gns*` para componentes e `gns-` para classes/tokens.
- Presença de enums para tamanhos e variantes em vários componentes.
- Contratos previsíveis como `Label`, `HelperText`, `ErrorMessage`, `AdditionalCssClass`, `IsDisabled`, `IsLoading`.
- Uso recorrente de `EditorRequired` em parâmetros críticos.

### Limitações percebidas

- A padronização de nomenclatura ainda depende de inspeção manual; não há documentação operacional formal no repositório.
- Muitos exemplos de consumo em páginas-lab usam `style=""` inline, o que enfraquece a leitura dos contratos do DS.
- Alguns estados existem visualmente no CSS, mas não há validação automatizada de comportamento.

## Inconsistências Visuais e Comportamentais

1. O DS é token-driven, mas as páginas de demonstração ainda aplicam muitos estilos inline.
   Evidência:
   `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabHome.razor`
   `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabDashboard.razor`
   `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabFormPage.razor`

2. A folha `ganesha.css` declara a intenção de importar estilos de layout e componentes, mas hoje a maior parte do styling de componente vive em CSS scoped por arquivo `.razor.css`.
   Isso não é erro, mas sinaliza coexistência de dois modelos de distribuição visual.
   Evidência:
   `src/Ganesha.DesignLab.Shared/wwwroot/css/ganesha.css`

3. Os gráficos têm interatividade complementar em JS global, enquanto os demais componentes seguem modelo puramente Razor/CSS.
   Isso cria um subecossistema técnico particular dentro do DS.
   Evidência:
   `src/Ganesha.DesignLab.Shared/wwwroot/js/ganesha-charts.js`

## Lacunas de Acessibilidade

### Evidências positivas

- `GnsButton` usa `aria-disabled` e `aria-busy`
- `GnsInputText` usa `label`, `aria-required`, `aria-invalid`, `aria-describedby`
- `GnsToastContainer` usa `aria-live="polite"`
- `GnsTable` sinaliza `aria-busy` e overlay com `role="status"`

### Lacunas

1. Não há evidência de suíte de a11y ou validação sistemática de navegação por teclado.
2. Os charts usam `role="img"` e `<title>` em alguns pontos, mas não há indicação de equivalentes textuais mais ricos para dados complexos.
3. A acessibilidade de modal/drawer precisa ser validada em foco-trap, escape e retorno de foco.
4. O contraste parece intencional, mas não há evidência de verificação automatizada.

## O que é DS vs Implementação de Tela

### É Design System

- Tudo em `src/Ganesha.DesignLab.Shared/Components/DesignSystem/`
- Tudo em `src/Ganesha.DesignLab.Shared/Components/Composites/`
- Tokens, temas, utilitários e base em `src/Ganesha.DesignLab.Shared/wwwroot/css/`
- `GnsThemeProvider`

### É implementação de tela / showcase

- `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/*.razor`
- `src/Ganesha.DesignLab.Shared/Components/Lab/Layout/LabLayout.razor`
- `src/Ganesha.DesignLab.Web/Components/Pages/Home.razor`

### É infraestrutura de suporte ao DS

- `ThemeService`, `ToastService`, `DrawerService`, `ModalService`
- Modelos de feedback, navegação e tabela

## Recomendações Progressivas de Consolidação

1. Formalizar contratos obrigatórios por tipo de componente: variantes, tamanhos, estados e a11y mínima.
2. Reduzir estilos inline nas páginas-lab, convertendo o que é estrutural em CSS scoped ou componentes compostos.
3. Definir padrão de quando algo nasce em `DesignSystem` vs `Composites` vs `Lab`.
4. Criar testes de contrato para pelo menos `GnsButton`, `GnsInputText`, `GnsTable`, `GnsAlert` e `GnsModal`.
5. Tratar charts como categoria especial do DS com diretrizes próprias, por dependerem de SVG + JS complementar.
