# Frontend Procedures For Agents

## Operational Overview

The main frontend host is `src/Ganesha.DesignLab.Web`. The reusable UI system lives in `src/Ganesha.DesignLab.Shared`.

## Architecture

- Keep the web host thin.
- Put reusable UI in `Shared`.
- Use `DesignSystem` for base components.
- Use `Composites` for proven reusable compositions.
- Treat `Lab` pages as showcase and pattern consumers.

## Read First

- `docs/architecture/frontend/00-overview.md`
- `docs/analysis/frontend/01-mapeamento-inicial.md`
- `docs/analysis/frontend/03-design-system.md`
- `.claude/rules/frontend-*.md`
- `.claude/commands/fe-*.md`

## Reuse Before New

Before creating anything new, inspect:

- `src/Ganesha.DesignLab.Shared/Components/DesignSystem/`
- `src/Ganesha.DesignLab.Shared/Components/Composites/`

If the case is only a variation, prefer extending the existing contract.

## Creating Features

Follow `.claude/commands/fe-implement-feature.md`.

## Creating Components

Follow `.claude/commands/fe-create-component.md`.

## Refactoring Components

Follow `.claude/commands/fe-refactor-component.md`.

## Adding Tests

Follow `.claude/commands/fe-add-tests.md`.

## Validating Test Reliability

Follow `.claude/commands/fe-validate-tests.md`.

## Risks

- No frontend test suite exists yet.
- Base component changes can affect many lab pages.
- Theme, toast timers and charts need extra care.

## Review Checklist

- [ ] Correct layer chosen
- [ ] Existing reusable pieces checked
- [ ] Tokens and naming conventions respected
- [ ] Accessibility and critical states considered
- [ ] Validation plan defined
