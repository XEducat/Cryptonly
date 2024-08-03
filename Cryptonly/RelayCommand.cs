using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action<object> _executeWithParameter;
    private readonly Action _executeWithoutParameter;
    private readonly Predicate<object> _canExecute;

    // Constructor for commands with parameters
    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {
        _executeWithParameter = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    // Constructor for commands without parameters
    public RelayCommand(Action execute, Predicate<object> canExecute = null)
    {
        _executeWithoutParameter = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        if (_executeWithoutParameter != null)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        if (_executeWithParameter != null)
        {
            _executeWithParameter(parameter);
        }
        else
        {
            _executeWithoutParameter();
        }
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}