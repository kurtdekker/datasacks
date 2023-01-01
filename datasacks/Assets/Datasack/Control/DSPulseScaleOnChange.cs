/*
	The following license supersedes all notices in the source code.

	Copyright (c) 2023 Kurt Dekker/PLBM Games All rights reserved.

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

// @kurtdekker - pulses the scale of underlying transform whenever the Datasack
// is poked. Useful for making score readouts "pop/pulse" when they change.

public class DSPulseScaleOnChange : MonoBehaviour
{
	[Header( "Datasack to watch for change.")]
	public Datasack dataSack;

	[Header( "Curve of scale pulse +1")]
	public AnimationCurve PulseCurve;

	// zero means not operational
	float timer;

	private void Reset()
	{
		PulseCurve.keys = new Keyframe[]
		{
			new Keyframe( 0.00f, 1.0f),
			new Keyframe( 0.10f, 1.8f),
			new Keyframe( 0.35f, 1.0f),
		};
	}

	void Update ()
	{
		if (timer > 0)
		{
			var keys = PulseCurve.keys;

			var last = keys[keys.Length - 1];

			Vector3 scale = Vector3.one;

			timer += Time.deltaTime;

			if (timer <= last.time)
			{
				float sample = PulseCurve.Evaluate(timer);
				scale *= sample;
			}
			else
			{
				timer = 0;
			}

			transform.localScale = scale;
		}
	}

	void OnValueChanged (Datasack ds)
	{
		timer = Time.deltaTime;
	}

	void OnEnable()
	{
		dataSack.OnChanged += OnValueChanged;
	}

	void OnDisable()
	{
		dataSack.OnChanged -= OnValueChanged;
	}
}
