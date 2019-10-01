using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
	public GameController game;

	private Vector3 movement;

	void FixedUpdate()
	{
		float movH = Input.GetAxis("Horizontal");

		switch(game.state)
		{
			case GameController.States.player1Serves:
				move(movH);
				if ( Input.GetAxis("Vertical") != 0 )
					game.state = GameController.States.player1Serving;
				break;

			case GameController.States.player1Serving:
				Vector2 puckPos = new Vector2(GetComponent<Rigidbody>().position.x,
											  GetComponent<Rigidbody>().position.z);

				Vector2 player1Pos = new Vector2(game.puck.GetComponent<Rigidbody>().position.x,
												 game.puck.GetComponent<Rigidbody>().position.z);

				Vector2 hitDir= player1Pos - puckPos;

				movement = new Vector3(hitDir.x, 0.0f, hitDir.y);
				GetComponent<Rigidbody>().position += movement * game.player1Reaction;
				break;

			case GameController.States.playing:
				move(movH);
				break;
		}
	}

	void move(float movH)
	{
		movement = new Vector3(movH, 0.0f, 0.0f);
		GetComponent<Rigidbody>().position += movement * game.player1Reaction;
		GetComponent<Rigidbody>().position = new Vector3(
				Mathf.Clamp(GetComponent<Rigidbody>().position.x,-1.5f,1.5f),
				0.0f,
				-2.75f);
	}

}
