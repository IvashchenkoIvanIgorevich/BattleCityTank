using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;
using _20200613_TankLibrary;

namespace UIConsole
{
    public class ViewGameObject
    {
        #region ===--- Data ---===

        public char[,] TankRightDestroy;
        public char[,] TankLeftDestroy;
        public char[,] TankDownDestroy;
        public char[,] TankUpDestroy;
        public char[,] TankRightHeavy;
        public char[,] TankLeftHeavy;
        public char[,] TankDownHeavy;
        public char[,] TankUpHeavy;
        public char[,] TankRightLight;
        public char[,] TankLeftLight;
        public char[,] TankDownLight;
        public char[,] TankUpLight;

        #endregion

        #region ===--- Constructors ---===

        public ViewGameObject()
        {
            TankRightDestroy = CreateRightSkin(SkinTank.Destroy);
            TankLeftDestroy = CreateLeftSkin(SkinTank.Destroy);
            TankUpDestroy = CreateUpSkin(SkinTank.Destroy);
            TankDownDestroy = CreateDownSkin(SkinTank.Destroy);

            TankRightHeavy = CreateRightSkin(SkinTank.Heavy);
            TankLeftHeavy = CreateLeftSkin(SkinTank.Heavy);
            TankUpHeavy = CreateUpSkin(SkinTank.Heavy);
            TankDownHeavy = CreateDownSkin(SkinTank.Heavy);

            TankRightLight = CreateRightSkin(SkinTank.Light);
            TankLeftLight = CreateLeftSkin(SkinTank.Light);
            TankUpLight = CreateUpSkin(SkinTank.Light);
            TankDownLight = CreateDownSkin(SkinTank.Light);
        }

        #endregion

        public char[,] this[SkinTank skin, Direction direction]
        {
            get
            {
                char[,] resultView = null;

                switch (skin)
                {
                    case SkinTank.Light:
                        switch (direction)
                        {
                            case Direction.Right:
                                resultView = TankRightLight;
                                break;
                            case Direction.Left:
                                resultView = TankLeftLight;
                                break;
                            case Direction.Up:
                                resultView = TankUpLight;
                                break;
                            case Direction.Down:
                                resultView = TankDownLight;
                                break;
                        }
                        break;
                    case SkinTank.Heavy:
                        switch (direction)
                        {
                            case Direction.Right:
                                resultView = TankRightHeavy;
                                break;
                            case Direction.Left:
                                resultView = TankLeftHeavy;
                                break;
                            case Direction.Up:
                                resultView = TankUpHeavy;
                                break;
                            case Direction.Down:
                                resultView = TankDownHeavy;
                                break;
                        }
                        break;
                    case SkinTank.Destroy:
                        switch (direction)
                        {
                            case Direction.Right:
                                resultView = TankRightDestroy;
                                break;
                            case Direction.Left:
                                resultView = TankLeftDestroy;
                                break;
                            case Direction.Up:
                                resultView = TankUpDestroy;
                                break;
                            case Direction.Down:
                                resultView = TankDownDestroy;
                                break;
                        }
                        break;

                }

                return resultView;
            }
        }

        #region ===--- CreateViewBase ---===

        public char[,] GetViewBase(int height = ConstantValue.HEIGHT_BASE,
           int width = ConstantValue.WIDTH_BASE)
        {
            char[,] viewBase = new char[height, width];

            for (int row = 0; row < viewBase.GetLength(0); row++)
            {
                for (int col = 0; col < viewBase.GetLength(1); col++)
                {
                    if (((row == 0) || (row == 1) || (row == 4))
                        && ((col == 0) || (col == 2) || (col == 4))
                        || (row == 2))
                    {
                        viewBase[row, col] = '*';
                    }

                    if ((row == 3) && ((col == 1)
                        || (col == 2) || (col == 3)))
                    {
                        viewBase[row, col] = '*';
                    }
                }
            }

            return viewBase;
        }

        #endregion  

        #region ===--- CreateViewTank ---===

        public char[,] CreateLeftSkin(SkinTank skin)
        {
            char[,] viewTankSkin = new char[ConstantValue.HEIGHT_TANK, ConstantValue.WIDTH_TANK];

            switch (skin)
            {
                case SkinTank.Light:
                    GetViewLeftTank(viewTankSkin,
                        ' ', ' ', '\x25A0', '\x2500', '\x2500', '\x2588', '*');
                    break;
                case SkinTank.Heavy:
                    GetViewLeftTank(viewTankSkin,
                        '\x2559', '\x2556', '\x25A0', '\x2500', '\x2588', '\x2588', '\x2588');
                    break;
                case SkinTank.Destroy:
                    GetViewLeftTank(viewTankSkin,
                        '#', '#', '\x25A0', '\x2500', '\x2500', '\x2588', '\x2588');
                    break;
                case SkinTank.NoSkin:
                    break;
            }

            return viewTankSkin;
        }

        public char[,] CreateRightSkin(SkinTank skin)
        {
            char[,] viewTankSkin = new char[ConstantValue.HEIGHT_TANK, ConstantValue.WIDTH_TANK];

            switch (skin)
            {
                case SkinTank.Light:
                    GetViewRightTank(viewTankSkin,
                        ' ', ' ', '\x25A0', '\x2500', '\x2500', '\x2588', '*');
                    break;
                case SkinTank.Heavy:
                    GetViewRightTank(viewTankSkin,
                        '\x2559', '\x2556', '\x25A0', '\x2500', '\x2588', '\x2588', '\x2588');
                    break;
                case SkinTank.Destroy:
                    GetViewRightTank(viewTankSkin,
                        '#', '#', '\x25A0', '\x2500', '\x2500', '\x2588', '\x2588');
                    break;
                case SkinTank.NoSkin:
                    break;
            }

            return viewTankSkin;
        }

        public char[,] CreateUpSkin(SkinTank skin)
        {
            char[,] viewTankSkin = new char[ConstantValue.HEIGHT_TANK, ConstantValue.WIDTH_TANK];

            switch (skin)
            {
                case SkinTank.Light:
                    GetViewUpTank(viewTankSkin,
                        ' ', ' ', '\x25A0', '\x2502', '\x2502', '\x2588', '*');
                    break;
                case SkinTank.Heavy:
                    GetViewUpTank(viewTankSkin,
                        '\x255A', '\x255D', '\x25A0', '\x2502', '\x2588', '\x2588', '\x2588');
                    break;
                case SkinTank.Destroy:
                    GetViewUpTank(viewTankSkin,
                        '#', '#', '\x25A0', '\x2502', '\x2502', '\x2588', '\x2588');
                    break;
                case SkinTank.NoSkin:
                    break;
            }

            return viewTankSkin;
        }

        public char[,] CreateDownSkin(SkinTank skin)
        {
            char[,] viewTankSkin = new char[ConstantValue.HEIGHT_TANK, ConstantValue.WIDTH_TANK];

            switch (skin)
            {
                case SkinTank.Light:
                    GetViewDownTank(viewTankSkin,
                        ' ', ' ', '\x25A0', '\x2502', '\x2502', '\x2588', '*');
                    break;
                case SkinTank.Heavy:
                    GetViewDownTank(viewTankSkin,
                        '\x255A', '\x255D', '\x25A0', '\x2502', '\x2588', '\x2588', '\x2588');
                    break;
                case SkinTank.Destroy:
                    GetViewDownTank(viewTankSkin,
                        '#', '#', '\x25A0', '\x2502', '\x2502', '\x2588', '\x2588');
                    break;
                case SkinTank.NoSkin:
                    break;
            }

            return viewTankSkin;
        }

        public char[,] GetViewLeftTank(char[,] leftAndRightView,
            char leftHarp, char RightHarp, char tankHatch, char tankBarrel, char befforBarrel,
            char tankLining, char elseLining)
        {
            for (int row = 0; row < leftAndRightView.GetLength(0); row++)
            {
                for (int col = 0; col < leftAndRightView.GetLength(1); col++)
                {
                    if (row == 0)
                    {
                        leftAndRightView[row, col] = leftHarp;
                    }
                    if (row == leftAndRightView.GetLength(0) - 1)
                    {
                        leftAndRightView[row, col] = RightHarp;
                    }
                    if ((row == 1) || (row == leftAndRightView.GetLength(0) - 2))
                    {
                        leftAndRightView[row, col] = elseLining;
                    }
                    if (row == 2)
                    {
                        if (col == (leftAndRightView.GetLength(1) - 1)
                            || (col == leftAndRightView.GetLength(1) - 2))
                        {
                            leftAndRightView[row, col] = tankLining;
                        }
                        if (row == col)
                        {
                            leftAndRightView[row, col] = tankHatch;
                        }
                        if (col == 0)
                        {
                            leftAndRightView[row, col] = tankBarrel;
                        }
                        if (col == 1)
                        {
                            leftAndRightView[row, col] = befforBarrel;
                        }
                    }
                }
            }

            return leftAndRightView;
        }

        public char[,] GetViewRightTank(char[,] leftAndRightView,
            char leftHarp, char RightHarp, char tankHatch, char tankBarrel, char befforBarrel,
            char tankLining, char elseLining)
        {
            for (int row = 0; row < leftAndRightView.GetLength(0); row++)
            {
                for (int col = 0; col < leftAndRightView.GetLength(1); col++)
                {
                    if (row == 0)
                    {
                        leftAndRightView[row, col] = leftHarp;
                    }
                    if (row == leftAndRightView.GetLength(0) - 1)
                    {
                        leftAndRightView[row, col] = RightHarp;
                    }
                    if ((row == 1) || (row == leftAndRightView.GetLength(0) - 2))
                    {
                        leftAndRightView[row, col] = elseLining;
                    }
                    if (row == 2)
                    {
                        if ((col == 0) || (col == 1))
                        {
                            leftAndRightView[row, col] = tankLining;
                        }
                        if (row == col)
                        {
                            leftAndRightView[row, col] = tankHatch;
                        }
                        if (col == (leftAndRightView.GetLength(1) - 1))
                        {
                            leftAndRightView[row, col] = tankBarrel;
                        }
                        if ((col == leftAndRightView.GetLength(1) - 2))
                        {
                            leftAndRightView[row, col] = befforBarrel;
                        }
                    }
                }
            }

            return leftAndRightView;
        }

        public char[,] GetViewDownTank(char[,] upAndDownView,
            char leftHarp, char RightHarp, char tankHatch, char tankBarrel, char befforBarrel,
            char tankLining, char elseLining)
        {
            for (int row = 0; row < upAndDownView.GetLength(0); row++)
            {
                for (int col = 0; col < upAndDownView.GetLength(1); col++)
                {
                    if (col == 0)
                    {
                        upAndDownView[row, col] = leftHarp;
                    }
                    if (col == upAndDownView.GetLength(1) - 1)
                    {
                        upAndDownView[row, col] = RightHarp;
                    }
                    if ((col == 1) || (col == upAndDownView.GetLength(0) - 2))
                    {
                        upAndDownView[row, col] = elseLining;
                    }
                    if (col == 2)
                    {
                        if ((row == 0) || (row == 1))
                        {
                            upAndDownView[row, col] = tankLining;
                        }
                        if (row == col)
                        {
                            upAndDownView[row, col] = tankHatch;
                        }
                        if (row == (upAndDownView.GetLength(1) - 1))
                        {
                            upAndDownView[row, col] = tankBarrel;
                        }
                        if ((row == upAndDownView.GetLength(1) - 2))
                        {
                            upAndDownView[row, col] = befforBarrel;
                        }
                    }
                }
            }

            return upAndDownView;
        }

        public char[,] GetViewUpTank(char[,] upAndDownView,
            char leftHarp, char RightHarp, char tankHatch, char tankBarrel, char befforBarrel,
            char tankLining, char elseLining)
        {
            for (int row = 0; row < upAndDownView.GetLength(0); row++)
            {
                for (int col = 0; col < upAndDownView.GetLength(1); col++)
                {
                    if (col == 0)
                    {
                        upAndDownView[row, col] = leftHarp;
                    }
                    if (col == upAndDownView.GetLength(1) - 1)
                    {
                        upAndDownView[row, col] = RightHarp;
                    }
                    if ((col == 1) || (col == upAndDownView.GetLength(0) - 2))
                    {
                        upAndDownView[row, col] = elseLining;
                    }
                    if (col == 2)
                    {
                        if (row == (upAndDownView.GetLength(1) - 1)
                            || (row == upAndDownView.GetLength(1) - 2))
                        {
                            upAndDownView[row, col] = tankLining;
                        }
                        if (row == col)
                        {
                            upAndDownView[row, col] = tankHatch;
                        }
                        if (row == 0)
                        {
                            upAndDownView[row, col] = tankBarrel;
                        }
                        if (row == 1)
                        {
                            upAndDownView[row, col] = befforBarrel;
                        }
                    }
                }
            }

            return upAndDownView;
        }

        #endregion
    }
}
