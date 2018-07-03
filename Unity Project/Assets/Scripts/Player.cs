using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public enum PlayerMode
	{
		up,down,left,right
	};
	public PlayerMode playerMode;
	public bool isAi = false;

	public bool hasBall
	{
		get { return _hasBall; }
		set
		{
			_hasBall = value;
			if( hasBall )
			{
				animator.Play( "get" );
			}
		}
	}

	public void Cry()
	{
		animator.Play( "fail" );
	}

	public void Win()
	{
		animator.Play( "success" );
	}
	private bool _hasBall = false;
	public int playerIndex = 0;
	public GamePlay gamePlay = null;
	public bool isPress = false;
	public Vector3 pressPos = Vector3.zero;
	public Transform[] neighborPoint = null;
	public Animator animator = null;

	public float timeHurry = 10;
	public float timer = 0;
	public bool isHurry = true;

	public void Update()
	{
		if (gamePlay.isGameing && hasBall == false)
			timer += Time.deltaTime;
		if (timer >= timeHurry && isHurry) {
			animator.SetBool ("isHurry", true);
			isHurry = false;
		}
		if( isAi == false && gamePlay.isFinishGame == false )
		{
			InputMode (playerMode);
		}
	}

	private void OnMouseDown()
	{
		gamePlay.FireToPlayer( this );
		
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="direction"></param>
	public void FireBall(int index)
	{
		if( hasBall && playerIndex != index )
		{
			gamePlay.FireBall( index );
			timer = 0;
			isHurry = true;
			
			
			


			switch( index )
			{
				case 0:
					animator.Play( "throw_up" );
					break;
				case 1:
					animator.Play( "throw_down" );
					break;
				case 2:
					animator.Play( "throw_left" );
					break;
				case 3:
					animator.Play( "throw_right" );
					break;
					
			}
		}
	}

	void InputMode(PlayerMode playerMode){
		switch (playerMode) {
		case PlayerMode.up:
			if (Input.GetKeyUp (KeyCode.W)) {
				FireBall (0);
			} else if (Input.GetKeyUp (KeyCode.S)) {
				FireBall (1);
			} else if (Input.GetKeyUp (KeyCode.A)) {
				FireBall (2);
			} else if (Input.GetKeyUp (KeyCode.D)) {
				FireBall (3);
			}
			break;
		case PlayerMode.down:
			if (Input.GetKeyUp (KeyCode.I)) {
				FireBall (0);
			} else if (Input.GetKeyUp (KeyCode.K)) {
				FireBall (1);
			} else if (Input.GetKeyUp (KeyCode.J)) {
				FireBall (2);
			} else if (Input.GetKeyUp (KeyCode.L)) {
				FireBall (3);
			}
			break;
		case PlayerMode.left:
			if (Input.GetButton("JoyL2")) {
				FireBall (0);
			} else if (Input.GetButton("JoyL1")) {
				FireBall (1);
			} else if (Input.GetButton("JoyL0")) {
				FireBall (2);
			} else if (Input.GetButton("JoyL3")) {
				FireBall (3);
			}
			break;
		case PlayerMode.right:
			if (Input.GetButton("JoyR1")) {
				FireBall (0);
			} else if (Input.GetButton("JoyR2")) {
				FireBall (1);
			} else if (Input.GetButton("JoyR3")) {
				FireBall (2);
			} else if (Input.GetButton("JoyR0")) {
				FireBall (3);
			}
			break;
		}
	}
}
