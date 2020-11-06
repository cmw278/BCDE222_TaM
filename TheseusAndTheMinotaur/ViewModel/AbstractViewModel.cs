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

        /// <summary>
        /// Notify all observers that a property has been changed
        /// </summary>
        /// <param name="propertyName">The name of the updated property</param>
        protected void NotifyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Update a referenced variable, but only if the new value is different.
        /// </summary>
        /// <param name="old">A reference to a variable</param>
        /// <param name="_new">The new value to be compared and applied</param>
        /// <returns>A boolean indicating whether the variable was changed.</returns>
        protected bool UpdateValue<T>(ref T old, T _new)
        {
            if (old != null && old.Equals(_new)) return false;
            old = _new;
            return true;
        }

        /// <summary>
        /// Update a target property, but only if the new value is different.<br />
        /// <b>Known limitation:</b> Only works for public properties.
        /// </summary>
        /// <param name="propertyName">The name of the target property</param>
        /// <param name="newValue">The new value to be compared and applied</param>
        /// <returns>A boolean indicating whether the property was changed.</returns>
        protected bool UpdateProperty<T>(string propertyName, T newValue)
        {
            // https://stackoverflow.com/a/1403036
            // See => Reflection
            var property = GetType().GetProperty(propertyName);
            var currentValue = property.GetValue(this);
            
            if (currentValue != null && currentValue.Equals(newValue)) return false;
            property.SetValue(this, newValue);
            return true;
        }
    }
}
