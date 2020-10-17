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
        private SaveGame saveLoad { get; set; }
        private event EnemyWasDead _enemyDead;
        public int NumEnemyOnField { get; private set; } = ConstantValue.START_NUM_ENEMYTANK;

        #endregion

        #region ===--- Constructor ---===

        public GameField(int fieldWidth, int fieldHeight)
        {
            Width = fieldWidth;
            Height = fieldHeight;
            saveLoad = new SaveGame(this);
            _enemyDead += SetEnemyDead;
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
                switch (countEnemy)
                {
                    case 0:
                        item.StrategyAtackObject(Player.Position);
                        break;
                    case 1:
                        item.StrategyAtackObject(_coordinateBase);
                        break;
                    default:
                        if (!item.IsPermitMove())
                        {
                            GameManager.ChangeActionEnemy(ref actionEnemies[countEnemy]);
                        }

                        item.Move(actionEnemies[countEnemy]);
                        break;
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
                    if (Player.CharacterTank.HP <= ConstantValue.LOW_HP_TANK)
                    {
                        Player.Color = ConstantValue.COLOR_LOW_HP;
                    }
                    break;
                case ObjectType.EnemyTank:
                    if (!dmgBullet.IsBotBullet)
                    {
                        ((EnemyTank)this[dmgBullet.Position]).CharacterTank.HP
                            -= dmgBullet.AtackDamage;
                        delBull.Add(dmgBullet);
                    }
                    if (((EnemyTank)this[dmgBullet.Position]).CharacterTank.HP
                        <= ConstantValue.LOW_HP_TANK)
                    {
                        ((EnemyTank)this[dmgBullet.Position]).Color = ConstantValue.COLOR_LOW_HP;
                    }
                    break;
                case ObjectType.Base:
                    break;
                case ObjectType.Bullet:
                    _gameObjects.Remove(dmgBullet.Position);
                    delBull.Add(dmgBullet);
                    break;
                case ObjectType.BrickBlock:
                    RemoveBullets(dmgBullet);
                    delBull.Add(dmgBullet);
                    break;
                case ObjectType.GrassBlock:
                    break;
                case ObjectType.IceBlock:
                    break;
                case ObjectType.MetalBlock:
                    if (dmgBullet.AtackDamage >= ConstantValue.ATACK_DAMAGE_DESTROY)
                    {
                        RemoveBullets(dmgBullet);
                    }
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
            Bullet newBullet = new Bullet(this);

            if (newBullet.CreateBullet(Player))
            {
                if (!_gameObjects.ContainsKey(newBullet.Position))
                {
                    _gameObjects[newBullet.Position] = newBullet;    // Add to _gameObjects because _gameObjects not contains object on this position
                }

                _bullets.Add(newBullet);    // Add to List<Bullet> not to _gameObjects because _gameObjects contains object on this position
            }
        }

        public void AddEnemiesBullet()
        {
            foreach (EnemyTank item in _enemies)
            {
                if (item.IsPermitShot(Player.Position))
                {
                    Bullet newBullet = new Bullet(this);

                    if (newBullet.CreateBullet(item))
                    {
                        if (!_gameObjects.ContainsKey(newBullet.Position))
                        {
                            _gameObjects[newBullet.Position] = newBullet;    // Add to _gameObjects because _gameObjects not contains object on this position
                        }

                        _bullets.Add(newBullet);    // Add to List<Bullet> not to _gameObjects because _gameObjects contains object on this position
                    }
                }
            }
        }

        #endregion

        #region ===--- Method RemoveBullets ---===

        private void RemoveBullets(Bullet bull)    // TODO: review
        {
            Coordinate posLeftLeft = new Coordinate(bull.Position.PosX, bull.Position.PosY - 2);
            Coordinate posLeft = new Coordinate(bull.Position.PosX, bull.Position.PosY - 1);
            Coordinate posRightRight = new Coordinate(bull.Position.PosX, bull.Position.PosY + 2);
            Coordinate posRight = new Coordinate(bull.Position.PosX, bull.Position.PosY + 1);
            Coordinate posUpUp = new Coordinate(bull.Position.PosX - 2, bull.Position.PosY);
            Coordinate posUp = new Coordinate(bull.Position.PosX - 1, bull.Position.PosY);
            Coordinate posDownDown = new Coordinate(bull.Position.PosX + 2, bull.Position.PosY);
            Coordinate posDown = new Coordinate(bull.Position.PosX + 1, bull.Position.PosY);

            switch (bull.Direction)
            {
                case Direction.Right:
                    if (_gameObjects.ContainsKey(posDown))
                    {
                        _gameObjects.Remove(posDown);
                    }
                    if (_gameObjects.ContainsKey(posUp))
                    {
                        _gameObjects.Remove(posUp);
                    }
                    if (_gameObjects.ContainsKey(posDownDown))
                    {
                        _gameObjects.Remove(posDownDown);
                    }
                    if (_gameObjects.ContainsKey(posUpUp))
                    {
                        _gameObjects.Remove(posUpUp);
                    }
                    break;
                case Direction.Left:
                    if (_gameObjects.ContainsKey(posDown))
                    {
                        _gameObjects.Remove(posDown);
                    }
                    if (_gameObjects.ContainsKey(posUp))
                    {
                        _gameObjects.Remove(posUp);
                    }
                    if (_gameObjects.ContainsKey(posDownDown))
                    {
                        _gameObjects.Remove(posDownDown);
                    }
                    if (_gameObjects.ContainsKey(posUpUp))
                    {
                        _gameObjects.Remove(posUpUp);
                    }
                    break;
                case Direction.Up:
                    if (_gameObjects.ContainsKey(posLeft))
                    {
                        _gameObjects.Remove(posLeft);
                    }
                    if (_gameObjects.ContainsKey(posRight))
                    {
                        _gameObjects.Remove(posRight);
                    }
                    if (_gameObjects.ContainsKey(posLeftLeft))
                    {
                        _gameObjects.Remove(posLeftLeft);
                    }
                    if (_gameObjects.ContainsKey(posRightRight))
                    {
                        _gameObjects.Remove(posRightRight);
                    }                    
                    break; 
                case Direction.Down:
                    if (_gameObjects.ContainsKey(posLeft))
                    {
                        _gameObjects.Remove(posLeft);
                    }
                    if (_gameObjects.ContainsKey(posRight))
                    {
                        _gameObjects.Remove(posRight);
                    }
                    if (_gameObjects.ContainsKey(posLeftLeft))
                    {
                        _gameObjects.Remove(posLeftLeft);
                    }
                    if (_gameObjects.ContainsKey(posRightRight))
                    {
                        _gameObjects.Remove(posRightRight);
                    }                    
                    break;
            }

            _gameObjects.Remove(bull.Position);
        }

        #endregion

        #region ===--- Dead Enemy Tank ---===

        public bool CheckEnemiesTank()    // check that one enemy dead
        {
            bool win = false;

            List<EnemyTank> delEnemy = new List<EnemyTank>(_enemies.Count);

            foreach (EnemyTank item in _enemies)
            {
                if (item.CharacterTank.HP <= 0)
                {
                    if (_enemyDead != null)
                    {
                        _enemyDead(item);
                        delEnemy.Add(item);
                    }
                }
            }

            foreach (EnemyTank item in delEnemy)
            {
                _enemies.Remove(item);

                if (NumEnemyOnField < ConstantValue.NUM_ALL_ENEMY)
                {
                    CharacterTank newCharacter = GameManager.GetRandomChracterTank();

                    Enemy = new EnemyTank(newCharacter, Direction.Down, GetCoorrdinateForNewEnemy(),
                        ColorSkin.Gray, this);

                    NumEnemyOnField++;
                }
            }

            if (_enemies.Count == 0)
            {
                win = true;
            }

            return win;
        }

        private void SetEnemyDead(Tank deadEnemy)    // delete dead enemy from game field
        {
            deadEnemy.DeleteTank();
        }

        #endregion

        #region ===--- Create enemy by time ---===

        public void CreateEnemyByTime()
        {
            if (NumEnemyOnField < ConstantValue.NUM_ALL_ENEMY)
            {
                CharacterTank newCharacter = GameManager.GetRandomChracterTank();

                Enemy = new EnemyTank(newCharacter, Direction.Down, GetCoorrdinateForNewEnemy(),
                    ColorSkin.Gray, this);

                NumEnemyOnField++;
            }
        }

        #endregion

        #region ===--- Get new position for New enemy ---===

        private Coordinate GetCoorrdinateForNewEnemy()
        {
            int startCol = ConstantValue.COL_START_ENEMY_1;
            int startRow = ConstantValue.ROW_START_ENEMY;

            for (int row = startRow; row < startRow + ConstantValue.HEIGHT_TANK; row++)
            {
                for (int col = startCol; col < startCol + ConstantValue.WIDTH_TANK; col++)
                {
                    if (IsContain(new Coordinate(row, col)))
                    {
                        startCol += ConstantValue.SHIFT_POS_COL_NEW_ENEMY;
                        col = startCol - 1;
                    }
                }
            }

            return new Coordinate(startRow, startCol);
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

        public bool IsPermitCreateBullet(Bullet newBullet)
        {
            bool permit = true;

            if (!_gameObjects.TryGetValue(newBullet.Position, out GameObject obj))
            {
                return true;
            }

            switch (obj.KindOfObject)
            {
                case ObjectType.PlayerTank:
                    break;
                case ObjectType.Base:
                    break;
                case ObjectType.BrickBlock:
                    RemoveBullets(newBullet);
                    permit = false;
                    break;
                case ObjectType.MetalBlock:
                    if (newBullet.AtackDamage < ConstantValue.ATACK_DAMAGE_DESTROY)
                    {
                        RemoveBullets(newBullet);
                    }
                    permit = false;
                    break;
            }

            return permit;
        }

        #endregion
    }
}
