using UnityEngine;

namespace CarsTest
{
    public class AudioPlayer
    {
        private readonly AudioSource _source;

        public AudioPlayer(AudioSource source)
        {
            _source = source;
        }

        public void Play(AudioClip clip, float volume)
        {
            _source.PlayOneShot(clip, volume);
        }
    }
}