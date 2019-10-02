using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoDispatcher
{
    /// <summary>
    /// Класс, для реализации команд
    /// </summary>
    class SimpleCommand : ICommand
    {

        private Action _action;
        private Action<object> _actionGroup;
        private Action<Collection<object>> addAirplanes;

        public SimpleCommand(Action Action)
        {
            _action = Action;
        }

        public SimpleCommand(Action<object> Actions)
        {
            _actionGroup = Actions;
        }

        public SimpleCommand(Action<Collection<object>> addAirplanes)
        {
            this.addAirplanes = addAirplanes;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action?.Invoke();
            _actionGroup?.Invoke(parameter);
            addAirplanes?.Invoke(parameter as Collection<object>);
        }
    }
}
