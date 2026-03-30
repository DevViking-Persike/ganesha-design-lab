# ADR-0004 Estratégia Testes Frontend

## Status

Aceito

## Contexto

O frontend não possui suíte automatizada.

## Decisão

Introduzir testes progressivamente por:

1. serviços de UI
2. componentes base críticos
3. integrações leves
4. cenários especiais de charts e timers

## Consequências

- confiança incremental
- menor risco de refatorar no escuro
