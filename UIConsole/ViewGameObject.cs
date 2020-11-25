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
        public ViewBase viewGameBase = new ViewBase(ConstantValue.PATH_FILE_BASE);

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

        public char GetViewBlock(SkinBlock skin)
        {
            char resultView = ' ';

            switch (skin)
            {
                case SkinBlock.Brick:
                    resultView = ConstantValue.BRICK_BLOCK;
                    break;
                case SkinBlock.Metal:
                    resultView = ConstantValue.METAL_BLOCK;
                    break;
                case SkinBlock.Grass:
                    resultView = ConstantValue.GRASS_BLOCK;
                    break;
                case SkinBlock.Ice:
                    resultView = ConstantValue.ICE_BLOCK;
                    break;
            }

            return resultView;
        }

        #region ===--- CreateViewBase ---===

        public char[,] GetViewBase()
        {
            return viewGameBase.ViewGameBase;
        }

        #endregion          
    }
}
