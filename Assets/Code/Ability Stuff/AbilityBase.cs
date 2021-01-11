using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase
{
	public virtual void Activate( AbilityInfo info )
	{
		
	}

	public virtual void Update()
	{

	}

	public abstract KeyCode GetActivationKey();

	KeyCode activateKey;
}
