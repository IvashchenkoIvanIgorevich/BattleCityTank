using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    public class Bullet : GameObject, IMovable
    {
        #region ===--- Data ---===

        protected int Range { get; } = 0;
        protected int AtackSpeed { get; } = 0;
        protected int AtackDamage { get; } = 0;
        protected bool IsBotBullet { get; } = false;
        protected SkinBullet Skin { get; } = SkinBullet.NoSkin;
        protected Direction BulletDirection { get; } = Direction.NoDirection;

        #endregion

        #region ===--- Constructor ---===

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
            BulletDirection = direction;
        }

        #endregion

        #region ===-- Methods ---===

        public void CreateBullet(Tank bulletTank)
        {
            Coordinate bulletPos = new Coordinate();

            switch (bulletTank.DirectionTank)
            {
                case Direction.Right:
                    bulletPos.PosX = bulletTank.Position.PosX + (ConstantValue.HEIGHT_TANK / 2);
                    bulletPos.PosY = bulletTank.Position.PosY + ConstantValue.WIDTH_TANK;
                    break;
                case Direction.Left:
                    bulletPos.PosX = bulletTank.Position.PosX + (ConstantValue.HEIGHT_TANK / 2);
                    bulletPos.PosY = bulletTank.Position.PosY - 1;
                    break;
                case Direction.Up:
                    bulletPos.PosX = bulletTank.Position.PosX - 1;
                    bulletPos.PosY = bulletTank.Position.PosY + (ConstantValue.WIDTH_TANK / 2);
                    break;
                case Direction.Down:
                    bulletPos.PosX = bulletTank.Position.PosX + ConstantValue.HEIGHT_TANK;
                    bulletPos.PosY = bulletTank.Position.PosY + (ConstantValue.WIDTH_TANK / 2);
                    break;
                case Direction.NoDirection:
                    break;
            }

            if (!bulletPos.IsPermitMoveCoordinate(bulletTank.DirectionTank) || !_owner.IsContain(bulletPos))
            {
                return;
            }

            bool isBot = (bulletTank is EnemyTank);

            Coordinate newBulletCoord = new Coordinate(bulletPos.PosX, bulletPos.PosY);

            _owner[newBulletCoord] = new Bullet(newBulletCoord, ColorSkin.White,
                _owner, bulletTank.CharacterTank.AtckRng, bulletTank.CharacterTank.AtckSp,
                bulletTank.CharacterTank.AtckDmg, isBot, SkinBullet.Metal, bulletTank.DirectionTank);
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

        public void Move(ActionPlayer actionPlayer)
        {
            throw new NotImplementedException();
        }

        public bool IsPermitMove()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
