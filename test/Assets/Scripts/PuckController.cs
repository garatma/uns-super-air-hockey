using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour
{
	public GameController game;

	private Vector3 movement;

	void OnTriggerEnter(Collider collision)
	{
		GameObject obj = collision.gameObject;

		if ( collision.gameObject.tag == "Player" )
		{
			game.state = GameController.States.playing;

			Vector2 puckPos = new Vector2(GetComponent<Rigidbody>().position.x, GetComponent<Rigidbody>().position.z);
			Vector2 playerPos = new Vector2(obj.GetComponent<Rigidbody>().position.x, obj.GetComponent<Rigidbody>().position.z);
			Vector2 hitDir = puckPos - playerPos;

			movement.z = -movement.z;
			movement += new Vector3(hitDir.x, 0.0f, hitDir.y);
			movement = new Vector3(
					Mathf.Clamp(movement.x, -1.25f, 1.25f),
					0.0f,
					Mathf.Clamp(movement.z, -1.25f, 1.25f)
			);
		}
		else if ( collision.gameObject.tag == "Side" )
			movement.x = -movement.x;
		else if ( collision.gameObject.tag == "Front" )
			movement.z = -movement.z;
		else if ( collision.gameObject.tag == "Player1Goal" )
		{
			movement = new Vector3(0.0f, 0.0f, 0.0f);
			game.player2Goals++;
			game.state = GameController.States.player2Scores;
		}
		else if ( collision.gameObject.tag == "Player2Goal" )
		{
			movement = new Vector3(0.0f, 0.0f, 0.0f);
			game.player1Goals++;
			game.state = GameController.States.player1Scores;
		}
	}

	void Update()
	{
		GetComponent<Rigidbody>().position += movement * game.puckReaction;
		GetComponent<Rigidbody>().position = new Vector3(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, -1.7f, 1.7f),
			-0.32f,
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, -4f, 4f)
		);
	}
}
