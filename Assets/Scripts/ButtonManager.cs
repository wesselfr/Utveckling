using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public int SceneNumber;
	private int sceneID;
	private int LoadSceneID;

	void Update ()
	{
		sceneID = SceneManager.GetActiveScene().buildIndex;
	}
	public void LoadLevel ()
	{
		SceneManager.LoadScene (SceneNumber);
	}
	public void QuitGame ()
	{
		Application.Quit ();
	
	}
	public void NextLevel ()
	{
		LoadSceneID = sceneID + 1;
		SceneManager.LoadScene (LoadSceneID);
	}
}
