# Diagnóstico Geral da Suíte

Não há evidência de suíte de testes frontend implementada neste repositório. Também não foram encontrados arquivos de configuração de testes para os ecossistemas mais comuns de frontend JS nem projetos de teste .NET específicos para os componentes analisados.

Na prática, a confiança atual parece depender de inspeção manual e execução visual do lab. Isso é aceitável apenas para estágio exploratório; para evolução segura do Design System, é um risco importante.

## Inventário por Tipo de Teste

### Testes unitários

- Não encontrados.

### Testes de componentes

- Não encontrados.

### Testes de integração

- Não encontrados.

### Testes e2e / smoke automatizados

- Não encontrados.

### Evidência

- Busca por arquivos de teste não retornou specs reais do projeto.
- Não foram encontrados `jest.config`, `vitest.config`, `playwright.config`, `cypress.config` nem `package.json`.
- Não foram encontrados projetos de teste dedicados na árvore inspecionada.

## Cobertura Útil por Área

### Design System base

- Cobertura automatizada: inexistente
- Consequência: regressões de contrato visual/comportamental podem passar despercebidas

### Composites

- Cobertura automatizada: inexistente
- Consequência: slots, composição e comportamento responsivo/global ficam sem guarda

### Tema e serviços globais

- Cobertura automatizada: inexistente
- Consequência: eventos de tema, dismiss de toast e ciclo de vida não têm verificação formal

### Charts

- Cobertura automatizada: inexistente
- Consequência: SVG, dados e interatividade JS estão especialmente expostos a regressões

## Riscos de Falso Positivo

Como não há suíte, o principal falso positivo atual é processual: assumir que “funciona” porque a página renderizou uma vez no lab.

Casos prováveis:

1. Um componente aparenta renderizar, mas perde estado de acessibilidade.
2. Um contrato de props muda e algumas páginas continuam compilando, mas outras quebram visualmente.
3. Um tema continua alternando, mas cores semânticas específicas ficam incorretas.
4. Um chart continua aparecendo, mas labels, tooltips ou escalas ficam erradas.

## Riscos de Falso Negativo

Sem testes automatizados, o risco de falso negativo aparece como dificuldade de detectar regressões reais antes de mudanças maiores.

Casos prováveis:

1. Refatorar `CssClassBuilder` ou CSS scoped e só descobrir quebras manualmente depois.
2. Alterar `GnsButton`, `GnsInputText` ou `GnsTable` e perder estados importantes como loading, error ou empty state.
3. Mudar serviços de toast/theme e introduzir bugs intermitentes sem perceber.

## Riscos de Flakiness

Hoje não há flakiness de suíte porque não há suíte. Se testes forem introduzidos sem cuidado, os pontos com maior tendência a instabilidade são:

1. Toasts com timers em `ToastService` e `GnsToastContainer`
2. Componentes com animação
3. Charts com interatividade JS e eventos de mouse
4. Comportamentos dependentes de re-render assíncrono do Blazor

## Lacunas Prioritárias

1. Testes de contrato para componentes base críticos:
   `GnsButton`, `GnsInputText`, `GnsSelect`, `GnsTable`, `GnsAlert`, `GnsModal`
2. Testes de serviços de UI:
   `ThemeService`, `ToastService`, `DrawerService`, `ModalService`
3. Testes de integração simples para:
   alternância de tema, emissão de toast, dismiss, empty state, loading state
4. Testes direcionados para charts:
   renderização com dados vazios, labels, séries múltiplas e semântica acessível

## Recomendações Progressivas

1. Introduzir primeiro testes de componente/serviço de baixo acoplamento e alto valor.
2. Evitar começar por snapshots massivos; priorizar comportamento observável e contratos.
3. Tratar timers e animações explicitamente para reduzir futura flakiness.
4. Só depois expandir para fluxos maiores nas páginas-lab.

## Evidências Encontradas

- Ausência de tooling JS de testes:
  não há `package.json` ou configs de Jest/Vitest/Playwright/Cypress na raiz inspecionada.
- Ausência de arquivos de teste relevantes:
  busca por `*test*` e `*spec*` não retornou specs do projeto.
- Foco atual do repositório em UI e catálogo:
  `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/`
