using CommonLib;
using System.Threading;
using System.Media;
using System;

namespace UIConsole
{
    public class UISound
    {
        private Sound _soundStartGame;
        private Sound _soundMove;
        private Sound _soundShot;
        private Sound _soundDeadEnemy;
        private Sound _soundDeadPlayer;

        public UISound()
        {
            _soundStartGame = new Sound(ConstantValue.PATH_FILE_SOUND_GAME);
            _soundMove = new Sound(ConstantValue.PATH_FILE_SOUND_MOVE);
            _soundShot = new Sound(ConstantValue.PATH_FILE_SOUND_SHOOT);
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
            //_ = _soundMove.ThreadSound.ThreadState;

            Thread th = new Thread(_soundMove.SoundPlay.Play);
            th.Start();

            //if (_soundMove.ThreadSound.ThreadState.HasFlag(ThreadState.Stopped & ThreadState.Unstarted))
            //{
            //    _soundMove.ThreadSound.Start();
            //}
        }

        public void ShootPlayerSound()
        {
            Thread th = new Thread(_soundShot.SoundPlay.Play);
            th.Start();

            //if (!_soundShot.ThreadSound.IsAlive)
            //{
            //    _soundShot.ThreadSound.Start();
            //}
        }

        public void DeadEnemySound()
        {
            Thread th = new Thread(_soundDeadEnemy.SoundPlay.Play);
            th.Start();            
        }

        public void DeadPlayerSound()
        {
            Thread th = new Thread(_soundDeadPlayer.SoundPlay.Play);
            th.Start();
        }
    }
}
