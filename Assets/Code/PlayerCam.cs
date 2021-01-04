using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam
	:
	MonoBehaviour
{
	void Start()
	{
		cam = transform.Find( "Main Camera" );

		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if( Input.GetKeyDown( KeyCode.Escape ) )
		{
			Cursor.lockState = CursorLockMode.None;
		}
		if( Input.GetMouseButtonDown( 0 ) )
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		var aim = new Vector2( Input.GetAxis( "Mouse X" ),
			Input.GetAxis( "Mouse Y" ) );

		cam.transform.eulerAngles = new Vector3(
			cam.eulerAngles.x - aim.y * rotationSpeed * Time.deltaTime,
			cam.eulerAngles.y + aim.x * rotationSpeed * Time.deltaTime,
			cam.eulerAngles.z );
	}

	Transform cam;

	[SerializeField] float rotationSpeed = 5.0f;
}
