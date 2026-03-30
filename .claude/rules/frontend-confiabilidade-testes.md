# Frontend Confiabilidade Testes

## Objetivo

Evitar que a futura suíte do frontend nasça frágil, flakey ou cheia de falso positivo.

## Regras obrigatórias

- Timers, animações e atrasos devem ser controlados explicitamente nos testes.
- Eventos assíncronos de serviços e componentes devem ser aguardados de forma determinística.
- Charts e interações com JS precisam de validação focada em contrato renderizado e comportamento essencial.
- Toda nova suíte deve considerar o que faria o teste falhar diante de uma regressão real.

## Permitido

- Isolar testes de interatividade complexa em nível apropriado.
- Usar doubles simples para dependências não essenciais.
- Separar cenários de contrato visual e cenários temporizados.

## Proibido

- Uso excessivo de `Task.Delay` real em teste.
- Mock em excesso que esconda o comportamento verdadeiro do componente.
- Teste que só confirma que “renderizou alguma coisa”.

## Sinais de alerta

- Falhas intermitentes por timing.
- Teste que precisa de sleeps arbitrários.
- Teste que passaria mesmo com bug em aria, loading, dismiss ou troca de tema.

## Checklist de revisão

- [ ] O teste falha se o bug real acontecer?
- [ ] O tempo foi controlado de forma determinística?
- [ ] O mock usado preserva o contrato relevante?
- [ ] Há risco conhecido de falso positivo ou flakiness?
