/*
	The following license supersedes all notices in the source code.

	Copyright (c) 2019 Kurt Dekker/PLBM Games All rights reserved.

	http://www.twitter.com/kurtdekker

	Redistribution and use in source and binary forms, with or without
	modification, are permitted provided that the following conditions are
	met:

	Redistributions of source code must retain the above copyright notice,
	this list of conditions and the following disclaimer.

	Redistributions in binary form must reproduce the above copyright
	notice, this list of conditions and the following disclaimer in the
	documentation and/or other materials provided with the distribution.

	Neither the name of the Kurt Dekker/PLBM Games nor the names of its
	contributors may be used to endorse or promote products derived from
	this software without specific prior written permission.

	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS
	IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED
	TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
	PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
	HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
	SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
	TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
	PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
	LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
	NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
	SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runtimeinspector : MonoBehaviour
{
	const string s_SceneName = "runtimeinspector";

	static System.Action OnDismiss;

	public	static	void	Activate( System.Action _OnDismiss = null)
	{
		OnDismiss = _OnDismiss;
		UnityEngine.SceneManagement.SceneManager.LoadScene(
			s_SceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
	}

	public	static	void	Dismiss()
	{
		UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync( s_SceneName);

		OnDismiss();
	}

	List<Datasack> CurrentlyDisplayedDatasacks;

	void Start ()
	{
		CurrentlyDisplayedDatasacks = new List<Datasack>();

		// for now we'll just stick them all in here
		foreach( var ds in DSM.I.Enumerate())
		{
			CurrentlyDisplayedDatasacks.Add( ds);
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Dismiss();
		}
	}

	Rect RECT( float x, float y, float w, float h)
	{
		return new Rect(
			x * Screen.width, y * Screen.height,
			w * Screen.width, h * Screen.height);
	}

	void OnGUI()
	{
		GUI.skin = ScaledSkin;

		// dim out what's behind us
		GUI.color = new Color( 0,0,0, 0.4f);
		GUI.DrawTexture( RECT( 0,0,1,1), t2d_white32x32);

		float TOP = 0.1f;

		GUI.color = Color.white;
		GUI.Label( RECT( 0, 0, 1, TOP), "Datasack Runtime Inspector");
		GUI.Label( RECT( 0, TOP, 1, TOP), "Tap anywhere to dismiss.");
	}

	Texture2D t2d_white32x32;

	public GUISkin	ReferenceGUISkin;
	GUISkin ScaledSkin;

	void CreateSkin()
	{
		if (ScaledSkin) Destroy(ScaledSkin);

		if (ReferenceGUISkin)
		{
			ScaledSkin = Instantiate<GUISkin>( ReferenceGUISkin);

			// <WIP> scale up the important bits
		}
	}

	void OnEnable()
	{
		t2d_white32x32 = new Texture2D( 32, 32);

		var pixels = t2d_white32x32.GetPixels();

		for (int i =  0; i < pixels.Length; i++)
		{
			pixels[i] = Color.white;
		}

		t2d_white32x32.SetPixels( pixels);
		t2d_white32x32.Apply();

		CreateSkin();
	}

	void OnDisable()
	{
		if (t2d_white32x32) Destroy( t2d_white32x32);
		if (ScaledSkin) Destroy( ScaledSkin);
	}
}
