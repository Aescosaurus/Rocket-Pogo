using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHop
	:
	MonoBehaviour
{
	void Start()
	{
		charCtrl = GetComponent<CharacterController>();
		player = FindObjectOfType<PogoMove>().gameObject;
	}

	void Update()
	{
		var grounded = charCtrl.isGrounded;

		if( grounded )
		{
			// moveDir = cam.transform.forward;
			moveDir = player.transform.position - transform.position;
			jumping = true;
		}

		var move = new Vector3(
			moveDir.x,
			0.0f,
			moveDir.z
		);
		move.Normalize();

		// var ang = cam.transform.eulerAngles.y * Mathf.Deg2Rad - Mathf.PI / 2.0f;
		// 
		// var xMove = Mathf.Cos( ang ) * move.z + Mathf.Sin( ang + Mathf.PI ) * move.x;
		// var yMove = -Mathf.Sin( ang ) * move.z + Mathf.Cos( ang + Mathf.PI ) * move.x;
		var xMove = move.x;
		var yMove = move.z;
		xMove *= moveSpeed;
		yMove *= moveSpeed;

		if( jumping )
		{
			yVel = jumpPower;

			if( jumpTimer.Update( Time.deltaTime ) )
			{
				StopJumping();
			}
		}
		else
		{
			yVel -= gravAcc * Time.deltaTime;
		}

		charCtrl.Move( new Vector3( xMove,yVel,yMove ) * Time.deltaTime );
	}

	void StopJumping()
	{
		jumping = false;
		jumpTimer.Reset();
		yVel /= 2.0f;
	}

	GameObject player;
	CharacterController charCtrl;

	[SerializeField] float moveSpeed = 10.0f;
	[SerializeField] float jumpPower = 3.0f;
	[SerializeField] Timer jumpTimer = new Timer( 0.4f );

	bool jumping = false;

	[SerializeField] float gravAcc = 10.0f;

	Vector3 moveDir = Vector3.zero;

	float yVel = 0.0f;
}
