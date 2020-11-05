using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    public abstract class AbstractViewModel : INotifyPropertyChanged
    {
        // https://docs.microsoft.com/en-us/windows/uwp/data-binding/data-binding-in-depth

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void NotifyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool UpdateValue<T>(ref T old, T _new)
        {
            if (old.Equals(_new)) return false;
            old = _new;
            return true;
        }
    }
}
