using System;
using UnityEngine;

namespace CarsTest
{
	public class MusicController : MonoBehaviour
	{
		[SerializeField] private AudioClip[] clips;
		[SerializeField] private AudioSource source;

		private void Awake()
		{
			OnToggleMusic(Scores.Instance.IsMusic);
			SoundManager.PlayMusic(clips[0]);
		}

		private void OnEnable()
		{
			SoundManager.PlayMusicAction += OnPlayMusic;
			SoundManager.MuteAction += OnMute;
			SoundManager.ToggleMusic += OnToggleMusic;
		}

		private void OnDisable()
		{
			SoundManager.PlayMusicAction -= OnPlayMusic;
			SoundManager.MuteAction -= OnMute;
			SoundManager.ToggleMusic -= OnToggleMusic;
		}

		private void OnToggleMusic(bool enable)
		{
			if (!enable)
				source.Pause();
			else
			{
				source.Play();
			}
		}

		private void OnMute(bool mute)
		{
			source.mute = mute;
		}

		private void OnPlayMusic(AudioClip audioClip, float volume)
		{
			if (audioClip == null)
				return;
			source.clip = audioClip;
			source.volume = volume;
			if (Scores.Instance.IsMusic)
				source.Play();
		}
	}
}