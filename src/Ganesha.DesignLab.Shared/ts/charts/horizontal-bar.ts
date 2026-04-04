/**
 * Ganesha Design Lab — Horizontal Bar Chart Interactivity
 * Tooltip + brightness effect on row hover.
 */

import {
  buildTooltipContent,
  showTooltip,
  positionTooltip,
  hideTooltip,
} from '../core/tooltip';
import { markInitialized, isInitialized } from './utils';

const SELECTOR = '.gns-hbar-chart__row';

export function initHorizontalBarCharts(): void {
  document.querySelectorAll<HTMLElement>(SELECTOR).forEach((row) => {
    if (isInitialized(row)) return;
    markInitialized(row);

    const fill = row.querySelector<HTMLElement>('.gns-hbar-chart__fill');
    const label = row.querySelector<HTMLElement>('.gns-hbar-chart__label');
    const value = row.querySelector<HTMLElement>('.gns-hbar-chart__value');
    if (!fill) return;

    row.addEventListener('mouseenter', (e: MouseEvent) => {
      const name = label?.textContent?.trim() ?? '';
      const val = value?.textContent?.trim() ?? '';
      const color = fill.style.backgroundColor || 'var(--gns-primary-500)';
      if (name || val) {
        showTooltip(e, buildTooltipContent(color, name, val));
      }
      fill.style.filter = 'brightness(1.15)';
    });

    row.addEventListener('mousemove', positionTooltip);

    row.addEventListener('mouseleave', () => {
      hideTooltip();
      fill.style.filter = '';
    });
  });
}
