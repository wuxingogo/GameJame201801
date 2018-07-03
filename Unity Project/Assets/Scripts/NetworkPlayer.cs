using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using wuxingogo.Runtime;

public class NetworkPlayer : NetworkLobbyPlayer {
	[X]
	[Command]
	public void CmdSendMsg(string msg)
	{
		XLogger.Log( "Cmd:" + msg );
		RpcReceiveMsg( msg );
	}
	[ClientRpc]
	public void RpcReceiveMsg(string msg)
	{
		XLogger.Log( "Rpc:" + msg );
	}

	private void Update()
	{
		if( isLocalPlayer )
		{
			if( Input.GetKeyUp( KeyCode.J ) )
			{
				CmdSendMsg( "j was pressed!" );
			}
			if( Input.GetKeyUp( KeyCode.K ) )
			{
				CmdSendMsg( "k was pressed!" );
			}
			if( Input.GetKeyUp( KeyCode.L ) )
			{
				CmdSendMsg( "l was pressed!" );
			}
		}
	}
}
