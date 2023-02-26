using SPT.Commands.Base;
using System;

namespace SPT.Commands
{
    public class RelayCommand : CommandBase
    {
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public override void Execute(object parameter) => _execute(parameter);
        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
    }
}
