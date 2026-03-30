# be-propose-improvements

## Quando usar

Use quando existir análise de backend e for necessário gerar um plano priorizado específico do backend.

## Pré-condições

- Ler `docs/analysis/backend/` quando existir
- Ler `docs/architecture/` e `docs/adrs/` relacionados ao backend

## Passos

1. Consolidar os achados do backend.
2. Classificar por arquitetura, domínio, persistência, integrações, segurança, testes e operação.
3. Priorizar quick wins e melhorias estruturais.
4. Mapear dependências e ordem de execução.

## Cuidados

- Não gerar plano backend fictício se não houver análise de backend.

## Anti-padrões

- Reaproveitar plano full-stack como se fosse backend puro.

## Checklist de qualidade

- [ ] Há base documental real de backend
- [ ] O plano é específico para backend
