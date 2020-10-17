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
    [Serializable]
    public class SaveGame
    {
        private GameField _field;

        public SaveGame(GameField field)
        {
            _field = field;            
        }

        public void SaveTheGame()
        {
            BinaryFormatter binFotmatter = new BinaryFormatter();

            using (FileStream streamSave = new FileStream(ConstantValue.PATH_FILE_SAVE_GAME, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFotmatter.Serialize(streamSave, _field);
            }
        }
    }
}
