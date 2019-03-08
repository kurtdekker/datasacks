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

// Originally from:
// http://forum.unity3d.com/threads/script-simple-script-that-automatically-adjust-anchor-to-gui-object-size-rect-transform.269690/

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using System.Collections;

public class AnchorExtensions : MonoBehaviour {
	// This will set the anchor points around our RectTransform.
	// This will make it scale in place as the parent UI adjusts.
	[MenuItem ("UI/Anchor Around Object")]
	static void AnchorAroundObject() {
		for (int i = 0; i < Selection.gameObjects.Length; i++) {
			GameObject obj = Selection.gameObjects [i];
			if (obj != null && obj.GetComponent<RectTransform> () != null) {
				var rectTransform = obj.GetComponent<RectTransform> ();
				var parentTransform = obj.transform.parent.GetComponent<RectTransform> ();
				var offsetMin = rectTransform.offsetMin;
				var offsetMax = rectTransform.offsetMax;
				var oldAnchorMin = rectTransform.anchorMin;
				var oldAnchorMax = rectTransform.anchorMax;
				var parentWidth = parentTransform.rect.width;
				var parentHeight = parentTransform.rect.height;
				var anchorMin = new Vector2 (oldAnchorMin.x + (offsetMin.x / parentWidth), 
					oldAnchorMin.y + (offsetMin.y / parentHeight));
				var anchorMax = new Vector2 (oldAnchorMax.x + (offsetMax.x / parentWidth), 
					oldAnchorMax.y + (offsetMax.y / parentHeight));
				rectTransform.anchorMin = anchorMin;
				rectTransform.anchorMax = anchorMax;
				rectTransform.offsetMin = new Vector2 (0, 0);
				rectTransform.offsetMax = new Vector2 (0, 0);
				rectTransform.pivot = new Vector2 (0.5f, 0.5f);
			}
		}
	}
	// This will set the anchor points at the center of our RectTransform.
	// This will make it remain in place as the parent UI adjusts.
	[MenuItem ("UI/Anchor Center Object")]
	static void AnchorCenterObject() {
		for (int i = 0; i < Selection.gameObjects.Length; i++) {
			GameObject obj = Selection.gameObjects [i];
			if (obj != null && obj.GetComponent<RectTransform> () != null) {
				var rectTransform = obj.GetComponent<RectTransform> ();
				var parentTransform = obj.transform.parent.GetComponent<RectTransform> ();
				var parentWidth = parentTransform.rect.width;
				var parentHeight = parentTransform.rect.height;
				var offsetMin = rectTransform.offsetMin;
				var oldAnchorMin = rectTransform.anchorMin;
				float width = rectTransform.rect.width;
				float height = rectTransform.rect.height;
				float changeX = (offsetMin.x + width/2)  / parentWidth;
				float changeY = (offsetMin.y + height/2) / parentHeight;
				var anchorCenter = new Vector2 (oldAnchorMin.x + changeX, oldAnchorMin.y + changeY);
				rectTransform.offsetMin = new Vector2 (-.5f * width, -.5f * height);
				rectTransform.offsetMax = new Vector2 (.5f  * width,  .5f * height);
				rectTransform.anchorMin = anchorCenter;
				rectTransform.anchorMax = anchorCenter;
				rectTransform.pivot = new Vector2 (0.5f, 0.5f);
			}
		}
	}
}

#endif