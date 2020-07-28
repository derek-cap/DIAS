using DIAS.DataModel.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIASWpfApp.ViewModels
{
    class WorklistViewModel : MvxViewModel
    {
        private ObservableCollection<Patient> _patients = new ObservableCollection<Patient>();
        public IEnumerable<Patient> Patients => _patients;

        private MvxCommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new MvxCommand(async () => await SearchAsync()));

        private Subject<WorklistQueryEvent> _queryEvent = new Subject<WorklistQueryEvent>();

        private WorklistModel _worklistModel;

        private bool _isSearchEnabled;
        public bool IsSearchEnabled
        {
            get => _isSearchEnabled;
            set => SetProperty(ref _isSearchEnabled, value);
        }

        public WorklistViewModel()
        {
            _isSearchEnabled = true;

            _queryEvent.ObserveOn(SynchronizationContext.Current)
               .Subscribe(Handle);

            _worklistModel = new WorklistModel(_queryEvent);

            _patients.Add(new Patient() { Name = "", AccessionNumber = 1, Age = 18 });
            
        }

        private async Task SearchAsync()
        {
            IsSearchEnabled = false;
            await _worklistModel.QueryAsync();
            IsSearchEnabled = true;
        }

        private void Handle(WorklistQueryEvent @event)
        {
            switch (@event.State)
            {
                case QueryStates.Start:
                    _patients.Clear();
                    break;
                case QueryStates.Pending:
                    _patients.Add(@event.Patient);
                    break;
                default:
                    break;
            }
        }
    }
}
