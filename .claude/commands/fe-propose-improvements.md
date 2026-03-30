# fe-propose-improvements

## Quando usar

Use após concluir a análise do frontend para gerar um plano priorizado de evolução do frontend.

## Pré-condições

- Ler todos os artefatos em `docs/analysis/frontend/`
- Ler `docs/architecture/frontend/`
- Ler os ADRs de frontend em `docs/adrs/`
- Ler `.claude/rules/frontend-*.md`

## Passos

1. Consolidar problemas de arquitetura, DS, reaproveitamento e testes.
2. Transformar problemas repetidos em melhorias únicas.
3. Classificar por categoria, tipo, impacto, esforço, risco e urgência.
4. Separar quick wins, melhorias estruturais e itens de planejamento.
5. Ordenar a execução por dependências.
6. Registrar o que não fazer agora.

## Cuidados

- Priorizar reaproveitamento e governança antes de expansão estrutural.
- Não assumir integração ou complexidade que o código atual ainda não tem.
- Considerar a ausência de suíte como fator de risco transversal.

## Anti-padrões

- Propor abstração pesada sem demanda real.
- Misturar melhoria de showcase com contrato de DS sem separar impacto.
- Ignorar dívida documental já identificada.

## Checklist de qualidade

- [ ] O plano cobre arquitetura, DS, reuso, testes e DX
- [ ] As prioridades estão justificadas
- [ ] Cada item é executável
- [ ] O plano distingue curto, médio e longo prazo
