using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSlide : Button {

	protected override void DoStateTransition(SelectionState state, bool instant)
	{
		base.DoStateTransition(state, instant);
		switch (state)
		{

		case SelectionState.Disabled:

			break;

		case SelectionState.Highlighted:

			GetComponent<SpriteChange> ().ChangeEnter ();

			break;

		case SelectionState.Normal:

			GetComponent<SpriteChange> ().ChangeExit ();

			break;

		case SelectionState.Pressed:

			break;

		default:

			break;

		}

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
