using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaM;

namespace TheseusAndTheMinotaur
{
    public abstract class AbstractMutableMazeImage : AbstractImmutableMazeImage
    {
        public AbstractMutableMazeImage() : base(0, 0) { }

        public void MoveTo(int row, int column)
        {
            Row = row;
            Column = column;
        }

        #region Public Properties
        /// <summary>
        /// The horizontal position of this MazeImage
        /// </summary>
        public override int Row
        {
            get => _Row;
            set { if (UpdateValue(ref _Row, value)) NotifyChange(); }
        }

        /// <summary>
        /// The vertical position of this MazeImage
        /// </summary>
        public override int Column
        {
            get => _Column;
            set { if (UpdateValue(ref _Column, value)) NotifyChange(); }
        }
        #endregion
    }
}
