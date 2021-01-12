using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump
	:
	AbilityBase
{
	public override void Activate( AbilityInfo info )
	{
		if( refire.IsDone() )
		{
			refire.Reset();

			info.playerMove.TriggerJump();
		}
	}

	public override void Update( AbilityInfo info )
	{
		refire.Update( Time.deltaTime );
	}

	public override KeyCode GetActivationKey()
	{
		return( KeyCode.Space );
	}

	Timer refire = new Timer( 5.0f );

}
