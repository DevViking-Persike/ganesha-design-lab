# Resumo Arquitetural do Frontend

O frontend segue uma arquitetura em camadas leves, centrada em biblioteca de componentes Razor compartilhados. O host web (`src/Ganesha.DesignLab.Web`) é fino e funciona como bootstrap de runtime, enquanto quase toda a implementação relevante fica em `src/Ganesha.DesignLab.Shared`.

O estilo arquitetural predominante é híbrido entre biblioteca de Design System e catálogo de padrões. Há boa separação entre componentes base, composições e páginas de laboratório, mas a separação entre componente reutilizável e implementação de demonstração ainda depende de disciplina manual. O estado é majoritariamente local aos componentes; quando compartilhado, usa serviços scoped com eventos simples.

## Mapa de Responsabilidades por Camada

### Host / runtime

- Responsável por DI, pipeline HTTP, assets estáticos e renderização interativa.
- Arquivos-chave:
  `src/Ganesha.DesignLab.Web/Program.cs`
  `src/Ganesha.DesignLab.Web/Components/App.razor`
  `src/Ganesha.DesignLab.Web/Components/Routes.razor`

### Infraestrutura de UI

- Responsável por tema global e serviços de feedback/overlay.
- Arquivos-chave:
  `src/Ganesha.DesignLab.Shared/DependencyInjection/ServiceCollectionExtensions.cs`
  `src/Ganesha.DesignLab.Shared/Components/Infrastructure/GnsThemeProvider.razor`
  `src/Ganesha.DesignLab.Shared/Services/ThemeService.cs`
  `src/Ganesha.DesignLab.Shared/Services/ToastService.cs`
  `src/Ganesha.DesignLab.Shared/Services/DrawerService.cs`
  `src/Ganesha.DesignLab.Shared/Services/ModalService.cs`

### Design System base

- Responsável por contratos visuais e comportamentais primários.
- Arquivos-chave:
  `src/Ganesha.DesignLab.Shared/Components/DesignSystem/**`
- Exemplo: `GnsButton`, `GnsInputText`, `GnsTable`, `GnsAlert`, `GnsModal`, `GnsLineChart`

### Composites

- Responsável por composições reutilizáveis acima do nível base.
- Arquivos-chave:
  `src/Ganesha.DesignLab.Shared/Components/Composites/**`
- Exemplo: `GnsAppShell`, `GnsPageHeader`, `GnsSearchFilter`

### Páginas de catálogo e padrões

- Responsável por demonstrar componentes e composições em uso.
- Arquivos-chave:
  `src/Ganesha.DesignLab.Shared/Components/Lab/Layout/LabLayout.razor`
  `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/*.razor`

## Fluxos de Dados Representativos

### Fluxo 1: alternância de tema

1. `LabLayout.razor` injeta `IThemeService`.
2. O switch chama `HandleThemeToggle`.
3. `ThemeService.SetTheme()` altera o tema atual e dispara `OnThemeChanged`.
4. `GnsThemeProvider.razor` está inscrito nesse evento e atualiza `data-theme`.
5. O CSS semântico troca valores via `[data-theme="dark"]`.

Evidência:
`src/Ganesha.DesignLab.Shared/Components/Lab/Layout/LabLayout.razor`
`src/Ganesha.DesignLab.Shared/Components/Infrastructure/GnsThemeProvider.razor`
`src/Ganesha.DesignLab.Shared/Services/ThemeService.cs`

### Fluxo 2: feedback por toast

1. `LabFeedback.razor` injeta `IToastService`.
2. Um `GnsButton` dispara `ToastService.ShowSuccess/Error/...`.
3. `ToastService` cria `ToastMessage`, armazena em memória e emite `OnToastAdded`.
4. `GnsToastContainer.razor` escuta o evento, atualiza a lista local e agenda remoção.

Evidência:
`src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabFeedback.razor`
`src/Ganesha.DesignLab.Shared/Services/ToastService.cs`
`src/Ganesha.DesignLab.Shared/Components/DesignSystem/Feedback/GnsToastContainer.razor`

## Problemas Arquiteturais Encontrados

### 1. Fronteira entre Design System e tela ainda é por convenção, não por barreira forte

As páginas do lab reutilizam muitos componentes base, mas ainda carregam bastante composição inline com estilos embutidos e dados mockados diretamente na view. Isso aparece com força em `LabHome.razor`, `LabDashboard.razor`, `LabTablePage.razor` e `LabFormPage.razor`.

Impacto:
- reduz a clareza sobre o que é contrato reutilizável e o que é apenas exemplo
- dificulta reaproveitamento direto de padrões mais ricos

### 2. Estado e lógica de feature estão acoplados às páginas do catálogo

Filtros, paginação mockada, listas e registros fictícios ficam diretamente dentro dos `.razor` das páginas-lab. Exemplo em `LabTablePage.razor` e `LabDashboard.razor`.

Impacto:
- baixa testabilidade
- pouca reutilização de lógica
- dificuldade em evoluir de showcase para aplicação real

### 3. Serviços globais são simples e úteis, mas não possuem contratos de escala

`ThemeService`, `ToastService`, `DrawerService` e `ModalService` funcionam bem para o estágio atual, porém são estritamente em memória, orientados a sessão e sem persistência, logging ou telemetria.

Impacto:
- suficiente para catálogo
- frágil para cenários multi-feature ou integrações mais complexas

### 4. O host MAUI aparenta divergir do foco arquitetural principal

O MAUI contém páginas padrão de template (`Counter`, `Weather`, `Home`) enquanto o host web já está estruturado como laboratório do DS.

Impacto:
- aumenta custo cognitivo
- enfraquece a narrativa arquitetural única do projeto

### 5. Não há camada de integração, domínio ou dados representativos

Não foram encontrados clients HTTP, adapters, stores, mapeadores, caches ou módulos de domínio no frontend.

Impacto:
- a arquitetura atual foi validada principalmente para UI estática e estado local
- ainda não há evidência de como o projeto se comporta sob regras de negócio reais

## Pontos Fortes

- O host web é fino e bem separado da biblioteca compartilhada.
- A estrutura `DesignSystem` vs `Composites` vs `Lab` é clara e legível.
- O tema global usa um mecanismo simples e previsível.
- Os serviços de UI são pequenos, coesos e fáceis de entender.
- O Design System já tem amplitude funcional relevante: ações, formulário, navegação, overlays, charts, surfaces, feedback.
- O CSS está organizado por fundações, tema, utilidades e páginas, com nomenclatura consistente `gns-`.

## Pontos Fracos

- Muita demonstração ainda depende de estilos inline.
- Não há abstração intermediária para features/patterns mais ricos.
- O repositório não expressa convenções operacionais formais para futuros agentes e contribuidores.
- Ausência de testes reduz confiança para refatorar contratos de componentes.
- O catálogo é forte visualmente, mas ainda pouco exercitado por casos reais de negócio.

## Riscos de Manutenção e Evolução

- Risco de crescimento desordenado das páginas-lab, com padrões replicados em vez de consolidados.
- Risco de evoluir componentes base sem suíte de regressão útil.
- Risco de acoplamento entre showcase e design system se exemplos passarem a carregar lógica específica demais.
- Risco de inconsistência entre web e MAUI caso ambos tentem evoluir sem diretriz compartilhada explícita.

## Recomendações Progressivas de Baixo Risco

### Estado atual

- Manter o host web fino.
- Preservar a separação física `DesignSystem`, `Composites`, `Lab`, `Services`, `Models`.

### Recomendações

1. Extrair das páginas-lab composições recorrentes para `Composites` quando houver repetição real.
2. Reduzir estilos inline nas páginas exemplares, migrando o que for estrutural para CSS dedicado.
3. Formalizar critérios de “base”, “composite” e “page example”.
4. Introduzir testes de contrato para componentes base críticos antes de refatorações maiores.
5. Definir papel do host MAUI: alinhar ao lab ou assumi-lo como secundário.

## Evidências, Hipóteses e Recomendações

### Evidências

- Arquitetura host fino + shared library:
  `src/Ganesha.DesignLab.Web/Program.cs`
  `src/Ganesha.DesignLab.Shared/Ganesha.DesignLab.Shared.csproj`
- Tema por serviço e provider:
  `src/Ganesha.DesignLab.Shared/Services/ThemeService.cs`
  `src/Ganesha.DesignLab.Shared/Components/Infrastructure/GnsThemeProvider.razor`
- Lógica e mocks em páginas:
  `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabDashboard.razor`
  `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabTablePage.razor`
  `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabFormPage.razor`

### Hipóteses

- O projeto está em fase de consolidação visual/arquitetural do DS, ainda antes da integração com produto real.
- Parte da nomenclatura recente indica transição de identidade anterior para `Gns*`, o que pode gerar dívida temporária de consistência.

### Recomendações

- Consolidar governança antes de ampliar escopo funcional.
- Evitar introduzir uma arquitetura mais pesada antes de haver features reais pedindo isso.
