using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    [Serializable]
    public class BrickBlock : Block
    {
        #region ===--- Constructor ---===

        public BrickBlock(Coordinate position, IField owner,
            ColorSkin color = ColorSkin.Gray, SkinBlock skin = SkinBlock.Brick,
            char charSkin = ConstantValue.BRICK_BLOCK)
            : base(position, color, skin, owner, charSkin)
        {
        }

        #endregion

        #region ===--- Properties ---===

        public override ObjectType KindOfObject
        {
            get
            {
                return ObjectType.BrickBlock;
            }
        }

        #endregion
    }
}
