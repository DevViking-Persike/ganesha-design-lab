/**
 * Ganesha Design Lab — Bubble Chart Interactivity
 * Tooltip + glow effect on bubble hover.
 */

import {
  buildTooltipContent,
  showTooltip,
  positionTooltip,
  hideTooltip,
} from '../core/tooltip';
import { markInitialized, isInitialized } from './utils';

const SELECTOR = '.gns-bubble-chart__bubble';

export function initBubbleCharts(): void {
  document.querySelectorAll<SVGCircleElement>(SELECTOR).forEach((bubble) => {
    if (isInitialized(bubble as unknown as HTMLElement)) return;
    markInitialized(bubble as unknown as HTMLElement);

    const titleEl = bubble.querySelector('title');
    if (!titleEl) return;

    bubble.addEventListener('mouseenter', (e: MouseEvent) => {
      const text = titleEl.textContent ?? '';
      const [label, ...rest] = text.split(': ');
      const value = rest.join(': ');
      const color =
        bubble.getAttribute('stroke') ??
        bubble.getAttribute('fill') ??
        'var(--gns-primary-500)';
      showTooltip(e, buildTooltipContent(color, label, value));
      bubble.style.filter = `drop-shadow(0 0 6px ${color})`;
    });

    bubble.addEventListener('mousemove', positionTooltip);

    bubble.addEventListener('mouseleave', () => {
      hideTooltip();
      bubble.style.filter = '';
    });
  });
}
