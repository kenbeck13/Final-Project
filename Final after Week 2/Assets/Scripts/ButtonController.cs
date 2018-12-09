using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SwitchtoControlScene(){
		SceneManager.LoadScene ("Controls");
	}
	public void SwitchtoCreditsScene(){
		SceneManager.LoadScene ("Credits");
	}
	public void SwitchtoStartScene(){
		SceneManager.LoadScene ("StartScreen");
	}
	public void SwitchtoGameScene(){
		SceneManager.LoadScene ("1-1");
	}
}
