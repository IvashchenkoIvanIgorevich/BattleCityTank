using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    [Serializable]
    public class MetalBlock : Block
    {
        #region ===--- Constructor ---===

        public MetalBlock(Coordinate position, IField owner,
            ColorSkin color = ColorSkin.DarkGray, SkinBlock skin = SkinBlock.Metal,
            char charSkin = ConstantValue.METAL_BLOCK)
            : base(position, color, skin, owner, charSkin)
        {
        }

        #endregion

        #region ===--- Properties ---===

        public override ObjectType KindOfObject
        {
            get
            {
                return ObjectType.MetalBlock;
            }
        }

        #endregion
    }
}
