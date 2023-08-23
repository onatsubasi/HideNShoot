#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(PlayerChanger))]
public class PlayerChangerEditor : Editor
{
	
	public override void OnInspectorGUI()
	{
		
		PlayerChanger myScript = (PlayerChanger)target;
		
		DrawDefaultInspector();
		
		EditorGUILayout.LabelField("Player Type: " , myScript.VRPlayer.activeSelf ? "VR" : "PC");
		
		if(GUILayout.Button("Change Player Type"))
		{
			
			myScript.ChangePlayerType();
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
			
		}
		
	}
	
	
	
    
}


#endif