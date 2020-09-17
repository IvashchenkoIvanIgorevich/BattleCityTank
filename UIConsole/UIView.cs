using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using CommonLib;

namespace UIConsole
{
    class UIView
    {
        #region ===--- Dataset ---===

        private char[,] _buffer = new char[ConstantValue.HEIGHT_GAMEFIELD,
            ConstantValue.WIDTH_GAMEFIELD];
        private UIController _controller;

        #endregion

        #region ===--- Constructor ---===

        public UIView(UIController controller)
        {
            _controller = controller;
        }

        #endregion

        #region ===--- PrintMessage ---===

        public void PrintMessageTheGame(string firstStr, string secondStr,
            string other = "")
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(ConstantValue.CUR_LEFT_PAUSE,
                ConstantValue.CUR_TOP_PAUSE);
            Console.Write(firstStr);
            Console.SetCursorPosition(ConstantValue.CUR_TOP_PAUSE + 2,
                ConstantValue.CUR_TOP_PAUSE + ConstantValue.SHIFT_START_WINDOW);
            Console.Write(secondStr);
            Console.SetCursorPosition(ConstantValue.CUR_TOP_PAUSE + 2,
                ConstantValue.CUR_TOP_PAUSE + ConstantValue.SHIFT_START_WINDOW + 5);
            Console.Write(other);
            Console.ReadKey(true);

            Console.ForegroundColor = Console.BackgroundColor;
            Console.SetCursorPosition(ConstantValue.CUR_LEFT_PAUSE,
                ConstantValue.CUR_TOP_PAUSE);
            Console.Write(firstStr);
            Console.SetCursorPosition(ConstantValue.CUR_TOP_PAUSE + 2,
                ConstantValue.CUR_TOP_PAUSE + ConstantValue.SHIFT_START_WINDOW);
            Console.Write(secondStr);
            Console.SetCursorPosition(ConstantValue.CUR_TOP_PAUSE + 2,
                ConstantValue.CUR_TOP_PAUSE + ConstantValue.SHIFT_START_WINDOW + 5);
            Console.Write(other);
            Console.ResetColor();
        }

        #endregion

        #region ===--- PrintStartWindow ---===

        public void PrintStartWindow()
        {
            try
            {
                Console.SetWindowSize(ConstantValue.BUFFER_HEIGHT, ConstantValue.BUFFER_WIDTH);
                Console.SetBufferSize(ConstantValue.BUFFER_HEIGHT, ConstantValue.BUFFER_WIDTH);
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("В свойствах даного окна консоли, измените <Шрифт>" +
                    " на <Точечный> <Размер> 8x9.\n И перезапустите проект.");
                Console.ReadKey();
            }

            Console.SetWindowSize(ConstantValue.WIDTH_GAMEFIELD +
                ConstantValue.WIDTH_RIGHT_BAR, ConstantValue.HEIGHT_GAMEFIELD);
            Console.SetBufferSize(ConstantValue.WIDTH_GAMEFIELD +
                ConstantValue.WIDTH_RIGHT_BAR, ConstantValue.HEIGHT_GAMEFIELD + 1);
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, ConstantValue.HEIGHT_GAMEFIELD / 4);

            int curLeft = Console.CursorLeft;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"         ____       _______ _______ _      ______        _____ _____ _________     __ ");
            Console.WriteLine(@"        |  _ \   /\|__   __|__   __| |    |  ____|      / ____|_   _|__   __\ \   / / ");
            Console.WriteLine(@"        | |_) | /  \  | |     | |  | |    | |__        | |      | |    | |   \ \_/ /  ");
            Console.WriteLine(@"        |  _ < / /\ \ | |     | |  | |    |  __|       | |      | |    | |    \   /   ");
            Console.WriteLine(@"        | |_) / ____ \| |     | |  | |____| |____      | |____ _| |_   | |     | |    ");
            Console.WriteLine(@"        |____/_/    \_\_|     |_|  |______|______|      \_____|_____|  |_|     |_|    ");
            Console.WriteLine(@"                           _____  ______ ____   ____  _____  _   _                    ");
            Console.WriteLine(@"                          |  __ \|  ____|  _ \ / __ \|  __ \| \ | |                   ");
            Console.WriteLine(@"                          | |__) | |__  | |_) | |  | | |__) |  \| |                   ");
            Console.WriteLine(@"                          |  _  /|  __| |  _ <| |  | |  _  /|     |                   ");
            Console.WriteLine(@"                          | | \ \| |____| |_) | |__| | | \ \| |\  |                   ");
            Console.WriteLine(@"                          |_|  \_\______|____/ \____/|_|  \_\_| \_|                   ");
            Console.WriteLine();

            int topStart = ConstantValue.HEIGHT_GAMEFIELD / 3 + ConstantValue.SHIFT_START_WINDOW + 4;
            int topExit = ConstantValue.HEIGHT_GAMEFIELD / 3 + ConstantValue.SHIFT_START_WINDOW + 6;
            int topPause = ConstantValue.HEIGHT_GAMEFIELD / 3 + ConstantValue.SHIFT_START_WINDOW + 8;

            Console.SetCursorPosition(ConstantValue.HEIGHT_GAMEFIELD / 2 - 1, topStart);
            Console.WriteLine("PRESS \"ENTER\" TO START");
            Console.WriteLine();
            Console.SetCursorPosition(ConstantValue.HEIGHT_GAMEFIELD / 2, topExit);
            Console.WriteLine("PRESS \"ESC\" TO EXIT");
            Console.SetCursorPosition(ConstantValue.HEIGHT_GAMEFIELD / 2 + 1, topPause);
            Console.WriteLine("PRESS \"P\" TO PAUSE");
            Console.SetCursorPosition(ConstantValue.HEIGHT_GAMEFIELD / 2 - 3, topPause
                + ConstantValue.TOP_RIGHT_ARROW);
            Console.WriteLine("PRESS: \"\u2192\" TO MOVE RIGHT ");
            Console.SetCursorPosition(ConstantValue.HEIGHT_GAMEFIELD / 2 - 3, topPause
                + ConstantValue.TOP_LEFT_ARROW);
            Console.WriteLine("       \"\u2190\" TO MOVE LEFT ");
            Console.SetCursorPosition(ConstantValue.HEIGHT_GAMEFIELD / 2 - 3, topPause
                + ConstantValue.TOP_UP_ARROW);
            Console.WriteLine("       \"\u2191\" TO MOVE UP ");
            Console.SetCursorPosition(ConstantValue.HEIGHT_GAMEFIELD / 2 - 3, topPause
                + ConstantValue.TOP_DOWN_ARROW);
            Console.WriteLine("       \"\u2193\" TO MOVE DOWN ");
            Console.SetCursorPosition(ConstantValue.HEIGHT_GAMEFIELD / 2 - 3, topPause
                + ConstantValue.TOP_FIRE);
            Console.WriteLine("       \"SPACE\" TO FIRE ");
            Console.SetCursorPosition(ConstantValue.HEIGHT_GAMEFIELD / 2 - 3, topPause
                + ConstantValue.TOP_LOAD);
            Console.WriteLine("       \"L\" TO LOAD THE GAME ");
            Console.ResetColor();
        }

        #endregion

        #region ===--- PrintGameBorder ---===

        public void PrintGameBorder()
        {
            Console.SetCursorPosition(0, 0);

            for (int posX = 0; posX < ConstantValue.HEIGHT_GAMEFIELD; posX++)
            {
                for (int posY = 0; posY < ConstantValue.WIDTH_GAMEFIELD; posY++)
                {
                    Console.SetCursorPosition(posY, posX);

                    if ((posY > 0) && (posY < (ConstantValue.WIDTH_GAMEFIELD - 1))
                        && (posX > 0) && (posX < (ConstantValue.HEIGHT_GAMEFIELD - 1)))
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write('\x2588');
                    }
                }
            }
        }

        #endregion

        #region ===--- PrintGameField ---===

        public void PrintGameField()
        {
            for (int row = 1; row < ConstantValue.HEIGHT_GAMEFIELD - 1; row++)
            {
                for (int col = 1; col < ConstantValue.WIDTH_GAMEFIELD - 1; col++)
                {
                    if (_buffer[row, col] != _controller[row, col])
                    {
                        Console.SetCursorPosition(col, row);

                        Console.Write(_controller[row, col]);

                        _buffer[row, col] = _controller[row, col];
                    }
                }
            }

            //Console.ResetColor();
        }

        public void PrintGameFieldToFile()
        {
            using (StreamWriter sw = File.CreateText(ConstantValue.PATH_FILE))
            {
                for (int row = 1; row < ConstantValue.HEIGHT_GAMEFIELD - 1; row++)
                {
                    for (int col = 1; col < ConstantValue.WIDTH_GAMEFIELD - 1; col++)
                    {
                        sw.Write(_controller[row, col]);
                    }

                    sw.WriteLine();
                }
            }
        }

        #endregion
    }
}
