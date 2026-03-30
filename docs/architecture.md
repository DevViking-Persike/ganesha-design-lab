# Arquitetura — Ganesha Design System

## Visão Geral

O Ganesha Design System é estruturado como uma biblioteca Razor compartilhada consumida por dois hosts: um host web em Blazor Server e um host MAUI Blazor Hybrid. O núcleo reutilizável está em `src/Ganesha.DesignLab.Shared`; os hosts existem para bootstrap e execução.

Este documento resume o estado atual do código. Para o detalhamento operacional do frontend, use também `docs/architecture/frontend/` e `docs/analysis/frontend/`.

## Estrutura da Solution

```text
src/
  Ganesha.DesignLab.Shared/   Núcleo reutilizável: componentes, tokens, serviços e pages do lab
  Ganesha.DesignLab.Web/      Host web principal
  Ganesha.DesignLab.Maui/     Host MAUI secundário
```

## Direção de Dependências

```text
Ganesha.DesignLab.Web   ┐
                        ├──> Ganesha.DesignLab.Shared
Ganesha.DesignLab.Maui  ┘
```

Regras atuais:

- `Web` depende de `Shared`
- `Maui` depende de `Shared`
- `Shared` não depende dos hosts
- nenhum host deve referenciar o outro

## Responsabilidades por Projeto

### `src/Ganesha.DesignLab.Shared`

Contém o núcleo do Design System:

- `Components/DesignSystem/`
  Componentes base reutilizáveis
- `Components/Composites/`
  Composições reutilizáveis de nível intermediário
- `Components/Infrastructure/`
  Infraestrutura compartilhada, como `GnsThemeProvider`
- `Components/Lab/`
  Layout e páginas de demonstração do catálogo
- `Services/`
  Serviços de UI em memória, como tema, toast, modal e drawer
- `Models/`
  Modelos auxiliares de feedback, layout, navegação e tabela
- `wwwroot/css/`
  Tokens, temas, utilitários e estilos globais do DS
- `wwwroot/js/`
  JS complementar, hoje principalmente para interatividade de charts

### `src/Ganesha.DesignLab.Web`

Host web principal:

- registra os serviços com `AddGaneshaDesignLab()`
- sobe Razor Components interativos no servidor
- mapeia os assets estáticos
- renderiza `App.razor` e o roteamento principal

Arquivos-chave:

- `src/Ganesha.DesignLab.Web/Program.cs`
- `src/Ganesha.DesignLab.Web/Components/App.razor`
- `src/Ganesha.DesignLab.Web/Components/Routes.razor`
- `src/Ganesha.DesignLab.Web/Components/Layout/MainLayout.razor`

### `src/Ganesha.DesignLab.Maui`

Host MAUI:

- registra a mesma base compartilhada
- mantém uma superfície de execução nativa/híbrida
- hoje aparenta estar menos alinhado ao catálogo web do que o host principal

## Estilo Arquitetural

O padrão predominante hoje é:

- host fino
- biblioteca compartilhada forte
- Design System explícito
- páginas de lab como consumidoras do DS
- estado simples baseado em parâmetros locais e serviços scoped

Não há evidência atual de:

- camada de integração HTTP no frontend
- stores globais complexas
- arquitetura Flux/Redux
- pipeline frontend baseada em Node

## Bootstrap e Runtime

Fluxo principal do host web:

1. `Program.cs` registra `AddGaneshaDesignLab()`
2. ativa `AddInteractiveServerComponents()`
3. mapeia `App`
4. `App.razor` carrega CSS compartilhado, CSS local e JS de charts
5. `Routes.razor` adiciona o assembly compartilhado ao router
6. `MainLayout.razor` envolve a aplicação com `GnsThemeProvider` e `GnsToastContainer`

## Arquitetura de CSS e Tokens

O projeto atual usa:

- prefixo de classes: `.gns-*`
- prefixo de custom properties: `--gns-*`
- CSS scoped por componente em `.razor.css`
- folha agregadora em `src/Ganesha.DesignLab.Shared/wwwroot/css/ganesha.css`

Organização principal em `wwwroot/css/`:

- `settings/`
  tokens de cor, tipografia, spacing, radius, sombras, breakpoints, tamanhos, z-index
- `base/`
  reset e tipografia base
- `theme/`
  temas light/dark
- `utilities/`
  utilitários
- `pages/`
  estilos de páginas do lab

## Sistema de Tema

O tema é aplicado por `data-theme` no wrapper do `GnsThemeProvider`.

Estado atual:

- temas disponíveis: `light` e `dark`
- serviço atual: `ThemeService`
- implementação atual: em memória, síncrona, com evento `OnThemeChanged`

Arquivos-chave:

- `src/Ganesha.DesignLab.Shared/Components/Infrastructure/GnsThemeProvider.razor`
- `src/Ganesha.DesignLab.Shared/Services/ThemeService.cs`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/theme/_theme-light.css`
- `src/Ganesha.DesignLab.Shared/wwwroot/css/theme/_theme-dark.css`

## Componentes por Camada

### Design System base

Exemplos reais:

- Actions: `GnsButton`, `GnsSwitch`
- Form: `GnsInputText`, `GnsInputPassword`, `GnsSelect`, `GnsTextarea`, `GnsCheckbox`, `GnsRadioGroup`, `GnsRadioOption`
- DataDisplay: `GnsAvatar`, `GnsBadge`, `GnsSectionHeader`, `GnsStatCard`, `GnsTable`, `GnsTag`
- Feedback: `GnsAlert`, `GnsEmptyState`, `GnsLoader`, `GnsToastContainer`, `GnsToastItem`
- Layout: `GnsContainer`, `GnsGrid`, `GnsSection`, `GnsStack`
- Navigation: `GnsBreadcrumb`, `GnsBreadcrumbItem`, `GnsNavItem`, `GnsPagination`, `GnsTab`, `GnsTabs`
- Overlay: `GnsDrawer`, `GnsModal`
- Surfaces: `GnsCard`, `GnsPanel`
- Charts: `GnsBarChart`, `GnsDonutChart`, `GnsHorizontalBarChart`, `GnsLineChart`, `GnsProgressBar`, `GnsRadialProgress`, `GnsSparkline`

### Composites

Exemplos reais:

- `GnsAppShell`
- `GnsPageHeader`
- `GnsSearchFilter`
- `GnsSidebar`
- `GnsTopBar`
- `GnsMetricGrid`
- `GnsActionList`

### Showcase / lab

As páginas em `Components/Lab/Pages/` são consumidoras do DS e servem como catálogo e padrões de composição. Elas não devem ser tratadas automaticamente como contratos permanentes do Design System.

## Gerenciamento de Estado

O projeto usa um modelo simples:

- estado local em componentes `.razor`
- `EventCallback` para comunicação de eventos
- serviços scoped para estado de UI transversal

Serviços compartilhados atuais:

- `ThemeService`
- `ToastService`
- `DrawerService`
- `ModalService`

## Registro de DI

O registro compartilhado está em:

- `src/Ganesha.DesignLab.Shared/DependencyInjection/ServiceCollectionExtensions.cs`

Hoje a extensão registra:

- `IThemeService`
- `IToastService`
- `IDrawerService`
- `IModalService`

## Limites Atuais da Arquitetura

O código atual é forte em UI reutilizável e catálogo visual, mas ainda não demonstra:

- integração real com backend
- domínio rico no frontend
- suíte de testes consolidada
- alinhamento explícito entre o catálogo web e o host MAUI

## Referências

- `docs/architecture/frontend/00-overview.md`
- `docs/architecture/frontend/02-architecture-map.md`
- `docs/analysis/frontend/01-mapeamento-inicial.md`
- `docs/analysis/frontend/02-diagnostico-arquitetural.md`
