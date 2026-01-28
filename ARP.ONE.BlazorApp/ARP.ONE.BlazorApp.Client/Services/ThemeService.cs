using System;
using MudBlazor.ThemeManager;

namespace ARP.ONE.BlazorApp.Client.Services
{
    public class ThemeService
    {
        public ThemeManagerTheme ThemeManager { get; private set; } = new ThemeManagerTheme();
        public bool IsDarkMode { get; private set; } = true;

        public event Action? OnChange;

        public void SetThemeManager(ThemeManagerTheme tm)
        {
            ThemeManager = tm ?? new ThemeManagerTheme();
            OnChange?.Invoke();
        }

        public void SetIsDark(bool isDark)
        {
            IsDarkMode = isDark;
            OnChange?.Invoke();
        }
    }
}
