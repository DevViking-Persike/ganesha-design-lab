# Diagnóstico Executivo Inicial do Frontend

O frontend analisado é um design lab em Blazor Server hospedado em `src/Ganesha.DesignLab.Web`, com a maior parte da UI e do Design System concentrada na biblioteca Razor compartilhada `src/Ganesha.DesignLab.Shared`. A solução também contém um host MAUI em `src/Ganesha.DesignLab.Maui`, mas o fluxo principal de navegação e demonstração do sistema visual está no host web.

Não há evidência de frontend SPA com `package.json`, `vite`, `next`, `webpack`, `eslint`, `prettier` ou `tailwind`. A stack real é .NET `net10.0`, Razor Components e CSS autoral baseado em tokens. O projeto atual se comporta mais como vitrine e playground de componentes do que como aplicação de negócio com integração remota.

## Árvore Resumida do Projeto com Explicação

```text
src/
  Ganesha.DesignLab.Web/
    Program.cs                  Host web, DI, pipeline HTTP, mapeamento do app
    Components/
      App.razor                 Shell HTML principal, CSS compartilhado e scripts
      Routes.razor              Router principal do host web
      Layout/MainLayout.razor   Layout mínimo com ThemeProvider + ToastContainer
      Pages/
        Home.razor              Landing page inicial
        NotFound.razor          Página de fallback

  Ganesha.DesignLab.Shared/
    DependencyInjection/        Registro de serviços de UI
    Services/                   Theme, toast, modal e drawer em memória
    Models/                     Modelos de feedback, layout, navegação e tabela
    Components/
      DesignSystem/             Componentes base reutilizáveis
      Composites/               Composições reutilizáveis de nível intermediário
      Infrastructure/           Provedor de tema
      Lab/
        Layout/                 Layout do catálogo/lab
        Pages/                  Páginas demonstrativas e padrões completos
    wwwroot/css/                Tokens, base, temas, utilitários e estilos do lab
    wwwroot/js/                 Interatividade complementar para charts

  Ganesha.DesignLab.Maui/
    MauiProgram.cs              Bootstrap do host MAUI
    Components/                 App de demonstração MAUI padrão
```

## Inventário Técnico do Frontend

### Framework, linguagem e runtime

- Framework principal: ASP.NET Core Razor Components / Blazor Server
  Evidência: `src/Ganesha.DesignLab.Web/Program.cs`
- Biblioteca compartilhada de UI: Razor Class Library
  Evidência: `src/Ganesha.DesignLab.Shared/Ganesha.DesignLab.Shared.csproj`
- Runtime alvo: `.NET 10`
  Evidência: `TargetFramework` em `src/Ganesha.DesignLab.Web/Ganesha.DesignLab.Web.csproj` e `src/Ganesha.DesignLab.Shared/Ganesha.DesignLab.Shared.csproj`
- Host secundário: .NET MAUI
  Evidência: `src/Ganesha.DesignLab.Maui/Ganesha.DesignLab.Maui.csproj`

### Tooling encontrado

- Não há `package.json` ou tooling Node no repositório analisado.
- Não há config de `tsconfig`, `vite`, `next`, `webpack`, `eslint`, `prettier`, `tailwind`, `jest`, `vitest`, `playwright` ou `cypress`.
- O build e o runtime aparentes dependem do ecossistema `dotnet`.

### Bootstrap e infraestrutura global

- Registro de serviços de UI via `AddGaneshaDesignLab()`
  Evidência: `src/Ganesha.DesignLab.Web/Program.cs`, `src/Ganesha.DesignLab.Shared/DependencyInjection/ServiceCollectionExtensions.cs`
- Renderização interativa server-side
  Evidência: `.AddInteractiveServerComponents()` e `.AddInteractiveServerRenderMode()` em `src/Ganesha.DesignLab.Web/Program.cs`
- Inclusão da biblioteca compartilhada no roteamento do host
  Evidência: `.AddAdditionalAssemblies(typeof(Ganesha.DesignLab.Shared._Imports).Assembly)` em `src/Ganesha.DesignLab.Web/Program.cs`
- Shell HTML principal com folha de estilo da library, CSS local e script de charts
  Evidência: `src/Ganesha.DesignLab.Web/Components/App.razor`

### Estilização e tema

- Sistema de tokens CSS com prefixo `--gns-`
  Evidência: `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/*.css`
- Temas explícitos light/dark via seletor `[data-theme]`
  Evidência: `src/Ganesha.DesignLab.Shared/wwwroot/css/theme/_theme-light.css`, `src/Ganesha.DesignLab.Shared/wwwroot/css/theme/_theme-dark.css`
- Arquivo agregador único para o Design System
  Evidência: `src/Ganesha.DesignLab.Shared/wwwroot/css/ganesha.css`

## Mapa Inicial de Rotas, Telas, Componentes, Hooks, Services e Stores

### Rotas do host web

- `/` em `src/Ganesha.DesignLab.Web/Components/Pages/Home.razor`
- `/not-found` em `src/Ganesha.DesignLab.Web/Components/Pages/NotFound.razor`

### Rotas do laboratório compartilhado

- `/lab` em `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabHome.razor`
- Foundations:
  `/lab/colors`, `/lab/typography`, `/lab/spacing`, `/lab/shadows`
- Componentes:
  `/lab/buttons`, `/lab/forms`, `/lab/data-display`, `/lab/feedback`, `/lab/navigation`, `/lab/surfaces`, `/lab/overlays`, `/lab/charts`, `/lab/grid`
- Padrões:
  `/lab/dashboard`, `/lab/table-page`, `/lab/form-page`, `/lab/profile`, `/lab/auth`

Evidência: diretório `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/` e `@page` mapeados em `src/Ganesha.DesignLab.Web/Components/Routes.razor`.

### Componentes base do Design System

- Actions: `GnsButton`, `GnsSwitch`
- Form: `GnsInputText`, `GnsInputPassword`, `GnsSelect`, `GnsTextarea`, `GnsCheckbox`, `GnsRadioGroup`, `GnsRadioOption`
- DataDisplay: `GnsAvatar`, `GnsBadge`, `GnsSectionHeader`, `GnsStatCard`, `GnsTable`, `GnsTag`
- Feedback: `GnsAlert`, `GnsEmptyState`, `GnsLoader`, `GnsToastContainer`, `GnsToastItem`
- Layout: `GnsContainer`, `GnsGrid`, `GnsSection`, `GnsStack`
- Navigation: `GnsBreadcrumb`, `GnsBreadcrumbItem`, `GnsNavItem`, `GnsPagination`, `GnsTab`, `GnsTabs`
- Overlay: `GnsDrawer`, `GnsModal`
- Surfaces: `GnsCard`, `GnsPanel`
- Charts: `GnsBarChart`, `GnsDonutChart`, `GnsHorizontalBarChart`, `GnsLineChart`, `GnsProgressBar`, `GnsRadialProgress`, `GnsSparkline`

Evidência: `src/Ganesha.DesignLab.Shared/Components/DesignSystem/`

### Componentes compostos

- `GnsActionList`, `GnsActionListItem`
- `GnsAppShell`
- `GnsMetricGrid`
- `GnsPageHeader`
- `GnsSearchFilter`
- `GnsSidebar`
- `GnsTopBar`

Evidência: `src/Ganesha.DesignLab.Shared/Components/Composites/`

### Hooks, stores e contextos

- Não há evidência de hooks estilo React, stores centralizados ou contextos complexos.
- O equivalente local é baseado em serviços scoped com eventos:
  `ThemeService`, `ToastService`, `DrawerService`, `ModalService`
- O estado de tela mais comum está dentro dos próprios componentes `.razor`.

### Services/clients HTTP

- Não há evidência de `HttpClient`, API clients, repositories ou integração remota no frontend analisado.
- Os services presentes são de infraestrutura de UI em memória.

## Como a Aplicação Sobe

1. O host web cria o `WebApplicationBuilder` em `src/Ganesha.DesignLab.Web/Program.cs`.
2. Registra serviços do design lab com `AddGaneshaDesignLab()`.
3. Ativa Razor Components interativos no servidor.
4. Mapeia ativos estáticos e o app raiz `App`.
5. `App.razor` carrega as folhas de estilo compartilhadas e locais e renderiza `Routes`.
6. `Routes.razor` usa `Router` com `AdditionalAssemblies` para enxergar as páginas do projeto compartilhado.
7. `MainLayout.razor` do host envolve o conteúdo com `GnsThemeProvider` e `GnsToastContainer`.

## Lista de Evidências Encontradas

- Host web e pipeline: `src/Ganesha.DesignLab.Web/Program.cs`
- Shell HTML e assets: `src/Ganesha.DesignLab.Web/Components/App.razor`
- Router com assembly adicional: `src/Ganesha.DesignLab.Web/Components/Routes.razor`
- Layout principal do host: `src/Ganesha.DesignLab.Web/Components/Layout/MainLayout.razor`
- Registro de serviços: `src/Ganesha.DesignLab.Shared/DependencyInjection/ServiceCollectionExtensions.cs`
- Layout do lab: `src/Ganesha.DesignLab.Shared/Components/Lab/Layout/LabLayout.razor`
- Exemplo de página catálogo: `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabHome.razor`
- Exemplo de padrão completo: `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabDashboard.razor`
- Tokens e temas: `src/Ganesha.DesignLab.Shared/wwwroot/css/ganesha.css`, `src/Ganesha.DesignLab.Shared/wwwroot/css/settings/*.css`, `src/Ganesha.DesignLab.Shared/wwwroot/css/theme/*.css`
- Interatividade complementar de charts: `src/Ganesha.DesignLab.Shared/wwwroot/js/ganesha-charts.js`

## Hipóteses e Pontos a Validar nas Próximas Etapas

- Hipótese: o foco principal do repositório hoje é consolidar um Design System e um catálogo visual, não uma aplicação de produto.
  Base: concentração de páginas em `Components/Lab/Pages` e ausência de integração HTTP.
- Hipótese: o host MAUI ainda é secundário ou está mais próximo do template padrão do que do Design Lab consolidado.
  Base: páginas padrão em `src/Ganesha.DesignLab.Maui/Components/Pages/`.
- Ponto a validar: se os componentes compostos já são usados de forma consistente nas páginas lab ou se ainda coexistem muitas composições inline.
- Ponto a validar: se os componentes base cobrem contratos suficientes para uso produtivo ou ainda estão orientados majoritariamente a demonstração.
- Ponto a validar: como o time pretende governar documentação operacional, já que não há `CLAUDE.md`, `AGENTS.md` ou `.claude/commands` na árvore atual.
