using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoShotgun
	:
	AbilityBase
{
	public override void Activate( AbilityInfo info )
	{
		if( refire.IsDone() )
		{
			refire.Reset();

			info.body.AddForce( -info.cam.transform.forward * knockbackForce,ForceMode.Impulse );
		}
	}

	public override void Update()
	{
		refire.Update( Time.deltaTime );
	}

	public override KeyCode GetActivationKey()
	{
		return ( KeyCode.Q );
	}

	const float knockbackForce = 35.0f;
	Timer refire = new Timer( 5.0f );
}
