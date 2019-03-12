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

public class popup2 : MonoBehaviour
{
	const string s_SceneName = "popup2";

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
		case "Popup2Dismiss":

			Debug.Log( GetType() + ": Popup2 dismissed");

			Dismiss();
			Destroy(this);
			break;
		}
	}

	void OnTouchCoordinates( Datasack ds)
	{
		Debug.Log( "OnTouchCoordinates:" + ds.v2Value);
	}

	void OnEnable()
	{
		// Use this method to let the underlying script(s) continue to receive events
		// DSM.UserIntent.OnChanged += OnUserIntent;
		// But we want to take over the UserIntent entirely since we are a modal popup.
		DSM.UserIntent.PushOnChanged( OnUserIntent);

		DSM.Popup2.TouchCoordinates.OnChanged += OnTouchCoordinates;
	}
	void OnDisable()
	{
		// see note above
		// DSM.UserIntent.OnChanged -= OnUserIntent;
		DSM.UserIntent.PopOnChanged();

		DSM.Popup2.TouchCoordinates.OnChanged -= OnTouchCoordinates;
	}
}
