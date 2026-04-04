/**
 * Ganesha Design Lab — Line Chart Interactivity
 * Tooltip + dot enlargement on hover for line chart data points.
 */

import {
  buildTooltipContent,
  showTooltip,
  positionTooltip,
  hideTooltip,
} from '../core/tooltip';
import { markInitialized, isInitialized } from './utils';

const SELECTOR = '.gns-line-chart__dot';
const DOT_RADIUS_DEFAULT = '4';
const DOT_RADIUS_HOVER = '6';

export function initLineCharts(): void {
  document.querySelectorAll<SVGCircleElement>(SELECTOR).forEach((dot) => {
    if (isInitialized(dot as unknown as HTMLElement)) return;
    markInitialized(dot as unknown as HTMLElement);

    const titleEl = dot.querySelector('title');
    if (!titleEl) return;

    dot.addEventListener('mouseenter', (e: MouseEvent) => {
      const text = titleEl.textContent ?? '';
      const color = dot.getAttribute('fill') ?? 'var(--gns-primary-500)';
      showTooltip(e, buildTooltipContent(color, text, ''));
      dot.setAttribute('r', DOT_RADIUS_HOVER);
      dot.style.filter = `drop-shadow(0 0 4px ${color})`;
    });

    dot.addEventListener('mousemove', positionTooltip);

    dot.addEventListener('mouseleave', () => {
      hideTooltip();
      dot.setAttribute('r', DOT_RADIUS_DEFAULT);
      dot.style.filter = '';
    });
  });
}
