# Frontend Testes

## Objetivo

Orientar a criação de testes úteis para componentes e serviços do Ganesha Design System, priorizando comportamento observável e contratos estáveis.

## Regras obrigatórias

- Todo componente base crítico alterado deve ganhar ou atualizar teste de contrato.
- Testes devem verificar comportamento visível ao usuário ou ao consumidor do componente.
- Serviços de UI com evento, timer ou estado global local devem ter testes dedicados.
- Componentes com estados `loading`, `error`, `empty`, `disabled` ou `dismissible` devem ter cenários para esses estados.

## Permitido

- Começar por uma suíte pequena e de alto valor.
- Mockar dependências externas mínimas quando realmente necessário.
- Separar testes de componente, serviço e integração leve.

## Proibido

- Depender só de snapshot amplo para validar componente.
- Testar detalhes internos irrelevantes em vez de contrato público.
- Criar testes que apenas repetem a implementação sem validar resultado observável.

## Sinais de alerta

- Teste passa mesmo se texto, estado ou semântica importante quebrar.
- Teste excessivamente acoplado a classes internas que mudam fácil.
- Cobertura grande em quantidade, mas pequena em valor comportamental.

## Checklist de revisão

- [ ] O teste valida comportamento relevante?
- [ ] Estados críticos foram cobertos?
- [ ] Há cenário de erro, vazio ou disabled quando aplicável?
- [ ] O teste continuaria útil após pequena refatoração interna?
