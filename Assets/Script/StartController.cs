﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour {

	public void OnStartClick()
	{
		SceneManager.LoadScene("Level1");
	}
	public void OnMapClick()
	{
		SceneManager.LoadScene("Map");
	}
	public void OnExitClick()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();		
	}
}
