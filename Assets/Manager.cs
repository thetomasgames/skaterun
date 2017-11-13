using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

	public float startSpeed = 3.0f;
	public float maxSpeed = 10.0f;
	private float currentSpeed;
	public bool playing;
	private float distance;

	public Canvas gameOverCanvas;
	public Text distanceText;
	public Hero hero;
	public ObstaclesManager obstaclesManager;
	public Button tryAgainButton;

	public AudioSource effectsSource;
	public AudioClip buttonClickClip;
	public AudioClip crashClip;

	public AudioSource musicSource;
	public AudioClip failedClip;
	public AudioClip musicClip;

	void Start ()
	{
		playNormalMusic ();
		tryAgainButton.enabled = false;
		hero.Restart ();
		distance = 0;
		gameOverCanvas.enabled = false;
		playing = true;
		currentSpeed = startSpeed;
		obstaclesManager.ClearObstacles ();
	}

	public void Restart ()
	{
		playEffect (buttonClickClip);
		Start ();
	}

	void Update ()
	{
		distance += currentSpeed * Time.deltaTime;
		updateDistanceText ();
		if (playing) {
			if (currentSpeed < maxSpeed) {
				currentSpeed += 1f * Time.deltaTime;
			}
		}
	}

	public void NofityHeroDied ()
	{
		playEffect (crashClip);
		playGameOverMusic ();
		tryAgainButton.enabled = true;
		gameOverCanvas.enabled = true;
		currentSpeed = 0;
		playing = false;
	}

	public Vector2 getObstaclesVelocity ()
	{
		return new Vector2 (-currentSpeed, 0);
	}

	private void updateDistanceText ()
	{
		this.distanceText.text = (int)distance + " m ";
	}

	private void playEffect (AudioClip clip)
	{
		effectsSource.clip = clip;
		effectsSource.Play ();
	}


	private void playGameOverMusic ()
	{
		musicSource.clip = failedClip;
		musicSource.Play ();
		musicSource.loop = false;
	}

	private void playNormalMusic ()
	{
		musicSource.clip = musicClip;
		musicSource.Play ();
		musicSource.loop = true;
	}
}
