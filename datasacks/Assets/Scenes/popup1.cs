using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup1 : MonoBehaviour
{
	const string s_SceneName = "popup1";

	public	static	void	Activate()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(
			s_SceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
	}

	public	static	void	Dismiss()
	{
		UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync( s_SceneName);
	}

	void OnUserIntent( Datasack ds)
	{
		switch( ds.Value)
		{
		case "PopupButton1":
		case "PopupButton2":
			Dismiss();
			DSM.Popup1Result.Value = ds.Value;
			Destroy(this);
			break;
		}
	}

	void OnEnable()
	{
		DSM.UISack.OnChanged += OnUserIntent;
	}
	void OnDisable()
	{
		DSM.UISack.OnChanged -= OnUserIntent;
	}
}
