using UnityEngine;
using Mirror;

public class Servidor : NetworkManager
{
    public ControladorJuego juego;
    public GameObject jugador1;
    public GameObject jugador2;
    public Transform spawnJugador1;
    public Transform spawnJugador2;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform start = numPlayers == 0 ? spawnJugador1 : spawnJugador2;
        GameObject playerASpawnear;
        playerASpawnear = numPlayers == 0 ? jugador1 : jugador2;
        GameObject player = Instantiate(playerASpawnear, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        juego.RpcConectarNuevoJugador(numPlayers);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        juego.RpcReiniciarTodo();
        base.OnServerDisconnect(conn);
    }

	public override void OnStopServer()
	{
		juego.RpcReiniciarTodo();
		base.OnStopServer();
	}

	public override void OnStopHost()
	{
		juego.RpcReiniciarTodo();
		base.OnStopHost();
	}

	public override void OnStartHost() 
	{
    	juego.RpcReiniciarTodo();
		base.OnStartHost();
    }

	public override void OnStartServer() 
	{
    	juego.RpcReiniciarTodo();
		base.OnStartServer();
    }
}
