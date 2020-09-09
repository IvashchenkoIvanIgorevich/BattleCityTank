using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _20200613_TankLibrary;
using CommonLib;

namespace UIConsole
{
    class UIController
    {
        private GameField _gameField;
        private ViewGameObject _viewObj = new ViewGameObject();

        public UIController(GameField field)
        {
            _gameField = field;
        }

        public ActionPlayer GetActionPlayer(ConsoleKey pressKey)
        {
            ActionPlayer pressPlayer = ActionPlayer.NoAction;

            switch (pressKey)
            {
                case ConsoleKey.LeftArrow:
                    pressPlayer = ActionPlayer.PressLeft;
                    break;
                case ConsoleKey.UpArrow:
                    pressPlayer = ActionPlayer.PressUp;
                    break;
                case ConsoleKey.RightArrow:
                    pressPlayer = ActionPlayer.PressRight;
                    break;
                case ConsoleKey.DownArrow:
                    pressPlayer = ActionPlayer.PressDown;
                    break;
                case ConsoleKey.Spacebar:
                    pressPlayer = ActionPlayer.PressFire;
                    break;
                case ConsoleKey.Escape:
                    pressPlayer = ActionPlayer.PressExit;
                    break;
                case ConsoleKey.P:
                    pressPlayer = ActionPlayer.PressPause;
                    break;
                case ConsoleKey.Enter:
                    pressPlayer = ActionPlayer.PressEnter;
                    break;
            }

            return pressPlayer;
        }

        public void MovePlayer(ActionPlayer action)
        {
            if (_gameField.Player != null)
            {
                _gameField.Player.Move(action);
            }
        }

        public void MoveEnemy(ActionPlayer[] actionEnemies)
        {
            if (_gameField.EnemyTanks.Count() != 0)
            {
                _gameField.MoveEnemies(actionEnemies);
            }
        }

        public char this[int row, int col]
        {
            get
            {
                char resultSymbol = ' ';

                Coordinate tempCoor = new Coordinate();

                tempCoor.PosX = row;
                tempCoor.PosY = col;

                if (_gameField.IsContain(tempCoor))
                {
                    int objRow = Math.Abs(_gameField[new Coordinate(row, col)].Position.PosX - row);
                    int objCol = Math.Abs(_gameField[new Coordinate(row, col)].Position.PosY - col);

                    switch (_gameField[tempCoor].KindOfObject)
                    {
                        case ObjectType.PlayerTank:
                            resultSymbol =_viewObj[((PlayerTank)_gameField[tempCoor]).CharacterTank.Skin,
                                ((PlayerTank)_gameField[tempCoor]).DirectionTank][objRow, objCol];
                            break;
                        case ObjectType.EnemyTank:
                            resultSymbol = _viewObj[((EnemyTank)_gameField[tempCoor]).CharacterTank.Skin,
                                ((EnemyTank)_gameField[tempCoor]).DirectionTank][objRow, objCol];
                            break;
                        case ObjectType.Base:
                            //tempView[row, col] = ViewGameObject.GetViewBase()[objRow, objCol];
                            break;
                        case ObjectType.BrickBlock:                          
                            break;
                        case ObjectType.GrassBlock:
                            break;
                        case ObjectType.IceBlock:
                            break;
                        case ObjectType.MetalBlock:
                            break;
                        default:
                            break;
                    }
                }

                return resultSymbol;
            }
        }
    }
}
