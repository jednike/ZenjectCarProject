using System;
using UnityEngine;

namespace CarsTest
{
	[Serializable]
	public class Scores
	{
		public static GameScores Instance
		{
			get =>
				_instance ?? (_instance = PlayerPrefs.HasKey("GameSave")
					? JsonUtility.FromJson<GameScores>(PlayerPrefs.GetString("GameSave"))
					: new GameScores());
			set => _instance = value;
		}

		private static GameScores _instance;

		public static void Save()
		{
			Instance.LastSaveDate = DateTime.Now;
			var saveString = JsonUtility.ToJson(Instance);
			PlayerPrefs.SetString("GameSave", saveString);
		}

		public static void LoadSave(GameScores scores)
		{
			Instance = scores;
		}

		public static GameScores StringToData(string saveString)
		{
			var scores = JsonUtility.FromJson<GameScores>(saveString);
			return scores;
		}
	}
}