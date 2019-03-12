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



public class game1 : MonoBehaviour
{
	string[] words = new string[] { "clack", "clock", "click", "cluck"};

	void InitializeGUIDs()
	{
		Datasack[] GUID_Datasacks = new Datasack[] {
			DSM.GUID,
			DSM.Folder.GUID,
			DSM.Folder_Subfolder.GUID,
		};

		foreach( var dataSack in GUID_Datasacks)
		{
			if (dataSack.Value.Length < 10)
			{
				dataSack.Value = System.Guid.NewGuid().ToString().Substring( 0, 10);
			}
		}
	}

	void Start()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene( "audio",
			UnityEngine.SceneManagement.LoadSceneMode.Additive);

		InitializeGUIDs();

		Debug.Log( GetType() + ".Start():");
		Debug.Log( DSM.Timer1.ToString());
		Debug.Log( DSM.Timer1Start.ToString());
	}

	void	NewGame()
	{
		DSM.PlayerState.Score.iValue = 0;

		DSM.GameRunning.bValue = true;

		DSM.Timer1.fValue = DSM.Timer1Start.fValue;

		if (DSM.HardMode.bValue)
		{
			DSM.Timer1.fValue /= 2;
		}

		DSM.LastWord.Value = "-word-";
	}

	void	AddPoints()
	{
		DSM.PlayerState.Score.iValue += Random.Range (10, 20);

		DSM.LastWord.Value = words[ Random.Range( 0, words.Length)];
	}

	void	GameOver()
	{
		DSM.GameRunning.bValue = false;

		if (DSM.PlayerState.Score.iValue > DSM.HighScore.iValue)
		{
			DSM.HighScore.iValue = DSM.PlayerState.Score.iValue;
		}
	}

	void	Update()
	{
		if (DSM.GameRunning.bValue)
		{
			DSM.Timer1.fValue -= Time.deltaTime;

			if (DSM.Timer1.fValue < 0)
			{
				GameOver();
			}
		}
	}

	void	OnUserIntent( Datasack ds)
	{
		Debug.Log (GetType () + ".OnUIIntent(): intent: " + ds.Value);

		DSM.AudioSourceClick.Poke();

		switch( ds.Value)
		{
		case "ButtonNewgame":
			NewGame ();
			break;

		case "ButtonAddpoints":
			AddPoints ();
			break;

		case "ButtonGameover":
			GameOver ();
			break;

		case "InputField":
			DSM.AudioSourceEnter.Poke();
			Debug.Log( "Entered:" + DSM.InputtedData.Value);
			break;

		case "ButtonPopup1":
			DSM.Popup1.Header.Value = "Answer Me!";
			DSM.Popup1.Message.Value = "WHAT is the airspeed velocity of an unladen swallow?";
			DSM.Popup1.Button1Text.Value = "African";
			DSM.Popup1.Button2Text.Value = "European";
			DSM.Popup1.Result.Value = "<none>";
			DSM.Popup1.Result.OnChangedOnceOnly += (xxx) => {
				Debug.Log( "Result was " + xxx.Value);
			};
			popup1.Activate();
			break;

		case "ButtonPopup2":
			popup2.Activate();
			break;

		default :
			Debug.LogWarning (GetType () + ".OnUIIntent(): unknown intent: " + ds.Value);
			break;
		}
	}

	void	OnEnable()
	{
		DSM.UserIntent.OnChanged += OnUserIntent;
	}

	void	OnDisable()
	{
		if (!DSM.shuttingDown)
		{
			DSM.UserIntent.OnChanged -= OnUserIntent;
		}
	}
}
