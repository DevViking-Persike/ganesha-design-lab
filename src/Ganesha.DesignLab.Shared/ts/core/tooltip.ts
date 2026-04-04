/**
 * Ganesha Design Lab — Tooltip
 * Singleton tooltip element for all chart hover interactions.
 */

let tooltip: HTMLDivElement | null = null;

const TOOLTIP_PADDING = 12;

const TOOLTIP_STYLES: Partial<CSSStyleDeclaration> = {
  position: 'fixed',
  zIndex: '9999',
  pointerEvents: 'none',
  opacity: '0',
  transition: 'opacity 150ms ease, transform 150ms ease',
  transform: 'translateY(4px)',
  padding: '6px 12px',
  borderRadius: 'var(--gns-radius-md, 6px)',
  fontSize: 'var(--gns-text-xs, 0.75rem)',
  fontFamily: 'var(--gns-font-sans, system-ui, sans-serif)',
  fontWeight: '600',
  lineHeight: '1.4',
  backgroundColor: 'var(--gns-color-surface-inverse, #221C2E)',
  color: 'var(--gns-color-text-inverse, #EFECF5)',
  boxShadow: 'var(--gns-shadow-lg, 0 10px 15px -3px rgba(0,0,0,.2))',
  whiteSpace: 'nowrap',
  maxWidth: '280px',
};

function ensureTooltip(): HTMLDivElement {
  if (tooltip) return tooltip;

  tooltip = document.createElement('div');
  tooltip.className = 'gnsh-chart-tooltip';
  tooltip.setAttribute('role', 'tooltip');
  tooltip.setAttribute('aria-hidden', 'true');
  Object.assign(tooltip.style, TOOLTIP_STYLES);
  document.body.appendChild(tooltip);

  return tooltip;
}

export function buildTooltipContent(
  color: string,
  label: string,
  value: string,
): DocumentFragment {
  const dot = document.createElement('span');
  Object.assign(dot.style, {
    display: 'inline-block',
    width: '8px',
    height: '8px',
    borderRadius: '50%',
    background: color,
    marginRight: '6px',
    verticalAlign: 'middle',
  });

  const strong = document.createElement('strong');
  strong.textContent = label;

  const text = document.createTextNode(value ? `: ${value}` : '');

  const fragment = document.createDocumentFragment();
  fragment.appendChild(dot);
  fragment.appendChild(strong);
  fragment.appendChild(text);

  return fragment;
}

export function showTooltip(e: MouseEvent, content: Node | string): void {
  const tt = ensureTooltip();
  tt.textContent = '';

  if (content instanceof Node) {
    tt.appendChild(content);
  } else {
    tt.textContent = content;
  }

  tt.style.opacity = '1';
  tt.style.transform = 'translateY(0)';
  tt.setAttribute('aria-hidden', 'false');
  positionTooltip(e);
}

export function positionTooltip(e: MouseEvent): void {
  if (!tooltip) return;

  const rect = tooltip.getBoundingClientRect();
  let x = e.clientX + TOOLTIP_PADDING;
  let y = e.clientY - rect.height - TOOLTIP_PADDING;

  if (x + rect.width > window.innerWidth) {
    x = e.clientX - rect.width - TOOLTIP_PADDING;
  }
  if (y < 0) {
    y = e.clientY + TOOLTIP_PADDING;
  }

  tooltip.style.left = `${x}px`;
  tooltip.style.top = `${y}px`;
}

export function hideTooltip(): void {
  if (!tooltip) return;

  tooltip.style.opacity = '0';
  tooltip.style.transform = 'translateY(4px)';
  tooltip.setAttribute('aria-hidden', 'true');
}
