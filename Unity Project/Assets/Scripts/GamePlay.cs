using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using wuxingogo.Runtime;
using wuxingogo.tools;

public class GamePlay : XMonoBehaviour {

	public Boss boss = null;
	public Ball ball = null;
	public SpriteRenderer ballSprite = null;
	public Player[] players = null;
	public Transform[] ballPoint = null;
	public int CurrentBallIndex = 0;
	public float ballMoveSpeed = 1f;
	public float bossMoveSpeed = 1f;

	public float patrolTime = 0f;
	public float patrolLimitTime = 5f;
	public int bossMoveableIndex = 0;

	public float gameTime = 0f;
	public float DelayEnterTime = 1f;
	public bool isFirstEnterGame = false;
	public bool isFinishGame = false;

	public float timeInterval = 5f;
	public float accelerrate = 0.2f;
	public float accelerrateTime = 0;
	public float bossSpeedLimit = 2.5f;

	private Vector3 initBossPos = Vector3.zero;
	public Camera mainCamera = null;
	public int Score
	{
		get { return score; }
		set
		{
			score = value;
			scoreLabel.text = string.Format("Score:{0}", score);
		}
	}

	private int score = 0;
	private float holdTime = 0;
	public bool isHighScore = false;
	public Text scoreLabel = null;
	public Text fadeOutLabel = null;

	public bool isGameing = false;
	public Player lastPlayer = null;
	private void Awake()
	{
		for( int i = 0; i < players.Length; i++ )
		{
			var p = players[ i ];
			p.playerIndex = i;
			p.gamePlay = this;
		}
		

		initBossPos = boss.transform.position;
		
	}

	private bool isThrowing = false;

	public void FireToPlayer( Player p )
	{
		for( int i = 0; i < players.Length; i++ )
		{
			var t = players[ i ];
			if( t == p && CurrentBallIndex != p.playerIndex)
			{
				FireBall( i );
				break;
				
			}
		}
	}
	public void FireBall( int index )
	{
		AudioManager.Inst.PlayThrow();
		if(isThrowing)
			return;
		
		isThrowing = true;
		if( isFinishGame )
			return;
		StopAllCoroutines();
		for( int i = 0; i < players.Length; i++ )
		{
			players[ i ].hasBall = false;
		}
		lastPlayer = players[ CurrentBallIndex ];
		CurrentBallIndex = index % 4;
		StartCoroutine(SetPlayerHasBall());

		patrolTime = 0;
		holdTime = 0;
		isHighScore = false;
		bossMoveableIndex = Random.Range( 0, 3 );
	}

	IEnumerator SetPlayerHasBall(){
		yield return new WaitForSeconds (0.3f);

		isThrowing = false;
		players[ CurrentBallIndex ].hasBall = true;
		lastPlayer = players[ CurrentBallIndex ];
		

	}

	public void InitCheat()
	{
		for( int i = 0; i < players.Length; i++ )
		{
			players[ i ].hasBall = false;
			players [i].timer = 0;
			players [i].animator.SetBool ("isHurry", false);
			players [i].isHurry = true;
		}
		CurrentBallIndex = Random.Range( 0, 4 );
		players[ CurrentBallIndex ].hasBall = true;
		lastPlayer = players[ CurrentBallIndex ];
		ball.transform.position = ballPoint[ CurrentBallIndex ].position;
		bossMoveableIndex = Random.Range( 0, 3 );
	}
	[X]
	public void EnterGame()
	{
		isGameing = true;
		isThrowing = false;
		boss.transform.position = initBossPos;
		InitCheat();
		boss.SetDefaultAnimation();
		patrolTime = 0f;
		gameTime = 0f;
		accelerrateTime = 0f;
		bossMoveSpeed = 1f;
		DelayEnterTime = 1f;
		isFirstEnterGame = false;
		isFinishGame = false;
		boss.SetWalk();
		AudioManager.Inst.StopFail();
		AudioManager.Inst.PlayPlaying();
		Score = 0;

		isHighScore = false;
		holdTime = 0;
		fadeOutLabel.color = fadeOutLabel.color.SetAlpha( 0 );
	}

	void Update()
	{
		accelerrateTime += Time.deltaTime;
		
		//----
		if (accelerrateTime > timeInterval) {
			bossMoveSpeed += accelerrate;
			if (bossMoveSpeed > bossSpeedLimit)
				bossMoveSpeed = bossSpeedLimit;
			accelerrateTime -= timeInterval;
		}
		//-----
		gameTime += Time.deltaTime;
		if( gameTime < DelayEnterTime || isFinishGame)
		{
			return;
		}
		var currentPlayer = players[ CurrentBallIndex ];
		var cheatPoint = ballPoint[ CurrentBallIndex ].position;
		ballSprite.sortingOrder = currentPlayer.As<SpriteRenderer>().sortingOrder +1;
		
		ball.transform.position = Vector3.MoveTowards( ball.transform.position, cheatPoint, Time.deltaTime * ballMoveSpeed );
		
		patrolTime += Time.deltaTime;

		var bossPos = boss.transform.position;
		var ballPos = ball.transform.position;

		holdTime += Time.deltaTime;
		
		if( holdTime > 2 && isHighScore == false )
		{
			if (currentPlayer.animator.GetBool ("isHurry")) {
				Score += 5;
				fadeOutLabel.text = "+5";
				currentPlayer.animator.SetBool ("isHurry", false);
				AudioManager.Inst.PlayLaugh3();
			} else {
				Score += 3;
				fadeOutLabel.text = "+3";
				
				var rand = Random.Range( 0, 2 );
				if(rand == 0)
					AudioManager.Inst.PlayLaugh1();
				else
				{
					AudioManager.Inst.PlayLaugh2();
				}
			}

			holdTime = 0;
			var position = currentPlayer.transform.position;
			var uiPos = mainCamera.WorldToScreenPoint( position );
			uiPos.z = 1;
			fadeOutLabel.transform.position = uiPos;
			var locaPosY = fadeOutLabel.transform.localPosition.y;
			fadeOutLabel.color = fadeOutLabel.color.SetAlpha( 1 );
			fadeOutLabel.transform.DOMoveY(uiPos.y + 100, 0.8f);
			fadeOutLabel.DOFade( 0, 0.8f );
			isHighScore = true;
		}else if( holdTime > 1 && isHighScore == true )
		{
			Score += 2;
			holdTime = 0;
			var position = currentPlayer.transform.position;
			var uiPos = mainCamera.WorldToScreenPoint( position );
			uiPos.z = 1;
			fadeOutLabel.transform.position = uiPos;
			fadeOutLabel.color = fadeOutLabel.color.SetAlpha( 1 );
			fadeOutLabel.transform.DOMoveY(uiPos.y + 100, 0.8f);
			fadeOutLabel.text = "+2";
			fadeOutLabel.DOFade (0, 0.8f);
		}
		
		if( patrolTime > patrolLimitTime )
		{
			boss.isAngry = true;
			AudioManager.Inst.PlayAngry();
			boss.transform.position =
				Vector3.Lerp( bossPos, ballPos, Time.deltaTime * bossMoveSpeed );
			boss.SetTarget( ballPos );
			bool isFilp = bossPos.x < ballPos.x;
			boss.isFilpX = isFilp;
			
		}
		else
		{
			boss.isAngry = false;
//			XLogger.Log( currentPlayer.neighborPoint[ bossMoveableIndex ].name, currentPlayer.neighborPoint[ bossMoveableIndex ].gameObject );
			var movePoint = currentPlayer.neighborPoint[ bossMoveableIndex ].position;
			boss.transform.position =
				Vector3.Lerp( bossPos, movePoint, Time.deltaTime * bossMoveSpeed );
			boss.SetTarget( movePoint );
			var manguite = ( bossPos - movePoint ).magnitude;
//			XLogger.Log( manguite );
			
			if( manguite < 1f )
			{
				bossMoveableIndex = Random.Range( 0, 3 );
			}
			
			
			bool isFilp = bossPos.x < movePoint.x;
			boss.isFilpX = isFilp;
		}
		if( boss.isInSector(ball.transform.position) )
		{
			XLogger.LogError( "FinishGame" );
			isFinishGame = true;
			boss.SetCatch();
			bool isFilp = bossPos.x < ballPos.x;
			boss.isFilpX = isFilp;
			AudioManager.Inst.PlayCatch();
			
			AudioManager.Inst.StopPlaying();
			AudioManager.Inst.PlayFail();
			AudioManager.Inst.PlayTeacherLaugh();
			
			StopAllCoroutines();
			ball.transform.position = boss.catchPoint(isFilp);
			ballSprite.sortingOrder = 1000;
			for( int i = 0; i < players.Length; i++ )
			{
				var c = players[ i ];
				if( c == lastPlayer )
				{
					c.Cry();
				}
				else
				{
					c.Win();
				}
			}
			lastPlayer.Cry();
			Invoke( "DelayFinish", 4 );
		}

//		var buttonUp = Input.GetMouseButtonUp( 0 );
//		if( buttonUp )
//		{
//			var mousePos = Input.mousePosition;
//			var mainCamera = Camera.main;
//			for( int i = 0; i < players.Length; i++ )
//			{
//				var rect = players[ i ].As<SpriteRenderer>();
//				var spriteRect = new Rect(players[i].transform.position, rect.size);
////				if( rect.bounds.Contains( mousePos ) )
////				{
////					XLogger.Log( i );
////					FireBall( i );
////					break;
////				}
//////				if( RectTransformUtility.RectangleContainsScreenPoint( rect, mousePos ) )
//////				{
//////					XLogger.Log( i );
//////					FireBall( i );
//////					break;
//////					
//////				}
//////				if( rect.rect.Contains( mousePos ) )
//////				{
//////					XLogger.Log( i.ToString() );
//////					FireBall( i );
//////					break;
//////					
//////				}
//
//			}
//		}


	}

	void DelayFinish()
	{
		UIManager.Inst.OpenGameOver();
	}
}
