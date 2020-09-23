using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    [Serializable]
    public class GrassBlock : Block
    {
        #region ===--- Constructor ---===

        public GrassBlock(Coordinate position, IField owner,
            ColorSkin color = ColorSkin.Green, SkinBlock skin = SkinBlock.Grass,
            char charSkin = ConstantValue.GRASS_BLOCK)
            : base(position, color, skin, owner, charSkin)
        {
        }

        #endregion

        #region ===--- Properties ---===

        public override ObjectType KindOfObject
        {
            get
            {
                return ObjectType.GrassBlock;
            }
        }

        #endregion
    }
}
