using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using wuxingogo.Runtime;

public class LobbyPanel : NetworkLobbyManager
{
	public LobbyDiscovery discovery = null;
	public bool isServer = false;
	public GameObject StartBtn = null;
	public GameObject CancelBtn = null;
	[X]
	void EnableDiscovery( bool enable )
	{
		discovery.gameObject.SetActive( enable );
	}
	[X]
	public void SetMainMachine()
	{
		EnableDiscovery( true );
	}

	IEnumerator SearchGame()
	{
		int count = 0;
		XLogger.Log( "Seach Game .... " );
		while( count < 3 )
		{
			count++;
			XLogger.Log( "No one " );
			yield return new WaitForSeconds( 1 );
		}
		
		//Connect
		if( discovery.Address.Count != 0 )
		{
			isServer = false;
			XLogger.Log( "Discovery" );
//			networkAddress = Discovery.Address[0];
			this.StartClient();
			EnableDiscovery( false );
//			Discovery.AsClient ();
			StartBtn.SetActive( false );
			XLogger.Log( "Found Game..Join party " );
		}
		//Host
		else
		{
			isServer = true;
			XLogger.Log( "NotFound..Create party" );
//			this.StartServer ();
			this.StartHost();
			discovery.StopBroadcast();

			discovery.Initialize();

			discovery.StartAsServer();
			//StartBtn.SetActive( true );


		}
		
	}

	private void Awake()
	{
		discovery.lobbyPanel = this;
		discovery.Initialize();
	}

	[X]
	public void CancelSearch()
	{
		EnableDiscovery( false );
	}
	[X]
	public void StartGame()
	{
		EnableDiscovery( true );
		StartCoroutine( SearchGame() );
	}
}
