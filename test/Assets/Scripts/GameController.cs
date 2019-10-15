using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject player1;
	public float reaccionPlayer1;

	public GameObject player2;
	public float reaccionPlayer2;

	public GameObject disco;
	public float reaccionDisco;

	public UnityEngine.UI.Text [] golesGUIPlayer1 = {null,null};
	public int golesPlayer1;

	public UnityEngine.UI.Text [] golesGUIPlayer2 = {null,null};
	public int golesPlayer2;

	public UnityEngine.UI.Text mensaje_fin;

	public enum Estados
	{
		inicio,
		sacaPlayer1,
		sacandoPlayer1,
		sacaPlayer2,
		sacandoPlayer2,
		golPlayer1,
		golPlayer2,
		jugando,
		fin,
		esperandoReinicio
	}

	public Estados estado;

	void Start()
	{
		estado = Estados.inicio;

		reaccionPlayer1 = 0.2f;
		reaccionPlayer2 = 0.2f;
		reaccionDisco  = 0.4f;
	}

	void Update()
	{
		golesGUIPlayer1[0].text = golesPlayer1.ToString();
		golesGUIPlayer2[0].text = golesPlayer1.ToString();

 		golesGUIPlayer1[1].text = golesPlayer2.ToString();
		golesGUIPlayer2[1].text = golesPlayer2.ToString();

		if (Application.platform == RuntimePlatform.Android)
		{
            if ( Input.GetKey(KeyCode.Escape) )
			{}

			if ( Input.GetKey(KeyCode.Home) )
			{}
		}

		switch (estado)
		{
			case Estados.inicio:
				mensaje_fin.text = "";
				resetearPosiciones(-3.0f);
				golesPlayer1 = 0;
				golesPlayer2 = 0;
				estado = Estados.sacaPlayer1;
				break;

			case Estados.golPlayer2:
			case Estados.golPlayer1:

				if ( golesPlayer1 == 1 || golesPlayer2 == 1 )
				{
					// juego terminado
					estado = Estados.fin;
					resetearPosiciones(0.0f);
				}
				else if ( estado == Estados.golPlayer1 )
				{
					// gol del player1
					estado = Estados.sacaPlayer2;
					resetearPosiciones(3.0f);
				}
				else
				{
					// gol del player2
					estado = Estados.sacaPlayer1;
					resetearPosiciones(-3.0f);
				}
				break;

			case Estados.fin:
				if ( golesPlayer1 == 1 )
					mensaje_fin.text = "Ganó el jugador 1! Apriete algún botón para reiniciar.";
			 	else
					mensaje_fin.text = "Ganó el jugador 2! Apriete algún botón para reiniciar.";
				estado = Estados.esperandoReinicio;
				break;
		}
	}

	void resetearPosiciones(float discoPosition)
	{
		player1.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, -6f);
		player2.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, 6f);
		disco.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, discoPosition);
	}

}

