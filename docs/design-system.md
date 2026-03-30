# Design System — Ganesha Design Lab

## Visão Geral

O Ganesha Design System é a biblioteca visual e comportamental compartilhada do projeto. Ele vive principalmente em `src/Ganesha.DesignLab.Shared` e hoje é consumido pelo host web e, em menor grau, pelo host MAUI.

O estado atual do DS é explícito: há tokens, temas, componentes base por categoria, componentes compostos e um lab dedicado para demonstração.

## Identidade Atual

O sistema usa:

- prefixo de componentes: `Gns*`
- prefixo de classes CSS: `.gns-*`
- prefixo de tokens: `--gns-*`

Qualquer documentação ou implementação nova deve seguir essa convenção.

## Fundações

As fundações do sistema estão em:

- `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-color.css`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-typography.css`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-spacing.css`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-borders.css`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-radius.css`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-shadows.css`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-breakpoints.css`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-sizes.css`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/_tokens-z-index.css`

## Tokens

### Cor

O sistema combina escalas primitivas e aliases semânticos.

Escalas primitivas atuais:

- `gray`
- `primary`
- `violet`
- `rosa`
- `ouro`
- `bronze`
- `success`
- `warning`
- `danger`
- `info`

Aliases semânticos atuais incluem:

- background
- surface
- text
- border
- interactive
- status

Exemplos reais:

- `--gns-color-bg-page`
- `--gns-color-surface-default`
- `--gns-color-text-primary`
- `--gns-color-border-default`
- `--gns-color-interactive-primary-bg`

### Tipografia

Exemplos reais:

- `--gns-font-sans`
- `--gns-font-mono`
- `--gns-font-display`
- `--gns-text-xs` até `--gns-text-6xl`
- `--gns-font-weight-regular` até `--gns-font-weight-extrabold`

### Espaçamento e outras escalas

Exemplos reais:

- `--gns-space-*`
- `--gns-radius-*`
- `--gns-shadow-*`
- `--gns-z-*`

## Temas

Os temas atuais são:

- `light`
- `dark`

O mecanismo de troca usa:

- `GnsThemeProvider`
- `ThemeService`
- atributo `data-theme`

Arquivos-chave:

- `src/Ganesha.DesignLab.Shared/Components/Infrastructure/GnsThemeProvider.razor`
- `src/Ganesha.DesignLab.Shared/Services/ThemeService.cs`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/theme/_theme-light.css`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/theme/_theme-dark.css`

## Categorias de Componentes Reais

### Actions

- `GnsButton`
- `GnsSwitch`

### Form

- `GnsInputText`
- `GnsInputPassword`
- `GnsSelect`
- `GnsTextarea`
- `GnsCheckbox`
- `GnsRadioGroup`
- `GnsRadioOption`

### DataDisplay

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

### Navigation

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

## Componentes Compostos Reais

- `GnsAppShell`
- `GnsPageHeader`
- `GnsSearchFilter`
- `GnsSidebar`
- `GnsTopBar`
- `GnsMetricGrid`
- `GnsActionList`
- `GnsActionListItem`

## Contratos Comuns

Os componentes atuais tendem a usar contratos previsíveis como:

- `AdditionalCssClass`
- `Label`
- `ChildContent`
- `IsDisabled`
- `IsLoading`
- `HelperText`
- `ErrorMessage`
- `Value` / `ValueChanged`

Também é comum o uso de enums para variantes e tamanhos, por exemplo:

- `ButtonVariant`
- `ButtonSize`
- `InputSize`
- `BadgeSeverity`
- `BadgeSize`

## Componentes com Suporte a SVG Loader

### GnsSvgLoader

Componente standalone de loading que usa `mask-image` CSS para preencher uma silhueta SVG com animação de cor. Aceita qualquer SVG como asset.

Parâmetros:

- `Size` (`LoaderSize`) — Small, Medium, Large
- `Label` (`string?`) — texto abaixo do loader
- `AssetPath` (`string`) — caminho do SVG usado como máscara
- `AspectRatio` (`string`) — aspect ratio do SVG (ex: `"2816 / 1536"`)

### GnsButton — Suporte a SVG Loading

O `GnsButton` aceita SVG loaders opcionais no estado `IsLoading`:

- `LoadingAssetPath` (`string?`) — quando definido, substitui o spinner padrão pelo SVG fill loader
- `LoadingAspectRatio` (`string`) — aspect ratio do SVG (default: `"1 / 1"`)

Quando `LoadingAssetPath` não é definido, o botão mantém o spinner circular padrão.

### Assets de Loader

Os SVGs de marca ficam em `src/Ganesha.DesignLab.Shared/wwwroot/assets/loaders/` e são servidos via `_content/Ganesha.DesignLab.Shared/assets/loaders/`.

## Acessibilidade Atual

Há evidência concreta de suporte básico em vários componentes:

- `GnsButton`
  usa `aria-disabled` e `aria-busy`
- `GnsInputText`
  usa `label`, `aria-required`, `aria-invalid`, `aria-describedby`
- `GnsToastContainer`
  usa `aria-live="polite"`
- `GnsTable`
  usa `aria-busy` e `role="status"` no loading

Isso não significa que toda a acessibilidade já esteja completa. Modal, drawer, navegação por teclado aprofundada e validação sistemática ainda precisam de consolidação.

## O Que É Design System vs Showcase

### É parte do Design System

- `src/Ganesha.DesignLab.Shared/Components/DesignSystem/`
- `src/Ganesha.DesignLab.Shared/Components/Composites/`
- `src/Ganesha.DesignLab.Shared/Components/Infrastructure/`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/`
- `src/Ganesha.DesignLab.Shared/wwwroot/js/ganesha-charts.js`

### É consumo do Design System

- `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/`
- `src/Ganesha.DesignLab.Shared/Components/Lab/Layout/`

## Regras Práticas

- Não criar novo componente antes de verificar equivalente existente.
- Não usar valores visuais hardcoded quando houver token apropriado.
- Não tratar página do lab como contrato permanente só porque ela demonstra um padrão bonito.
- Extrair novo composite apenas com repetição real.

## Referências

- `docs/analysis/frontend/03-design-system.md`
- `docs/analysis/frontend/04-mapa-reaproveitamento.md`
- `docs/architecture/frontend/05-design-system-analysis.md`
