using System.IO;

using CommonLib;

namespace UIConsole
{
    public class ViewBase
    {
        #region ===--- Data ---===

        public char[,] ViewGameBase { get; private set; } = new char[ConstantValue.HEIGHT_BASE, ConstantValue.WIDTH_BASE];

        #endregion

        #region ===--- Constructor ---===

        public ViewBase(string filePath)
        {
            CreateViewBase(filePath);
        }

        #endregion

        #region ===--- Method ---===

        public void CreateViewBase(string filePath)
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
                        if (fileRow >= 0 && fileRow < ConstantValue.HEIGHT_BASE)
                        {
                            for (int col = 0; col < s.Length; col++)
                            {
                                ViewGameBase[row, col] = s[col];
                            }

                            row++;
                        }                        
                    }
                }
            }
        }

        #endregion
    }
}
