using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CarsTest
{
	[RequireComponent(typeof(Button))]
	public class ChangeStateButton : MonoBehaviour
	{
		[HideInInspector] [SerializeField] private GameState nextState;
		[SerializeField] private bool toLastState;

		private GameStateManager _gameStateManager;
		[Inject]
		public void Constructor(GameStateManager gameStateManager)
		{
			_gameStateManager = gameStateManager;
		}

		private void Awake()
		{
			if (nextState != GameState.None || toLastState)
				GetComponent<Button>().onClick.AddListener(ChangeState);
		}

		private void ChangeState()
		{
			if (toLastState)
				ChangeStateToLast();
			else
				ChangeStateTo(nextState);
		}

		public void ChangeStateTo(GameState newState)
		{
			_gameStateManager.ChangeState(newState);
		}

		public void ChangeStateToLast()
		{
			_gameStateManager.ToLastState();
		}

		public void ChangeStateTo(string newState)
		{
			var state = (GameState) Enum.Parse(typeof(GameState), newState, true);
			ChangeStateTo(state);
		}
	}
}