using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wuxingogo.tools;

public class AnalyticsManager : SingletonC<AnalyticsManager> {
    private void Awake()
    {
       
        Debug.Log("Unity SDK  init begin "); 
//        TalkingDataGA.OnStart("CF5AE56152C449618B8BBB844D18FA5D", "TalkingData");
        Debug.Log("Unity SDK  init completed ");

        

    }

    public void RecordEnterSingleGame()
    {
//        TDGAAccount.SetAccount( TalkingDataGA.GetDeviceId() );
//        TDGAMission.OnBegin( "EnterSingleGame" );
    }

    public void RecordNewUser()
    {
        int v = PlayerPrefs.GetInt( "FirstEnterGame", 0 );
        if( v == 0 )
        {
            //todo list
            PlayerPrefs.SetInt( "FirstEnterGame", 1 );
        }
    }

    public void RecordScore( int score, int maxScore)
    {
//        TDGAMission.OnCompleted("EnterSingleGame");
//        Dictionary<string, object> dict = new Dictionary<string, object>();
//        dict.Add( "score", score );
//        dict.Add( "maxScore", maxScore );
//        TalkingDataGA.OnEvent( "FinishGame", dict );
    }
}
