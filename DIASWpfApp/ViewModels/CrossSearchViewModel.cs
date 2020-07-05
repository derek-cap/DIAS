using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
