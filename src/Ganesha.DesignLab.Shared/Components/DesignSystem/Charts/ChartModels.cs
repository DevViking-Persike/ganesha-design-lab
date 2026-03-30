namespace Ganesha.DesignLab.Shared.Components.DesignSystem.Charts;

public record ChartDataPoint(string Label, double Value, string? Color = null);

public record ChartSeries(string Name, IReadOnlyList<double> Values, string? Color = null);

public enum ChartSize { Small, Medium, Large }

public enum ProgressBarVariant { Default, Success, Warning, Danger, Info }

public enum ProgressBarSize { Small, Medium, Large }
