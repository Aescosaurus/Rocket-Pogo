using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardForce
	:
	AbilityBase
{
	public override void Update( AbilityInfo info )
	{
		info.body.AddForce( info.cam.transform.forward * passiveForce * Time.deltaTime );
	}

	public override KeyCode GetActivationKey()
	{
		return ( KeyCode.None );
	}

	const float passiveForce = 150.0f;
}
