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
