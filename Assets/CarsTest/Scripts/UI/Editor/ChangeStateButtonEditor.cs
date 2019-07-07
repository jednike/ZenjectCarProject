using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CarsTest
{
	[CustomEditor(typeof(ChangeStateButton))]
	public class ChangeStateButtonEditor : Editor
	{
		private SerializedProperty _toLastState;
		private SerializedProperty _nextState;

		protected void OnEnable()
		{
			_nextState = serializedObject.FindProperty("nextState");
			_toLastState = serializedObject.FindProperty("toLastState");
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			serializedObject.Update();

			if (!_toLastState.boolValue)
				EditorGUILayout.PropertyField(_nextState);

			serializedObject.ApplyModifiedProperties();
		}
	}
}