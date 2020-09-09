using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    public interface IUpSkinTank
    {
        char this[int posX, int posY] { get; set; }
    }
}
