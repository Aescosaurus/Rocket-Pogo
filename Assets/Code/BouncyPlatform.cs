using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform
	:
	MonoBehaviour
{
	void OnCollisionEnter( Collision coll )
	{
		if( coll.gameObject.tag == "Player" )
		{
			coll.gameObject.GetComponent<Rigidbody>().AddForce(
				boostDir * boostForce,ForceMode.Impulse );
		}
	}

	[SerializeField] Vector3 boostDir = Vector3.up;
	[SerializeField] float boostForce = 10.0f;
}
