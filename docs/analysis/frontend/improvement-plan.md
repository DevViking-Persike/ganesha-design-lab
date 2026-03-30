# Plano de Melhorias do Frontend

## Resumo Executivo

O frontend já tem uma base boa de Design System em `src/Ganesha.DesignLab.Shared`, mas ainda depende de governança nova, redução de drift documental e introdução gradual de testes para sustentar evolução segura. As cinco melhorias mais importantes hoje são:

1. Corrigir o drift entre documentação antiga e código atual `Gns`/`gns-`
2. Introduzir uma primeira suíte de testes de contrato para componentes e serviços críticos
3. Reduzir estilos inline recorrentes nas páginas-lab
4. Consolidar critérios de reaproveitamento em decisões de implementação futuras
5. Definir o papel do host MAUI em relação ao lab web

## Quick Wins

### FE-001

- Categoria: DX
- Tipo: Governança
- Impacto: Alto
- Esforço: P
- Risco: Baixo
- Urgência: Importante
- Problema atual: `docs/architecture.md`, `docs/design-system.md` e `docs/conventions.md` ainda descrevem prefixos `hlx-`, contratos e componentes que não refletem o código atual.
- Melhoria proposta: alinhar a documentação antiga ao estado real `Gns`/`gns-` e remover componentes/conceitos não implementados.
- Arquivos afetados: `docs/architecture.md`, `docs/design-system.md`, `docs/conventions.md`, `docs/component-criteria.md`
- Como validar: revisão cruzada com `src/Ganesha.DesignLab.Shared/**`
- Dependências: nenhuma

### FE-002

- Categoria: Testes
- Tipo: Quick win
- Impacto: Alto
- Esforço: M
- Risco: Médio
- Urgência: Importante
- Problema atual: não há suíte automatizada de frontend.
- Melhoria proposta: criar primeiros testes para `ThemeService`, `ToastService`, `GnsButton`, `GnsInputText` e `GnsTable`.
- Arquivos afetados: futuro projeto de testes + componentes/serviços citados
- Como validar: execução local de testes focados
- Dependências: definição mínima da stack de testes para Blazor/.NET

### FE-003

- Categoria: Design System
- Tipo: Consolidação
- Impacto: Médio
- Esforço: M
- Risco: Baixo
- Urgência: Importante
- Problema atual: páginas-lab usam muito `style=""` inline para estrutura repetida.
- Melhoria proposta: mover padrões estruturais recorrentes para CSS scoped ou composites específicos.
- Arquivos afetados: `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/*.razor`
- Como validar: diffs menores nas páginas, aparência preservada e build OK
- Dependências: nenhuma

## Melhorias Estruturais

### FE-004

- Categoria: Reaproveitamento
- Tipo: Consolidação
- Impacto: Alto
- Esforço: M
- Risco: Médio
- Urgência: Importante
- Problema atual: algumas composições do lab apontam repetição potencial, mas ainda não existe régua operacional forte para decidir quando extrair composite.
- Melhoria proposta: aplicar sistematicamente os commands/rules gerados e extrair composites apenas para padrões repetidos em mais de um contexto.
- Arquivos afetados: `src/Ganesha.DesignLab.Shared/Components/Composites/`, `src/Ganesha.DesignLab.Shared/Components/Lab/Pages/`
- Como validar: queda de duplicação e melhor separação entre showcase e contrato
- Dependências: FE-001

### FE-005

- Categoria: Design System
- Tipo: Evolução
- Impacto: Médio
- Esforço: M
- Risco: Médio
- Urgência: Desejável
- Problema atual: charts dependem de um subconjunto técnico específico com SVG + JS global, sem diretriz formal de testes e acessibilidade.
- Melhoria proposta: documentar governança específica de charts e cobrir casos vazios, labels e tooltips em validação automatizada.
- Arquivos afetados: `src/Ganesha.DesignLab.Shared/Components/DesignSystem/Charts/*`, `src/Ganesha.DesignLab.Shared/wwwroot/js/ganesha-charts.js`
- Como validar: checklist técnico específico para charts
- Dependências: FE-002

### FE-006

- Categoria: Arquitetura
- Tipo: Refatoração
- Impacto: Médio
- Esforço: M
- Risco: Médio
- Urgência: Desejável
- Problema atual: o host MAUI parece secundário e menos alinhado com o lab web.
- Melhoria proposta: decidir explicitamente se o MAUI será trazido para o mesmo catálogo do web ou mantido como host secundário mínimo.
- Arquivos afetados: `src/Ganesha.DesignLab.Maui/**`, ADRs e docs correlatos
- Como validar: decisão arquitetural explícita + docs alinhadas
- Dependências: ADR novo ou atualização de ADR

## Melhorias de Planejamento

### FE-007

- Categoria: Testes
- Tipo: Evolução
- Impacto: Alto
- Esforço: G
- Risco: Médio
- Urgência: Desejável
- Problema atual: não existe proteção de regressão em páginas compostas.
- Melhoria proposta: expandir a suíte para fluxos de tema, feedback, overlays e páginas-padrão mais sensíveis.
- Arquivos afetados: futuro projeto de testes
- Como validar: cobertura de cenários críticos e redução de risco em refatorações
- Dependências: FE-002

### FE-008

- Categoria: DX
- Tipo: Governança
- Impacto: Médio
- Esforço: M
- Risco: Baixo
- Urgência: Desejável
- Problema atual: ainda faltam alguns prompts/commands de qualidade e operação materializados no repositório.
- Melhoria proposta: completar os commands e relatórios restantes conforme necessidade real: `code-review`, `review-testes`, `review-loop`, `review-performance`, `review-seguranca`, `setup-scripts`.
- Arquivos afetados: `.claude/commands/`, `docs/review/`, `scripts/`
- Como validar: comandos existentes e executáveis
- Dependências: nenhuma

## Mapa de Dependências

- FE-001 destrava documentação coerente para FE-004 e melhora execução diária.
- FE-002 destrava FE-005 e FE-007.
- FE-006 depende de decisão arquitetural explícita.
- FE-008 pode ser executada em paralelo, mas ajuda o processo inteiro.

## Ordem de Execução

1. FE-001
2. FE-002
3. FE-003
4. FE-004
5. FE-008
6. FE-005
7. FE-006
8. FE-007

## O Que Não Fazer Agora

- Não introduzir arquitetura de dados/estado mais pesada sem features reais exigindo isso.
- Não extrair composites demais só porque o catálogo tem exemplos visualmente ricos.
- Não tentar cobrir todo o frontend com testes de uma vez.
- Não reescrever a documentação inteira antes de corrigir os pontos que hoje contradizem o código.
