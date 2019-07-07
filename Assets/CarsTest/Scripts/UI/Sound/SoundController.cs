using System;
using UnityEngine;

namespace CarsTest
{
	public class SoundController : MonoBehaviour
	{
		[SerializeField] private AudioClip[] clips;

		[SerializeField] private AudioSource source;
		[SerializeField] private AudioSource loopSource;

		private void Awake()
		{
			OnToggleEffects(Scores.Instance.IsEffects);
		}

		private void OnEnable()
		{
			SoundManager.PlaySoundClipAction += OnPlaySoundClip;
			SoundManager.MuteAction += OnMute;
			SoundManager.ToggleEffects += OnToggleEffects;
			SoundManager.StopSoundAction += OnStopSound;
		}

		private void OnDisable()
		{
			SoundManager.PlaySoundClipAction -= OnPlaySoundClip;
			SoundManager.MuteAction -= OnMute;
			SoundManager.ToggleEffects -= OnToggleEffects;
			SoundManager.StopSoundAction -= OnStopSound;
		}

		private void OnPlaySoundClip(AudioClip soundClip, float volume, bool loop)
		{
			if (!Scores.Instance.IsEffects)
				return;

			if (loop)
			{
				loopSource.clip = soundClip;
				loopSource.volume = volume;
				loopSource.Play();
			}
			else
			{
				source.PlayOneShot(soundClip, volume);
			}
		}

		private void OnStopSound()
		{
			loopSource.Stop();
		}

		private void OnMute(bool mute)
		{
			source.mute = mute;
			loopSource.mute = mute;
		}

		private void OnToggleEffects(bool enable)
		{
			if (enable) return;
			source.Stop();
			loopSource.Stop();
		}
	}
}