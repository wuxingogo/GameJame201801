using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChange : MonoBehaviour {
	
	public Sprite MouseEnter;
	public Sprite MouseExit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeEnter(){
		GetComponent<Image> ().sprite = MouseEnter;
	}

	public void ChangeExit(){
		GetComponent<Image> ().sprite = MouseExit;
	}
}
