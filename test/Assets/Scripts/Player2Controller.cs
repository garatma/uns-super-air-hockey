using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public GameController game;

    private Vector3 movement;

    void FixedUpdate()
    {
        float movH = Input.GetAxis("Horizontal2");
		if ( movH > 0 ) movH = 1;
		else if ( movH < 0 ) movH = -1;

        switch(game.state)
        {
            case GameController.States.player2Serves:
                move(movH);
				if ( game.player2ClickedButton() )
                    game.state = GameController.States.player2Serving;
                break;

            case GameController.States.player2Serving:
                Vector2 puckPos = new Vector2(GetComponent<Rigidbody>().position.x,
                                              GetComponent<Rigidbody>().position.z);

                Vector2 player2Pos = new Vector2(game.puck.GetComponent<Rigidbody>().position.x,
                                                 game.puck.GetComponent<Rigidbody>().position.z);

                Vector2 hitDir= player2Pos - puckPos;

                movement = new Vector3(hitDir.x, 0.0f, hitDir.y);
                GetComponent<Rigidbody>().position += movement * game.player2Reaction;
                break;

            case GameController.States.playing:
                move(movH);
                break;
        }
    }

    void move(float movH)
    {
        movement = new Vector3(movH, 0.0f, 0.0f);
        GetComponent<Rigidbody>().position += movement * game.player2Reaction;
        GetComponent<Rigidbody>().position = new Vector3(
                Mathf.Clamp(GetComponent<Rigidbody>().position.x,-3.3f,3.3f),
                0.0f,
                6f);
    }

}
