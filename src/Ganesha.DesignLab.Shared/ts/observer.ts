/**
 * Ganesha Design Lab — DOM Observer
 * Re-initializes chart interactivity after Blazor enhanced navigation
 * or SignalR reconnect injects new chart elements.
 */

const CHART_CLASSES = [
  'gns-bar-chart',
  'gns-line-chart',
  'gns-donut-chart',
  'gns-hbar-chart',
  'gns-radial-progress',
  'gns-progress-bar',
  'gns-bubble-chart',
] as const;

const CHART_SELECTOR = CHART_CLASSES.map((c) => `.${c}`).join(', ');

function hasChartNodes(mutations: MutationRecord[]): boolean {
  for (const mutation of mutations) {
    for (const node of mutation.addedNodes) {
      if (node.nodeType !== Node.ELEMENT_NODE) continue;

      const el = node as Element;
      const matchesSelf = CHART_CLASSES.some((cls) => el.classList?.contains(cls));
      if (matchesSelf) return true;

      if (el.querySelector?.(CHART_SELECTOR)) return true;
    }
  }
  return false;
}

export function observeChartChanges(initAll: () => void): void {
  const observer = new MutationObserver((mutations) => {
    if (hasChartNodes(mutations)) {
      requestAnimationFrame(initAll);
    }
  });

  observer.observe(document.body, { childList: true, subtree: true });
}
