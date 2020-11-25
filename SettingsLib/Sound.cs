using System.Threading;
using System.Media;

namespace CommonLib
{
    public class Sound
    {
        public SoundPlayer SoundPlay { get; set; }
        public readonly Thread ThreadSound;

        public Sound(string soundLocation)
        {
            SoundPlay = new SoundPlayer(soundLocation);
            //ThreadSound = new Thread(_soundPlay.Play);
        }
    }
}
