using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using CommonLib;

namespace _20200613_TankLibrary
{
    [Serializable]
    public class Tank : GameObject, IMovable
    {
        #region ===--- Dataset ---===

        public ulong TimeShoot { get; internal set; }
        public CharacteristicTank Characteristic { get; protected set; }
        public Direction DirectionTank { get; protected set; }
        private MovedPlayer _movedPlayer;
        protected ShootPlayer _shotedPlayer;

        #endregion

        #region ===--- Events ---===

        public event MovedPlayer Moved
        {
            add
            {
                _movedPlayer += value;
            }
            remove
            {
                _movedPlayer -= value;
            }
        }

        public event ShootPlayer Shooted
        {
            add
            {
                _shotedPlayer += value;
            }
            remove
            {
                _shotedPlayer -= value;
            }
        }

        #endregion

        #region ===--- Constructor ---===

        public Tank(CharacteristicTank character, Direction direction, Coordinate coordinate,
            ColorSkin skin, IField gameField) : base(coordinate, skin, gameField)
        {
            Characteristic = character;
            DirectionTank = direction;
        }

        #endregion

        #region ===--- Properties ---===

        public override ObjectType KindOfObject
        {
            get
            {
                return ObjectType.Tank;
            }
        }

        #endregion

        #region ===--- Methods ---===

        internal void DeletePrePosition(Coordinate prePosition)
        {
            Coordinate coorForMove = new Coordinate(prePosition);

            switch (DirectionTank)
            {
                case Direction.Right:
                    for (int row = prePosition.PosX; row < ConstantValue.HEIGHT_TANK + prePosition.PosX; row++)
                    {
                        coorForMove.PosX = row;
                        _owner.DeleteGameObj(coorForMove);
                    }
                    break;
                case Direction.Left:
                    coorForMove.PosY += ConstantValue.WIDTH_TANK - 1;

                    for (int row = prePosition.PosX; row < ConstantValue.HEIGHT_TANK + prePosition.PosX; row++)
                    {
                        coorForMove.PosX = row;
                        _owner.DeleteGameObj(coorForMove);
                    }
                    break;
                case Direction.Up:
                    coorForMove.PosX += ConstantValue.HEIGHT_TANK - 1;

                    for (int col = prePosition.PosY; col < ConstantValue.WIDTH_TANK + prePosition.PosY; col++)
                    {
                        coorForMove.PosY = col;
                        _owner.DeleteGameObj(coorForMove);
                    }
                    break;
                case Direction.Down:
                    for (int col = prePosition.PosY; col < ConstantValue.WIDTH_TANK + prePosition.PosY; col++)
                    {
                        coorForMove.PosY = col;
                        _owner.DeleteGameObj(coorForMove);
                    }
                    break;
            }
        }

        private void AddNewPostion(GameObject playerTank)
        {
            Coordinate tempForMove = new Coordinate(playerTank.Position);

            switch (DirectionTank)
            {
                case Direction.Right:
                    tempForMove.PosY += ConstantValue.WIDTH_TANK - 1;

                    for (int row = playerTank.Position.PosX; row < ConstantValue.HEIGHT_TANK + playerTank.Position.PosX; row++)
                    {
                        _owner[new Coordinate(row, tempForMove.PosY)] = playerTank;
                    }
                    break;
                case Direction.Left:
                    for (int row = playerTank.Position.PosX; row < ConstantValue.HEIGHT_TANK + playerTank.Position.PosX; row++)
                    {
                        _owner[new Coordinate(row, tempForMove.PosY)] = playerTank;
                    }
                    break;
                case Direction.Up:
                    for (int col = playerTank.Position.PosY; col < ConstantValue.WIDTH_TANK + tempForMove.PosY; col++)
                    {
                        _owner[new Coordinate(tempForMove.PosX, col)] = playerTank;
                    }
                    break;
                case Direction.Down:
                    tempForMove.PosX += ConstantValue.HEIGHT_TANK - 1;

                    for (int col = playerTank.Position.PosY; col < ConstantValue.WIDTH_TANK + playerTank.Position.PosY; col++)
                    {
                        _owner[new Coordinate(tempForMove.PosX, col)] = playerTank;
                    }
                    break;
            }
        }

        public void CreateTank()
        {
            for (int i = Position.PosX; i < ConstantValue.HEIGHT_TANK + Position.PosX; i++)
            {
                for (int j = Position.PosY; j < ConstantValue.WIDTH_TANK + Position.PosY; j++)
                {
                    _owner[new Coordinate(i, j)] = this;
                }
            }
        }

        public void DeleteTank()
        {
            for (int i = Position.PosX; i < ConstantValue.HEIGHT_TANK + Position.PosX; i++)
            {
                for (int j = Position.PosY; j < ConstantValue.WIDTH_TANK + Position.PosY; j++)
                {
                    _owner.DeleteGameObj(new Coordinate(i, j));
                }
            }
        }

        public bool IsEventShotPlayer()
        {
            return _shotedPlayer != null;
        }

        public void InvokeShotPlayer()
        {
            _shotedPlayer();
        }

        #endregion

        #region ===--- Implementation interfaces ---===

        public bool IsPermitMove()
        {
            bool permitOk = true;

            Coordinate permitCoordinate = new Coordinate(Position.PosX, Position.PosY);

            //start posX, posY for tank is upper left  corner
            // new coordinate with direction same tank move new coordinate on step equal movespeed(MS) tank
            switch (DirectionTank)
            {
                case Direction.Right:
                    permitCoordinate.PosX = Position.PosX;
                    permitCoordinate.PosY = Position.PosY + ConstantValue.WIDTH_TANK;
                    break;
                case Direction.Left:
                    permitCoordinate.PosX = Position.PosX;
                    permitCoordinate.PosY = Position.PosY - 1;
                    break;
                case Direction.Up:
                    permitCoordinate.PosX = Position.PosX - 1;
                    permitCoordinate.PosY = Position.PosY;
                    break;
                case Direction.Down:
                    permitCoordinate.PosX = Position.PosX + ConstantValue.HEIGHT_TANK;
                    permitCoordinate.PosY = Position.PosY;
                    break;
            }

            if (!permitCoordinate.IsPermitMoveCoordinate(DirectionTank))
            {
                return false;
            }

            Coordinate tempForMove = new Coordinate(permitCoordinate);

            if (DirectionTank.HasFlag(Direction.Right) || DirectionTank.HasFlag(Direction.Left))
            {
                for (int permitRow = permitCoordinate.PosX; permitRow < ConstantValue.HEIGHT_TANK
                        + permitCoordinate.PosX; permitRow++)
                {
                    tempForMove.PosX = permitRow;                    

                    if (_owner.IsContain(tempForMove))
                    {
                        if (!(_owner[tempForMove] is GrassBlock))
                        {
                            permitOk = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int permitCol = permitCoordinate.PosY; permitCol < ConstantValue.WIDTH_TANK
                        + permitCoordinate.PosY; permitCol++)
                {
                    tempForMove.PosY = permitCol;

                    if (_owner.IsContain(tempForMove))
                    {
                        if (!(_owner[tempForMove] is GrassBlock))
                        {
                            permitOk = false;
                            break;
                        }
                    }
                }
            }

            return permitOk;
        }

        public virtual void Move(ActionPlayer action)
        {
            if (_movedPlayer != null)
            {                
                _movedPlayer();
            }            

            bool okDirect = false;

            Coordinate prePosition = new Coordinate(Position.PosX, Position.PosY);    // position before move, previous position, it was create for delete coordinate tank after his move

            switch (action)
            {
                case ActionPlayer.PressRight:
                    if (DirectionTank.HasFlag(Direction.Right))
                    {
                        okDirect = true;
                    }
                    else
                    {
                        DirectionTank = Direction.Right;
                    }
                    break;
                case ActionPlayer.PressLeft:
                    if (DirectionTank.HasFlag(Direction.Left))
                    {
                        okDirect = true;
                    }
                    else
                    {
                        DirectionTank = Direction.Left;
                    }
                    break;
                case ActionPlayer.PressUp:
                    if (DirectionTank.HasFlag(Direction.Up))
                    {
                        okDirect = true;
                    }
                    else
                    {
                        DirectionTank = Direction.Up;
                    }
                    break;
                case ActionPlayer.PressDown:
                    if (DirectionTank.HasFlag(Direction.Down))
                    {
                        okDirect = true;
                    }
                    else
                    {
                        DirectionTank = Direction.Down;
                    }
                    break;
                case ActionPlayer.NoAction:
                    break;
                default:
                    //TODO: throw
                    break;
            }

            if (okDirect && IsPermitMove())
            {
                Position.Move(DirectionTank, Characteristic.MS);    // move tank
                DeletePrePosition(prePosition);    // delete 1 row or 1 column in owner game field 
                AddNewPostion(this);    // add 1 row or 1 column in owner game field 
            }
        }

        #endregion
    }
}
