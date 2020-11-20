using System.Threading;
using System.Media;

namespace CommonLib
{
    public class Sound
    {
        private readonly SoundPlayer _soundPlay;
        public readonly Thread ThreadSound;

        public Sound(string soundLocation)
        {
            _soundPlay = new SoundPlayer(soundLocation);
            ThreadSound = new Thread(_soundPlay.Play);
        }
    }
}
