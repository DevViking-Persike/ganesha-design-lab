# quality-gate

## Quando usar

Use antes de merge, release ou encerramento de uma rodada de mudanças.

## Pré-condições

- Identificar o escopo real da mudança
- Ler os arquivos alterados e a documentação impactada
- Descobrir build, testes e verificações aplicáveis

## Passos

1. Delimitar o escopo validado.
2. Rodar build, testes e checks seguros e aplicáveis.
3. Registrar o que passou, falhou e não foi executado.
4. Verificar aderência a ADRs, docs e governança.
5. Registrar riscos residuais e a decisão final.

## Cuidados

- Não declarar pronto sem evidência.
- O que não foi validado precisa virar risco explícito.

## Anti-padrões

- Esconder bloqueio por linguagem vaga.
- Tratar ausência de teste como detalhe menor quando a área é crítica.

## Checklist de qualidade

- [ ] O escopo está claro
- [ ] Os checks executados têm evidência
- [ ] Os checks não executados foram listados
- [ ] A decisão final é defensável
