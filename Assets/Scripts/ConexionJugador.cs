using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ConexionJugador : NetworkBehaviour
{
	public GameObject jugador;
	
    // Start is called before the first frame update
    void Start()
    {
		if ( isLocalPlayer )
			CmdSpawnJugador();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	[Command]
	void CmdSpawnJugador()
	{
		GameObject go = Instantiate(jugador); 
		NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
	}
}
