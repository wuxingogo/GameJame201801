using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using wuxingogo.tools;
using wuxingogo.Runtime;
public class LobbyDiscovery : NetworkDiscovery {

	public LobbyPanel lobbyPanel = null;
	public List<string> Address = new List<string> ();
	private bool isInit = false;
	void OnEnable ()
	{
		isInit = true;
		XLogger.Log( "Discovery Enable" );
		Address.Clear ();
		Initialize();
		this.StartAsClient ();

	}

	[ContextMenu("StartAsClient")]
	public void AsClient()
	{
		this.StartAsClient ();
	}
	[ContextMenu("StartAsServer")]
	public void AsServer()
	{
		this.StartAsServer ();
	}
	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		if( !isInit )
			return;
		base.OnReceivedBroadcast (fromAddress, data);
		XLogger.Log (string.Format("OnReceivedBroadcast : fromaddress {0}, data {1}", fromAddress, data));

		lobbyPanel.networkAddress = fromAddress;
		lobbyPanel.networkPort = lobbyPanel.networkPort;
		lobbyPanel.StopAllCoroutines();
		lobbyPanel.StartClient ();

		StopBroadcastNetwork ();

		if (!Address.Contains (fromAddress)) {
			Address.Add (fromAddress);
		}
	}

	public void StopBroadcastNetwork()
	{
		var initialize = this.Initialize ();
		if (initialize) {
			this.StopBroadcast ();
		}
	}
}
