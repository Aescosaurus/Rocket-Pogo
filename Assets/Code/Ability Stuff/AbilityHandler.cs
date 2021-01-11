using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler
	:
	MonoBehaviour
{
	void Start()
	{
		info = new AbilityInfo(
			GetComponent<Rigidbody>(),
			GetComponent<PogoForces>()
			);

		abilities.Add( new DropSlam() );
		abilities.Add( new DoubleJump() );
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
	
	AbilityInfo info;
}

public class AbilityInfo
{
	public AbilityInfo( Rigidbody body,
		PogoForces playerMove )
	{
		this.body = body;
		this.playerMove = playerMove;
	}

	public Rigidbody body;
	public PogoForces playerMove;
}