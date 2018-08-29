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