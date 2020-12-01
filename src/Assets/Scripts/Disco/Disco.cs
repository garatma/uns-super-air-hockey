using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Disco : NetworkBehaviour
{
	public ControladorJuego juego;
	private Vector3 direccion;
    private float velocidad = 0.3f;
    private Vector3 nuevaDireccion;

	void Start()
	{
		desactivar();
	}

    [ServerCallback]
    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector3 collisionNormal)
    {
        nuevaDireccion = Vector3.Reflect(direccion, collisionNormal);
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
            juego.RpcGolJugador2();
        }
        else if (colision.gameObject.tag == "ArcoJugador2")
        {
            direccion = new Vector3(0.0f, 0.0f, 0.0f);
            juego.RpcGolJugador1();
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
		GetComponent<MeshCollider>().enabled = true;
	}

	public void desactivar()
	{
		GetComponent<MeshCollider>().enabled = false;
	}

    public void setearDireccion(float x, float y, float z)
    {
        direccion = new Vector3(x, y, z);
    }

    public Vector2 obtenerPosicion()
    {
        return new Vector2(GetComponent<Rigidbody>().position.x, GetComponent<Rigidbody>().position.z);
    }

    public void setearPosicion(float x, float y, float z)
    {
        GetComponent<Rigidbody>().position = new Vector3(x, y, z);
    }
}
