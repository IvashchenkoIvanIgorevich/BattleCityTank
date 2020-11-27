using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;
using DB;

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
        private Base _base;
        private List<EnemyTank> _enemies = new List<EnemyTank>(ConstantValue.NUM_ALL_ENEMY);
        private Coordinate _coordinateBase = new Coordinate(ConstantValue.POS_ROW_BASE,
            ConstantValue.POS_COL_BASE);
        private List<Bullet> _bullets = new List<Bullet>(ConstantValue.MAX_NUM_BULLETS);
        private SaveGame saveLoad { get; set; }
        [field: NonSerialized]
        private event EnemyWasDead _getEnemyDeaded;
        [field: NonSerialized]
        private event EnemyDeaded _enemyDead;
        [field: NonSerialized]
        private event PlayerOrBaseDeaded _gameOver;
        public int NumEnemyOnField { get; private set; } = ConstantValue.START_NUM_ENEMYTANK;
        private DBTank _db = new DBTank(ConstantValue.DB_CONN_STR);
        public short IDPlayer { get; private set; } = -1;    //TODO: вынести в отдельный класс
        public string PlayerName { get; private set; } = "NO NAME";    //TODO: вынести в отдельный класс
        public int GameNumber { get; private set; }

        #endregion

        #region ===--- Constructor ---===

        public GameField(int fieldWidth, int fieldHeight)
        {
            Width = fieldWidth;
            Height = fieldHeight;
            saveLoad = new SaveGame(this);
            _getEnemyDeaded += SetEnemyDead;
        }

        #endregion

        #region ===--- Events ---===

        public event EnemyDeaded EnemyDead
        {
            add
            {
                _enemyDead += value;
            }
            remove
            {
                _enemyDead -= value;
            }
        }

        public event PlayerOrBaseDeaded GameOver
        {
            add
            {
                _gameOver += value;
            }
            remove
            {
                _gameOver -= value;
            }
        }

        public event EnemyWasDead GetEnemyDeaded
        {
            add
            {
                _getEnemyDeaded += value;
            }
            remove
            {
                _getEnemyDeaded -= value;
            }
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

        public Base GameBase
        {
            get
            {
                return _base;
            }
            internal set
            {
                _base = value;
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
                        && _gameObjects[bull.Position] is Bullet)    // удаляем только в том случае если она существует и она действительно Пуля иначе будет удалять все элементы которые на позиции
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
                        Player.Characteristic.HP -= dmgBullet.AtackDamage;
                        Player.GetDamage += dmgBullet.AtackDamage;
                        delBull.Add(dmgBullet);
                    }
                    if (Player.Characteristic.HP <= ConstantValue.LOW_HP_TANK)
                    {
                        Player.Color = ConstantValue.COLOR_LOW_HP;
                    }
                    break;
                case ObjectType.EnemyTank:
                    if (!dmgBullet.IsBotBullet)
                    {
                        ((EnemyTank)this[dmgBullet.Position]).Characteristic.HP
                            -= dmgBullet.AtackDamage;
                        Player.SetDamage += dmgBullet.AtackDamage;
                        delBull.Add(dmgBullet);
                    }
                    if (((EnemyTank)this[dmgBullet.Position]).Characteristic.HP
                        <= ConstantValue.LOW_HP_TANK)
                    {
                        ((EnemyTank)this[dmgBullet.Position]).Color = ConstantValue.COLOR_LOW_HP;
                    }
                    break;
                case ObjectType.Base:
                    _base.BaseDead = true;
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
                    throw new DamageObjNotFoundException
                        (string.Format("\n{0} Method: SetDamageToObject, Class: GameField, parametrs KindOfObject: {1} - not found!!!",
                        DateTime.Now, this[dmgBullet.Position].KindOfObject));
            }
        }

        #endregion

        #region ===--- Method AddBullets ---===

        public void AddPlayerBullet(ulong gameTime)
        {
            Bullet newBullet = new Bullet(this);

            if (gameTime - Player.TimeShoot >= ConstantValue.TIME_BEETWEN_SHOT)
            {
                if (newBullet.CreateBullet(Player))
                {
                    if (_player.IsEventShotPlayer())
                    {
                        _player.InvokeShotPlayer();
                        Player.NumFirePlayer++;
                    }

                    _bullets.Add(newBullet);    // Add to List<Bullet> not to _gameObjects because _gameObjects contains object on this position

                    Player.TimeShoot = gameTime;
                }
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
                        _bullets.Add(newBullet);    // Add to List<Bullet> not to _gameObjects because _gameObjects contains object on this position
                    }
                }
            }
        }

        #endregion

        #region ===--- Method RemoveBullets ---===

        private void RemoveBullets(Bullet bull)
        {
            if (bull.Direction.HasFlag(Direction.Left)
                || bull.Direction.HasFlag(Direction.Right))
            {
                Coordinate posUpUp = new Coordinate(bull.Position.PosX - ConstantValue.SHIFT_TWICE, bull.Position.PosY);
                Coordinate posUp = new Coordinate(bull.Position.PosX - ConstantValue.SHIFT_ONCE, bull.Position.PosY);
                Coordinate posDownDown = new Coordinate(bull.Position.PosX + ConstantValue.SHIFT_TWICE, bull.Position.PosY);
                Coordinate posDown = new Coordinate(bull.Position.PosX + ConstantValue.SHIFT_ONCE, bull.Position.PosY);

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
            }
            else
            {
                Coordinate posLeftLeft = new Coordinate(bull.Position.PosX, bull.Position.PosY - ConstantValue.SHIFT_TWICE);
                Coordinate posLeft = new Coordinate(bull.Position.PosX, bull.Position.PosY - ConstantValue.SHIFT_ONCE);
                Coordinate posRightRight = new Coordinate(bull.Position.PosX, bull.Position.PosY + ConstantValue.SHIFT_TWICE);
                Coordinate posRight = new Coordinate(bull.Position.PosX, bull.Position.PosY + ConstantValue.SHIFT_ONCE);

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
            }

            _gameObjects.Remove(bull.Position);
        }

        #endregion

        #region ===--- Dead Enemy Tank ---===

        private bool CheckEnemiesTank()    // check that one enemy dead
        {
            bool win = false;

            List<EnemyTank> delEnemy = new List<EnemyTank>(_enemies.Count);

            foreach (EnemyTank item in _enemies)
            {
                if (item.Characteristic.HP <= 0)
                {
                    if (_getEnemyDeaded != null)
                    {
                        _getEnemyDeaded(item);

                        if (_enemyDead != null)
                        {
                            _enemyDead();
                        }

                        delEnemy.Add(item);
                    }
                }
            }

            foreach (EnemyTank item in delEnemy)
            {
                _enemies.Remove(item);

                if (NumEnemyOnField < ConstantValue.NUM_ALL_ENEMY)
                {
                    CharacteristicTank newCharacter = GameManager.GetRandomChracterTank();

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

        public void SetEnemyDead(Tank deadEnemy)    // delete dead enemy from game field
        {
            deadEnemy.DeleteTank();
            _player.NumKilledEnemy++;
        }

        #endregion

        #region ===--- Create enemy by time ---===

        public void CreateEnemyByTime()
        {
            if (NumEnemyOnField < ConstantValue.NUM_ALL_ENEMY)
            {
                CharacteristicTank newCharacter = GameManager.GetRandomChracterTank();

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

        #region ===--- Dead Player Or Base ---===

        private bool CheckPlayerBase()
        {
            return (Player.Characteristic.HP <= 0 || _base.BaseDead) ? true : false;
        }

        #endregion

        #region ===--- EndGame ---===

        public bool EndGame(out bool loseOrWin)
        {
            bool res = false;
            loseOrWin = false;

            if (CheckEnemiesTank())
            {
                loseOrWin = true;
                res = true;
            }
            else
            {
                if (CheckPlayerBase())
                {
                    if (_gameOver != null)
                    {
                        _gameOver();
                    }
                    res = true;
                }
            }

            return res;
        }

        #endregion

        #region ===--- Database ---===

        public void CreateNewPlayer(string _name, string _email, string _gender)
        {
            if (_db.CreatePlayer(_name, _email, _gender == "male" ? false : true))
            {
                IDPlayer = _db.ReadAllPlayer()
                    .FirstOrDefault(p => p.Name == _name && p.Email == _email).PlayerId;
                PlayerName = _name;
            }
        }

        public bool CheckPlayerDB(short id)
        {
            bool res = false;

            if (_db.ReadPlayer(id) != null)
            {
                IDPlayer = _db.ReadPlayer(id).PlayerId;
                PlayerName = _db.ReadPlayer(id).Name;
                res = true;
            }

            return res;
        }

        public bool CreateGameTank(short _model, int _numFire,
            short _killedEnemies, int _setDamage, int _getDamage)
        {
            bool res = false;

            if (_db.CreateGameTank(_model, _numFire, _killedEnemies, _setDamage, _getDamage, out int _tankSN))
            {
                Player.SerialNumTank = _tankSN;
                res = true;
            }

            return res;
        }

        //public bool CreateTankCharacter(string _nameModel, short _atackSpeed,
        //    short _healthPoint, short _moveSpeed)
        //{
        //    return _db.CreateTankCharacter(_nameModel, _atackSpeed, _healthPoint, _moveSpeed);
        //}

        public bool CreateGame(short _gameResult, int _resultingTime, DateTime _gameDate)
        {
            return _db.CreateGame(_gameResult, Player.SerialNumTank, IDPlayer, _resultingTime, _gameDate, out int GameNumber);
        }

        #endregion
    }
}
