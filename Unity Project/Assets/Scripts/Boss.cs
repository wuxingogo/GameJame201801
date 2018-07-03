using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wuxingogo.Runtime;
using wuxingogo.tools;

public class Boss : MonoBehaviour
{
	public SpriteRenderer renderer = null;
	public SectorShapeXY collider = null;
	public Animator animator = null;
	public readonly int isRun = Animator.StringToHash( "isRun" );
	public BoxCollider2D collider2D = null;
	public Transform catchLeft = null;
	public Transform catchRight = null;
	public bool isAngry
	{
		get { return _isAngry; }
		set
		{
			if( _isAngry != value )
			{
				_isAngry = value;
				ResetAnimation();
			}
		}
	}

	public Vector3 catchPoint(bool isFilp)
	{
		return isFilp ? catchRight.position : catchLeft.position;
	}
	
	public void SetTarget(Vector3 vector3)
	{
		//var dis = (collider.transform.position - vector3).normalized;
		//collider.transform.up = dis;

	}

	public bool isWalk { get; set; }

	public void SetDefaultAnimation()
	{
		animator.Play( "Teacher_stand" );
	}
	public void SetWalk()
	{
		animator.Play( "Teacher_walk" );
	}

	public void SetCatch()
	{
		animator.Play( "Teacher_catch" );
	}
	

	private bool _isAngry = false;

	[X]
	public void ResetAnimation()
	{
		animator.SetBool( isRun, isAngry );
	}

	public bool isFilpX
	{
		get { return _isFilpX; }
		set
		{
			if( _isFilpX != value )
			{
				_isFilpX = value;
				renderer.flipX = value;
			}

		}
	}

	private bool _isFilpX = false;

	public bool isInSector(Vector3 point)
	{
		return collider2D.bounds.Contains( point );
		//return collider.isInSector();
	}
}
