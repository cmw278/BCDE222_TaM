using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TheseusAndTheMinotaur
{
    public abstract class AbstractImmutableMazeImage : AbstractViewModel
    {
        #region Flyweight stuff
        private static readonly BitmapImage Theseus;
        private static readonly BitmapImage Minotaur;
        private static readonly BitmapImage Exit;

        static AbstractImmutableMazeImage()
        {
            var baseUri = new Uri("ms-appx:///", UriKind.RelativeOrAbsolute);
            Theseus = new BitmapImage(new Uri(baseUri, "Assets/theseus_.png"));
            Minotaur = new BitmapImage(new Uri(baseUri, "Assets/minotaur_.png"));
            Exit = new BitmapImage(new Uri(baseUri, "Assets/exit_.png"));
        }

        protected BitmapImage _Image;

        private void SetImage(AbstractImmutableMazeImage target)
        {
            switch (target)
            {
                case EmptyMazeImage _:
                case BottomWallMazeImage _:
                case LeftWallMazeImage _:
                case RightWallMazeImage _:
                case TopWallMazeImage _:
                    return;
                case ExitMazeImage image:
                    image._Image = Exit;
                    return;
                case TheseusMazeImage image:
                    image._Image = Theseus;
                    return;
                case MinotaurMazeImage image:
                    image._Image = Minotaur;
                    return;
                default:
                    throw new NotSupportedException($"{nameof(target)} is not supported at this time");
            }
        }
        #endregion

        protected int _Row;
        protected int _Column;

        public AbstractImmutableMazeImage(int row, int column)
        {
            _Row = row;
            _Column = column;
            SetImage(this);
        }

        #region Public properties
        public BitmapImage Image { get { return _Image; } }

        /// <summary>
        /// The horizontal position of this MazeImage
        /// </summary>
        public virtual int Row
        {
            get { return _Row; }
            set { throw new FieldAccessException($"Unable to assign {this.GetType()}.Row"); }
        }

        /// <summary>
        /// The vertical position of this MazeImage
        /// </summary>
        public virtual int Column
        {
            get { return _Column; }
            set { throw new FieldAccessException($"Unable to assign {this.GetType()}.Column"); }
        }
        #endregion
    }
}
