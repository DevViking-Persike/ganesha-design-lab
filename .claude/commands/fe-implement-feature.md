# fe-implement-feature

## Quando usar

Use para implementar uma feature ou tela respeitando a arquitetura atual, o Design System e a estratégia de reaproveitamento.

## Pré-condições

- Executar a análise prévia do command `fe-analyze-feature`
- Ler as rules em `.claude/rules/frontend-*.md`
- Verificar os componentes `Gns*` relevantes

## Passos

1. Definir a menor superfície nova necessária.
2. Reaproveitar componentes base e compostos existentes primeiro.
3. Colocar a implementação no projeto correto: `Web` para host, `Shared` para UI reutilizável.
4. Usar tokens `--gns-` e contratos consistentes.
5. Se a feature for apenas demonstração, mantê-la em `Lab/Pages`.
6. Se surgir repetição real, extrair composite em vez de copiar markup.
7. Adicionar ou planejar testes úteis para o contrato alterado.

## Cuidados

- Não deixar o showcase virar dono do contrato.
- Não criar wrappers paralelos sem necessidade.
- Evitar estilos inline estruturais.

## Anti-padrões

- Criar novo botão, input, table ou header sem checar os existentes.
- Adicionar lógica global no host web.
- Acoplar a feature a detalhes visuais específicos da página-lab.

## Checklist de qualidade

- [ ] A feature ficou na camada correta
- [ ] O máximo de reuso razoável foi aproveitado
- [ ] Tokens, estados e a11y foram tratados
- [ ] Não foi criado componente paralelo desnecessário
