using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20200613_TankLibrary
{
    public interface IField
    {
        GameObject this[Coordinate objCoordinate] { get; set; }

        void DeleteGameObj(Coordinate coordinateDelete);

        void AddGameObj(Coordinate objCoor, GameObject objAdd);

        bool IsContain(Coordinate coordinateContain);
    }
}
