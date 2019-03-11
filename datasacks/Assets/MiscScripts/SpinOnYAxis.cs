using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinOnYAxis : MonoBehaviour
{
	public float RateOfSpin;

	float age;

	void Reset()
	{
		RateOfSpin = 100.0f;
	}

	void Update ()
	{
		age += Time.deltaTime;
		transform.localRotation = Quaternion.Euler( 0, age * RateOfSpin, 0);
	}
}
