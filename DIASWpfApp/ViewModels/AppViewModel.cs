using MvvmCross.Commands;
using MvvmCross.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIASWpfApp.ViewModels
{
    public class AppViewModel : MvxViewModel
    {

        private string _information;
        public string Information
        {
            get => _information;
            set => SetProperty(ref _information, value);
        }

        private INotifyPropertyChanged _currentContent;
        public INotifyPropertyChanged CurrentContent
        {
            get => _currentContent;
            set => SetProperty(ref _currentContent, value);
        }

        private readonly List<INotifyPropertyChanged> _contents;
        public IEnumerable<INotifyPropertyChanged> Contents => _contents;

        private MvxAsyncCommand _openCommand;
        public ICommand OpenCommand => _openCommand ?? (_openCommand = new MvxAsyncCommand(OpenAsync));


        public AppViewModel()
        {
            _contents = new List<INotifyPropertyChanged>();
            _contents.Add(new CrossSearchViewModel());

            _contents.Add(new MainViewModel());

            _currentContent = _contents.First();
        }

        private async Task OpenAsync()
        {
            CurrentContent = Contents.Last();
            Information = "Do open.";
            await Task.CompletedTask;
        }

    }
}
