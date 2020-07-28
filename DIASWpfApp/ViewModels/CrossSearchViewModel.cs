using MvvmCross.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace DIASWpfApp.ViewModels
{
    public class CrossSearchViewModel : MvxViewModel
    {
        private string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
        }

        private ObservableCollection<string> _searchResults = new ObservableCollection<string>();
        public IEnumerable<string> SearchResults => _searchResults;


        public CrossSearchViewModel()
        {
            var a = this.ObservableForProperty(v => v.SearchTerm)
                .Select(vm => vm.Value)
                .Throttle(TimeSpan.FromSeconds(1))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(Search);
        }

        private void Search(string searchText)
        {
            _searchResults.Add(searchText);
        }

    }
}
