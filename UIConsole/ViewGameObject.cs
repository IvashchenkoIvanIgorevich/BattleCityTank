using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using CommonLib;
using _20200613_TankLibrary;

namespace UIConsole
{
    public class ViewGameObject
    {
        #region ===--- Data ---===

        public ViewTank lightTank = new ViewTank(ConstantValue.PATH_FILE_LIGHTTANK);
        public ViewTank heavyTank = new ViewTank(ConstantValue.PATH_FILE_HEAVYTANK);
        public ViewTank destroyTank = new ViewTank(ConstantValue.PATH_FILE_DESTROYTANK);

        #endregion

        #region ===--- Constructors ---===

        public ViewGameObject()
        {
        }

        #endregion

        #region ===--- Indexator get char Tank ---===

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
                                resultView = lightTank.ViewRight;
                                break;
                            case Direction.Left:
                                resultView = lightTank.ViewLeft;
                                break;
                            case Direction.Up:
                                resultView = lightTank.ViewUp;
                                break;
                            case Direction.Down:
                                resultView = lightTank.ViewDown;
                                break;
                        }
                        break;
                    case SkinTank.Heavy:
                        switch (direction)
                        {
                            case Direction.Right:
                                resultView = heavyTank.ViewRight;
                                break;
                            case Direction.Left:
                                resultView = heavyTank.ViewLeft;
                                break;
                            case Direction.Up:
                                resultView = heavyTank.ViewUp;
                                break;
                            case Direction.Down:
                                resultView = heavyTank.ViewDown;
                                break;
                        }
                        break;
                    case SkinTank.Destroy:
                        switch (direction)
                        {
                            case Direction.Right:
                                resultView = destroyTank.ViewRight;
                                break;
                            case Direction.Left:
                                resultView = destroyTank.ViewLeft;
                                break;
                            case Direction.Up:
                                resultView = destroyTank.ViewUp;
                                break;
                            case Direction.Down:
                                resultView = destroyTank.ViewDown;
                                break;
                        }
                        break;

                }

                return resultView;
            }
        }

        #endregion       

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
    }
}
