using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    [Serializable]
    public class Base : GameObject
    {
        #region ===--- Dataset ---===
        
        public bool BaseDead { get; internal set; } = false;

        #endregion

        #region ===--- Constructor ---===

        public Base(Coordinate position, ColorSkin color, IField owner)
            : base(position, color, owner)
        {
        }

        #endregion

        #region ===--- Properties ---===

        public override ObjectType KindOfObject
        {
            get
            {
                return ObjectType.Base;
            }
        }

        #endregion

        #region ===--- Methods ---===

        public void CreateBase()
        {
            for (int row = Position.PosX; row < ConstantValue.HEIGHT_BASE + Position.PosX; row++)
            {
                for (int col = Position.PosY; col < ConstantValue.WIDTH_BASE + Position.PosY; col++)
                {
                    _owner[new Coordinate(row, col)] = this;
                }
            }
        }

        #endregion
    }
}
