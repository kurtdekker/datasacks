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

// Put this where there is a Text and when the
// Datasack gets a string, it is sent here and
// shown for a brief duration, then fades out.

public class DSTextDisplayStringQuickFlash : MonoBehaviour
{
	public	Datasack	dataSack;

	public float FlashInterval = 0.6f;

	public bool RapidBlinking = true;

	const float BlinkInterval = 0.2f;
	const float BlinkOnDutyCycle = 0.1f;

	// nonzero means we are flashing
	float flashingTimer;

	private DSTextAbstraction _textAbstraction;
	private DSTextAbstraction textAbstraction
	{
		get
		{
			if (!_textAbstraction) _textAbstraction = DSTextAbstraction.Attach(this);
			return _textAbstraction;
		}
	}

	private void Update()
	{
		if (flashingTimer > 0)
		{
			bool onoff = (flashingTimer % BlinkInterval) < BlinkOnDutyCycle;

			flashingTimer += Time.deltaTime;

			if (!RapidBlinking)
			{
				onoff = true;
			}

			if (flashingTimer >= FlashInterval)
			{
				onoff = false;
			}

			string s = onoff ? dataSack.Value : "";
			
			textAbstraction.SetText(s);
		}
	}

	void	OnChanged( Datasack ds)
	{
		// don't actually set it; it will be blinked appropriately in Update()
		flashingTimer = 0.001f;
	}

	void	OnEnable()
	{
		dataSack.OnChanged += OnChanged;
	}
	void	OnDisable()
	{
		dataSack.OnChanged -= OnChanged;	
	}
}
