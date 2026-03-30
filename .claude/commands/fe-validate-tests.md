# fe-validate-tests

## Quando usar

Use para revisar a qualidade e a confiabilidade de testes novos ou existentes do frontend.

## Pré-condições

- Ler `docs/analysis/frontend/05-diagnostico-testes.md`
- Entender o contrato do componente/serviço testado
- Revisar se há timers, animações, JS complementar ou re-render assíncrono

## Passos

1. Perguntar qual bug real o teste detecta.
2. Verificar se o teste falha quando o contrato observável quebra.
3. Identificar dependência de timing, sleeps ou animações reais.
4. Revisar mocks e doubles para garantir que não escondem comportamento essencial.
5. Revisar se o teste depende demais de implementação interna.

## Cuidados

- Serviços com timer e toasts têm risco natural de flakiness.
- Charts e interações com mouse podem precisar de abordagem mais focada.
- Refatorações internas não devem invalidar testes bons sem motivo.

## Anti-padrões

- Aceitar teste só porque aumentou cobertura numérica.
- Ignorar falso positivo em nome de rapidez.
- Deixar `Task.Delay` real ou sleeps arbitrários como base do teste.

## Checklist de qualidade

- [ ] O teste pega bug real e relevante
- [ ] Não há falso positivo óbvio
- [ ] Timing está sob controle
- [ ] O acoplamento à implementação interna é aceitável
