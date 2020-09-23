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

        bool IsContain(Coordinate coordinateContain);

        void DeleteBullet(Bullet delBullet);    // delete bullet from List<Bullet> in class Bullet

        void AddBullet(Bullet addBullet);
    }
}
