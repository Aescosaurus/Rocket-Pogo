using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
	:
	MonoBehaviour
{
	void Start()
	{
		body = GetComponent<Rigidbody>();
		cam = Camera.main;
		// animCtrl = GetComponent<Animator>();
		// coll = GetComponent<Collider>();
		charCtrl = GetComponent<CharacterController>();
	}

	void FixedUpdate()
	{
		var move = new Vector3(
			Input.GetAxis( "Horizontal" ),
			0.0f,
			Input.GetAxis( "Vertical" )
		);
		move.Normalize();
		var ang = cam.transform.eulerAngles.y * Mathf.Deg2Rad - Mathf.PI / 2.0f;

		var xMove = Mathf.Cos( ang ) * move.z + Mathf.Sin( ang + Mathf.PI ) * move.x;
		var yMove = -Mathf.Sin( ang ) * move.z + Mathf.Cos( ang + Mathf.PI ) * move.x;

		if( Mathf.Abs( xMove ) > 0.0f || Mathf.Abs( yMove ) > 0.0f )
		{
			var rot = transform.eulerAngles;
			rot.y = Mathf.Atan2( xMove,yMove ) * Mathf.Rad2Deg;
			rot.y = Mathf.LerpAngle( transform.eulerAngles.y,rot.y,rotSpeed * Time.deltaTime );
			// transform.eulerAngles = rot;

			// animCtrl.SetBool( "walk",true );
		}
		else
		{
			// animCtrl.SetBool( "walk",false );
		}

		bool canJump = CanJump();

		if( canJump )
		{
			yVel = 0.0f;
		}

		if( Input.GetAxis( "Jump" ) > 0.0f )
		{
			if( !jumping && canJump )
			{
				jumping = true;

				// todo play jumping animation
				// body.AddForce( Vector3.up * jumpForce,ForceMode.Impulse );
			}
		}
		else
		{
			if( jumping && minJump.Update( Time.deltaTime ) )
			{
				StopJumping();
			}
		}

		if( jumping )
		{
			// var jumpForce = Vector3.up * jumpPower;

			// body.MovePosition( body.position + jumpForce * Time.deltaTime );
			// var vel = body.velocity;
			// vel.y = jumpPower;
			// body.velocity = vel;
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

		// body.MovePosition( transform.position + new Vector3( xMove,yVel,yMove ) * moveSpeed * Time.deltaTime );
		charCtrl.Move( new Vector3( xMove,yVel,yMove ) * moveSpeed * Time.deltaTime );

		// animCtrl.SetBool( "jump",yVel > 0.0f );
		// animCtrl.SetBool( "jump",!canJump );
	}

	bool CanJump()
	{
		return ( charCtrl.isGrounded );
		// // var checkLoc = coll.bounds.center + Vector3.down * coll.bounds.size.y / 2.0f;
		// var checkLoc = transform.position + Vector3.down * charCtrl.height;
		// 
		// // var colls = Physics.OverlapSphere( checkLoc,coll.bounds.size.x / 2.0f,LayerMask.NameToLayer( "World" ) );
		// var colls = Physics.OverlapBox( checkLoc,
		// 	new Vector3( coll.bounds.size.x,0.1f,coll.bounds.size.z ) / 3.0f,
		// 	transform.rotation,
		// 	LayerMask.GetMask( "World" ) );
		// 
		// return( colls.Length > 0 );
	}

	void StopJumping()
	{
		jumping = false;
		jumpTimer.Reset();
		minJump.Reset();
		yVel /= 2.0f;
	}

	Rigidbody body;
	Camera cam;
	// Animator animCtrl;
	// Collider coll;
	CharacterController charCtrl;

	[SerializeField] float moveSpeed = 10.0f;
	[SerializeField] float rotSpeed = 2.9f;

	[SerializeField] float jumpPower = 3.0f;
	[SerializeField] Timer jumpTimer = new Timer( 2.0f );
	[SerializeField] Timer minJump = new Timer( 0.5f );

	bool jumping = false;

	[SerializeField] float gravAcc = 0.3f;

	float yVel = 0.0f;
}
