using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
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

        #endregion

        #region ===--- Constructor ---===

        public GameField(int fieldWidth, int fieldHeight)
        {
            Width = fieldWidth;
            Height = fieldHeight;
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

        internal Bullet AddBullet
        {
            set
            {
                _bullets.Add(value);
            }
        }

        public List<Bullet> Bullets
        {
            get
            {
                return _bullets;
            }
        }

        #endregion

        #region ===--- Methods ---===

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

        public void MoveBullets()
        {
            if (_bullets.Count != 0)
            {
                List<Bullet> delBullets = new List<Bullet>();

                foreach (Bullet bull in _bullets)
                {
                    _gameObjects.Remove(bull.Position);

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

        public void SetDamageToObject(Bullet damageBullet, List<Bullet> delBull)
        {
            switch (this[damageBullet.Position].KindOfObject)
            {
                case ObjectType.PlayerTank:
                    if (damageBullet.IsBotBullet)
                    {
                        Player.CharacterTank.HP -= damageBullet.AtackDamage;
                        delBull.Add(damageBullet);
                    }
                    break;
                case ObjectType.EnemyTank:
                    if (!damageBullet.IsBotBullet)
                    {
                        ((EnemyTank)this[damageBullet.Position]).CharacterTank.HP
                            -= damageBullet.AtackDamage;
                        delBull.Add(damageBullet);
                    }
                    break;
                case ObjectType.Base:
                    break;
                case ObjectType.Bullet:
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

        public void AddPlayerBullet()
        {
            Bullet newBullet = new Bullet(this);

            if (newBullet.CreateBullet(Player))
            {
                _bullets.Add(newBullet);
            }
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

        #endregion
    }
}
