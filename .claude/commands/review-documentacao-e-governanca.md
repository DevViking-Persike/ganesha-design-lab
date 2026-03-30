# review-documentacao-e-governanca

## Quando usar

Use após mudanças em docs, ADRs, rules, commands, `CLAUDE.md` ou `AGENTS.md`, ou quando houver suspeita de drift documental.

## Pré-condições

- Ler `CLAUDE.md`, `AGENTS.md`, `docs/adrs/`, `docs/analysis/`, `docs/architecture/` e `.claude/commands/`
- Ler o código real nas áreas documentadas

## Passos

1. Comparar documentação e governança com o código atual.
2. Identificar drift crítico, moderado e melhorias de clareza.
3. Verificar commands obsoletos ou artefatos faltantes.
4. Verificar ADRs coerentes, obsoletos ou faltantes.
5. Registrar ações mínimas de correção.

## Cuidados

- Priorizar contradições que induzem execução errada.
- Diferenciar ausência de detalhe de erro real.

## Anti-padrões

- Marcar todo texto desatualizado como crítico.
- Revisar só documentação sem validar o código correspondente.

## Checklist de qualidade

- [ ] Código e docs foram cruzados
- [ ] As inconsistências têm evidência
- [ ] A severidade está proporcional ao impacto operacional
