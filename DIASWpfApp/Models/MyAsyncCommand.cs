using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIASWpfApp.Models
{
    public abstract class MyAsyncCommandBase : MvxCommandBase
    {
        private readonly object _syncRoot = new object();

        private readonly bool _allowConcurrentExecutions;

        private CancellationTokenSource _cts;

        private int _concurrentExecutions;

        protected MyAsyncCommandBase(bool allowConcurrentExecutions = false)
        {
            _allowConcurrentExecutions = allowConcurrentExecutions;
        }

        public bool IsRunning => _concurrentExecutions > 0;

        protected CancellationToken CancelToken => _cts.Token;

        protected abstract bool CanExecuteImpl(object parameter);

        protected abstract Task ExecuteAsyncImpl(object parameter);

        public void Cancel()
        {
            lock (_syncRoot)
            {
                if (_cts == null)
                {
                }
                else
                {
                    _cts.Cancel();
                }
            }
        }

        public bool CanExecute()
        {
            return CanExecute(null);
        }

        public bool CanExecute(object parameter)
        {
            if (!_allowConcurrentExecutions && IsRunning)
                return false;
            else
                return CanExecuteImpl(parameter);
        }

        public async void Execute(object parameter)
        {
            try
            {
                await ExecuteAsync(parameter, true).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Execute()
        {
            Execute(null);
        }

        protected async Task ExecuteAsync(object parameter, bool hideCanceledException)
        {
            if (CanExecuteImpl(parameter))
            {
                await ExecuteConcurrentAsync(parameter, hideCanceledException).ConfigureAwait(false);
            }
        }

        private async Task ExecuteConcurrentAsync(object parameter, bool hideCanceledException)
        {
            bool started = false;
            try
            {
                lock (_syncRoot)
                {
                    if (_concurrentExecutions == 0)
                    {
                        InitCancellationTokenSource();
                    }
                    else if (!_allowConcurrentExecutions)
                    {
                        return;
                    }
                    _concurrentExecutions++;
                    started = true;
                }

                if (!_allowConcurrentExecutions)
                {
                    RaiseCanExecuteChanged();
                }

                if (!CancelToken.IsCancellationRequested)
                {
                    try
                    {
                        // With configure await false, the CanExecuteChanged raised in finally clause might run in another thread.
                        // This should not be an issue as long as ShouldAlwaysRaiseCECOnUserInterfaceThread is true.
                        await ExecuteAsyncImpl(parameter).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException e)
                    {
                        //Rethrow if the exception does not come from the current cancellation token
                        if (!hideCanceledException || e.CancellationToken != CancelToken)
                        {
                            throw;
                        }
                    }
                }
            }
            finally
            {
                if (started)
                {
                    lock (_syncRoot)
                    {
                        _concurrentExecutions--;
                        if (_concurrentExecutions == 0)
                        {
                            ClearCancellationTokenSource();
                        }
                    }
                    if (!_allowConcurrentExecutions)
                    {
                        RaiseCanExecuteChanged();
                    }
                }
            }
        }

        private void ClearCancellationTokenSource()
        {
            if (_cts == null)
            {
            }
            else
            {
                _cts.Dispose();
                _cts = null;
            }
        }

        private void InitCancellationTokenSource()
        {
            if (_cts != null)
            {
            }
            _cts = new CancellationTokenSource();
        }
    }

    public class MyMvxAsyncCommand : MyAsyncCommandBase, IMvxAsyncCommand
    {
        private readonly Func<CancellationToken, Task> _execute;
        private readonly Func<bool> _canExecute;

        public MyMvxAsyncCommand(Func<Task> execute, Func<bool> canExecute = null, bool allowConcurrentExecutions = false)
            : base(allowConcurrentExecutions)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = (cancellationToken) => execute();
            _canExecute = canExecute;
        }

        public MyMvxAsyncCommand(Func<CancellationToken, Task> execute, Func<bool> canExecute = null, bool allowConcurrentExecutions = false)
            : base(allowConcurrentExecutions)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        protected override bool CanExecuteImpl(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        protected override Task ExecuteAsyncImpl(object parameter)
        {
            return _execute(CancelToken);
        }

        public static MvxAsyncCommand<T> CreateCommand<T>(Func<T, Task> execute, Func<T, bool> canExecute = null, bool allowConcurrentExecutions = false)
        {
            return new MvxAsyncCommand<T>(execute, canExecute, allowConcurrentExecutions);
        }


        public static MvxAsyncCommand<T> CreateCommand<T>(Func<T, CancellationToken, Task> execute, Func<T, bool> canExecute = null, bool allowConcurrentExecutions = false)
        {
            return new MvxAsyncCommand<T>(execute, canExecute, allowConcurrentExecutions);
        }

        public async Task ExecuteAsync(object parameter = null)
        {
            await base.ExecuteAsync(parameter, false).ConfigureAwait(false);
        }

    }
}
