# ADR-0001 Arquitetura Frontend Identificada

## Status

Aceito

## Contexto

O repositório já opera com host web fino e biblioteca Razor compartilhada forte.

## Decisão

Manter a arquitetura baseada em:

- `Web` como bootstrap
- `Shared` como núcleo de UI
- `Lab` como consumidor/showcase

## Consequências

- melhor separação de responsabilidades
- menor risco de espalhar contratos de UI no host
