# Convenções — Ganesha Design System

## Visão Geral

Este documento registra as convenções vigentes do projeto no estado atual do código. A referência principal é o que existe hoje em `src/Ganesha.DesignLab.Shared`, não convenções históricas.

## Convenções C#

### Namespaces file-scoped

O projeto usa namespaces com escopo de arquivo.

Exemplo real:

```csharp
namespace Ganesha.DesignLab.Shared.Services;

public sealed class ThemeService : IThemeService
{
}
```

### Nullable habilitado

Os projetos usam `<Nullable>enable</Nullable>`, então parâmetros opcionais e contratos devem tratar nulabilidade explicitamente.

### Acesso explícito

Prefira modificadores explícitos e classes/records pequenas e coesas.

## Nomenclatura de Componentes

### Prefixo `Gns`

Todo componente compartilhado deve usar o prefixo `Gns`.

Exemplos reais:

- `GnsButton`
- `GnsInputText`
- `GnsTable`
- `GnsModal`
- `GnsAppShell`
- `GnsPageHeader`

### Categorias reais

- Actions
- Form
- DataDisplay
- Feedback
- Layout
- Navigation
- Overlay
- Surfaces
- Charts
- Composites

## Nomenclatura CSS

### Prefixo `gns-`

As classes seguem convenção tipo BEM com prefixo `gns-`.

Exemplos reais:

```css
.gns-button
.gns-button__icon
.gns-button--primary
.gns-button--loading

.gns-input-text
.gns-input-text__field
.gns-input-text--error
```

Regras:

- usar `kebab-case`
- usar `__` para elementos internos
- usar `--` para modifiers/estados
- manter coerência com o nome do componente

## Nomenclatura de Tokens

Todos os tokens do DS usam prefixo `--gns-`.

Exemplos reais:

- `--gns-color-text-primary`
- `--gns-color-border-default`
- `--gns-space-4`
- `--gns-text-base`
- `--gns-radius-md`
- `--gns-shadow-lg`

Regra:

- componentes consomem tokens existentes
- evitar criar token específico de um único componente sem necessidade real

## Organização de Arquivos

### Componentes base

Ficam em:

- `src/Ganesha.DesignLab.Shared/Components/DesignSystem/{Categoria}/`

### Componentes compostos

Ficam em:

- `src/Ganesha.DesignLab.Shared/Components/Composites/{Nome}/`

### Infraestrutura

Fica em:

- `src/Ganesha.DesignLab.Shared/Components/Infrastructure/`
- `src/Ganesha.DesignLab.Shared/Services/`

### Showcase/lab

Fica em:

- `src/Ganesha.DesignLab.Shared/Components/Lab/`

## Convenções de Parâmetros Blazor

### `AdditionalCssClass`

Quando o componente expõe customização de classe adicional, o nome padrão usado no projeto é `AdditionalCssClass`.

### `EventCallback`

Eventos públicos devem usar `EventCallback` ou `EventCallback<T>`.

Exemplo real:

```csharp
[Parameter] public EventCallback OnClick { get; set; }
[Parameter] public EventCallback<string> ValueChanged { get; set; }
```

### `RenderFragment`

Use `RenderFragment` e `RenderFragment<T>` para slots e composição onde isso tornar a API mais previsível.

## Convenções de Estado e API

Os componentes atuais tendem a seguir estes nomes:

- `IsDisabled`
- `IsLoading`
- `IsRequired`
- `IsReadOnly`
- `Label`
- `Placeholder`
- `HelperText`
- `ErrorMessage`
- `Value` / `ValueChanged`

Para variantes e tamanhos, prefira enums coesos e específicos.

## Convenções de Estilo

- preferir tokens `--gns-*`
- preferir CSS scoped por componente para o estilo específico do componente
- usar `CssClassBuilder` para composição de classes quando aplicável
- evitar concatenação manual de classes quando já houver padrão utilitário

## Convenções de Arquitetura

- `Web` é host fino
- UI reutilizável nasce em `Shared`
- `Lab` é showcase e consumidor
- não criar contrato novo diretamente em páginas do lab
- reaproveitar antes de criar novo

## Referências

- `CLAUDE.md`
- `AGENTS.md`
- `.claude/rules/frontend-*.md`
- `docs/analysis/frontend/`
