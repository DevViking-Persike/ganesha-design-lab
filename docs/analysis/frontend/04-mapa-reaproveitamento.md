# Mapa de Reuso de Componentes

O reuso saudável já existe no nível base do Design System. A maior oportunidade atual não é “criar novos componentes”, mas transformar composições repetidas do lab em compostos mais formais quando houver recorrência suficiente.

## Componentes com reuso comprovado

- `GnsButton`
  Reaparece em home, feedback, dashboard, table page, form page e várias páginas do catálogo.
- `GnsCard`
  Reaparece como base de blocos de catálogo e superfícies de telas-padrão.
- `GnsSection` e `GnsGrid`
  Estruturam quase todas as páginas-lab.
- `GnsPageHeader`
  Reaparece como padrão de cabeçalho de páginas de showcase.
- `GnsTable`
  Já suporta uso em cenários diferentes: dashboard e table page.
- `GnsBadge`, `GnsTag`, `GnsAvatar`
  Reusados em tabelas, cartões e páginas compostas.

Evidência:
`src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabHome.razor`
`src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabDashboard.razor`
`src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabTablePage.razor`
`src/Ganesha.DesignLab.Shared/Components/Lab/Pages/LabFormPage.razor`

## Componentes candidatos à consolidação ou extração

### 1. Blocos de preview/cartão de catálogo

Em `LabHome.razor` existem diversos cards com a mesma estrutura base: título, descrição, CTA e conteúdo de preview. Ainda parecem exemplos específicos, mas já sugerem um possível composite de “catalog card”.

Decisão:
- Curto prazo: manter como está
- Médio prazo: extrair se o padrão aparecer em outras páginas

### 2. Toolbars de listagem com filtros

`LabTablePage.razor` combina busca + selects + CTA. Parte disso já conversa com a existência de `GnsSearchFilter`, que deve ser avaliado antes de criar outro wrapper.

Decisão:
- Reaproveitar primeiro `GnsSearchFilter` se cobrir o caso
- Só criar novo composite se a composição real divergir de forma consistente

### 3. Cabeçalhos de páginas com ações, breadcrumb e seletor

`GnsPageHeader` já existe e é usado de formas diferentes em dashboard, form page e feedback. Parece o composite correto para expansão por slot, não para duplicação.

Decisão:
- Reaproveitar e evoluir `GnsPageHeader`
- Evitar novos headers paralelos

## Mapa de Reuso de Hooks

Não há hooks dedicados no repositório. O equivalente mais próximo é estado local em componentes `.razor` e serviços scoped de UI.

Conclusão:
- Não há oportunidade real de “consolidar hooks” porque eles não existem como artefatos separados.
- O risco futuro é criar lógica repetida nas páginas-lab sem extrair serviços/helpers leves quando necessário.

## Mapa de Reuso de Services e Utilitários

### Services já reaproveitáveis

- `ThemeService`
  Reuso confirmado por `LabLayout.razor` e `GnsThemeProvider.razor`
- `ToastService`
  Reuso confirmado por `LabFeedback.razor` e `GnsToastContainer.razor`
- `DrawerService`, `ModalService`
  Infraestrutura pronta para uso transversal

### Helpers/utilitários

- `CssClassBuilder`
  Reaparecendo amplamente nos componentes base e compostos
  Evidência:
  `src/Ganesha.DesignLab.Shared/Extensions/CssClassBuilder.cs`
- `FormatHelper`
  Utilitário disponível, mas o reuso precisa ser validado conforme a evolução do catálogo

### Ausências relevantes

- Não há clients HTTP ou mapeadores a consolidar.
- Não há camada utilitária de feature além dos helpers mínimos.

## Tabela de Decisão: Criar Novo vs Reaproveitar

| Caso | Evidência atual | Decisão |
|---|---|---|
| Novo botão/CTA | `GnsButton` já cobre variantes, tamanhos, loading, ícones | Reaproveitar |
| Novo campo textual | `GnsInputText` e `GnsInputPassword` já existem | Reaproveitar |
| Nova listagem tabular | `GnsTable` já suporta header, row template e empty state | Reaproveitar |
| Novo cabeçalho de página | `GnsPageHeader` já absorve ações e breadcrumb | Reaproveitar/evoluir |
| Novo shell com sidebar/topbar | `GnsAppShell` já existe | Reaproveitar |
| Novo card de showcase | `GnsCard` já resolve a base | Reaproveitar |
| Novo bloco de filtro simples | Avaliar `GnsSearchFilter` antes de criar | Reaproveitar primeiro |
| Nova composição de catálogo repetida em várias páginas | Ainda não consolidada | Extrair composite quando houver repetição comprovada |
| Nova lógica de tema ou feedback global | Serviços existentes já resolvem | Reaproveitar |
| Novo service de dados remoto | Não existe equivalente | Criar novo quando houver caso real |

## Riscos de Abstração Prematura

1. Extrair composites do lab cedo demais e acabar cristalizando apenas layout de showcase.
2. Criar wrappers em torno de wrappers sem validar se os componentes base já resolvem o problema.
3. Introduzir camada de services/feature antes de existir integração de dados real.
4. Tentar “simular hooks” ou padrões de frontend JS que não combinam naturalmente com Razor Components.

## Riscos de Duplicação Atual

1. Estilos inline repetidos em páginas-lab.
2. Cartões de catálogo com estrutura parecida, ainda sem composite dedicado.
3. Blocos de toolbar e filtros com potencial de repetição futura.
4. Páginas-padrão que podem crescer por cópia se não houver disciplina de reaproveitamento.

## Recomendações Progressivas

1. Toda nova UI deve começar verificando `DesignSystem/` e `Composites/`.
2. Extrair para `Composites/` apenas quando houver repetição em pelo menos dois contextos reais.
3. Preservar `Lab/Pages` como consumidor do DS, não como lugar para consolidar novos contratos.
4. Usar `CssClassBuilder` e tokens antes de introduzir novos padrões estilísticos.
