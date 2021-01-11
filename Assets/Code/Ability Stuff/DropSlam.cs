using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSlam
	:
	AbilityBase
{
	public override void Activate( AbilityInfo info )
	{
		if( refire.IsDone() )
		{
			refire.Reset();

			info.body.AddForce( slamDir * slamForce,ForceMode.Impulse );
		}
	}

	public override void Update()
	{
		refire.Update( Time.deltaTime );
	}

	public override KeyCode GetActivationKey()
	{
		return( KeyCode.C );
	}

	static Vector3 slamDir = Vector3.down;
	const float slamForce = 50.0f;
	Timer refire = new Timer( 5.0f );

}
