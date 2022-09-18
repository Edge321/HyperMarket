using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehavior : MonoBehaviour
{
	public static AudioBehavior Instance { get; private set; }

	private AudioSource audioSource;
	private void Start()
	{
		audioSource = GetComponent<AudioSource>();

		Instance = this;
	}
	public void ChangeBGM(AudioClip music)
	{
		audioSource.clip = music;
		audioSource.Play();
	}
	public void PlaySound(AudioClip sound)
	{
		audioSource.PlayOneShot(sound);
	}
}
