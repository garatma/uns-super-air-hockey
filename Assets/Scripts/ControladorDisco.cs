using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDisco : MonoBehaviour
{
	public ControladorJuego juego;

	private Vector3 movimiento;

    private float reaccion = 0.3f;

    void OnTriggerEnter(Collider colision)
	{
		GameObject obj = colision.gameObject;

        if (colision.gameObject.tag == "Jugador")
        {
            juego.colisionDiscoJugador();

            Vector2 posDisco = new Vector2(GetComponent<Rigidbody>().position.x, GetComponent<Rigidbody>().position.z);
            Vector2 posPlayer = new Vector2(obj.GetComponent<Rigidbody>().position.x, obj.GetComponent<Rigidbody>().position.z);
            Vector2 dirGolpe = posDisco - posPlayer;

            movimiento.z = -movimiento.z;
            movimiento += new Vector3(dirGolpe.x, 0.0f, dirGolpe.y);
            movimiento = new Vector3(
                    Mathf.Clamp(movimiento.x, -1.25f, 1.25f),
                    0.0f,
                    Mathf.Clamp(movimiento.z, -1.25f, 1.25f)
            );
        }
        else if (colision.gameObject.tag == "Costado")
            movimiento.x = -movimiento.x;
        else if (colision.gameObject.tag == "Fondo")
        {
            juego.colisionDiscoFondo();
            movimiento.z = -movimiento.z;
        }
        else if (colision.gameObject.tag == "ArcoJugador1")
        {
            movimiento = new Vector3(0.0f, 0.0f, 0.0f);
            juego.golIA();
        }
        else if (colision.gameObject.tag == "ArcoIA")
        {
            movimiento = new Vector3(0.0f, 0.0f, 0.0f);
            juego.golJugador1();
        }
	}

	void Update()
	{
		GetComponent<Rigidbody>().position += movimiento * reaccion * Time.deltaTime *40;
		GetComponent<Rigidbody>().position = new Vector3(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, -50f, 50f),
			0.4f,
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, -100f, 100f)
		);
	}

    public void setMovimiento(float x, float y, float z)
    {
        movimiento = new Vector3(x, y, z);
    }

    public Vector2 getPosicion()
    {
        return new Vector2(GetComponent<Rigidbody>().position.x, GetComponent<Rigidbody>().position.z);
    }

    public void setPosicion(float x, float y, float z)
    {
        GetComponent<Rigidbody>().position = new Vector3(x, y, z);
    }
}
