using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

	public AudioSource backgroundMusic;
	public AudioSource ambientShore;

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	public void StopMusic()
	{
			ambientShore.Stop();
	}

}
