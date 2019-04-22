using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class examplehealthmeter : MonoBehaviour
{
	const int NumPips = 8;		// presently baked into the pips texture

	const float RateToChange = 1.0f;		// per second

	void Start()
	{
		DSM.HealthMeter.AnalogPortion.fValue = 1.0f;

		DSM.HealthMeter.PipsPortion.fValue = 1.0f;
	}

	void AdjustHealth( float Amount)
	{
		Amount *= Time.deltaTime;

		float temp = DSM.HealthMeter.AnalogPortion.fValue + Amount;

		if (temp < 0)
		{
			temp = 1.0f;

			// take away an entire pip
			DSM.HealthMeter.PipsPortion.fValue -= 1.0f / NumPips;
			if (DSM.HealthMeter.PipsPortion.fValue < 0)
			{
				DSM.HealthMeter.PipsPortion.fValue = 0;
				temp = 0.0f;
			}
		}
		if (temp > 1.0f)
		{
			temp = 0.0f;

			// compact it into a pip
			DSM.HealthMeter.PipsPortion.fValue += 1.0f / NumPips;
			if (DSM.HealthMeter.PipsPortion.fValue >= 1.0f)
			{
				DSM.HealthMeter.PipsPortion.fValue = 1.0f;
				temp = 1.0f;
			}
		}

		DSM.HealthMeter.AnalogPortion.fValue = temp;
	}

	void Update ()
	{
		if (Input.GetKey( KeyCode.UpArrow))
		{
			AdjustHealth (RateToChange);
		}
		if (Input.GetKey( KeyCode.DownArrow))
		{
			AdjustHealth (-RateToChange);
		}
	}
}
