# Fluxo de Execução

1. `Program.cs` registra `AddGaneshaDesignLab()` e Razor Components interativos.
2. O host mapeia `App`.
3. `App.razor` carrega CSS compartilhado, CSS local e JS de charts.
4. `Routes.razor` monta o `Router` com páginas do host e da library compartilhada.
5. `MainLayout.razor` envolve a UI com `GnsThemeProvider` e `GnsToastContainer`.
6. Páginas-lab consomem componentes `Gns*` e serviços de UI.
