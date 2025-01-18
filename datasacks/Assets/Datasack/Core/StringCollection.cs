using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collections/StringCollection")]
public class StringCollection : ScriptableObject
{
	[Header( "This is just for my edification.")]
	[Multiline]
	public string Description;

	[Space]

	public string[] Strings;

	public int Length
	{
		get
		{
			if (Strings != null)
			{
				return Strings.Length;
			}
			return 0;
		}
	}

	public string GetString( int i)
	{
		// no bounding! That's on you...
		return Strings[i];
	}
}
