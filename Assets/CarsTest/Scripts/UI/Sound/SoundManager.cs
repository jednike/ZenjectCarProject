﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace CarsTest
 {
	 public class SoundManager : MonoBehaviour
	 {
		 public static event Action<bool> MuteAction = delegate { };

		 public static event Action<AudioClip, float, bool> PlaySoundClipAction = delegate { };
		 public static event Action StopSoundAction = delegate { };

		 public static event Action<AudioClip, float> PlayMusicAction = delegate { };

		 public static event Action<float> SetMusicVolumeAction = delegate { };
		 public static event Action<float> SetSoundVolumeAction = delegate { };

		 public static event Action<bool> ToggleMusic = delegate { };
		 public static event Action<bool> ToggleEffects = delegate { };

		 public static void SetMute(bool mute)
		 {
			 MuteAction(mute);
		 }

		 public static void SetMusicVolume(float volume, float runTime)
		 {
			 SetMusicVolume(volume);
		 }

		 public static void SetMusicVolume(float volume)
		 {
			 SetMusicVolumeAction(volume);
		 }

		 public static void SetSoundVolume(float volume)
		 {
			 SetSoundVolumeAction(volume);
		 }

		 public static void PlayMusic(AudioClip music, float volume = 1f)
		 {
			 PlayMusicAction(music, volume);
		 }
		 public static void Play(AudioClip clip, float volume = 1f, bool loop = false)
		 {
			 PlaySoundClipAction(clip, volume, loop);
		 }
		 public static void StopSoundLooping()
		 {
			 StopSoundAction();
		 }
		 public static void OnToggleMusic(bool state)
		 {
			 ToggleMusic(state);
		 }
		 public static void OnToggleEffects(bool state)
		 {
			 ToggleEffects(state);
		 }
	 }
 }