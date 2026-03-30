# Mapa Arquitetural

## Estrutura principal

- `src/Ganesha.DesignLab.Web`
  Host web, bootstrap e roteamento principal
- `src/Ganesha.DesignLab.Shared`
  Biblioteca compartilhada com DS, composites, infraestrutura, serviços, modelos e assets
- `src/Ganesha.DesignLab.Maui`
  Host MAUI secundário

## Direção de dependências

- `Web` depende de `Shared`
- `Shared` não depende do `Web`
- `Lab/Pages` depende de `DesignSystem`, `Composites`, `Services` e `Models`

## Padrão predominante

Biblioteca de componentes + catálogo de exemplos, com host fino e estado simples baseado em serviços scoped.
