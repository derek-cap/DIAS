using DIASWpfApp.Models;
using MvvmCross.Base;
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

        private MyMvxAsyncCommand _openCommand;
        public ICommand OpenCommand => _openCommand;

        private int _index;


        public AppViewModel()
        {
            _contents = new List<INotifyPropertyChanged>();
            _contents.Add(new WorklistViewModel());
            _contents.Add(new CrossSearchViewModel());

            _contents.Add(new MainViewModel());

            _currentContent = _contents.First();
            _index = 0;

            _openCommand = new MyMvxAsyncCommand(OpenAsync, () => true, false);
        }

        private async Task OpenAsync()
        {
            Information = $"Do open. {_index++}";
            await Task.Delay(6000);

         //   _openCommand.RaiseCanExecuteChanged();
        }



    }
}
