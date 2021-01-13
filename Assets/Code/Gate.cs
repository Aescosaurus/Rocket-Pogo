using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate
	:
	MonoBehaviour
{
	void OnTriggerEnter( Collider coll )
	{
		coll.GetComponent<PogoHold>()?.Boost( boostPercent );
	}

	[SerializeField] float boostPercent = 1.1f;
}
