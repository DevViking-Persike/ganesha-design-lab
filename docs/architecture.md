# Arquitetura — Ganesha Design System

## Visão Geral

O Ganesha Design System é uma solução multi-plataforma construída em .NET/Blazor, projetada para compartilhar componentes, tokens e lógica de UI entre uma aplicação Web e uma aplicação MAUI Blazor Hybrid, com total separação entre o núcleo do sistema e os hosts de execução.

---

## Estrutura da Solution

```
Ganesha.DesignLab.sln
├── src/
│   ├── Ganesha.DesignLab.Shared/        ← Núcleo do Design System
│   ├── Ganesha.DesignLab.Web/           ← Host Blazor Web App
│   └── Ganesha.DesignLab.Maui/          ← Host MAUI Blazor Hybrid
```

### Diagrama de Dependências

```
┌─────────────────────┐       ┌─────────────────────┐
│  Ganesha.DesignLab.Web │       │ Ganesha.DesignLab.Maui │
│   (Blazor Web App)   │       │ (MAUI Blazor Hybrid) │
└──────────┬──────────┘       └──────────┬──────────┘
           │                             │
           │    referencia               │    referencia
           ▼                             ▼
      ┌────────────────────────────────────────┐
      │       Ganesha.DesignLab.Shared           │
      │  (tokens · componentes · serviços)     │
      └────────────────────────────────────────┘
```

**Regra fundamental:** nenhum projeto host referencia o outro. Todo código compartilhável vive exclusivamente em `Shared`.

---

## Responsabilidades por Camada

### Ganesha.DesignLab.Shared

Contém todo o código agnóstico de plataforma:

| Pasta | Responsabilidade |
|-------|-----------------|
| `Components/DesignSystem/` | Componentes Blazor do Design System (Atoms, Composites, Primitives) |
| `Components/Layout/` | Componentes de estrutura de página (AppShell, Sidebar, TopBar) |
| `Models/` | Records e classes de domínio imutáveis |
| `Services/` | Contratos de serviço (interfaces) e implementações portáveis |
| `wwwroot/css/` | Tokens CSS, temas e estilos base |
| `wwwroot/js/` | Scripts JavaScript utilitários (interop) |

### Ganesha.DesignLab.Web

Host para ambiente de navegador:

- Configuração de DI para serviços específicos de Web
- `Program.cs` com registro via extensão `AddGaneshaDesignLab()`
- Páginas do Lab (galeria de componentes, sandbox)
- Configurações de render mode (SSR, InteractiveServer, etc.)

### Ganesha.DesignLab.Maui

Host para ambiente desktop/mobile via MAUI:

- Configuração de DI para serviços específicos de MAUI
- `MauiProgram.cs` com registro via extensão `AddGaneshaDesignLab()`
- Adaptações de plataforma (splash screen, ciclo de vida nativo)
- Acesso a APIs nativas quando necessário

---

## Arquitetura CSS (ITCSS + BEM)

O sistema de estilos segue a metodologia **ITCSS** (Inverted Triangle CSS) combinada com nomenclatura **BEM**, aplicando o prefixo `hlx-` em todos os seletores do Design System.

### Camadas ITCSS

```
Settings   → Tokens CSS (variáveis custom properties)
Tools      → Mixins e funções (quando aplicável)
Generic    → Reset / normalize
Elements   → Estilos base em tags HTML (sem classes)
Objects    → Estruturas de layout genéricas (grid, container)
Components → Componentes do Design System (hlx-button, hlx-card…)
Utilities  → Classes utilitárias de override pontual
```

### Convenção BEM com prefixo hlx-

```
Bloco:     .hlx-button
Elemento:  .hlx-button__icon
Modifier:  .hlx-button--primary
           .hlx-button--disabled
           .hlx-button--loading
```

---

## Sistema de Tokens

Todos os valores visuais do Design System são expressos como **CSS Custom Properties**, organizados em duas camadas:

### Tokens Primitivos (escala bruta)

Definem os valores absolutos da paleta e escalas:

```css
--hlx-color-indigo-500: #6366f1;
--hlx-space-4: 1rem;
--hlx-font-size-base: 1rem;
--hlx-radius-md: 0.375rem;
```

### Tokens Semânticos (aliases)

Mapeiam os tokens primitivos para intenções de uso:

```css
--hlx-color-bg-primary:     var(--hlx-color-indigo-500);
--hlx-color-text-default:   var(--hlx-color-gray-900);
--hlx-color-border-subtle:  var(--hlx-color-gray-200);
--hlx-shadow-card:          var(--hlx-shadow-sm);
```

Categorias de tokens semânticos:

| Categoria | Exemplo de token |
|-----------|-----------------|
| color | `--hlx-color-bg-primary`, `--hlx-color-text-muted` |
| typography | `--hlx-font-size-sm`, `--hlx-font-weight-semibold` |
| spacing | `--hlx-space-2`, `--hlx-space-8` |
| shadow | `--hlx-shadow-sm`, `--hlx-shadow-lg` |
| border | `--hlx-border-width-default`, `--hlx-border-color-default` |
| radius | `--hlx-radius-sm`, `--hlx-radius-full` |
| z-index | `--hlx-z-dropdown`, `--hlx-z-modal` |
| size | `--hlx-size-icon-sm`, `--hlx-size-icon-md` |

---

## Sistema de Temas

### Mecanismo de Aplicação

Os temas são aplicados via atributo `data-theme` no elemento raiz do documento:

```html
<html data-theme="dark">
```

Cada tema sobrescreve os tokens semânticos mantendo os tokens primitivos intactos:

```css
[data-theme="light"] {
  --hlx-color-bg-surface: var(--hlx-color-gray-50);
  --hlx-color-text-default: var(--hlx-color-gray-900);
}

[data-theme="dark"] {
  --hlx-color-bg-surface: var(--hlx-color-gray-900);
  --hlx-color-text-default: var(--hlx-color-gray-100);
}
```

### IThemeService

O contrato de serviço gerencia o tema ativo em runtime:

```csharp
public interface IThemeService
{
    Theme CurrentTheme { get; }
    Task SetThemeAsync(Theme theme);
    event Action OnThemeChanged;
}
```

Implementações separadas existem para Web (via JS interop, `localStorage`) e MAUI (via `Preferences` nativo), ambas registradas nos respectivos hosts.

---

## Hierarquia de Componentes

Os componentes são organizados em quatro níveis de complexidade crescente:

```
Primitives
│   Elementos base sem semântica visual própria
│   Exemplos: HlxFocusRing, HlxVisuallyHidden, HlxPortal
│
└── Atoms
    │   Componentes atômicos com responsabilidade única
    │   Exemplos: HlxButton, HlxBadge, HlxAvatar, HlxIcon, HlxSpinner
    │
    └── Composites (Molecules)
        │   Composições de atoms com lógica coordenada
        │   Exemplos: HlxSearchBar, HlxDropdown, HlxToast, HlxModal
        │
        └── Pages / Layouts
                Estruturas de página completa
                Exemplos: AppShell, TopBar, Sidebar, PageHeader
```

---

## Gerenciamento de Estado

O Design System adota abordagem minimalista para estado, priorizando previsibilidade:

| Padrão | Uso |
|--------|-----|
| **Parâmetros Blazor** | Estado de renderização e configuração de componentes |
| **EventCallback** | Comunicação filho → pai (preferido sobre `Action`) |
| **Serviços Scoped** | Estado compartilhado na sessão (tema, preferências, notificações) |
| **CascadingValue** | Contexto compartilhado dentro de árvores de componentes (ex.: formulários) |

Estado **não** é gerenciado por stores globais ou padrões Flux/Redux — a simplicidade do modelo Blazor é suficiente para o escopo do Design System.

---

## Padrão de Registro de DI

Toda a configuração de DI é encapsulada em um método de extensão localizado em `Shared`:

```csharp
// Ganesha.DesignLab.Shared/Extensions/ServiceCollectionExtensions.cs
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGaneshaDesignLab(
        this IServiceCollection services)
    {
        services.AddScoped<IThemeService, ThemeService>();
        services.AddScoped<INotificationService, NotificationService>();
        // ... demais registros
        return services;
    }
}
```

Cada host chama a extensão no seu ponto de entrada:

```csharp
// Web → Program.cs
builder.Services.AddGaneshaDesignLab();

// MAUI → MauiProgram.cs
builder.Services.AddGaneshaDesignLab();
```

Serviços com implementações distintas por plataforma são registrados diretamente no host, sobrescrevendo ou complementando os registros de `Shared`.

---

## Estratégia Cross-Platform

| Preocupação | Abordagem |
|-------------|-----------|
| Renderização | Blazor renderiza o mesmo `.razor` em ambas as plataformas |
| CSS | Arquivos `.razor.css` isolam estilos por componente; tokens garantem consistência visual |
| JS Interop | Encapsulado em serviços; implementações por plataforma quando necessário |
| APIs Nativas | Acessadas somente nos projetos host, nunca em `Shared` |
| Fontes e Assets | Referenciados via `wwwroot` de `Shared`, copiados pelos hosts em build |
| Temas | `IThemeService` com implementações separadas por host |

**Regra de ouro:** se o código faz referência a `Microsoft.Maui.*` ou a qualquer API de browser direta (sem abstração), ele não pertence ao projeto `Shared`.
