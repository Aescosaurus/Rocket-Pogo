using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler
	:
	MonoBehaviour
{
	void Start()
	{
		body = GetComponent<Rigidbody>();

		info = new AbilityInfo( body );

		abilities.Add( new DropSlam() );
	}

	void Update()
	{
		foreach( var ability in abilities )
		{
			ability.Update();

			if( Input.GetKey( ability.GetActivationKey() ) )
			{
				ability.Activate( info );
			}
		}
	}

	List<AbilityBase> abilities = new List<AbilityBase>();

	Rigidbody body;
	AbilityInfo info;
}

public class AbilityInfo
{
	public AbilityInfo( Rigidbody body )
	{
		this.body = body;
	}

	public Rigidbody body;
}