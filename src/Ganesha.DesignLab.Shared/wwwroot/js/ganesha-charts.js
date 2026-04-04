"use strict";
(() => {
  // ts/core/tooltip.ts
  var tooltip = null;
  var TOOLTIP_PADDING = 12;
  var TOOLTIP_STYLES = {
    position: "fixed",
    zIndex: "9999",
    pointerEvents: "none",
    opacity: "0",
    transition: "opacity 150ms ease, transform 150ms ease",
    transform: "translateY(4px)",
    padding: "6px 12px",
    borderRadius: "var(--gns-radius-md, 6px)",
    fontSize: "var(--gns-text-xs, 0.75rem)",
    fontFamily: "var(--gns-font-sans, system-ui, sans-serif)",
    fontWeight: "600",
    lineHeight: "1.4",
    backgroundColor: "var(--gns-color-surface-inverse, #221C2E)",
    color: "var(--gns-color-text-inverse, #EFECF5)",
    boxShadow: "var(--gns-shadow-lg, 0 10px 15px -3px rgba(0,0,0,.2))",
    whiteSpace: "nowrap",
    maxWidth: "280px"
  };
  function ensureTooltip() {
    if (tooltip) return tooltip;
    tooltip = document.createElement("div");
    tooltip.className = "gnsh-chart-tooltip";
    tooltip.setAttribute("role", "tooltip");
    tooltip.setAttribute("aria-hidden", "true");
    Object.assign(tooltip.style, TOOLTIP_STYLES);
    document.body.appendChild(tooltip);
    return tooltip;
  }
  function buildTooltipContent(color, label, value) {
    const dot = document.createElement("span");
    Object.assign(dot.style, {
      display: "inline-block",
      width: "8px",
      height: "8px",
      borderRadius: "50%",
      background: color,
      marginRight: "6px",
      verticalAlign: "middle"
    });
    const strong = document.createElement("strong");
    strong.textContent = label;
    const text = document.createTextNode(value ? `: ${value}` : "");
    const fragment = document.createDocumentFragment();
    fragment.appendChild(dot);
    fragment.appendChild(strong);
    fragment.appendChild(text);
    return fragment;
  }
  function showTooltip(e, content) {
    const tt = ensureTooltip();
    tt.textContent = "";
    if (content instanceof Node) {
      tt.appendChild(content);
    } else {
      tt.textContent = content;
    }
    tt.style.opacity = "1";
    tt.style.transform = "translateY(0)";
    tt.setAttribute("aria-hidden", "false");
    positionTooltip(e);
  }
  function positionTooltip(e) {
    if (!tooltip) return;
    const rect = tooltip.getBoundingClientRect();
    let x = e.clientX + TOOLTIP_PADDING;
    let y = e.clientY - rect.height - TOOLTIP_PADDING;
    if (x + rect.width > window.innerWidth) {
      x = e.clientX - rect.width - TOOLTIP_PADDING;
    }
    if (y < 0) {
      y = e.clientY + TOOLTIP_PADDING;
    }
    tooltip.style.left = `${x}px`;
    tooltip.style.top = `${y}px`;
  }
  function hideTooltip() {
    if (!tooltip) return;
    tooltip.style.opacity = "0";
    tooltip.style.transform = "translateY(4px)";
    tooltip.setAttribute("aria-hidden", "true");
  }

  // ts/charts/utils.ts
  var INIT_FLAG = "_gnshInit";
  function isInitialized(el) {
    return !!el[INIT_FLAG];
  }
  function markInitialized(el) {
    el[INIT_FLAG] = true;
  }

  // ts/charts/bar-chart.ts
  var SELECTOR = ".gns-bar-chart__bar-group";
  function initBarCharts() {
    document.querySelectorAll(SELECTOR).forEach((group) => {
      if (isInitialized(group)) return;
      markInitialized(group);
      const bar = group.querySelector(".gns-bar-chart__bar");
      const titleEl = bar?.querySelector("title");
      if (!titleEl) return;
      group.addEventListener("mouseenter", (e) => {
        const text = titleEl.textContent ?? "";
        const [label, ...rest] = text.split(": ");
        const value = rest.join(": ");
        const color = bar.getAttribute("fill") ?? "var(--gns-primary-500)";
        showTooltip(e, buildTooltipContent(color, label, value));
      });
      group.addEventListener("mousemove", positionTooltip);
      group.addEventListener("mouseleave", hideTooltip);
      group.addEventListener("click", () => {
        if (!bar) return;
        bar.style.transition = "opacity 100ms ease";
        bar.style.opacity = "0.5";
        setTimeout(() => {
          bar.style.opacity = "1";
        }, 200);
      });
    });
  }

  // ts/charts/line-chart.ts
  var SELECTOR2 = ".gns-line-chart__dot";
  var DOT_RADIUS_DEFAULT = "4";
  var DOT_RADIUS_HOVER = "6";
  function initLineCharts() {
    document.querySelectorAll(SELECTOR2).forEach((dot) => {
      if (isInitialized(dot)) return;
      markInitialized(dot);
      const titleEl = dot.querySelector("title");
      if (!titleEl) return;
      dot.addEventListener("mouseenter", (e) => {
        const text = titleEl.textContent ?? "";
        const color = dot.getAttribute("fill") ?? "var(--gns-primary-500)";
        showTooltip(e, buildTooltipContent(color, text, ""));
        dot.setAttribute("r", DOT_RADIUS_HOVER);
        dot.style.filter = `drop-shadow(0 0 4px ${color})`;
      });
      dot.addEventListener("mousemove", positionTooltip);
      dot.addEventListener("mouseleave", () => {
        hideTooltip();
        dot.setAttribute("r", DOT_RADIUS_DEFAULT);
        dot.style.filter = "";
      });
    });
  }

  // ts/charts/donut-chart.ts
  var SELECTOR3 = ".gns-donut-chart__segment";
  function initDonutCharts() {
    document.querySelectorAll(SELECTOR3).forEach((seg) => {
      if (isInitialized(seg)) return;
      markInitialized(seg);
      const titleEl = seg.querySelector("title");
      if (!titleEl) return;
      seg.addEventListener("mouseenter", (e) => {
        const text = titleEl.textContent ?? "";
        const color = seg.getAttribute("stroke") ?? "var(--gns-primary-500)";
        showTooltip(e, buildTooltipContent(color, text, ""));
        seg.style.filter = `drop-shadow(0 0 6px ${color})`;
      });
      seg.addEventListener("mousemove", positionTooltip);
      seg.addEventListener("mouseleave", () => {
        hideTooltip();
        seg.style.filter = "";
      });
    });
  }

  // ts/charts/horizontal-bar.ts
  var SELECTOR4 = ".gns-hbar-chart__row";
  function initHorizontalBarCharts() {
    document.querySelectorAll(SELECTOR4).forEach((row) => {
      if (isInitialized(row)) return;
      markInitialized(row);
      const fill = row.querySelector(".gns-hbar-chart__fill");
      const label = row.querySelector(".gns-hbar-chart__label");
      const value = row.querySelector(".gns-hbar-chart__value");
      if (!fill) return;
      row.addEventListener("mouseenter", (e) => {
        const name = label?.textContent?.trim() ?? "";
        const val = value?.textContent?.trim() ?? "";
        const color = fill.style.backgroundColor || "var(--gns-primary-500)";
        if (name || val) {
          showTooltip(e, buildTooltipContent(color, name, val));
        }
        fill.style.filter = "brightness(1.15)";
      });
      row.addEventListener("mousemove", positionTooltip);
      row.addEventListener("mouseleave", () => {
        hideTooltip();
        fill.style.filter = "";
      });
    });
  }

  // ts/charts/radial-progress.ts
  var SELECTOR5 = ".gns-radial-progress";
  function initRadialProgress() {
    document.querySelectorAll(SELECTOR5).forEach((el) => {
      if (isInitialized(el)) return;
      markInitialized(el);
      const arc = el.querySelector(".gns-radial-progress__arc");
      const labelEl = el.querySelector(".gns-radial-progress__label");
      if (!arc || !labelEl) return;
      el.addEventListener("mouseenter", () => {
        arc.style.filter = "drop-shadow(0 0 6px currentColor)";
        labelEl.style.fontWeight = "800";
      });
      el.addEventListener("mouseleave", () => {
        arc.style.filter = "";
        labelEl.style.fontWeight = "700";
      });
    });
  }

  // ts/charts/progress-bar.ts
  var SELECTOR6 = ".gns-progress-bar__fill";
  function initProgressBars() {
    document.querySelectorAll(SELECTOR6).forEach((fill) => {
      if (isInitialized(fill)) return;
      markInitialized(fill);
      const container = fill.closest(".gns-progress-bar");
      if (!container) return;
      container.addEventListener("mouseenter", () => {
        fill.style.filter = "brightness(1.12)";
        fill.style.boxShadow = "0 0 8px rgba(0,0,0,0.15)";
      });
      container.addEventListener("mouseleave", () => {
        fill.style.filter = "";
        fill.style.boxShadow = "";
      });
    });
  }

  // ts/charts/bubble-chart.ts
  var SELECTOR7 = ".gns-bubble-chart__bubble";
  function initBubbleCharts() {
    document.querySelectorAll(SELECTOR7).forEach((bubble) => {
      if (isInitialized(bubble)) return;
      markInitialized(bubble);
      const titleEl = bubble.querySelector("title");
      if (!titleEl) return;
      bubble.addEventListener("mouseenter", (e) => {
        const text = titleEl.textContent ?? "";
        const [label, ...rest] = text.split(": ");
        const value = rest.join(": ");
        const color = bubble.getAttribute("stroke") ?? bubble.getAttribute("fill") ?? "var(--gns-primary-500)";
        showTooltip(e, buildTooltipContent(color, label, value));
        bubble.style.filter = `drop-shadow(0 0 6px ${color})`;
      });
      bubble.addEventListener("mousemove", positionTooltip);
      bubble.addEventListener("mouseleave", () => {
        hideTooltip();
        bubble.style.filter = "";
      });
    });
  }

  // ts/observer.ts
  var CHART_CLASSES = [
    "gns-bar-chart",
    "gns-line-chart",
    "gns-donut-chart",
    "gns-hbar-chart",
    "gns-radial-progress",
    "gns-progress-bar",
    "gns-bubble-chart"
  ];
  var CHART_SELECTOR = CHART_CLASSES.map((c) => `.${c}`).join(", ");
  function hasChartNodes(mutations) {
    for (const mutation of mutations) {
      for (const node of mutation.addedNodes) {
        if (node.nodeType !== Node.ELEMENT_NODE) continue;
        const el = node;
        const matchesSelf = CHART_CLASSES.some((cls) => el.classList?.contains(cls));
        if (matchesSelf) return true;
        if (el.querySelector?.(CHART_SELECTOR)) return true;
      }
    }
    return false;
  }
  function observeChartChanges(initAll2) {
    const observer = new MutationObserver((mutations) => {
      if (hasChartNodes(mutations)) {
        requestAnimationFrame(initAll2);
      }
    });
    observer.observe(document.body, { childList: true, subtree: true });
  }

  // ts/index.ts
  function initAll() {
    initBarCharts();
    initLineCharts();
    initDonutCharts();
    initHorizontalBarCharts();
    initRadialProgress();
    initProgressBars();
    initBubbleCharts();
  }
  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", initAll);
  } else {
    initAll();
  }
  observeChartChanges(initAll);
})();
