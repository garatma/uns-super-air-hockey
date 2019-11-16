using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco : MonoBehaviour
{
	public ControladorJuego juego;
	public MeshCollider meshCollider;
	private Vector3 direccion;
    private float velocidad = 0.3f;

	void Start()
	{
		desactivar();
	}

    void OnTriggerEnter(Collider colision)
	{
		GameObject obj = colision.gameObject;

        if (colision.gameObject.tag == "Jugador")
        {
            juego.colisionDiscoJugador();

            Vector2 posDisco = new Vector2(GetComponent<Rigidbody>().position.x, GetComponent<Rigidbody>().position.z);
            Vector2 posJugador = new Vector2(obj.GetComponent<Rigidbody>().position.x, obj.GetComponent<Rigidbody>().position.z);
            Vector2 dirGolpe = posDisco - posJugador;

            direccion.z = -direccion.z;
            direccion += new Vector3(dirGolpe.x, 0.0f, dirGolpe.y);
            direccion = new Vector3(
                    Mathf.Clamp(direccion.x, -1.25f, 1.25f),
                    0.0f,
                    Mathf.Clamp(direccion.z, -1.25f, 1.25f)
            );
        }
        else if (colision.gameObject.tag == "Costado")
            direccion.x = -direccion.x;
        else if (colision.gameObject.tag == "Fondo")
        {
            juego.colisionDiscoFondo();
            direccion.z = -direccion.z;
        }
        else if (colision.gameObject.tag == "ArcoJugador1")
        {
            direccion = new Vector3(0.0f, 0.0f, 0.0f);
            juego.golJugador2();
        }
        else if (colision.gameObject.tag == "ArcoJugador2")
        {
            direccion = new Vector3(0.0f, 0.0f, 0.0f);
            juego.golJugador1();
        }
	}

	void Update()
	{
		GetComponent<Rigidbody>().position += direccion * velocidad * Time.deltaTime * 40;
		GetComponent<Rigidbody>().position = new Vector3(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, -50f, 50f),
			0.4f,
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, -100f, 100f)
		);
	}

	public void activar()
	{
		meshCollider.enabled = true;
	}

	public void desactivar()
	{
		meshCollider.enabled = false;
	}

    public void setDireccion(float x, float y, float z)
    {
        direccion = new Vector3(x, y, z);
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
