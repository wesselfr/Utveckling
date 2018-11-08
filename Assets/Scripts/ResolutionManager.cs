using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour 
{
	[SerializeField]
	Resolution[] resolutions;
	[SerializeField]
	Text resText;
	int CurrentResolution = 0;
	public void Start()
	{
		resolutions = Screen.resolutions;
		ShowResolution ();
	}
	public void ChangeFullscreen ()
	{
		Screen.fullScreen = !Screen.fullScreen;
	}
	public void ChangeResolution ()
	{
		CurrentResolution += 1;
		if (CurrentResolution >= resolutions.Length) 
		{
			CurrentResolution = 0;
		}
		Screen.SetResolution (resolutions [CurrentResolution].width, resolutions [CurrentResolution].height, Screen.fullScreen);
		ShowResolution ();
	}
	public void ShowResolution ()
	{
		resText.text = "Current resolution: " + (resolutions [CurrentResolution].width) + "x" + (resolutions [CurrentResolution].height);
	}
}