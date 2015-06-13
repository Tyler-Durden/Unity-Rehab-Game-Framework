﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RemoteInfo : MonoBehaviour
{
	public string infoHostId;

	public string infoType;
	public string valueType;

	public void RefreshServerInfo()
	{
		Debug.Log( "Atualizando Informações" );
		ConnectionManager.InfoClient.Connect( "169.254.110.158"/*PlayerPrefs.GetString( infoHostId, ConnectionManager.LOCAL_SERVER_HOST )*/, 
												50000/*PlayerPrefs.GetInt( ConnectionManager.SERVER_PORT_ID, ConnectionManager.DEFAULT_SERVER_PORT )*/ );
		//ConnectionManager.InfoClient.SendString( infoType + " Info" );

		SelectionList.stringLists[ valueType ] = new List<string>();

		StartCoroutine( ReceiveInfo() );
	}

	IEnumerator ReceiveInfo()
	{
		while( Application.isPlaying ) 
		{
			string infoString = ConnectionManager.InfoClient.ReceiveString().Trim();

			if( infoString != "" ) 
			{
				foreach( string info in infoString.Split( ':' ) )
					SelectionList.stringLists[ valueType ].Add( info );
			}

			yield return null;
		}
	}

	void OnDestroy()
	{
		//ConnectionManager.InfoClient.Disconnect();
	}
}

