# Frontend Arquitetura

## Objetivo

Preservar a arquitetura real do repositório: host web fino, biblioteca compartilhada forte e páginas-lab como consumidoras do Design System.

## Regras obrigatórias

- Toda nova UI reutilizável deve nascer em `src/Ganesha.DesignLab.Shared`, não no host web.
- `src/Ganesha.DesignLab.Web` deve permanecer responsável por bootstrap, roteamento host, assets e layout mínimo.
- Componentes base devem ficar em `Components/DesignSystem/`.
- Composições reutilizáveis acima do nível base devem ficar em `Components/Composites/`.
- Infraestrutura transversal de UI deve ficar em `Components/Infrastructure/`, `Services/` ou `Models/`.
- Páginas em `Components/Lab/Pages/` devem consumir contratos existentes e servir como showcase/padrão, não como fonte principal de abstrações.

## Permitido

- Criar novos componentes compostos quando houver repetição comprovada em mais de um contexto real.
- Manter estado local em páginas-lab quando ele for exclusivamente demonstrativo.
- Evoluir serviços scoped de UI quando o uso transversal justificar.

## Proibido

- Colocar lógica de Design System diretamente em `src/Ganesha.DesignLab.Web`.
- Criar contrato reutilizável novo direto dentro de `Lab/Pages`.
- Duplicar estrutura já coberta por `DesignSystem` ou `Composites`.
- Misturar lógica de showcase e infraestrutura global no mesmo arquivo sem necessidade clara.

## Sinais de alerta

- Página-lab acumulando markup repetido com chance evidente de virar composite.
- Host web recebendo componentes, estilos ou lógica que deveriam estar no projeto compartilhado.
- Crescimento de estilos inline em vez de consolidação estrutural.

## Checklist de revisão

- [ ] A mudança ficou no projeto correto (`Web` vs `Shared`)?
- [ ] O artefato novo é base, composite, infraestrutura ou showcase?
- [ ] A direção de dependência continua apontando do host para a library compartilhada?
- [ ] A página-lab continua sendo consumidora, não dona do contrato?
