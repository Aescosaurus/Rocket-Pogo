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
			GetComponent<PogoForces>(),
			Camera.main
			);

		abilities.Add( new DropSlam() );
		abilities.Add( new DoubleJump() );
		abilities.Add( new PseudoShotgun() );
		abilities.Add( new ForwardForce() );
	}

	void Update()
	{
		foreach( var ability in abilities )
		{
			ability.Update( info );

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
		PogoForces playerMove,
		Camera cam )
	{
		this.body = body;
		this.playerMove = playerMove;
		this.cam = cam;
	}

	public Rigidbody body;
	public PogoForces playerMove;
	public Camera cam;
}