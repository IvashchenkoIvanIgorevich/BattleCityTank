using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    public class Coordinate  //not struct because Equals and GetHashCode
    {
        #region ===--- Date ---===

        public int PosX { get; set; }    // row 
        public int PosY { get; set; }    // col

        #endregion

        #region ===--- Constructors ---===

        public Coordinate()
        {
            PosX = 0;
            PosY = 0;
        }

        public Coordinate(int x, int y, int stepXY = 1)
        {
            PosX = x;
            PosY = y;
        }

        public Coordinate(Coordinate copyCoordinate)
        {
            PosX = copyCoordinate.PosX;
            PosY = copyCoordinate.PosY;
        }

        #endregion

        #region ===--- Methods ---===

        public int GetSquareToObj(int posObjX = ConstantValue.POS_ROW_BASE, int posObjY = ConstantValue.POS_COL_BASE)
        {
            int lineX = Math.Abs(posObjX - PosX);
            int lineY = Math.Abs(posObjY - PosY);

            return lineX * lineY;
        }

        public bool Move(Direction moveDirect, int step = 1)   
        {
            bool isPermit = false;

            switch (moveDirect)
            {
                case Direction.Right:
                    if ((PosY + step) < ConstantValue.WIDTH_GAMEFIELD)
                    {
                        isPermit = true;
                        PosY += step;
                    }
                    break;
                case Direction.Left:
                    if ((PosY - step) > 0)
                    {
                        isPermit = true;
                        PosY -= step;
                    }
                    break;
                case Direction.Up:
                    if ((PosX - step) > 0)
                    {
                        isPermit = true;
                        PosX -= step;
                    }
                    break;
                case Direction.Down:
                    if ((PosX + step) < ConstantValue.HEIGHT_GAMEFIELD)
                    {
                        isPermit = true;
                        PosX += step;
                    }
                    break;
            }

            return isPermit;
        }

        public bool IsPermitMoveCoordinate(Direction moveDirect)
        {
            bool isPermit = false;

            switch (moveDirect)
            {
                case Direction.Right:
                    if (PosY < ConstantValue.WIDTH_GAMEFIELD - 1)
                    {
                        isPermit = true;
                    }
                    break;
                case Direction.Left:
                    if (PosY > 0)
                    {
                        isPermit = true;
                    }
                    break;
                case Direction.Up:
                    if (PosX > 0)
                    {
                        isPermit = true;
                    }
                    break;
                case Direction.Down:
                    if (PosX < ConstantValue.HEIGHT_GAMEFIELD - 1)
                    {
                        isPermit = true;
                    }
                    break;
            }

            return isPermit;
        }

        public int GetDeltaWidth(Coordinate checkCoordinate)
        {
            return Math.Abs(PosY - checkCoordinate.PosY);
        }

        public int GetDeltaHeight(Coordinate checkCoordinate)
        {
            return Math.Abs(PosX - checkCoordinate.PosX);
        }

        #endregion

        #region ===--- Ovveride Methods ---===

        public override int GetHashCode()
        {
            return (PosX | (PosY << 8));   
        }

        public override bool Equals(object obj)
        {
            Coordinate otherObj = obj as Coordinate;

            if (otherObj == null)
            {
                return false;
            }

            return (PosX == otherObj.PosX) && (PosY == otherObj.PosY);
        }

        #endregion
    }
}
