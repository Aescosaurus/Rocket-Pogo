using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoHold
	:
	MonoBehaviour
{
	void Start()
	{
		cam = Camera.main;
		body = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if( Input.GetMouseButton( 0 ) && requestJump && !holdPercent.IsDone() )
		{
			holdPercent.Update( Time.deltaTime );
		}
		else
		{
			if( requestJump && body.velocity.y <= 0.0f ) TriggerJump();

			holdPercent.Reset();
		}
	}

	void OnTriggerEnter( Collider coll )
	{
		RequestJump();
	}

	// void OnTriggerStay( Collider coll )
	// {
	// 	RequestJump();
	// }

	void RequestJump()
	{
		requestJump = true;
	}

	public void TriggerJump()
	{
		requestJump = false;

		var targetJumpDir = cam.transform.forward;
		targetJumpDir.Normalize();
		targetJumpDir.y = verticalJumpScale;

		// body.velocity = Vector3.zero;
		body.AddForce( targetJumpDir * jumpForce *
			Mathf.Max( holdPercent.GetPercent(),minJumpPercent ),
			ForceMode.Impulse );
	}

	Camera cam;
	Rigidbody body;

	[SerializeField] float jumpForce = 10.0f;
	[SerializeField] float verticalJumpScale = 1.0f;

	[SerializeField] Timer holdPercent = new Timer( 1.0f );
	[Range( 0.0f,1.0f )]
	[SerializeField] float minJumpPercent = 0.2f;

	bool requestJump = false;
}
