using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject player1;
	public float player1Reaction;

	public GameObject player2;
	public float player2Reaction;

	public GameObject puck;
	public float puckReaction;

	public UnityEngine.UI.Text player1GUIGoals;
	public int player1Goals;

	public UnityEngine.UI.Text player2GUIGoals;
	public int player2Goals;

	private int puckInGame;

	public enum States
	{
		init,
		player1Serves,
		player1Serving,
		player2Serves,
		player2Serving,
		player1Scores,
		player2Scores,
		playing,
		waiting,
		end
	}

	public States state;

	void Start()
	{
		state = States.init;

		resetPositions(-1.0f);

		player1Reaction = 0.1f;
		player2Reaction = 0.1f;
		puckReaction  = 0.1f;

		puckInGame = 1;
		player1Goals = 0;
		player2Goals = 0;

		state = States.player1Serves;
	}

	void Update()
	{
		player1GUIGoals.text = "Goles Player 1: "+player1Goals.ToString();
		player2GUIGoals.text = "Goles Player 2: "+player2Goals.ToString();

		switch (state)
		{
			case States.player2Scores:
			case States.player1Scores:
				puckInGame++;

				if ( player1Goals == 10 || player2Goals == 10 )
				{
					// juego terminado
					state = States.end;
					resetPositions(0.0f);
				}
				else if ( state == States.player1Scores )
				{
					// gol del player1
					// state = States.waiting;
					state = States.player2Serves;
					resetPositions(1.0f);
				}
				else
				{
					// gol del player2
					// state = States.waiting;
					state = States.player1Serves;
					resetPositions(-1.0f);
				}
				break;

			// case States.waiting:
			// case States.end:
			// 	if ( player1Goals == 10 )
			// 		// imprimir gana player1
			// 	else
			// 		// imprimir gana player2
		}
	}

	void resetPositions(float puckPosition)
	{
		player1.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, -2.75f);
		player2.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, 2.75f);
		puck.GetComponent<Rigidbody>().position = new Vector3(0.0f, -0.32f, puckPosition);
	}
}

