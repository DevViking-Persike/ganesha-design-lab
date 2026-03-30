# fe-add-tests

## Quando usar

Use ao adicionar ou alterar componentes e serviços do frontend que precisem de confiança automatizada.

## Pré-condições

- Ler `docs/analysis/frontend/05-diagnostico-testes.md`
- Identificar o contrato público do componente ou serviço
- Listar estados críticos e fluxos de erro

## Passos

1. Escolher o nível certo do teste: componente, serviço ou integração leve.
2. Priorizar comportamento observável ao usuário/consumidor.
3. Cobrir estados obrigatórios: default, disabled, loading, error, empty e dismiss quando aplicáveis.
4. Cobrir eventos importantes: click, input, theme change, toast dismiss, timers controlados.
5. Evitar dependência desnecessária de detalhes internos.

## Cuidados

- Começar pequeno e útil.
- Timers e animações precisam de controle determinístico.
- Charts exigem foco em contrato renderizado e interatividade essencial.

## Anti-padrões

- Teste só de snapshot amplo.
- Teste que passa apenas porque o componente “renderizou”.
- Mock demais e não validar o comportamento real.

## Checklist de qualidade

- [ ] O teste valida contrato público
- [ ] Há cobertura dos estados críticos
- [ ] Timing e assincronia foram tratados com controle
- [ ] O teste falharia diante de uma regressão real
