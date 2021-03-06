using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using Caliburn.Micro;

using EAShow.Core.Helpers;
using EAShow.Core.Services;

using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace EAShow.Core.ViewModels
{
    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings.md
    public class SettingsViewModel : Screen
    {
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { Set(ref _elementTheme, value); }
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get { return _versionDescription; }

            set { Set(ref _versionDescription, value); }
        }

        public async void SwitchTheme(ElementTheme theme)
        {
            await ThemeSelectorService.SetThemeAsync(theme);
        }

        public SettingsViewModel()
        {
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            VersionDescription = GetVersionDescription();
            return base.OnInitializeAsync(cancellationToken: cancellationToken);
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
