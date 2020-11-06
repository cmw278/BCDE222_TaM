using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;

namespace TheseusAndTheMinotaur
{
    public abstract class AbstractImmutableMazeImage : AbstractViewModel
    {
        #region Flyweight stuff
        private static readonly string BackgroundTile;
        private static readonly string ExitTile;
        private static readonly string BottomWall;
        private static readonly string LeftWall;
        private static readonly string RightWall;
        private static readonly string TopWall;
        private static readonly string Theseus;
        private static readonly string Minotaur;

        static AbstractImmutableMazeImage()
        {
            BackgroundTile =    "[    empty    ]";
            ExitTile =          "[    exit     ]";
            BottomWall =        "[ wall_bottom ]";
            LeftWall =          "[wall_left    ]";
            RightWall =         "[   wall_right]";
            TopWall =           "[ wall_top    ]";
            Theseus =           "[     theseus ]";
            Minotaur =          "[ minotaur    ]";
        }

        protected string _Image;

        private void SetImage(AbstractImmutableMazeImage target)
        {
            switch (target)
            {
                case EmptyMazeImage image:
                    image._Image = BackgroundTile;
                    break;
                case ExitMazeImage image:
                    image._Image = ExitTile;
                    break;
                case BottomWallMazeImage image:
                    image._Image = BottomWall;
                    break;
                case LeftWallMazeImage image:
                    image._Image = LeftWall;
                    break;
                case RightWallMazeImage image:
                    image._Image = RightWall;
                    break;
                case TopWallMazeImage image:
                    image._Image = TopWall;
                    break;
                case TheseusMazeImage image:
                    image._Image = Theseus;
                    break;
                case MinotaurMazeImage image:
                    image._Image = Minotaur;
                    break;
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
        public string Image { get { return _Image; } }

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
