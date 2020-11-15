using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TheseusAndTheMinotaur
{
    public class CharacterViewBinding
    {
        protected LevelPlayer LevelView;
        protected Border ViewElement;
        protected AbstractMutableMazeImage Character;

        public CharacterViewBinding(LevelPlayer levelView, Border viewElement, AbstractMutableMazeImage character)
        {
            LevelView = levelView;
            ViewElement = viewElement;
            Character = character;
            Character.PropertyChanged += Character_PropertyChanged;
        }

        protected void Character_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Row":
                case "Column":
                    OnCharacterMoved();
                    return;
                default:
                    return;
            }
        }

        protected void OnCharacterMoved()
        {
            LevelView.SetCanvasPosition(Character, ViewElement);
        }
    }
}
