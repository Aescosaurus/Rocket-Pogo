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
		forwardAxisPower = Mathf.Lerp( forwardAxisPower,Input.GetAxis( "Vertical" ),reverseAccelMod * Time.deltaTime );

		var move = new Vector3(
			Input.GetAxis( "Horizontal" ),
			0.0f,
			forwardAxisPower
		);
		// move.Normalize();
		var ang = cam.transform.eulerAngles.y * Mathf.Deg2Rad - Mathf.PI / 2.0f;

		move.z *= bhMod;
		move.x *= bhMod * bhStrafeMod;

		var xMove = Mathf.Cos( ang ) * move.z + Mathf.Sin( ang + Mathf.PI ) * move.x;
		var yMove = -Mathf.Sin( ang ) * move.z + Mathf.Cos( ang + Mathf.PI ) * move.x;

		// xMove *= bhMod * bhStrafeMod;
		// yMove *= bhMod;

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

		if( Input.GetAxis( "Jump" ) > 0.0f || autoJump )
		{
			if( !jumping && canJump )
			{
				jumping = true;
			}
		}
		else if( variableJump )
		{
			if( jumping && minJump.Update( Time.deltaTime ) )
			{
				StopJumping();
			}
		}

		if( jumping )
		{
			yVel = jumpPower * bhMod * bhJumpMod;

			if( jumpTimer.Update( Time.deltaTime ) )
			{
				StopJumping();
			}
		}
		else
		{
			yVel -= gravAcc * Time.deltaTime;
		}

		charCtrl.Move( new Vector3( xMove,yVel,yMove ) * moveSpeed * Time.deltaTime );

		// animCtrl.SetBool( "jump",yVel > 0.0f );
		// animCtrl.SetBool( "jump",!canJump );

		if( !canJump )
		{
			moveTracker.Update();

			// if( moveTracker.Succeeded() )
			// {
			// 	moveTracker.Reset();
			// 	bhMod += bhSpeedup;
			// }
			bhMod = 1.0f + moveTracker.CheckScore() * bhSpeedup;
			bhReset.Reset();

			// if( moveTracker.CheckScore() < -0.5f )
			// {
			// 	bhMod = 0.0f;
			// }
		}
		else
		{
			bhMod -= bhDecay * Time.deltaTime;
			if( bhMod < 1.0f )
			{
				bhMod = 1.0f;
				moveTracker.Reset();
			}

			// if( bhReset.Update( Time.deltaTime ) )
			// {
			// 	bhMod = 1.0f;
			// 	bhReset.Reset();
			// 	moveTracker.Reset();
			// }

			// print( bhMod );

			// bhMod = 1.0f;
			// moveTracker.Reset();
		}
	}

	bool CanJump()
	{
		return ( charCtrl.isGrounded );
	}

	void StopJumping()
	{
		jumping = false;
		jumpTimer.Reset();
		minJump.Reset();
		yVel /= 2.0f;
	}

	public float GetBoostPercent()
	{
		return( bhMod );
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
	[SerializeField] bool variableJump = true;
	[SerializeField] bool autoJump = false;

	bool jumping = false;

	[SerializeField] float gravAcc = 0.3f;

	float yVel = 0.0f;

	[SerializeField] MoveTracker moveTracker = new MoveTracker( KeyCode.A,KeyCode.D,"Mouse X" );
	float bhMod = 1.0f;
	[SerializeField] float bhSpeedup = 10.0f;
	[SerializeField] float bhDecay = 1.0f;
	[SerializeField] Timer bhReset = new Timer( 0.4f );

	[SerializeField] float bhStrafeMod = 0.6f;
	[SerializeField] float bhJumpMod = 0.2f;

	float forwardAxisPower = 0.0f;
	[SerializeField] float reverseAccelMod = 1.0f;
}
