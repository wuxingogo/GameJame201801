using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wuxingogo.tools;

public class AudioManager : SingletonC<AudioManager>
{
	public AudioSource PressButton;
	public AudioSource ThrowPaper;
	public AudioSource Laugh1;
	public AudioSource Laugh2;
	public AudioSource Laugh3;
	public AudioSource Playing;
	public AudioSource TeacherAngry;
	public AudioSource TeacherCatch;
	public AudioSource Fail;
	public AudioSource TeacherLaugh;
	public void PlayButton(){
		PressButton.Play ();
	}

	public void PlayThrow(){
		ThrowPaper.Play ();
	}

	public void PlayLaugh1()
	{
		Laugh1.time = 0;
		Laugh1.Play ();
	}

	public void PlayLaugh2(){
		Laugh2.time = 0;
		Laugh2.Play ();
	}

	public void PlayLaugh3()
	{
		Laugh3.time = 0;
		Laugh3.Play ();
	}

	public void PlayPlaying(){
		Playing.Play ();
	}

	public void StopPlaying()
	{
		Playing.Stop();
	}

	public void PlayAngry(){
		TeacherAngry.Play ();
	}

	public void PlayCatch(){
		TeacherCatch.Play ();
	}

	public void PlayFail(){
		Fail.Play ();
	}

	public void StopFail()
	{
		Fail.Stop();
	}

	public void PlayTeacherLaugh()
	{
		TeacherLaugh.Play();
	}
}
