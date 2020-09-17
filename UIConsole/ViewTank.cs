using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using CommonLib;

namespace UIConsole
{
    public class ViewTank
    {
        #region ===--- Data ---===

        public char[,] ViewUp { get; private set; } = new char[ConstantValue.HEIGHT_TANK, ConstantValue.WIDTH_TANK];
        public char[,] ViewRight { get; private set; } = new char[ConstantValue.HEIGHT_TANK, ConstantValue.WIDTH_TANK];
        public char[,] ViewDown { get; private set; } = new char[ConstantValue.HEIGHT_TANK, ConstantValue.WIDTH_TANK];
        public char[,] ViewLeft { get; private set; } = new char[ConstantValue.HEIGHT_TANK, ConstantValue.WIDTH_TANK];

        #endregion

        #region ===--- Constructor ---===

        public ViewTank(string filePath)
        {
            CreateViewTank(filePath);
        }

        #endregion

        #region ===--- Method ---===

        public void CreateViewTank(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader sr = File.OpenText(filePath))    // open to read
                {
                    string s;
                    int fileRow = 0;
                    int row = 0;

                    while ((s = sr.ReadLine()) != null)
                    {
                        if (fileRow >= 0 && fileRow < ConstantValue.HEIGHT_TANK)
                        {
                            for (int col = 0; col < s.Length; col++)
                            {
                                ViewUp[row, col] = s[col];
                            }

                            row++;
                        }

                        if (fileRow >= ConstantValue.HEIGHT_TANK && fileRow < ConstantValue.HEIGHT_TANK * 2)
                        {
                            for (int col = 0; col < s.Length; col++)
                            {
                                ViewRight[row, col] = s[col];
                            }

                            row++;
                        }

                        if (fileRow >= ConstantValue.HEIGHT_TANK * 2 && fileRow < ConstantValue.HEIGHT_TANK * 3)
                        {
                            for (int col = 0; col < s.Length; col++)
                            {
                                ViewDown[row, col] = s[col];
                            }

                            row++;
                        }

                        if (fileRow >= ConstantValue.HEIGHT_TANK * 3 && fileRow < ConstantValue.HEIGHT_TANK * 4)
                        {
                            for (int col = 0; col < s.Length; col++)
                            {
                                ViewLeft[row, col] = s[col];
                            }

                            row++;
                        }

                        fileRow++;

                        if (row == 5)
                        {
                            row = 0;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
