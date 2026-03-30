# Convenções — Ganesha Design System

## Visão Geral

Este documento define os padrões de codificação, nomenclatura e organização adotados no Ganesha Design System. A consistência nessas convenções garante legibilidade, manutenibilidade e coerência entre componentes criados por diferentes membros do time.

---

## Convenções C#

### Namespaces com file-scoped

Todos os arquivos C# utilizam a sintaxe de namespace com escopo de arquivo (disponível desde C# 10), eliminando um nível de indentação desnecessário:

```csharp
// Correto
namespace Ganesha.DesignLab.Shared.Components.DesignSystem.Actions;

public class HlxButton : ComponentBase { }
```

```csharp
// Evitar
namespace Ganesha.DesignLab.Shared.Components.DesignSystem.Actions
{
    public class HlxButton : ComponentBase { }
}
```

### Records para modelos imutáveis

Modelos de dados, opções de configuração e DTOs são definidos como `record` para garantir imutabilidade e igualdade por valor:

```csharp
// Modelo imutável
public record ThemeOptions(string Name, string DataAttribute, bool IsDark);

// Configuração de componente
public record ButtonConfig(ButtonVariant Variant, ButtonSize Size, bool IsLoading);
```

Use `class` apenas quando mutabilidade ou herança polimórfica forem requisitos explícitos.

### Modificadores de acesso explícitos

Sempre declare o modificador de acesso, mesmo quando `private` for o padrão implícito. Isso melhora a legibilidade em code reviews.

### Tipos nullable

Habilite nullable reference types (`<Nullable>enable</Nullable>`) e trate todos os parâmetros opcionais de componentes como `string?` quando puderem ser nulos.

---

## Nomenclatura de Componentes

### Prefixo Hlx

Todo componente do Design System utiliza o prefixo `Hlx` no nome do arquivo e da classe:

```
HlxButton.razor
HlxBadge.razor
HlxCard.razor
HlxModal.razor
HlxAppShell.razor
HlxTopBar.razor
```

O prefixo serve para:
- Evitar conflitos com componentes de terceiros (MudBlazor, Radzen, etc.)
- Tornar imediatamente identificável a origem do componente em templates
- Facilitar buscas no codebase

### Exemplos por categoria

| Categoria | Exemplos de nome |
|-----------|-----------------|
| Actions | `HlxButton`, `HlxIconButton`, `HlxLinkButton` |
| Form | `HlxInput`, `HlxSelect`, `HlxCheckbox`, `HlxRadio` |
| DataDisplay | `HlxBadge`, `HlxAvatar`, `HlxCard`, `HlxTable` |
| Feedback | `HlxAlert`, `HlxToast`, `HlxSpinner`, `HlxSkeleton` |
| Navigation | `HlxTabs`, `HlxBreadcrumb`, `HlxPagination` |
| Overlay | `HlxModal`, `HlxDrawer`, `HlxTooltip`, `HlxPopover` |
| Layout | `HlxGrid`, `HlxStack`, `HlxDivider` |
| Surfaces | `HlxPanel`, `HlxSection` |

---

## Nomenclatura CSS

### BEM com prefixo hlx-

A nomenclatura de classes CSS segue estritamente o padrão BEM com o prefixo `hlx-`:

```
Bloco:     .hlx-{componente}
Elemento:  .hlx-{componente}__{parte}
Modifier:  .hlx-{componente}--{variante}
```

#### Exemplos práticos

```css
/* Bloco */
.hlx-button { }

/* Elementos (partes internas) */
.hlx-button__icon { }
.hlx-button__label { }
.hlx-button__spinner { }

/* Modifiers (variantes e estados) */
.hlx-button--primary { }
.hlx-button--secondary { }
.hlx-button--ghost { }
.hlx-button--sm { }
.hlx-button--lg { }
.hlx-button--loading { }
.hlx-button--disabled { }
.hlx-button--full-width { }
```

#### Regras de nomenclatura CSS

- Sempre em **kebab-case** (minúsculo, hífens)
- Nunca usar underscores duplos fora da notação BEM de elemento
- Modifiers de estado (loading, disabled, error) são aplicados no bloco, não em elementos
- Evitar aninhamento além de dois níveis (bloco → elemento)

---

## Nomenclatura de Tokens CSS

Tokens seguem o padrão: `--hlx-{categoria}-{propriedade}-{variante}`

```css
/* Padrão geral */
--hlx-{categoria}-{propriedade}-{variante}

/* Exemplos */
--hlx-color-bg-primary
--hlx-color-bg-secondary
--hlx-color-text-default
--hlx-color-text-muted
--hlx-color-border-default
--hlx-color-border-subtle

--hlx-font-size-xs
--hlx-font-size-sm
--hlx-font-size-base
--hlx-font-size-lg
--hlx-font-size-xl

--hlx-space-1          /* 0.25rem */
--hlx-space-2          /* 0.5rem  */
--hlx-space-4          /* 1rem    */
--hlx-space-8          /* 2rem    */

--hlx-radius-sm
--hlx-radius-md
--hlx-radius-lg
--hlx-radius-full

--hlx-shadow-sm
--hlx-shadow-md
--hlx-shadow-lg

--hlx-z-dropdown
--hlx-z-sticky
--hlx-z-overlay
--hlx-z-modal
--hlx-z-toast
```

Tokens **nunca** devem referenciar contexto de componente (ex.: `--hlx-button-bg` é errado; use `--hlx-color-bg-primary`). Os componentes consomem tokens semânticos, não criam seus próprios.

---

## Organização de Arquivos

### Um componente por arquivo

Cada componente Blazor ocupa exatamente um arquivo `.razor`. Não agrupe múltiplos componentes em um único arquivo.

### CSS co-localizado

O arquivo de estilos CSS é co-localizado com o componente (CSS isolation do Blazor):

```
Components/
  DesignSystem/
    Actions/
      HlxButton.razor
      HlxButton.razor.css     ← CSS isolado do componente
      HlxIconButton.razor
      HlxIconButton.razor.css
```

### Estrutura de pastas

```
Ganesha.DesignLab.Shared/
  Components/
    DesignSystem/
      Actions/         ← HlxButton, HlxIconButton
      DataDisplay/     ← HlxBadge, HlxCard, HlxAvatar
      Feedback/        ← HlxAlert, HlxToast, HlxSpinner
      Form/            ← HlxInput, HlxSelect, HlxCheckbox
      Layout/          ← HlxStack, HlxGrid, HlxDivider
      Navigation/      ← HlxTabs, HlxBreadcrumb
      Overlay/         ← HlxModal, HlxDrawer, HlxTooltip
      Surfaces/        ← HlxCard, HlxPanel
    Composites/
      AppShell/
      TopBar/
      Sidebar/
      PageHeader/
  Models/
  Services/
  wwwroot/
    css/
    js/
```

---

## Convenções de Parâmetros Blazor

### AdditionalCssClass

Todo componente expõe um parâmetro `AdditionalCssClass` para permitir customizações pontuais pelo consumidor sem quebrar o encapsulamento:

```csharp
[Parameter]
public string? AdditionalCssClass { get; set; }
```

Aplicado ao elemento raiz via `CssClassBuilder`:

```csharp
private string RootCssClass => new CssClassBuilder("hlx-button")
    .AddClass($"hlx-button--{Variant.Tocss()}")
    .AddClass("hlx-button--loading", IsLoading)
    .AddClass("hlx-button--disabled", Disabled)
    .AddClass(AdditionalCssClass)
    .Build();
```

### EventCallback para eventos

Use `EventCallback` (ou `EventCallback<T>`) para todos os eventos expostos por componentes. Nunca use `Action` ou `Func<T>` como tipo de parâmetro de evento:

```csharp
// Correto
[Parameter] public EventCallback OnClick { get; set; }
[Parameter] public EventCallback<string> OnValueChanged { get; set; }

// Evitar
[Parameter] public Action? OnClick { get; set; }
[Parameter] public Func<string, Task>? OnValueChanged { get; set; }
```

`EventCallback` é await-able, invoca `StateHasChanged` automaticamente e lida corretamente com o ciclo de vida do Blazor.

### Parâmetros booleanos

Parâmetros booleanos utilizam o padrão de nomeação afirmativa e são `false` por padrão:

```csharp
[Parameter] public bool Disabled { get; set; }
[Parameter] public bool IsLoading { get; set; }
[Parameter] public bool FullWidth { get; set; }
[Parameter] public bool IsReadOnly { get; set; }
```

### ChildContent e fragmentos nomeados

Use `ChildContent` como nome padrão para o slot principal. Slots adicionais usam nomes descritivos com sufixo `Content`:

```csharp
[Parameter] public RenderFragment? ChildContent { get; set; }
[Parameter] public RenderFragment? HeaderContent { get; set; }
[Parameter] public RenderFragment? FooterContent { get; set; }
[Parameter] public RenderFragment? ActionsContent { get; set; }
```

---

## Enums de Componente

Enums relacionados a um componente são declarados **no mesmo arquivo** que o componente (ou em um arquivo separado na mesma pasta se compartilhados entre componentes):

```csharp
// HlxButton.razor.cs ou no próprio HlxButton.razor
public enum ButtonVariant
{
    Primary,
    Secondary,
    Ghost,
    Danger,
    Link
}

public enum ButtonSize
{
    Small,
    Medium,
    Large
}
```

Enums compartilhados entre múltiplos componentes pertencem à pasta `Models/Enums/`.

---

## Convenções de Namespace

O namespace de cada componente reflete fielmente a sua localização no sistema de pastas:

```
Ganesha.DesignLab.Shared.Components.DesignSystem.Actions
Ganesha.DesignLab.Shared.Components.DesignSystem.DataDisplay
Ganesha.DesignLab.Shared.Components.DesignSystem.Feedback
Ganesha.DesignLab.Shared.Components.DesignSystem.Form
Ganesha.DesignLab.Shared.Components.DesignSystem.Layout
Ganesha.DesignLab.Shared.Components.DesignSystem.Navigation
Ganesha.DesignLab.Shared.Components.DesignSystem.Overlay
Ganesha.DesignLab.Shared.Components.DesignSystem.Surfaces
Ganesha.DesignLab.Shared.Components.Composites
Ganesha.DesignLab.Shared.Models
Ganesha.DesignLab.Shared.Services
```

---

## _Imports.razor

O arquivo `_Imports.razor` do projeto `Shared` deve conter todos os `@using` necessários para que os componentes do Design System sejam acessíveis sem declaração explícita em cada arquivo:

```razor
@using Microsoft.AspNetCore.Components.Web
@using Ganesha.DesignLab.Shared.Components.DesignSystem.Actions
@using Ganesha.DesignLab.Shared.Components.DesignSystem.DataDisplay
@using Ganesha.DesignLab.Shared.Components.DesignSystem.Feedback
@using Ganesha.DesignLab.Shared.Components.DesignSystem.Form
@using Ganesha.DesignLab.Shared.Components.DesignSystem.Layout
@using Ganesha.DesignLab.Shared.Components.DesignSystem.Navigation
@using Ganesha.DesignLab.Shared.Components.DesignSystem.Overlay
@using Ganesha.DesignLab.Shared.Components.DesignSystem.Surfaces
@using Ganesha.DesignLab.Shared.Components.Composites
@using Ganesha.DesignLab.Shared.Models
@using Ganesha.DesignLab.Shared.Services
```

Cada host (Web e MAUI) possui seu próprio `_Imports.razor` que inclui o de `Shared` via herança e adiciona seus namespaces específicos.

---

## Utilitário CssClassBuilder

Toda lógica de composição de classes CSS deve utilizar o `CssClassBuilder` — nunca concatenação manual de strings ou expressões condicionais inline no template:

```csharp
// Correto: via CssClassBuilder no code-behind
private string CssClass => new CssClassBuilder("hlx-alert")
    .AddClass($"hlx-alert--{Variant.ToLowerInvariant()}")
    .AddClass("hlx-alert--dismissible", IsDismissible)
    .AddClass(AdditionalCssClass)
    .Build();
```

```razor
// Evitar: lógica de classe inline no template
<div class="hlx-alert @(Variant == AlertVariant.Error ? "hlx-alert--error" : "") @AdditionalCssClass">
```
