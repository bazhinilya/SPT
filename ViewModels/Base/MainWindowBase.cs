using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SPT.ViewModels.Base
{
    public abstract class MainWindowBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual bool Set<T>(ref T field,T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName); 
            return true;
        }
    }
}
