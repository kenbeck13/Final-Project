using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour {

	public AudioSource audioSource1;
	public AudioClip[] clips;
	Scene currentScene;
	string currentSceneName;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		audioSource1.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		currentScene = SceneManager.GetActiveScene ();
		currentSceneName = currentScene.name;
		Debug.Log (currentSceneName);
		if (currentSceneName == "StartScreen" || currentSceneName == "Controls" || currentSceneName == "Credits" || currentSceneName == "End") {
			audioSource1.clip = clips[0];
			audioSource1.volume = 1f;
		} else {
			audioSource1.clip = clips[1];
			audioSource1.volume = 0.1f;
		}
	}
}
