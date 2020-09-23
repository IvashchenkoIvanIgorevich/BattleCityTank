using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    [Serializable]
    public class EnemyTank : Tank
    {
        #region ===--- Constructor ---===

        public EnemyTank(CharacterTank character, Direction direction,
            Coordinate coordinate, ColorSkin skin, IField gameField)
            : base(character, direction, coordinate, skin, gameField)
        {
        }

        #endregion

        #region ===--- Override properties ---===

        public override ObjectType KindOfObject
        {
            get
            {
                return ObjectType.EnemyTank;
            }
        }

        #endregion

        #region ===--- Methods for move ---===

        public void StrategyAtackObject(Coordinate coordinateAtackObj)    // can be pattern IStrategy
        {
            Coordinate previousPosition = new Coordinate(Position);
            Coordinate afterPosition = new Coordinate(Position);
            ActionPlayer[] permitActions = GetPermitActions();
            bool isMove = false;

            for (int numAction = 0; numAction < permitActions.Length; numAction++)
            {
                switch (permitActions[numAction])
                {
                    case ActionPlayer.PressRight:
                        if (DirectionTank != Direction.Right)
                        {
                            DirectionTank = Direction.Right;
                        }
                        else
                        {
                            afterPosition.Move(Direction.Right);
                            isMove = true;
                        }
                        break;
                    case ActionPlayer.PressLeft:
                        if (DirectionTank != Direction.Left)
                        {
                            DirectionTank = Direction.Left;
                        }
                        else
                        {
                            afterPosition.Move(Direction.Left);
                            isMove = true;
                        }
                        break;
                    case ActionPlayer.PressUp:
                        if (DirectionTank != Direction.Up)
                        {
                            DirectionTank = Direction.Up;
                        }
                        else
                        {
                            afterPosition.Move(Direction.Up);
                            isMove = true;
                        }
                        break;
                    case ActionPlayer.PressDown:
                        if (DirectionTank != Direction.Down)
                        {
                            DirectionTank = Direction.Down;
                        }
                        else
                        {
                            afterPosition.Move(Direction.Down);
                            isMove = true;
                        }
                        break;
                }

                if (isMove && (afterPosition.GetSquareToObj(coordinateAtackObj.PosX, coordinateAtackObj.PosY)
                    <= previousPosition.GetSquareToObj(coordinateAtackObj.PosX, coordinateAtackObj.PosY)))
                {
                    Move(permitActions[numAction]);
                    break;
                }
            }
        }

        private ActionPlayer[] GetPermitActions()
        {
            ActionPlayer[] actionsEnemy = new ActionPlayer[ConstantValue.NUM_DIRECTION];
            Direction preDirection = DirectionTank;

            for (int numDirection = 0; numDirection < actionsEnemy.Length; numDirection++)
            {
                if (IsPermitMove())
                {
                    actionsEnemy[numDirection] = GameManager.GetActionByDirection(DirectionTank);
                }

                DirectionTank = GameManager.ChangeDirectionEnemy(DirectionTank);
            }

            DirectionTank = preDirection;

            return actionsEnemy;
        }

        #endregion

        #region ===--- Methods permit shot---===

        public bool IsPermitShot(Coordinate atackCoordinate)
        {
            return (CharacterTank.AtckRng > Math.Min(Position.GetDeltaHeight(atackCoordinate),
                Position.GetDeltaWidth(atackCoordinate)));
        }

        #endregion
    }
}
