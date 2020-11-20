using CommonLib;
using System.Threading;

namespace UIConsole
{
    public class UISound
    {
        private Sound _soundStartGame;
        private Sound _soundMove;
        private Sound _soundShot;
        private Sound _soundWorkEngine;
        private Sound _soundDeadEnemy;
        private Sound _soundDeadPlayer;

        public UISound()
        {
            _soundStartGame = new Sound(ConstantValue.PATH_FILE_SOUND_GAME);
            _soundMove = new Sound(ConstantValue.PATH_FILE_SOUND_MOVE);
            _soundShot = new Sound(ConstantValue.PATH_FILE_SOUND_SHOOT);
            _soundWorkEngine = new Sound(ConstantValue.PATH_FILE_SOUND_ENGINE);
            _soundDeadEnemy = new Sound(ConstantValue.PATH_FILE_SOUND_DEAD_ENEMY);
            _soundDeadPlayer = new Sound(ConstantValue.PATH_FILE_SOUND_DEAD_PLAYER);
        }

        public void StartGameSound()
        {
            if (!_soundStartGame.ThreadSound.IsAlive)
            {
                _soundStartGame.ThreadSound.Start();
            }
        }

        public void MovePlayerSound()
        {
            _ = _soundMove.ThreadSound.ThreadState;

            //if (_soundMove.ThreadSound.ThreadState.HasFlag(ThreadState.Stopped & ThreadState.Unstarted))
            //{
            //    _soundMove.ThreadSound.Start();
            //}
        }

        public void ShootPlayerSound()
        {
            //if (!_soundShot.ThreadSound.IsAlive)
            //{
            //    _soundShot.ThreadSound.Start();
            //}
        }
    }
}
