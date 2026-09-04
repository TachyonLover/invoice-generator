using InvoiceGen.Services;
using QuestPDF.Infrastructure;
using System.Windows;

namespace InvoiceGen
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            QuestPDF.Settings.License = LicenseType.Community;

            SettingsService settingsService = new SettingsService();
            ThemeService themeService = new ThemeService();

            var settings = settingsService.LoadSettings();

            themeService.ApplyTheme(settings.Appearance);
        }
    }
}