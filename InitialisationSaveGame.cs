using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using CommonLib;

namespace _20200613_TankLibrary
{
    public class InitialisationSaveGame : ILoader
    {
        #region ===--- Data ---===

        private GameField _field;

        #endregion

        #region ===--- Constructor ---===

        public InitialisationSaveGame(GameField field)
        {
            _field = field;
        }

        #endregion

        #region ===--- Implementation Interface ---===

        public GameField LoadGame()
        {
            BinaryFormatter binFotmatter = new BinaryFormatter();

            using (FileStream streamLoad = new FileStream(ConstantValue.PATH_FILE_SAVE_GAME, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                _field = (GameField)binFotmatter.Deserialize(streamLoad);
            }

            return _field;
        }

        #endregion
    }
}
