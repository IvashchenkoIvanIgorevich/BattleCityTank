using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _20200613_TankLibrary;
using CommonLib;

namespace UIConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            ulong gameTime = 0;

            GameField newGameField = new GameField(ConstantValue.WIDTH_GAMEFIELD,
                ConstantValue.HEIGHT_GAMEFIELD);

            UIController controller = new UIController(newGameField);
            UIView viewConsole = new UIView(controller);

            ActionPlayer actionPlayer = ActionPlayer.NoAction;
            ActionPlayer[] actionEnemy = new ActionPlayer[ConstantValue.NUM_RND_ACTION_ENEMY];

            viewConsole.PrintStartWindow();

            do
            {
                actionPlayer = controller.GetActionPlayer(Console.ReadKey(true).Key);
            }
            while ((actionPlayer & ActionPlayer.StartAction) == 0);

            if (ActionPlayer.PressEnter.HasFlag(actionPlayer))
            {
                Initialisator initGame = new Initialisator(newGameField);
                viewConsole.PrintGameBorder();
                //viewGame.PrintGameField(initGame.Field);
                //viewGame.PrintRigthBar(initGame.Field);
                //viewGame.PrintStatistic(initGame.Field,(PlayerTank)initGame.Field[initGame.Field.GetPlayerPosition(), initGame.Field.GetPlayerPosY()]);
            }

            do
            {
                if (actionPlayer == ActionPlayer.PressExit)
                {
                    break;
                }

                if (Console.KeyAvailable)
                {
                    actionPlayer = controller.GetActionPlayer(Console.ReadKey(true).Key);

                    if (actionPlayer == ActionPlayer.PressPause)
                    {
                        viewConsole.PrintMessageTheGame("PAUSE!!!", "PRESS ANY KEY TO CONTINUE");
                    }

                    if ((actionPlayer & ActionPlayer.MoveAction) > 0)
                    {
                        controller.MovePlayer(actionPlayer);
                    }
                }

                if (gameTime % ConstantValue.TIME_MOVE_ENEMY == 0)
                {
                    if (gameTime % ConstantValue.TIME_DIRECT_ENEMY == 0)
                    {
                        actionEnemy = GameManager.GetRandomAction();                        
                    }

                    controller.MoveEnemy(actionEnemy);
                }

                viewConsole.PrintGameField();

                ++gameTime;
                System.Threading.Thread.Sleep(ConstantValue.TIME_SLEEP);
            } 
            while ((actionPlayer != ActionPlayer.PressExit) && (!exit));

            Console.ReadKey();
        }
    }
}
