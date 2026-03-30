# Frontend Design System

## Objetivo

Garantir que tokens, componentes base e contratos visuais/comportamentais do Ganesha Design System evoluam com consistência.

## Regras obrigatórias

- Todo componente reutilizável deve usar tokens `--gns-` em vez de valores mágicos hardcoded quando o valor for estrutural.
- Componentes base devem expor contratos previsíveis para variante, tamanho, estado e conteúdo quando fizer sentido.
- Estados críticos devem ser considerados explicitamente: default, hover, focus, active, disabled e, quando aplicável, loading, error e empty.
- Acessibilidade mínima deve ser tratada no componente, não delegada à página consumidora.
- O nome e o prefixo do componente devem seguir o padrão `Gns*`.

## Permitido

- Usar CSS scoped por componente quando o styling for específico do próprio componente.
- Manter JS complementar em componentes especiais, como charts, quando Razor/CSS puro não for suficiente.
- Ter slots e `RenderFragment` quando isso melhorar composição sem quebrar previsibilidade.

## Proibido

- Introduzir novo padrão visual fora dos tokens sem justificativa explícita.
- Criar componente base novo sem avaliar componente equivalente já existente.
- Tratar página de showcase como componente do DS só porque ela é bonita ou complexa.
- Dependência excessiva de `style=""` inline para contratos que deveriam ser permanentes.

## Sinais de alerta

- Props semelhantes com nomes diferentes entre componentes equivalentes.
- Variantes ou tamanhos inventados localmente em uma página.
- Estado de erro/loading/foco ausente em componente interativo.
- Componente depender de CSS inline do consumidor para parecer correto.

## Checklist de revisão

- [ ] O componente usa tokens e semântica `gns-`?
- [ ] Variante, tamanho e estados obrigatórios foram considerados?
- [ ] Labels, aria, foco e feedback visual mínimo foram tratados?
- [ ] O contrato está consistente com os componentes irmãos?
