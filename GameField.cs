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
        private List<Bullet> _bullets = new List<Bullet>();

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

        public void AddBulletToGameField()
        {
            
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

        void IField.AddGameObj(Coordinate coor, GameObject obj)
        {
            _gameObjects.Add(coor, obj);
        }

        public bool IsContain(Coordinate coordinateContain)
        {
            return _gameObjects.ContainsKey(coordinateContain);
        }

        #endregion
    }
}
