using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    [Serializable]
    public class PlayerTank : Tank
    {
        public PlayerTank(CharacterTank character, Direction direction,
            Coordinate coordinate, ColorSkin skin, IField gameField)
            : base(character, direction, coordinate, skin, gameField)
        {
        }

        public override ObjectType KindOfObject
        {
            get
            {
                return ObjectType.PlayerTank;
            }
        }
    }
}
