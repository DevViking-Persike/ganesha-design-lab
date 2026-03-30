# Checklist de Pull Request — Ganesha Design System

## Como Usar

Antes de abrir um PR, revise cada item abaixo. Itens marcados como **obrigatório** bloqueiam o merge se não estiverem conformes. Itens marcados como **recomendado** devem ter justificativa documentada no PR caso não sejam atendidos.

Copie a seção de checklist para a descrição do seu PR ao criá-lo.

---

## Checklist Completo

### Nomenclatura e Convenções

- [ ] **[obrigatório]** Nome do componente segue o padrão `Gns` + PascalCase
- [ ] **[obrigatório]** Classes CSS seguem BEM com prefixo `hlx-` (ex.: `hlx-button`, `hlx-button--primary`, `hlx-button__icon`)
- [ ] **[obrigatório]** Namespace do arquivo reflete a estrutura de pastas (`Ganesha.DesignLab.Shared.Components.DesignSystem.{Categoria}`)
- [ ] **[obrigatório]** Arquivo está na pasta correta conforme a categoria do componente

### Tokens e Estilos

- [ ] **[obrigatório]** Nenhuma cor, tamanho ou espaçamento hardcoded — todos os valores usam tokens CSS (`var(--hlx-*)`)
- [ ] **[obrigatório]** Funciona corretamente no tema **light**
- [ ] **[obrigatório]** Funciona corretamente no tema **dark**
- [ ] **[obrigatório]** Arquivo `.razor.css` de isolamento CSS está co-localizado com o componente

### API do Componente

- [ ] **[obrigatório]** `EventCallback` (ou `EventCallback<T>`) utilizado para todos os eventos — sem uso de `Action` ou `Func` como parâmetros de evento
- [ ] **[obrigatório]** `CssClassBuilder` utilizado para composição dinâmica de classes CSS
- [ ] **[obrigatório]** Parâmetro `AdditionalCssClass` (tipo `string?`) presente e aplicado ao elemento raiz
- [ ] **[recomendado]** Parâmetros documentados com comentários XML (`/// <summary>`)

### Estados Interativos

- [ ] **[obrigatório]** Todos os estados interativos implementados: hover, focus, active, disabled
- [ ] **[obrigatório]** Estado de `disabled` usa `aria-disabled` e `pointer-events: none` (não apenas `opacity`)
- [ ] **[recomendado]** Estado de `loading` implementado com spinner e `aria-busy="true"` quando aplicável
- [ ] **[recomendado]** Estados de `error` e `success` implementados quando o componente é usado em contexto de formulário ou feedback

### Acessibilidade

- [ ] **[obrigatório]** Navegação por teclado funcional (`Tab`, `Enter`, `Space`; teclas de seta onde aplicável)
- [ ] **[obrigatório]** Suporte a leitores de tela: atributos `aria-*`, `role` e `aria-label`/`aria-labelledby` adequados
- [ ] **[obrigatório]** Foco visível implementado — `outline: none` sem alternativa visual é proibido
- [ ] **[recomendado]** Contraste de cor verificado para ambos os temas (mínimo WCAG AA: 4.5:1 para texto normal)

### Isolamento e Portabilidade

- [ ] **[obrigatório]** Nenhum código específico de plataforma no projeto `Shared` (sem referências a `Microsoft.Maui.*` ou APIs de browser sem abstração)
- [ ] **[obrigatório]** Sem dependências de bibliotecas de terceiros não previamente aprovadas no projeto `Shared`

### Responsividade

- [ ] **[obrigatório]** Sem overflow ou quebra de layout em viewports estreitas
- [ ] **[recomendado]** Comportamento responsivo definido e validado nos breakpoints relevantes

### Compatibilidade e Qualidade

- [ ] **[obrigatório]** Build sem erros e sem warnings no projeto `Shared`
- [ ] **[obrigatório]** Build sem erros e sem warnings no projeto `Web`
- [ ] **[obrigatório]** Build sem erros e sem warnings no projeto `MAUI`
- [ ] **[obrigatório]** Nenhuma quebra de API em componentes existentes (adições são ok; remoção/renomeação de parâmetros requer versioning)

### Lab e Documentação

- [ ] **[obrigatório]** Página de Lab criada ou atualizada com exemplos do componente novo/alterado
- [ ] **[recomendado]** Exemplos no Lab cobrem todas as variantes e estados implementados

---

## Template de Descrição para o PR

Ao abrir o PR, use o template abaixo na descrição:

```markdown
## O que este PR faz

<!-- Descreva brevemente o que foi implementado ou alterado -->

## Tipo de mudança

- [ ] Novo componente
- [ ] Melhoria em componente existente
- [ ] Correção de bug
- [ ] Refatoração (sem mudança de comportamento)
- [ ] Atualização de tokens ou temas
- [ ] Documentação

## Componentes afetados

<!-- Liste os componentes criados ou modificados -->

## Screenshots / Demonstração

<!-- Adicione capturas de tela ou GIFs das mudanças visuais (tema light e dark quando aplicável) -->

## Checklist

- [ ] Nome do componente segue o padrão `Gns` + PascalCase
- [ ] Classes CSS seguem BEM com prefixo `hlx-`
- [ ] Nenhum valor hardcoded — usa tokens CSS
- [ ] Funciona no tema light e dark
- [ ] Todos os estados interativos implementados (hover, focus, active, disabled)
- [ ] Acessibilidade: navegação por teclado e suporte a leitores de tela
- [ ] Nenhum código específico de plataforma no projeto Shared
- [ ] EventCallbacks usados para eventos (sem Action delegates)
- [ ] CssClassBuilder usado para classes dinâmicas
- [ ] AdditionalCssClass implementado
- [ ] Arquivo .razor.css co-localizado
- [ ] Lab atualizado com exemplos
- [ ] Sem breaking changes em componentes existentes
- [ ] Build sem warnings nos três projetos
- [ ] Testado no host Web
- [ ] Testado no host MAUI
```

---

## Processo de Review

### O que o autor deve fazer antes de solicitar review

1. Revisar o próprio PR linha a linha (self-review)
2. Verificar todos os itens **obrigatórios** do checklist
3. Testar manualmente no browser (Web) e no MAUI
4. Verificar temas light e dark visualmente
5. Verificar navegação por teclado nos componentes afetados

### O que o revisor deve verificar

1. API do componente é intuitiva e consistente com o restante do DS
2. Tokens utilizados são os semânticos corretos para cada propriedade
3. Implementação CSS usa BEM corretamente
4. Acessibilidade básica está presente
5. Não há lógica de negócio no componente
6. Não há breaking changes ocultos

### Critérios para bloqueio de merge

Um PR deve ser bloqueado se:

- Qualquer item **obrigatório** não está atendido
- O build falha em qualquer um dos três projetos
- Existe breaking change não documentada em componente existente
- Código específico de plataforma foi adicionado ao projeto `Shared`
- Valores hardcoded de cor, tamanho ou espaçamento foram introduzidos

---

## Notas Adicionais

### Breaking Changes

Se o PR introduz mudanças incompatíveis em componentes existentes (remoção ou renomeação de parâmetros, mudança de tipo de evento, alteração de estrutura HTML que afete CSS externo):

1. Documente explicitamente na descrição do PR
2. Forneça guia de migração para os consumidores
3. Considere um período de deprecation antes da remoção

### Tokens Novos

Se o PR adiciona novos tokens CSS:

1. Verifique se o token não duplica um existente
2. Siga o padrão de nomenclatura `--hlx-{categoria}-{propriedade}-{variante}`
3. Adicione o token em **ambos** os temas (light e dark)
4. Certifique-se que o valor primitivo correspondente existe (ou adicione-o)

### Componentes Composites

PRs que adicionam ou modificam composites (`AppShell`, `TopBar`, `Sidebar`, etc.) têm impacto estrutural alto e requerem aprovação de pelo menos dois revisores.
