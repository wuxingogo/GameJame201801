using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;
using wuxingogo.Runtime;
using wuxingogo.tools;

public class UIManager : SingletonC<UIManager>
{
	public Player[] players = null;
	public GameObject[] gameGroup = null;
	
	public GameObject gameOver = null;
	public GameObject gameStart = null;
	public GameObject producer = null;
	public GamePlay gamePlay = null;
	public Text gameFailedScore = null;
	public Text bestScoreLabel = null;

	private void Awake()
	{
		var analytics = AnalyticsManager.Inst;
	}

	void Start()
	{
		AnalyticsManager.Inst.RecordNewUser();
	}
	public void StartSingleGame(){
		AnalyticsManager.Inst.RecordEnterSingleGame();
		for( int i = 0; i < gameGroup.Length; i++ )
		{
			gameGroup[i].SetActive( true );
		}
		

		for( int i = 0; i < players.Length; i++ )
		{
			var p = players[ i ];
			p.playerMode = Player.PlayerMode.up;
		}
		gamePlay.EnterGame();
		gameStart.SetActive( false );
		gameOver.SetActive( false );
		producer.SetActive( false );
	}

	public void StartMultiGame(){
		for( int i = 0; i < gameGroup.Length; i++ )
		{
			gameGroup[i].SetActive( true );
		}
		
		gamePlay.EnterGame();
		gameStart.SetActive( false );
		gameOver.SetActive( false );
		producer.SetActive( false );
		
		players[ 0 ].playerMode = Player.PlayerMode.up;
		players[ 1 ].playerMode = Player.PlayerMode.down;
		
		players[ 2 ].playerMode = Player.PlayerMode.left;
		players[ 3 ].playerMode = Player.PlayerMode.right;
		
	}

	public void SettingOpen()
	{
		producer.SetActive( true );
		gameStart.SetActive( false );
	}

	public void SettingClose()
	{
		producer.SetActive( false );
		gameStart.SetActive( true );
	}

	public void RestartGame(){
		for( int i = 0; i < gameGroup.Length; i++ )
		{
			gameGroup[i].SetActive( true );
		}

		gamePlay.EnterGame();
		gameStart.SetActive( false );
		gameOver.SetActive( false );
		producer.SetActive( false );
	}

	public void OpenGameOver()
	{
		gamePlay.isGameing = false;
		gameOver.SetActive( true );
		for( int i = 0; i < gameGroup.Length; i++ )
		{
			gameGroup[i].SetActive( false );
		}
		gameFailedScore.text = gamePlay.Score.ToString();
		var bestValue = PlayerPrefs.GetInt( "BestScore", 0 );
		if( gamePlay.Score > bestValue )
		{
			bestValue = gamePlay.Score;
			PlayerPrefs.SetInt( "BestScore", gamePlay.Score );
			PlayerPrefs.Save();
		}
		AnalyticsManager.Inst.RecordScore( gamePlay.Score, bestValue );
		bestScoreLabel.text = bestValue.ToString();
	}
	

	public void BackToMain(){
		for( int i = 0; i < gameGroup.Length; i++ )
		{
			gameGroup[i].SetActive( false );
		}
		
		gameStart.SetActive( true );
		gameOver.SetActive( false );
	}
}
