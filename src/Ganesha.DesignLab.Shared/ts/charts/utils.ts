/**
 * Ganesha Design Lab — Chart Utilities
 * Shared helpers for chart initialization tracking.
 */

const INIT_FLAG = '_gnshInit';

interface InitializableElement extends HTMLElement {
  [INIT_FLAG]?: boolean;
}

export function isInitialized(el: HTMLElement): boolean {
  return !!(el as InitializableElement)[INIT_FLAG];
}

export function markInitialized(el: HTMLElement): void {
  (el as InitializableElement)[INIT_FLAG] = true;
}
