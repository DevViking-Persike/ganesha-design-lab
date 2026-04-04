/**
 * Ganesha Design Lab — Progress Bar Interactivity
 * Brightness + shadow effect on hover.
 */

import { markInitialized, isInitialized } from './utils';

const SELECTOR = '.gns-progress-bar__fill';

export function initProgressBars(): void {
  document.querySelectorAll<HTMLElement>(SELECTOR).forEach((fill) => {
    if (isInitialized(fill)) return;
    markInitialized(fill);

    const container = fill.closest<HTMLElement>('.gns-progress-bar');
    if (!container) return;

    container.addEventListener('mouseenter', () => {
      fill.style.filter = 'brightness(1.12)';
      fill.style.boxShadow = '0 0 8px rgba(0,0,0,0.15)';
    });

    container.addEventListener('mouseleave', () => {
      fill.style.filter = '';
      fill.style.boxShadow = '';
    });
  });
}
