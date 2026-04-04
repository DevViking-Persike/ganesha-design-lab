/**
 * Ganesha Design Lab — Chart Interactivity
 * Entry point: initializes tooltips, hover effects, and click interactions
 * for all SVG/CSS chart components.
 */

import { initBarCharts } from './charts/bar-chart';
import { initLineCharts } from './charts/line-chart';
import { initDonutCharts } from './charts/donut-chart';
import { initHorizontalBarCharts } from './charts/horizontal-bar';
import { initRadialProgress } from './charts/radial-progress';
import { initProgressBars } from './charts/progress-bar';
import { initBubbleCharts } from './charts/bubble-chart';
import { observeChartChanges } from './observer';

function initAll(): void {
  initBarCharts();
  initLineCharts();
  initDonutCharts();
  initHorizontalBarCharts();
  initRadialProgress();
  initProgressBars();
  initBubbleCharts();
}

// Run on DOMContentLoaded or immediately if already loaded
if (document.readyState === 'loading') {
  document.addEventListener('DOMContentLoaded', initAll);
} else {
  initAll();
}

// Re-init after Blazor enhanced navigation / SignalR reconnect
observeChartChanges(initAll);
