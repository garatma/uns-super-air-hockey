using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ConexionJugador : NetworkBehaviour
{
	public GameObject jugador;

    // Start se invoca antes del primer frame de update
    void Start()
    {
		if ( isLocalPlayer )
			CmdSpawnJugador();
    }

    // Update es invocado en cada frame
    void Update()
    {

    }

	[Command]
	void CmdSpawnJugador()
	{
		GameObject iniciar = Instantiate(jugador);
		NetworkServer.SpawnWithClientAuthority(iniciar, connectionToClient);
	}
}
