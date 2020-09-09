using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    public interface IDownSkinTank
    {
        char this[int posX, int posY] { get; set; }
    }
}
