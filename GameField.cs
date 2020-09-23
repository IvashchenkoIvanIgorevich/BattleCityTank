using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    [Serializable]
    public class GameField : IField
    {
        #region ===--- Dataset ---===

        public int Width { get; private set; }
        public int Height { get; private set; }
        protected Dictionary<Coordinate, GameObject> _gameObjects = new Dictionary<Coordinate, GameObject>
            ((ConstantValue.HEIGHT_GAMEFIELD - 1) * (ConstantValue.WIDTH_GAMEFIELD - 1));
        private PlayerTank _player;
        private List<EnemyTank> _enemies = new List<EnemyTank>(ConstantValue.NUM_ALL_ENEMY);
        private Coordinate _coordinateBase = new Coordinate(ConstantValue.POS_ROW_BASE, 
            ConstantValue.POS_COL_BASE);
        private List<Bullet> _bullets = new List<Bullet>(ConstantValue.MAX_NUM_BULLETS);
        private SaveOrLoad saveLoad { get; set; }

        #endregion

        #region ===--- Constructor ---===

        public GameField(int fieldWidth, int fieldHeight)
        {
            Width = fieldWidth;
            Height = fieldHeight;
            saveLoad = new SaveOrLoad(this);
        }

        #endregion

        #region ===--- Properties ---===

        public PlayerTank Player    
        {
            get
            {
                return _player;
            }
            internal set
            {
                _player = value;
            }
        }

        internal EnemyTank Enemy
        {
            set
            {
                _enemies.Add(value);
            }
        }

        public List<EnemyTank> EnemyTanks
        {
            get
            {
                return _enemies;
            }
        }

        #endregion

        #region ===--- Method MoveEnemies ---===

        public void MoveEnemies(ActionPlayer[] actionEnemies)
        {
            int countEnemy = 0;

            foreach (EnemyTank item in _enemies)
            {
                if (countEnemy == 0)
                {
                    if (!item.IsPermitMove())
                    {
                        GameManager.ChangeActionEnemy(ref actionEnemies[countEnemy]);
                    }

                    item.Move(actionEnemies[countEnemy]);
                }
                else
                {
                    if (countEnemy == 1)
                    {
                        item.StrategyAtackObject(Player.Position);
                    }
                    else
                    {
                        item.StrategyAtackObject(_coordinateBase);
                    }
                }               

                countEnemy++;
            }
        }

        #endregion

        #region ===--- Method MoveBullets ---===

        public void MoveBullets()
        {
            if (_bullets.Count != 0)
            {
                List<Bullet> delBullets = new List<Bullet>();

                foreach (Bullet bull in _bullets)
                {
                    if (_gameObjects.ContainsKey(bull.Position) 
                        && _gameObjects[bull.Position] is Bullet)    // TODO: удаляем только в том случае если она существует и она действительно Пуля иначе будет удалять все элементы которые на позиции
                    {
                        _gameObjects.Remove(bull.Position);
                    }

                    if ((bull.Range <= 0) || !bull.Position.Move(bull.Direction))
                    {
                        delBullets.Add(bull);
                        continue;
                    }

                    if (bull.IsPermitMove())
                    {
                        bull.Move();
                    }
                    else
                    {
                        SetDamageToObject(bull, delBullets);
                    }
                }

                foreach (Bullet bull in delBullets)
                {
                    _bullets.Remove(bull);
                }
            }
        }

        public void SetDamageToObject(Bullet dmgBullet, List<Bullet> delBull)
        {
            switch (this[dmgBullet.Position].KindOfObject)
            {
                case ObjectType.PlayerTank:
                    if (dmgBullet.IsBotBullet)
                    {
                        Player.CharacterTank.HP -= dmgBullet.AtackDamage;
                        delBull.Add(dmgBullet);
                    }
                    break;
                case ObjectType.EnemyTank:
                    if (!dmgBullet.IsBotBullet)
                    {
                        ((EnemyTank)this[dmgBullet.Position]).CharacterTank.HP
                            -= dmgBullet.AtackDamage;
                        delBull.Add(dmgBullet);
                    }
                    break;
                case ObjectType.Base:
                    break;
                case ObjectType.Bullet:
                    _gameObjects.Remove(dmgBullet.Position);
                    delBull.Add(dmgBullet);
                    break;
                case ObjectType.BrickBlock:
                    _gameObjects.Remove(dmgBullet.Position);
                    delBull.Add(dmgBullet);
                    break;
                case ObjectType.GrassBlock:
                    break;
                case ObjectType.IceBlock:
                    break;
                case ObjectType.MetalBlock:
                    _gameObjects.Remove(dmgBullet.Position);
                    delBull.Add(dmgBullet);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ===--- Method AddBullets ---===

        public void AddPlayerBullet()
        {
            Bullet newBullet = new Bullet(this);    //TODO

            if (newBullet.CreateBullet(Player))
            {
                _bullets.Add(newBullet);
            }
        }

        public void AddEnemiesBullet()
        {
            foreach (EnemyTank item in _enemies)
            {
                if (item.IsPermitShot(Player.Position))
                {
                    Bullet newBullet = new Bullet(this);    // TODO

                    if (newBullet.CreateBullet(item))
                    {
                        _bullets.Add(newBullet);
                    }
                }
            }
        }

        #endregion

        #region ===--- Method Save And Load Game ---===

        public void SaveFiled()
        {
            saveLoad.SaveTheGame();
        }

        #endregion

        #region ===--- Implementation interfaces ---===

        public GameObject this[Coordinate objCoord]
        {
            get
            {
                return _gameObjects[objCoord];
            }
            set
            {
                if (value is GameObject)
                {
                    _gameObjects[objCoord] = value;
                }
            }
        }

        void IField.DeleteGameObj(Coordinate coordinateObj)
        {
            if (_gameObjects.ContainsKey(coordinateObj))
            {
                _gameObjects.Remove(coordinateObj);
            }
        }

        public bool IsContain(Coordinate coordinateContain)
        {
            return _gameObjects.ContainsKey(coordinateContain);
        }

        public void DeleteBullet(Bullet delBullet)
        {
            _bullets.Remove(delBullet);
        }

        public void AddBullet(Bullet addBullet)
        {
            _bullets.Add(addBullet);
        }

        #endregion

        public GameField GetCopy()
        {
            GameField copyGF = new GameField(ConstantValue.WIDTH_GAMEFIELD,
                ConstantValue.HEIGHT_GAMEFIELD);

            copyGF.Width = Width;
            copyGF.Height = Height;


            return copyGF;
        }
    }
}
