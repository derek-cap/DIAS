using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIAS.DataModel.Models
{
    public class WorklistModel
    {
        private TaskCompletionSource<bool> _queryTask;
        private CancellationTokenSource _tokenSource;

        private readonly object _mutex = new object();

        private IObserver<WorklistQueryEvent> _observer;

        public WorklistModel(IObserver<WorklistQueryEvent> observer)
        {
            _queryTask = new TaskCompletionSource<bool>();
            _queryTask.SetResult(true);
            _observer = observer;
            
            var _ = TimerQueryAsync(observer);
        }

        public async Task QueryAsync()
        {
            lock (_mutex)
            {
                _tokenSource?.Cancel();
            }
            await _queryTask.Task.ConfigureAwait(false);
            await Task.Run(() => DoQuery(_observer));         
        }

        private async Task TimerQueryAsync(IObserver<WorklistQueryEvent> observer)
        {
            await Task.Delay(10).ConfigureAwait(false);

            //while (true)
            //{
            //    await Task.Delay(TimeSpan.FromSeconds(5));
            //    DoQuery(observer);
            //}
            Observable.Interval(TimeSpan.FromSeconds(5))
                .Subscribe(t =>
                {
                    //await _queryTask.Task.ConfigureAwait(false);
                    //// Forbide update right after query by manual.
                    //await Task.Delay(5000);
                    DoQuery(observer);
                    Task.Delay(5000).Wait();
                });
        }      

        private void DoQuery(IObserver<WorklistQueryEvent> observer)
        {
            lock (_mutex)
            {
                if (_queryTask.Task.IsCompleted == false) return;
                _queryTask = new TaskCompletionSource<bool>();
                _tokenSource = new CancellationTokenSource();
            }
            observer.OnNext(WorklistQueryEvent.StartEvent());
            foreach (var item in PatientFactory.DoQuery(_tokenSource.Token))
            {
                var @event = WorklistQueryEvent.NewPatient(item);
                observer.OnNext(@event);
            }
            observer.OnNext(WorklistQueryEvent.EndEvent());
            lock (_mutex)
            {
                _queryTask.SetResult(true);
            }

        }
   
    }
}
