# propose-improvements

## Quando usar

Use após concluir análises relevantes do projeto para gerar um plano priorizado de evolução full-stack.

## Pré-condições

- Ler `docs/analysis/` disponível para frontend e backend
- Ler `docs/architecture/`, `docs/adrs/`, `CLAUDE.md` e `AGENTS.md` quando existirem
- Identificar limitações já conhecidas em arquitetura, testes, segurança, DX e governança

## Passos

1. Consolidar os achados dos artefatos de análise.
2. Agrupar problemas repetidos em melhorias únicas.
3. Classificar cada melhoria por categoria, tipo, impacto, esforço, risco e urgência.
4. Montar quick wins, melhorias estruturais e melhorias de planejamento.
5. Ordenar a execução por dependências reais.
6. Registrar o que não deve ser feito agora.

## Cuidados

- Evitar reescrita total.
- Não propor melhoria sem evidência em código ou documentação.
- Priorizar passos incrementais e validáveis.

## Anti-padrões

- Listar ideias genéricas sem arquivos ou áreas afetadas.
- Tratar todo problema como urgente.
- Ignorar dependências entre melhorias.

## Checklist de qualidade

- [ ] O plano prioriza impacto real
- [ ] As melhorias são incrementais
- [ ] Há ordem de execução clara
- [ ] Existem itens explicitamente adiados
