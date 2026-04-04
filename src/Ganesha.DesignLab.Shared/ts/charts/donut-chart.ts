/**
 * Ganesha Design Lab — Donut Chart Interactivity
 * Tooltip + glow effect on segment hover.
 */

import {
  buildTooltipContent,
  showTooltip,
  positionTooltip,
  hideTooltip,
} from '../core/tooltip';
import { markInitialized, isInitialized } from './utils';

const SELECTOR = '.gns-donut-chart__segment';

export function initDonutCharts(): void {
  document.querySelectorAll<SVGElement>(SELECTOR).forEach((seg) => {
    if (isInitialized(seg as unknown as HTMLElement)) return;
    markInitialized(seg as unknown as HTMLElement);

    const titleEl = seg.querySelector('title');
    if (!titleEl) return;

    seg.addEventListener('mouseenter', (e: MouseEvent) => {
      const text = titleEl.textContent ?? '';
      const color = seg.getAttribute('stroke') ?? 'var(--gns-primary-500)';
      showTooltip(e, buildTooltipContent(color, text, ''));
      seg.style.filter = `drop-shadow(0 0 6px ${color})`;
    });

    seg.addEventListener('mousemove', positionTooltip);

    seg.addEventListener('mouseleave', () => {
      hideTooltip();
      seg.style.filter = '';
    });
  });
}
