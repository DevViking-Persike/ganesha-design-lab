# fe-create-component

## Quando usar

Use quando for realmente necessário criar componente novo ou evoluir um contrato reutilizável do Design System.

## Pré-condições

- Ler `docs/analysis/frontend/03-design-system.md`
- Ler `docs/analysis/frontend/04-mapa-reaproveitamento.md`
- Pesquisar equivalentes em `Components/DesignSystem/` e `Components/Composites/`

## Passos

1. Classificar o caso como base, variante, composite ou showcase.
2. Se houver componente equivalente, preferir evolução de contrato ou variante.
3. Se não houver equivalente e o caso for reutilizável, criar no diretório correto.
4. Definir props, estados, slots e acessibilidade mínima.
5. Aplicar tokens e convenções `Gns*`.
6. Criar página-lab ou adaptar uma existente para demonstrar o componente.

## Cuidados

- Componente base deve ser mais estável e previsível que uma tela.
- Composite deve encapsular repetição real, não esconder uso simples do DS.
- Charts e componentes especiais podem exigir orientação específica de interação.

## Anti-padrões

- Criar componente novo para mudar apenas texto, ícone ou layout trivial.
- Usar páginas-lab como lugar definitivo do contrato.
- Pular estados obrigatórios como disabled, error, loading ou empty quando aplicáveis.

## Checklist de qualidade

- [ ] Validei que componente novo é realmente necessário
- [ ] O nível correto é base, variante ou composite
- [ ] Props e estados estão claros e consistentes
- [ ] O componente usa tokens e semântica acessível
