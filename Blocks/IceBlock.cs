using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    public class IceBlock : Block
    {
        #region ===--- Constructor ---===

        public IceBlock(Coordinate position, IField owner,
            ColorSkin color = ColorSkin.White, SkinBlock skin = SkinBlock.Ice,
            char charSkin = ConstantValue.ICE_BLOCK)
            : base(position, color, skin, owner, charSkin)
        {
        }

        #endregion

        #region ===--- Properties ---===

        public override ObjectType KindOfObject
        {
            get
            {
                return ObjectType.IceBlock;
            }
        }

        #endregion
    }
}
