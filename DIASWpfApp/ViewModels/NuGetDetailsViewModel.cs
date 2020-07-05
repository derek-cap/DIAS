using Accessibility;
using NuGet.Protocol.Core.Types;
using NuGet.Protocol.Plugins;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;

namespace DIASWpfApp.ViewModels
{
    public class NuGetDetailsViewModel : ReactiveObject
    {
        private readonly IPackageSearchMetadata _metadata;
        private readonly Uri _defaultUrl;

        public Uri IconUrl => _metadata.IconUrl ?? _defaultUrl;
        public string Description => _metadata.Description;
        public Uri ProjectUrl => _metadata.ProjectUrl;
        public string Title => _metadata.Title;

        public ReactiveCommand<Unit, Unit> OpenPage { get; }

        public NuGetDetailsViewModel(IPackageSearchMetadata metadata)
        {
            _metadata = metadata;
            _defaultUrl = new Uri("https://git.io/fAlfh");

            OpenPage = ReactiveCommand.Create(() =>
            {
                Process.Start(new ProcessStartInfo(this.ProjectUrl.ToString())
                {
                    UseShellExecute = true
                });
            });
        }
    }
}