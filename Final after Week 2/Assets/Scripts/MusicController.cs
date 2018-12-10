using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour {

	public AudioSource audioSource1;
	public AudioSource audioSource2;
	public AudioSource audioSource3;
	Scene currentScene;
	string currentSceneName;

	bool music1;
	bool music2;
	bool music3;

	private static MusicController instance = null;
	public static MusicController Instance {
		get { return instance; }
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		audioSource1.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}

		currentScene = SceneManager.GetActiveScene ();
		currentSceneName = currentScene.name;
		Debug.Log (currentSceneName);
		if ((currentSceneName == "StartScreen" || currentSceneName == "Controls" || currentSceneName == "Credits" || currentSceneName == "End") && !music1) {
			audioSource1.Play ();
			audioSource2.Stop ();
			audioSource3.Stop ();
			music1 = true;
			music2 = false;
			music3 = false;
		} else if ((currentSceneName == "FireTutorial" || currentSceneName == "IceTutorial" || currentSceneName == "LightningTutorial") && !music2) {
			audioSource1.Stop ();
			audioSource2.Play ();
			audioSource3.Stop ();
			music1 = false;
			music2 = true;
			music3 = false;
		} else if ((currentSceneName == "StartScreen" || currentSceneName == "Controls" || currentSceneName == "Credits" || currentSceneName == "End") && music1) {
			
		} else if ((currentSceneName == "FireTutorial" || currentSceneName == "IceTutorial" || currentSceneName == "LightningTutorial") && music2) {
			
		} else if (!music3) {
			audioSource1.Stop ();
			audioSource2.Stop ();
			audioSource3.Play ();
			music1 = false;
			music2 = false;
			music3 = true;
		}
	}
}
