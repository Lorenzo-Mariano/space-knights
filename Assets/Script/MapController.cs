using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour {

	public void On1Click()
	{
		SceneManager.LoadScene("Level1");
	}
	public void On2Click()
	{
		SceneManager.LoadScene("Level2");
	}
	public void On3Click()
	{
		SceneManager.LoadScene("Level3");
	}
	public void On4Click()
	{
		SceneManager.LoadScene("Level4");
	}
	public void OnExitClick()
	{
		SceneManager.LoadScene("Menu");
	}
}
