using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20200613_TankLibrary
{
    [Serializable]
    public class CharacteristicTank
    {
        #region ===--- Dataset ---===

        public SkinTank Skin { get; }
        public short HP { get; internal set; }
        public short MS { get; }
        public short AtckSp { get; }
        public int AtckRng { get; }
        public short AtckDmg { get; }

        #endregion

        #region ===--- Constructor ---===

        public CharacteristicTank(short hp, short moveSpeed, short atackSpeed, int atackRange,
            short atackDamage, SkinTank skin)
        {
            HP = hp;
            MS = moveSpeed;
            AtckSp = atackSpeed;
            AtckRng = atackRange;
            AtckDmg = atackDamage;
            Skin = skin;
        }

        #endregion

        #region ===--- Properites ---===        

        #endregion

        #region ===--- Methods ---===

        #endregion
    }
}
