using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20200613_TankLibrary
{
    public interface IMovable
    {
        void Move(ActionPlayer actionPlayer = ActionPlayer.NoAction);
        bool IsPermitMove();
    }
}
