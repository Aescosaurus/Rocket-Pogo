using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet
	:
	MonoBehaviour
{
	void Update()
	{
		if( lifetime.Update( Time.deltaTime ) )
		{
			Destroy( gameObject );
		}
	}

	void OnTriggerEnter( Collider coll )
	{
		if( coll.GetComponent<PogoMove>() != null )
		{
			// todo damage player

		}
		Destroy( gameObject );
	}

	void OnCollisionEnter( Collision coll )
	{
		Destroy( gameObject );
	}

	[SerializeField] Timer lifetime = new Timer( 5.0f );
}
