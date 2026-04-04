/**
 * Ganesha Design Lab — Radial Progress Interactivity
 * Glow effect on arc + bold label on hover.
 */

import { markInitialized, isInitialized } from './utils';

const SELECTOR = '.gns-radial-progress';

export function initRadialProgress(): void {
  document.querySelectorAll<HTMLElement>(SELECTOR).forEach((el) => {
    if (isInitialized(el)) return;
    markInitialized(el);

    const arc = el.querySelector<SVGElement>('.gns-radial-progress__arc');
    const labelEl = el.querySelector<HTMLElement>('.gns-radial-progress__label');
    if (!arc || !labelEl) return;

    el.addEventListener('mouseenter', () => {
      arc.style.filter = 'drop-shadow(0 0 6px currentColor)';
      labelEl.style.fontWeight = '800';
    });

    el.addEventListener('mouseleave', () => {
      arc.style.filter = '';
      labelEl.style.fontWeight = '700';
    });
  });
}
