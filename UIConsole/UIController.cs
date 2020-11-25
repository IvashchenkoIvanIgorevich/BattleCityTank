using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

using _20200613_TankLibrary;
using CommonLib;

namespace UIConsole
{
    class UIController    // Publisher
    {
        private UISound _uiSound = new UISound();
        private GameField _gameField;
        private ViewGameObject _viewObj = new ViewGameObject();

        public UIController(GameField field)
        {
            _gameField = field;
        }

        public GameField SetGameField
        {
            set
            {
                _gameField = value;
                _gameField.Player.Moved += _uiSound.MovePlayerSound;
                _gameField.Player.Shooted += _uiSound.ShootPlayerSound;
                _gameField.EnemyDead += _uiSound.DeadEnemySound;
                _gameField.GameOver += _uiSound.DeadPlayerSound;
            }
        }

        public void StartMainMusic()
        {
            _uiSound.StartGameSound();
            Thread.Sleep(2000);
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
                case ConsoleKey.S:
                    pressPlayer = ActionPlayer.PressSave;
                    break;
                case ConsoleKey.L:
                    pressPlayer = ActionPlayer.PressLoad;
                    break;
                case ConsoleKey.Enter:
                    pressPlayer = ActionPlayer.PressEnter;
                    break;
            }

            return pressPlayer;
        }

        public void MovePlayer(ActionPlayer action)
        {
            try
            {
                if (_gameField.Player != null)
                {
                    _gameField.Player.Move(action);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(ConstantValue.PATH_LOG, ex.Message + Environment.NewLine);
            }
        }

        public void MoveEnemy(ActionPlayer[] actionEnemies)
        {
            try
            {
                if (_gameField.EnemyTanks.Count() != 0)
                {
                    _gameField.MoveEnemies(actionEnemies);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(ConstantValue.PATH_LOG, ex.Message + Environment.NewLine);
            }
        }

        public void ShotPlayer(ulong gameTime)
        {
            try
            {
                _gameField.AddPlayerBullet(gameTime);
            }
            catch (Exception ex)
            {
                File.AppendAllText(ConstantValue.PATH_LOG, ex.Message + Environment.NewLine);
            }
        }

        public void ShotEnemies()
        {
            try
            {
                _gameField.AddEnemiesBullet();
            }
            catch (Exception ex)
            {
                File.AppendAllText(ConstantValue.PATH_LOG, ex.Message + Environment.NewLine);
            }
        }

        public void MoveBullets()
        {
            try
            {
                _gameField.MoveBullets();
            }
            catch (Exception ex)
            {
                File.AppendAllText(ConstantValue.PATH_LOG, ex.Message + Environment.NewLine);
            }
        }

        public void SaveGame()
        {
            _gameField.SaveFiled();
        }

        public bool CheckEndGame(out bool loseOrWin)
        {
            bool endGame = false;
            loseOrWin = false;

            try
            {
                endGame = _gameField.EndGame(out loseOrWin);
            }
            catch (Exception ex)
            {
                File.AppendAllText(ConstantValue.PATH_LOG, 
                    "Method: CheckEnemyDead" + ex.Message + Environment.NewLine);
            }

            return endGame;
        }

        public void CreateNewEnem()
        {
            _gameField.CreateEnemyByTime();
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
                            resultSymbol = _viewObj[((PlayerTank)_gameField[tempCoor]).Characteristic.Skin,
                                ((PlayerTank)_gameField[tempCoor]).DirectionTank][objRow, objCol];
                            break;
                        case ObjectType.EnemyTank:
                            resultSymbol = _viewObj[((EnemyTank)_gameField[tempCoor]).Characteristic.Skin,
                                ((EnemyTank)_gameField[tempCoor]).DirectionTank][objRow, objCol];
                            break;
                        case ObjectType.Base:
                            resultSymbol = _viewObj.GetViewBase()[objRow, objCol];                         
                            break;
                        case ObjectType.BrickBlock:
                            resultSymbol = _viewObj.GetViewBlock(((BrickBlock)_gameField[tempCoor]).Skin);
                            break;
                        case ObjectType.GrassBlock:
                            resultSymbol = _viewObj.GetViewBlock(((GrassBlock)_gameField[tempCoor]).Skin);
                            break;
                        case ObjectType.IceBlock:
                            resultSymbol = _viewObj.GetViewBlock(((IceBlock)_gameField[tempCoor]).Skin);
                            break;
                        case ObjectType.MetalBlock:
                            resultSymbol = _viewObj.GetViewBlock(((MetalBlock)_gameField[tempCoor]).Skin);
                            break;
                        case ObjectType.Bullet:
                            resultSymbol = ConstantValue.BULLET;
                            break;
                        default:
                            break;
                    }
                }

                return resultSymbol;
            }
        }

        public string GetPlayerCharacteristic(out int health, out int atckDmg, out int numKilledEnemy)
        {
            health = _gameField.Player.Characteristic.HP;
            atckDmg = _gameField.Player.Characteristic.AtckDmg;
            numKilledEnemy = _gameField.Player.NumKilledEnemy;

            return _gameField.Player.Characteristic.Skin.ToString();
        }

        public ColorSkin GetColorSkin(int row, int col)
        {
            Coordinate colorCoordinate = new Coordinate(row, col);
            ColorSkin colorSkin = ColorSkin.NoColor;

            if (_gameField.IsContain(colorCoordinate))
            {
                colorSkin = _gameField[colorCoordinate].Color;
            }

            return colorSkin;
        }
    }
}
