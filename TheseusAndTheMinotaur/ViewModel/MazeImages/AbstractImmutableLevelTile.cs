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
    public abstract class AbstractImmutableLevelTile : AbstractViewModel
    {
        #region Flyweight stuff
        private static readonly BitmapImage Theseus;
        private static readonly BitmapImage Minotaur;
        private static readonly BitmapImage Exit;

        static AbstractImmutableLevelTile()
        {
            var baseUri = new Uri("ms-appx:///", UriKind.RelativeOrAbsolute);
            Theseus = new BitmapImage(new Uri(baseUri, "Assets/theseus_.png"));
            Minotaur = new BitmapImage(new Uri(baseUri, "Assets/minotaur_.png"));
            Exit = new BitmapImage(new Uri(baseUri, "Assets/exit_.png"));
        }

        protected BitmapImage _Image;

        private void SetImage(AbstractImmutableLevelTile target)
        {
            switch (target)
            {
                case EmptyLevelTile _:
                case BottomWallLevelTile _:
                case LeftWallLevelTile _:
                case RightWallLevelTile _:
                case TopWallLevelTile _:
                    return;
                case ExitLevelTile tile:
                    tile._Image = Exit;
                    return;
                case TheseusLevelTile tile:
                    tile._Image = Theseus;
                    return;
                case MinotaurLevelTile tile:
                    tile._Image = Minotaur;
                    return;
                default:
                    throw new NotSupportedException($"{nameof(target)} is not supported at this time");
            }
        }
        #endregion


        #region Public properties
        public BitmapImage Image { get { return _Image; } }

        protected int _Row;
        /// <summary>
        /// The horizontal position of this MazeImage
        /// </summary>
        public virtual int Row
        {
            get { return _Row; }
            set { throw new FieldAccessException($"Unable to assign {this.GetType()}.Row"); }
        }

        protected int _Column;
        /// <summary>
        /// The vertical position of this MazeImage
        /// </summary>
        public virtual int Column
        {
            get { return _Column; }
            set { throw new FieldAccessException($"Unable to assign {this.GetType()}.Column"); }
        }
        #endregion

        public AbstractImmutableLevelTile(int row, int column)
        {
            _Row = row;
            _Column = column;
            SetImage(this);
        }
    }
}
