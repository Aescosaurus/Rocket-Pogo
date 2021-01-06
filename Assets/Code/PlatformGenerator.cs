using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class PlatformGenerator
	:
	MonoBehaviour
{
	void Start()
	{
		curPos = transform.position;
		player = FindObjectOfType<PlayerMove>();

		Assert.IsNotNull( platPrefab );
	}

	void Update()
	{
		var diff = player.transform.position - curPos;
		if( diff.sqrMagnitude < Mathf.Pow( platformSpacing * 3,2 ) )
		{
			CreateNewPlatform();
		}

		if( player.transform.position.y < -200.0f )
		{
			SceneManager.LoadScene( SceneManager.GetActiveScene().name );
		}
	}

	void CreateNewPlatform()
	{
		moveDir.x = Mathf.Cos( moveAng * Mathf.Deg2Rad );
		moveDir.z = Mathf.Sin( moveAng * Mathf.Deg2Rad );

		curPos += moveDir * ( platformSpacing + player.GetBoostPercent() * spacingIncrease );

		var plat = Instantiate( platPrefab,curPos,Quaternion.identity,transform );

		moveAng += Random.Range( -angVariance,angVariance );
	}

	float moveAng = 0.0f;
	Vector3 moveDir = Vector3.zero;
	Vector3 curPos;
	PlayerMove player;

	[SerializeField] float platformSpacing = 110.0f;
	[SerializeField] float spacingIncrease = 10.0f;
	[SerializeField] float angVariance = 40.0f;
	[SerializeField] GameObject platPrefab = null;
}
