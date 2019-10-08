using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Button player1ServeButton;
    public Button player2ServeButton;

	private bool player1served = false,
				 player2served = false;

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
		player1ServeButton.onClick.AddListener(buttonPlayer1ServeListener);
		player2ServeButton.onClick.AddListener(buttonPlayer2ServeListener);
		player1ServeButton.interactable = true;
		player2ServeButton.interactable = false;

		state = States.init;

		resetPositions(-1.0f);

		player1Reaction = 0.2f;
		player2Reaction = 0.2f;
		puckReaction  = 0.2f;

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
					state = States.player2Serves;
					resetPositions(3.0f);
					player2ServeButton.interactable = true;
				}
				else
				{
					// gol del player2
					state = States.player1Serves;
					resetPositions(-3.0f);
					player1ServeButton.interactable = true;
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
		player1.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, -6f);
		player2.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, 6f);
		puck.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, puckPosition);
	}

	public void buttonPlayer1ServeListener()
	{
		player1served = true;
		player1ServeButton.interactable = false;
	}

	public void buttonPlayer2ServeListener()
	{
		player2served = true;
		player2ServeButton.interactable = false;
	}

	public bool player1ClickedButton()
	{
		bool retorno = player1served;
		player1served = false;
		return retorno;
	}

	public bool player2ClickedButton()
	{
		bool retorno = player2served;
		player2served = false;
		return retorno;
	}

}

