using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePinger
	:
	MonoBehaviour
{
	void Start()
	{
		mat = GetComponent<MeshRenderer>().material;
		mat.color = Color.red;
	}

	void OnTriggerEnter( Collider coll )
	{
		if( coll.gameObject.GetComponent<PlayerMove>() != null )
		{
			mat.color = Color.white;
		}
	}

	Material mat;
}
