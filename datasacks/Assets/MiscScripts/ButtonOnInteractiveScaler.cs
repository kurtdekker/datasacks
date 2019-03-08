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
using UnityEngine.UI;

[RequireComponent( typeof( Button))]
public class ButtonOnInteractiveScaler : MonoBehaviour
{
	public AnimationCurve OnInteractableCurve;

	Button btn;
	bool PreviousInteractable;

	void OnEnable()
	{
		btn = GetComponent<Button>();
	}

	void Reset()
	{
		var keys = new Keyframe[] {
			new Keyframe( 0.0f, 1.0f),
			new Keyframe( 0.1f, 1.5f),
			new Keyframe( 0.3f, 1.0f),
		};
		OnInteractableCurve = new AnimationCurve(keys);
	}

	IEnumerator InteractableBobble()
	{
		if (OnInteractableCurve.keys != null &&
			OnInteractableCurve.keys.Length >= 2)
		{
			float minTime = OnInteractableCurve.keys[0].time;
			float maxTime = OnInteractableCurve.keys[ OnInteractableCurve.keys.Length - 1].time;

			for (float t = minTime; true; t += Time.deltaTime)
			{
				float scale = OnInteractableCurve.Evaluate( t);
				if (t >= maxTime)
				{
					scale = 1.0f;
				}

				transform.localScale = Vector3.one * scale;

				if (t >= maxTime)
				{
					break;
				}
				yield return null;
			}
		}
	}

	void Update ()
	{
		bool inter = btn.interactable;

		if (inter && !PreviousInteractable)
		{
			StartCoroutine( InteractableBobble());
		}

		PreviousInteractable = inter;
	}
}
