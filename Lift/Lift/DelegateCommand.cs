using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lift
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> m_Execute;
        private readonly Func<object, bool> m_CanExecute;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute)
            : this(execute, o => true)
        { /* empty */ }

        public DelegateCommand(Action<object> execute, Func<object, bool> canexecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            m_Execute = execute;
            m_CanExecute = canexecute;
        }

        public bool CanExecute(object p)
        {
            return m_CanExecute == null || m_CanExecute(p);
        }

        public void Execute(object p)
        {
            if (CanExecute(p))
                m_Execute(p);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
