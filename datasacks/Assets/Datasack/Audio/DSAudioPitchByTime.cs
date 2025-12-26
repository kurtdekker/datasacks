
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 5:11 PM 12/24/2025 - Merry Christmas!
//
// Differences:
//
// DSAudioPitch sets the pitch
//
// DSAudioPitchByTime sets the pitch to play each
//	AudioClip out in the float-specified duration.
//
public class DSAudioPitchByTime : MonoBehaviour
{
	[Header( "Signal time in seconds into here:")]
	public	Datasack	dataSack;

	private AudioSource[] azzs;

	void Start ()
	{
		OnChanged (dataSack);
	}

	void	OnChanged( Datasack ds)
	{
		float playbackDuration = ds.fValue;

		if (playbackDuration >= 0.1f)
		{
			foreach (var az in azzs)
			{
				float sampleDuration = az.clip.length;

				float pitch = sampleDuration / playbackDuration;

				az.pitch = pitch;
			}
		}
	}

	void	OnEnable()
	{
		azzs = GetComponentsInChildren<AudioSource>();
		dataSack.OnChanged += OnChanged;	
	}
	void	OnDisable()
	{
		dataSack.OnChanged -= OnChanged;	
	}
}
