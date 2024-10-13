/*
	The following license supersedes all notices in the source code.

	Copyright (c) 2024 Kurt Dekker/PLBM Games All rights reserved.

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
using UnityEngine.UI;

// This is a bit more complicated control of a single
// datasack using two up/down increment/decrement buttons.
//
// Here is what you need to stand it up:
//
//	- make the Datasack you want to control (change integer up / down)
//
//	- make a UI region thingy: box, whatever
//	- put this script on that top level UI thingy
//	- drag the Datasack reference into this script
//
//	- make two child Buttons
//	- drag the Button references into this script's fields
//	- set the Minimum / Maximum values up properly
//
//	- make a child Text within it to show the value
//	- put a DSTextDisplayInt to display the Datasack
//	- put formatting in that DSTextDisplayInt
//

public class DSButtonIncAndDec : MonoBehaviour
{
	[Header( "You must drag these in.")]
	public Button ButtonMinus;
	public Button ButtonPlus;

	public Datasack dataSack;

	public int Minimum = 1;
	public int Maximum = 5;

	void Start ()
	{
		ButtonMinus.onClick.AddListener( delegate {
			Change(-1);
		});
		ButtonPlus.onClick.AddListener( delegate {
			Change(+1);
		});

		Change(0);
	}

	void Change( int direction)
	{
		int i = dataSack.iValue;

		i += direction;

		i = Mathf.Clamp( i, Minimum, Maximum);

		dataSack.iValue = i;
	}
}
