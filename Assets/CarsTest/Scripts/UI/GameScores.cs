﻿using System;

 namespace CarsTest
 {
     [Serializable]
     public class GameScores
     {
         public bool AdsDisabled;
         public DateTime LastSaveDate = DateTime.MinValue;

         public string Language;

         public bool IsEffects
         {
             get => _isEffects;
             set
             {
                 _isEffects = value;
                 SoundManager.OnToggleEffects(_isEffects);
             }
         }
         private bool _isEffects = true;

         public bool IsMusic
         {
             get => _isMusic;
             set
             {
                 _isMusic = value;
                 SoundManager.OnToggleMusic(_isMusic);
             }
         }
         private bool _isMusic = true;

         public int SelectedCar;
         public int SelectedLevel;

     }
 }