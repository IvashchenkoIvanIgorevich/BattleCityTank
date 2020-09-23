using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    [Serializable]
    public class Block : GameObject
    {
        #region ===--- Dataset ---===

        protected SkinBlock _skin;
        protected char _charSkin;

        #endregion

        #region ===--- Constructors ---===

        public Block(Coordinate position, ColorSkin color, SkinBlock skin,
            IField owner, char viewSkin = ConstantValue.BLOCK)
            : base(position, color,  owner)
        {
            _skin = skin;
            _charSkin = viewSkin;
        }

        public Block(Block copyBlock)
            : this(copyBlock.Position, copyBlock.Color,
                 copyBlock.Skin, copyBlock._owner, copyBlock._charSkin)
        {

        }

        #endregion

        #region ===--- Properties ---===

        public override ObjectType KindOfObject
        {
            get
            {
                return ObjectType.Block;
            }
        }

        public SkinBlock Skin
        {
            get
            {
                return _skin;
            }
        }

        #endregion

        #region ===--- Methods ---===

        public virtual void CreateBlock()
        {
            for (int i = Position.PosX; i < ConstantValue.HEIGHT_BLOCK + Position.PosX; i++)
            {
                for (int j = Position.PosY; j < ConstantValue.WIDTH_BLOCK + Position.PosY; j++)
                {
                    _owner[new Coordinate(i,j)] = this;
                }
            }
        }

        public bool IsGrassBlock()
        {
            return _owner.IsContain(Position) && _owner[Position] is GrassBlock; 
        }

        public bool IsBrickBlock()
        {
            return _owner.IsContain(Position) && _owner[Position] is BrickBlock;
        }

        #endregion
    }
}
