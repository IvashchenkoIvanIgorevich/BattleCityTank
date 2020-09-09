using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20200613_TankLibrary
{
    public class CharacterTank
    {
        #region ===--- Dataset ---===

        public SkinTank Skin { get; }
        public int HP { get; set; }
        public int MS { get; }
        public int AtckSp { get; }
        public int AtckRng { get; }
        public int AtckDmg { get; }

        #endregion

        #region ===--- Constructor ---===

        public CharacterTank(int hp, int moveSpeed, int atackSpeed, int atackRange,
            int atackDamage, SkinTank skin)
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
