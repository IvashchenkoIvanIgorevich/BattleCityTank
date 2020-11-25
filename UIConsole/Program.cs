using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

using _20200613_TankLibrary;
using CommonLib;

namespace UIConsole
{
    class Program
    {
        //TODO: - do data base

        static void Main(string[] args)
        {
            bool exit = false;
            ulong gameTime = 0;
            bool init = true;            

            GameField newGameField = new GameField(ConstantValue.WIDTH_GAMEFIELD,
                ConstantValue.HEIGHT_GAMEFIELD);

            ILoader initGame = null;

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

            if (actionPlayer == ActionPlayer.PressLoad)
            {
                init = false;
            }

            if (ActionPlayer.PressEnter.HasFlag(actionPlayer)
                || ActionPlayer.PressLoad.HasFlag(actionPlayer))
            {
                //controller.StartMainMusic();

                if (init)
                {
                    initGame = new InitialisationNewGame(newGameField);
                }
                else
                {
                    initGame = new InitialisationSaveGame(newGameField);
                }

                newGameField = initGame.LoadGame();
                controller.SetGameField = newGameField;     // после загрузки в LoadGame() newGameField не передеается в UIController
                viewConsole.PrintGameBorder();
                viewConsole.PrintRigthBar();
            }

            Stopwatch watch = new Stopwatch();

            watch.Start();

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

                    if (actionPlayer == ActionPlayer.PressFire) 
                    {
                        controller.ShotPlayer(gameTime);
                    }
                }

                if (gameTime % ConstantValue.TIME_MOVE_ENEMY == 0)    // moving enemies on game field
                {
                    if (gameTime % ConstantValue.TIME_DIRECT_ENEMY == 0)    // get random direction to enemies
                    {
                        actionEnemy = GameManager.GetRandomAction();                        
                    }

                    controller.MoveEnemy(actionEnemy);
                }

                if (gameTime % ConstantValue.TIME_CREATE_BULLET == 0)    // shot enemies
                {
                    controller.ShotEnemies();
                }

                if (actionPlayer == ActionPlayer.PressSave)    // take screenshot and save game to *.dat
                {
                    viewConsole.PrintGameFieldToFile();
                    controller.SaveGame();
                }

                if (gameTime % ConstantValue.TIME_MOVE_BULLET == 0)    // moving bullets on game field
                {
                    controller.MoveBullets();
                }

                if (controller.CheckEndGame(out bool result))
                {
                    watch.Stop();

                    if (result)
                    {
                        string score = viewConsole.GetViewScore(watch.Elapsed.Minutes, watch.Elapsed.Seconds);
                        viewConsole.PrintMessageTheGame("YOU WON", score, "PRESS \"ESC\" TO EXIT");
                    }
                    else
                    {
                        string score = string.Empty;
                        viewConsole.PrintMessageTheGame("YOU LOSE", score, ""); 
                    }

                    exit = true;
                }

                if ((gameTime % ConstantValue.TIME_CREATE_NEW_ENEMY == 0)    // moving bullets on game field
                    && (gameTime != 0))
                {
                    controller.CreateNewEnem();
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
