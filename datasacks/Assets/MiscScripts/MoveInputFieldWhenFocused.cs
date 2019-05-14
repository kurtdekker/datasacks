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
using UnityEngine.EventSystems;

[RequireComponent( typeof( InputField))]
public class MoveInputFieldWhenFocused : MonoBehaviour
{
	[Header( "NOTE: This script goes ON the InputField GameObject")]

	[Tooltip( "What you want to move.")]
	public GameObject TargetToMove;

	[Tooltip( "Where you want to move Target")]
	public GameObject DestinationWhenFocused;

	[Tooltip( "Optional items to Activate/Deactivate on focus.")]
	public GameObject[] ActivateWhenFocused;
	public GameObject[] DeactivateWhenFocused;

	InputField inputField;

	[Tooltip( "How much time to tween.")]
	public float TransitionInterval;

	void Reset()
	{
		TransitionInterval = 0.2f;
	}

	void Start()
	{
		inputField = GetComponent<InputField>();
		InitialPosition = TargetToMove.transform.position;
	}

	void OnEnable()
	{
		PreviousFraction = -1;
		TransitionTimer = -0.01f;
	}

	Vector3 InitialPosition;
	bool PreviousFocused;
	float PreviousFraction;

	// goes from 0.0 up to TransitionInterval
	float TransitionTimer;

	void Update()
	{
		bool focused = false;
		if (inputField)
		{
			focused = inputField.isFocused;
		}

		// <WIP> if you want to support TMPro InputFields or other
		// types of input fields, search for them here and decide if
		// they have focus

		float DesiredFocusAmount = 0.0f;		// presume NOT focused

		if (focused)
		{
			DesiredFocusAmount = TransitionInterval;			// become completely focused
			if (!PreviousFocused)
			{
				InitialPosition = TargetToMove.transform.position;
			}
		}
		PreviousFocused = focused;

		bool Tweening = false;

		// transitioning into focus
		if (TransitionTimer < DesiredFocusAmount)
		{
			Tweening = true;
			TransitionTimer += Time.deltaTime;
			if (TransitionTimer >= DesiredFocusAmount)
			{
				TransitionTimer = DesiredFocusAmount;
			}
		}

		// transitioning out of focus
		if (TransitionTimer > DesiredFocusAmount)
		{
			Tweening = true;
			TransitionTimer -= Time.deltaTime;
			if (TransitionTimer <= DesiredFocusAmount)
			{
				TransitionTimer = DesiredFocusAmount;
			}
		}

		// presume instantaneous transition to/from focus
		float Fraction = focused ? 1.0f : 0.0f;

		if (TransitionInterval > 0)
		{
			Fraction = TransitionTimer / TransitionInterval;
		}

		// only drive anything when there is change
		if (Tweening || (PreviousFraction != Fraction))
		{
			// move the thing
			TargetToMove.transform.position = Vector3.Lerp( InitialPosition, DestinationWhenFocused.transform.position, Fraction);

			// activate the things
			foreach (var go in ActivateWhenFocused)
			{
				go.SetActive(focused);
			}

			// deactivate the things
			bool activate = !focused;
			foreach (var go in DeactivateWhenFocused)
			{
				go.SetActive(activate);
			}
		}

		PreviousFraction = Fraction;
	}
}
