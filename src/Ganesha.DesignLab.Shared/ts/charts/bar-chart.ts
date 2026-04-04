/**
 * Ganesha Design Lab — Bar Chart Interactivity
 * Tooltip on hover + click pulse effect for vertical bar charts.
 */

import {
  buildTooltipContent,
  showTooltip,
  positionTooltip,
  hideTooltip,
} from '../core/tooltip';
import { markInitialized, isInitialized } from './utils';

const SELECTOR = '.gns-bar-chart__bar-group';

export function initBarCharts(): void {
  document.querySelectorAll<HTMLElement>(SELECTOR).forEach((group) => {
    if (isInitialized(group)) return;
    markInitialized(group);

    const bar = group.querySelector<SVGElement>('.gns-bar-chart__bar');
    const titleEl = bar?.querySelector('title');
    if (!titleEl) return;

    group.addEventListener('mouseenter', (e: MouseEvent) => {
      const text = titleEl.textContent ?? '';
      const [label, ...rest] = text.split(': ');
      const value = rest.join(': ');
      const color = bar!.getAttribute('fill') ?? 'var(--gns-primary-500)';
      showTooltip(e, buildTooltipContent(color, label, value));
    });

    group.addEventListener('mousemove', positionTooltip);
    group.addEventListener('mouseleave', hideTooltip);

    group.addEventListener('click', () => {
      if (!bar) return;
      bar.style.transition = 'opacity 100ms ease';
      bar.style.opacity = '0.5';
      setTimeout(() => {
        bar.style.opacity = '1';
      }, 200);
    });
  });
}
