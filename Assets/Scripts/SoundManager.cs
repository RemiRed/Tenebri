using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;
	void Awake(){

		if(instance == null){

			instance = this as SoundManager;

		}else{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(this);
	}

	AudioSource audioSource;

	public bool on; 
	public AudioClip[] SFX;
	public AudioClip[] BGM;	

	void Start(){

		audioSource = GetComponent<AudioSource> ();
	}

	public void PlaySFX(int _sfx){

		if(on){

			audioSource.PlayOneShot(SFX[_sfx]);		
		}
	}

	public void PlayBGM(int _bgm){

		if(on){

			if(BGM[_bgm] != audioSource.clip ){

				audioSource.clip = BGM[_bgm];
				GetComponent<AudioSource>().Play();
			}
		}
	}
	public void PlayBGM(AudioClip _clip){

		if(on){

			if(_clip != audioSource.clip ){

				audioSource.clip = _clip;
				GetComponent<AudioSource>().Play();
			}
		}
	}
	public void PauseAndUnpauseBGM(bool _b){

		if(on){
			
			if(_b){
				audioSource.volume *= 0.1f;
			}else{
				audioSource.volume *= 10.0f;
			}
		}
	}
}