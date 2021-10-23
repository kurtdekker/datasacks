﻿/*
	The following license supersedes all notices in the source code.

	Copyright (c) 2021 Kurt Dekker/PLBM Games All rights reserved.

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

// Purpose: uses a Datasack's floating point value to control the rotation
// of a GameObject, either local or global. Has scale and base offsets.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSRotationSetAxis : MonoBehaviour
{
	public	Datasack	dataSack;

	public	bool		LocalCoordinates;

	public	DSAxis		Axis;

	public	float		BaseValue;
	public	float		Scale;

	void Reset()
	{
		Axis = DSAxis.Z;
		BaseValue = 0.0f;
		Scale = 360.0f;
	}

	void Start ()
	{
		OnChanged (dataSack);
	}

	void	OnChanged( Datasack ds)
	{
		float angle = BaseValue + dataSack.fValue * Scale;

		Quaternion q = Quaternion.identity;

		switch(Axis)
		{
		case DSAxis.X :
			q = Quaternion.Euler( angle, 0, 0);
			break;
		case DSAxis.Y :
			q = Quaternion.Euler( 0, angle, 0);
			break;
		case DSAxis.Z :
			q = Quaternion.Euler( 0, 0, angle);
			break;
		}

		if (LocalCoordinates)
		{
			transform.localRotation = q;
		}
		else
		{
			transform.rotation = q;
		}
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
