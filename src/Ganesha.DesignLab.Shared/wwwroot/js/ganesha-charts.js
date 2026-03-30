/**
 * Ganesha Design Lab — Chart Interactivity
 * Tooltips, hover effects, and click interactions for SVG/CSS charts.
 */
(function () {
    'use strict';

    // =========================================================================
    // TOOLTIP
    // =========================================================================
    let tooltip = null;

    function ensureTooltip() {
        if (tooltip) return tooltip;
        tooltip = document.createElement('div');
        tooltip.className = 'gnsh-chart-tooltip';
        tooltip.setAttribute('role', 'tooltip');
        tooltip.setAttribute('aria-hidden', 'true');
        Object.assign(tooltip.style, {
            position: 'fixed',
            zIndex: '9999',
            pointerEvents: 'none',
            opacity: '0',
            transition: 'opacity 150ms ease, transform 150ms ease',
            transform: 'translateY(4px)',
            padding: '6px 12px',
            borderRadius: 'var(--gns-radius-md, 6px)',
            fontSize: 'var(--gns-text-xs, 0.75rem)',
            fontFamily: 'var(--gns-font-sans, system-ui, sans-serif)',
            fontWeight: '600',
            lineHeight: '1.4',
            backgroundColor: 'var(--gns-color-surface-inverse, #221C2E)',
            color: 'var(--gns-color-text-inverse, #EFECF5)',
            boxShadow: 'var(--gns-shadow-lg, 0 10px 15px -3px rgba(0,0,0,.2))',
            whiteSpace: 'nowrap',
            maxWidth: '280px'
        });
        document.body.appendChild(tooltip);
        return tooltip;
    }

    function buildTooltipContent(color, label, value) {
        const dot = document.createElement('span');
        Object.assign(dot.style, {
            display: 'inline-block',
            width: '8px',
            height: '8px',
            borderRadius: '50%',
            background: color,
            marginRight: '6px',
            verticalAlign: 'middle'
        });

        const strong = document.createElement('strong');
        strong.textContent = label;

        const text = document.createTextNode(value ? ': ' + value : '');
        const fragment = document.createDocumentFragment();
        fragment.appendChild(dot);
        fragment.appendChild(strong);
        fragment.appendChild(text);
        return fragment;
    }

    function showTooltip(e, content) {
        const tt = ensureTooltip();
        tt.textContent = '';
        if (content instanceof DocumentFragment || content instanceof Node) {
            tt.appendChild(content);
        } else {
            tt.textContent = content;
        }
        tt.style.opacity = '1';
        tt.style.transform = 'translateY(0)';
        tt.setAttribute('aria-hidden', 'false');
        positionTooltip(e);
    }

    function positionTooltip(e) {
        if (!tooltip) return;
        const pad = 12;
        const rect = tooltip.getBoundingClientRect();
        let x = e.clientX + pad;
        let y = e.clientY - rect.height - pad;
        if (x + rect.width > window.innerWidth) x = e.clientX - rect.width - pad;
        if (y < 0) y = e.clientY + pad;
        tooltip.style.left = x + 'px';
        tooltip.style.top = y + 'px';
    }

    function hideTooltip() {
        if (!tooltip) return;
        tooltip.style.opacity = '0';
        tooltip.style.transform = 'translateY(4px)';
        tooltip.setAttribute('aria-hidden', 'true');
    }

    // =========================================================================
    // BAR CHART INTERACTIVITY
    // =========================================================================
    function initBarCharts() {
        document.querySelectorAll('.gns-bar-chart__bar-group').forEach(group => {
            if (group._gnshInit) return;
            group._gnshInit = true;

            const bar = group.querySelector('.gns-bar-chart__bar');
            const titleEl = bar?.querySelector('title');
            if (!titleEl) return;

            group.addEventListener('mouseenter', function (e) {
                const text = titleEl.textContent;
                const [label, ...rest] = text.split(': ');
                const value = rest.join(': ');
                const color = bar.getAttribute('fill') || 'var(--gns-primary-500)';
                showTooltip(e, buildTooltipContent(color, label, value));
            });
            group.addEventListener('mousemove', positionTooltip);
            group.addEventListener('mouseleave', hideTooltip);

            // Click pulse effect
            group.addEventListener('click', function () {
                if (!bar) return;
                bar.style.transition = 'opacity 100ms ease';
                bar.style.opacity = '0.5';
                setTimeout(() => { bar.style.opacity = '1'; }, 200);
            });
        });
    }

    // =========================================================================
    // LINE CHART DOT INTERACTIVITY
    // =========================================================================
    function initLineCharts() {
        document.querySelectorAll('.gns-line-chart__dot').forEach(dot => {
            if (dot._gnshInit) return;
            dot._gnshInit = true;

            const titleEl = dot.querySelector('title');
            if (!titleEl) return;

            dot.addEventListener('mouseenter', function (e) {
                const text = titleEl.textContent;
                const color = dot.getAttribute('fill') || 'var(--gns-primary-500)';
                showTooltip(e, buildTooltipContent(color, text, ''));
                dot.setAttribute('r', '6');
                dot.style.filter = 'drop-shadow(0 0 4px ' + color + ')';
            });
            dot.addEventListener('mousemove', positionTooltip);
            dot.addEventListener('mouseleave', function () {
                hideTooltip();
                dot.setAttribute('r', '4');
                dot.style.filter = '';
            });
        });
    }

    // =========================================================================
    // DONUT CHART INTERACTIVITY
    // =========================================================================
    function initDonutCharts() {
        document.querySelectorAll('.gns-donut-chart__segment').forEach(seg => {
            if (seg._gnshInit) return;
            seg._gnshInit = true;

            const titleEl = seg.querySelector('title');
            if (!titleEl) return;

            seg.addEventListener('mouseenter', function (e) {
                const text = titleEl.textContent;
                const color = seg.getAttribute('stroke') || 'var(--gns-primary-500)';
                showTooltip(e, buildTooltipContent(color, text, ''));
                seg.style.filter = 'drop-shadow(0 0 6px ' + color + ')';
            });
            seg.addEventListener('mousemove', positionTooltip);
            seg.addEventListener('mouseleave', function () {
                hideTooltip();
                seg.style.filter = '';
            });
        });
    }

    // =========================================================================
    // HORIZONTAL BAR INTERACTIVITY
    // =========================================================================
    function initHorizontalBarCharts() {
        document.querySelectorAll('.gns-hbar-chart__row').forEach(row => {
            if (row._gnshInit) return;
            row._gnshInit = true;

            const fill = row.querySelector('.gns-hbar-chart__fill');
            const label = row.querySelector('.gns-hbar-chart__label');
            const value = row.querySelector('.gns-hbar-chart__value');
            if (!fill) return;

            row.addEventListener('mouseenter', function (e) {
                const name = label?.textContent?.trim() || '';
                const val = value?.textContent?.trim() || '';
                const color = fill.style.backgroundColor || 'var(--gns-primary-500)';
                if (name || val) {
                    showTooltip(e, buildTooltipContent(color, name, val));
                }
                fill.style.filter = 'brightness(1.15)';
            });
            row.addEventListener('mousemove', positionTooltip);
            row.addEventListener('mouseleave', function () {
                hideTooltip();
                fill.style.filter = '';
            });
        });
    }

    // =========================================================================
    // RADIAL PROGRESS INTERACTIVITY
    // =========================================================================
    function initRadialProgress() {
        document.querySelectorAll('.gns-radial-progress').forEach(el => {
            if (el._gnshInit) return;
            el._gnshInit = true;

            const arc = el.querySelector('.gns-radial-progress__arc');
            const labelEl = el.querySelector('.gns-radial-progress__label');
            if (!arc || !labelEl) return;

            el.addEventListener('mouseenter', function () {
                arc.style.filter = 'drop-shadow(0 0 6px currentColor)';
                labelEl.style.fontWeight = '800';
            });
            el.addEventListener('mouseleave', function () {
                arc.style.filter = '';
                labelEl.style.fontWeight = '700';
            });
        });
    }

    // =========================================================================
    // PROGRESS BAR HOVER
    // =========================================================================
    function initProgressBars() {
        document.querySelectorAll('.gns-progress-bar__fill').forEach(fill => {
            if (fill._gnshInit) return;
            fill._gnshInit = true;

            fill.closest('.gns-progress-bar')?.addEventListener('mouseenter', function () {
                fill.style.filter = 'brightness(1.12)';
                fill.style.boxShadow = '0 0 8px rgba(0,0,0,0.15)';
            });
            fill.closest('.gns-progress-bar')?.addEventListener('mouseleave', function () {
                fill.style.filter = '';
                fill.style.boxShadow = '';
            });
        });
    }

    // =========================================================================
    // INIT — run on page load and after Blazor enhanced navigation
    // =========================================================================
    function initAll() {
        initBarCharts();
        initLineCharts();
        initDonutCharts();
        initHorizontalBarCharts();
        initRadialProgress();
        initProgressBars();
    }

    // Run on DOMContentLoaded
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initAll);
    } else {
        initAll();
    }

    // Re-init after Blazor enhanced navigation / SignalR reconnect
    const observer = new MutationObserver(function (mutations) {
        let hasChartChanges = false;
        for (const m of mutations) {
            if (m.addedNodes.length > 0) {
                for (const node of m.addedNodes) {
                    if (node.nodeType === 1 && (
                        node.classList?.contains('gns-bar-chart') ||
                        node.classList?.contains('gns-line-chart') ||
                        node.classList?.contains('gns-donut-chart') ||
                        node.classList?.contains('gns-hbar-chart') ||
                        node.classList?.contains('gns-radial-progress') ||
                        node.classList?.contains('gns-progress-bar') ||
                        node.querySelector?.('.gns-bar-chart, .gns-line-chart, .gns-donut-chart, .gns-hbar-chart, .gns-radial-progress, .gns-progress-bar')
                    )) {
                        hasChartChanges = true;
                        break;
                    }
                }
            }
            if (hasChartChanges) break;
        }
        if (hasChartChanges) {
            requestAnimationFrame(initAll);
        }
    });

    observer.observe(document.body, { childList: true, subtree: true });

})();
