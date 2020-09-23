using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    [Serializable]
    public class Bullet : GameObject, IMovable
    {
        #region ===--- Data ---===

        public int Range { get; private set; } = 0;
        public int AtackSpeed { get; private set; } = 0;
        public int AtackDamage { get; private set; } = 0;
        public bool IsBotBullet { get; set; } = false;
        public SkinBullet Skin { get; internal set; } = SkinBullet.NoSkin;
        public Direction Direction { get; private set; } = Direction.NoDirection;

        #endregion

        #region ===--- Constructors ---===

        public Bullet(IField owner)
            : base(owner)
        {
        }

        public Bullet(Coordinate coordinate, ColorSkin color, IField owner,
            int range, int atckSpeed, int atckDamage, bool botBullet,
            SkinBullet skin, Direction direction)
            : base(coordinate, color, owner)
        {
            Range = range;
            AtackSpeed = atckSpeed;
            AtackDamage = atckDamage;
            IsBotBullet = botBullet;
            Skin = skin;
            Direction = direction;
        }

        #endregion

        #region ===-- Methods ---===

        public bool CreateBullet(Tank bulletTank)
        {
            bool isCreate = true;

            switch (bulletTank.DirectionTank)
            {
                case Direction.Right:
                    Position.PosX = bulletTank.Position.PosX + (ConstantValue.HEIGHT_TANK / 2);
                    Position.PosY = bulletTank.Position.PosY + ConstantValue.WIDTH_TANK;
                    break;
                case Direction.Left:
                    Position.PosX = bulletTank.Position.PosX + (ConstantValue.HEIGHT_TANK / 2);
                    Position.PosY = bulletTank.Position.PosY - 1;
                    break;
                case Direction.Up:
                    Position.PosX = bulletTank.Position.PosX - 1;
                    Position.PosY = bulletTank.Position.PosY + (ConstantValue.WIDTH_TANK / 2);
                    break;
                case Direction.Down:
                    Position.PosX = bulletTank.Position.PosX + ConstantValue.HEIGHT_TANK;
                    Position.PosY = bulletTank.Position.PosY + (ConstantValue.WIDTH_TANK / 2);
                    break;
                case Direction.NoDirection:
                    break;
            }

            if (!Position.IsPermitMoveCoordinate(bulletTank.DirectionTank) 
                || (_owner.IsContain(Position)))
            {
                isCreate = false;
            }
            else
            {
                Coordinate newBulletCoord = new Coordinate(Position.PosX, Position.PosY);

                Color = ColorSkin.White;
                Range = bulletTank.CharacterTank.AtckRng;
                AtackSpeed = bulletTank.CharacterTank.AtckSp;
                AtackDamage = bulletTank.CharacterTank.AtckDmg;
                IsBotBullet = (bulletTank is EnemyTank);
                Skin = SkinBullet.ArmorPiercing;
                Direction = bulletTank.DirectionTank;

                if (_owner.IsContain(Position) && !((Block)_owner[Position]).IsBrickBlock())
                {
                    _owner.AddBullet(this);
                }
                else
                {
                    _owner[newBulletCoord] = this;
                } 
            }

            return isCreate;
        }

        #endregion

        #region ===--- Override Properties ---===

        public override ObjectType KindOfObject
        {
            get
            {
                return ObjectType.Bullet;
            }
        }

        #endregion

        #region ===--- Interface implementation ---===

        public void Move(ActionPlayer actionPlayer = ActionPlayer.NoAction)
        {
            _owner[Position] = this;
            Range--;
        }

        public bool IsPermitMove()
        {
            return !_owner.IsContain(Position);
        }

        #endregion
    }
}
