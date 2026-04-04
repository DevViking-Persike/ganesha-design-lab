using Ganesha.DesignLab.Shared.Components.DesignSystem.Charts;

namespace Ganesha.DesignLab.Shared.Components.Composites.InteractiveChart;

public enum InteractiveChartType { Bar, Line, Bubble }

public record InteractiveChartOption(
    string Id,
    string Label,
    IReadOnlyList<ChartDataPoint> Data);
