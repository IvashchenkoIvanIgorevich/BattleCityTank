using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    public static class GameManager
    {
        public static Random rndCharacterTank = new Random();
        public static Random rndActionEnemy = new Random();

        public static CharacteristicTank GetRandomChracterTank()
        {
            int randomNum = rndCharacterTank.Next(1, ConstantValue.NUM_RND_CHARAC);
            CharacteristicTank characterTank = null;

            switch (randomNum)
            {
                case 1:
                    characterTank = new CharacteristicTank(ConstantValue.HP_LIGHT, 1, 1,
                        ConstantValue.ATACK_RANGE_LIGHT, ConstantValue.ATACK_DAMAGE_LIGHT,
                        SkinTank.Light);
                    break;
                case 2:
                    characterTank = new CharacteristicTank(ConstantValue.HP_DESTROY, 1, 1,
                        ConstantValue.ATACK_RANGE_DESTROY, ConstantValue.ATACK_DAMAGE_DESTROY,
                        SkinTank.Destroy);
                    break;
                case 3:
                    characterTank = new CharacteristicTank(ConstantValue.HP_HEAVY, 1, 1,
                        ConstantValue.ATACK_RANGE_HEAVY, ConstantValue.ATACK_DAMAGE_HEAVY,
                        SkinTank.Heavy);
                    break;
                case 4:
                    characterTank = new CharacteristicTank(ConstantValue.HP_DESTROY, 1, 1,
                        ConstantValue.ATACK_RANGE_DESTROY, ConstantValue.ATACK_DAMAGE_DESTROY,
                        SkinTank.Destroy);
                    break;
                case 5:
                    characterTank = new CharacteristicTank(ConstantValue.HP_LIGHT, 1, 1,
                        ConstantValue.ATACK_RANGE_LIGHT, ConstantValue.ATACK_DAMAGE_LIGHT,
                        SkinTank.Light);
                    break;
            }

            return characterTank;
        }

        public static ActionPlayer[] GetRandomAction()
        {
            ActionPlayer[] actionEnemy = new ActionPlayer[ConstantValue.NUM_RND_ACTION_ENEMY];

            for (int numAction = 0; numAction < actionEnemy.Length; numAction++)
            {
                int tempAction = rndActionEnemy.Next(0, ConstantValue.NUM_RND_ACTION);

                switch (tempAction)
                {
                    case 0:
                        actionEnemy[numAction] = ActionPlayer.PressRight;
                        break;
                    case 1:
                        actionEnemy[numAction] = ActionPlayer.PressLeft;
                        break;
                    case 2:
                        actionEnemy[numAction] = ActionPlayer.PressUp;
                        break;
                    case 3:
                        actionEnemy[numAction] = ActionPlayer.PressDown;
                        break;
                    case 4:
                        actionEnemy[numAction] = ActionPlayer.PressUp;
                        break;
                    case 5:
                        actionEnemy[numAction] = ActionPlayer.PressDown;
                        break;
                    case 6:
                        actionEnemy[numAction] = ActionPlayer.PressRight;
                        break;
                    case 7:
                        actionEnemy[numAction] = ActionPlayer.PressRight;
                        break;
                    case 8:
                        actionEnemy[numAction] = ActionPlayer.PressUp;
                        break;
                    case 9:
                        actionEnemy[numAction] = ActionPlayer.PressLeft;
                        break;
                    case 10:
                        actionEnemy[numAction] = ActionPlayer.PressDown;
                        break;
                    case 11:
                        actionEnemy[numAction] = ActionPlayer.PressLeft;
                        break;
                    case 12:
                        actionEnemy[numAction] = ActionPlayer.PressLeft;
                        break;
                    case 13:
                        actionEnemy[numAction] = ActionPlayer.PressDown;
                        break;
                    case 14:
                        actionEnemy[numAction] = ActionPlayer.PressRight;
                        break;
                    case 15:
                        actionEnemy[numAction] = ActionPlayer.PressUp;
                        break;
                    case 16:
                        actionEnemy[numAction] = ActionPlayer.PressUp;
                        break;
                    case 17:
                        actionEnemy[numAction] = ActionPlayer.PressDown;
                        break;
                    case 18:
                        actionEnemy[numAction] = ActionPlayer.PressDown;
                        break;
                    case 19:
                        actionEnemy[numAction] = ActionPlayer.PressRight;
                        break;
                    case 20:
                        actionEnemy[numAction] = ActionPlayer.PressLeft;
                        break;
                }
            }

            return actionEnemy;
        }

        public static void ChangeActionEnemy(ref ActionPlayer action)
        {
            switch (action)
            {
                case ActionPlayer.PressRight:
                    action = ActionPlayer.PressUp;
                    break;
                case ActionPlayer.PressLeft:
                    action = ActionPlayer.PressDown;
                    break;
                case ActionPlayer.PressUp:
                    action = ActionPlayer.PressLeft;
                    break;
                case ActionPlayer.PressDown:
                    action = ActionPlayer.PressRight;
                    break;
            }
        }

        public static Direction ChangeDirectionEnemy(Direction dir)
        {
            Direction resultDirection = Direction.NoDirection;

            switch (dir)
            {
                case Direction.Right:
                    resultDirection = Direction.Up;
                    break;
                case Direction.Left:
                    resultDirection = Direction.Down;
                    break;
                case Direction.Up:
                    resultDirection = Direction.Left;
                    break;
                case Direction.Down:
                    resultDirection = Direction.Right;
                    break;
            }

            return resultDirection;
        }

        public static ActionPlayer GetActionByDirection(Direction inputDirection)
        {
            ActionPlayer resultAction = ActionPlayer.NoAction;

            switch (inputDirection)
            {
                case Direction.Right:
                    resultAction = ActionPlayer.PressRight;
                    break;
                case Direction.Left:
                    resultAction = ActionPlayer.PressLeft;
                    break;
                case Direction.Up:
                    resultAction = ActionPlayer.PressUp;
                    break;
                case Direction.Down:
                    resultAction = ActionPlayer.PressDown;
                    break;
            }

            return resultAction;
        }
    }
}
