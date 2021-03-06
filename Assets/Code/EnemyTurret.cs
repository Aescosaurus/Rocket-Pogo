﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret
	:
	MonoBehaviour
{
	void Start()
	{
		player = GameObject.Find( "Player" );
		playerBody = player.GetComponent<Rigidbody>();

		turret = transform.GetChild( 0 );

		bulletPrefab = Resources.Load<GameObject>( "Prefabs/Bullet" );
		shotPos = transform.Find( "Turret/Aimer/ShotPos" );

		shotRefire.Randomize();
	}

	void Update()
	{
		var diff = player.transform.position - transform.position;

		if( diff.sqrMagnitude < Mathf.Pow( shootRange,2 ) )
		{
			turret.transform.LookAt( player.transform );

			if( shotRefire.Update( Time.deltaTime ) )
			{
				shotRefire.Reset();

				var bullet = Instantiate( bulletPrefab );
				bullet.transform.position = shotPos.position;
				bullet.transform.LookAt( player.transform.position +
					playerBody.velocity.normalized * predictFactor );
				// bullet.transform.rotation = turret.transform.rotation;

				bullet.GetComponent<Rigidbody>().AddForce(
					bullet.transform.forward * shotSpeed,
					ForceMode.Impulse );
			}
		}
		else
		{
			shotRefire.Reset();
		}
	}

	GameObject player;
	Rigidbody playerBody;
	[SerializeField] float shootRange = 30.0f;
	Transform turret;

	Timer shotRefire = new Timer( 3.0f );

	GameObject bulletPrefab;
	Transform shotPos;
	[SerializeField] float shotSpeed = 1.0f;

	const float predictFactor = 10.5f;
}
