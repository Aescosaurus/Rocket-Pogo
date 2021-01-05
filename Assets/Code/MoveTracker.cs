using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTracker
{
	public MoveTracker( KeyCode key1,KeyCode key2,string mouseAxis )
	{
		this.key1 = key1;
		this.key2 = key2;
		this.mouseAxis = mouseAxis;
	}

	public void Update()
	{
		var axisMove = Input.GetAxis( mouseAxis );
		if( ( Input.GetKeyDown( key1 ) && axisMove < 0.0f ) ||
			( Input.GetKeyDown( key2 ) && axisMove > 0.0f ) )
		{
			score += Time.deltaTime;
		}
		// else if( ( Input.GetKeyDown( key2 ) && axisMove < 0.0f ) ||
		// 	( Input.GetKeyDown( key1 ) && axisMove > 0.0f ) )
		// {
		// 	score -= Time.deltaTime;
		// }
	}

	public void Reset()
	{
		score = 0.0f;
	}

	public float CheckScore()
	{
		return( score );
	}

	public bool Succeeded()
	{
		return( score > scoreThresh );
	}

	KeyCode key1;
	KeyCode key2;
	string mouseAxis;
	float score = 0.0f;
	const float scoreThresh = 0.07f;
}
