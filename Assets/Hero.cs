using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
	private bool rising;
	private bool grounded;
	private float timeJumpStart;
	private Vector3 initialPos;

	public Transform leftLimit;
	public Transform rightLimit;


	private Rigidbody2D rb;
	private Animator ac;
	private Manager manager;

	private AudioSource audioSource;


	public float force = 0.1f;
	public float risingTime = 0.1f;

	public AudioClip jumpClip;
	public AudioClip landClip;

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody2D> ();
		ac = GetComponent<Animator> (); 
		manager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<Manager> ();
		initialPos = transform.position;
	}

	// Update is called once per frame
	void Update ()
	{
		if (manager.playing) {
			if (Input.GetButtonDown ("Jump")) {
				if (grounded) {
					timeJumpStart = Time.time;
					addForce (20);
					rising = true;
					ac.SetTrigger ("Jump");
					playJumpSound ();
				}
			} else if (Input.GetButton ("Jump")) {
				if (rising) {
					addForce ();
				}
			} else if (Input.GetButton ("Jump")) {
				rising = false;
			}

			if ((Time.time - timeJumpStart) > risingTime) {
				rising = false;
			}

			rb.velocity = new Vector2 (Input.GetAxis ("Horizontal") * 10, rb.velocity.y);
		}

	}

	private void addForce (float multiplier = 1f)
	{
		rb.AddForce (Vector2.up * force * multiplier);
	}

	public void OnCollisionEnter2D (Collision2D c)
	{

		if (manager.playing) {
			if (c.collider.CompareTag ("Obstacle")) {
				manager.NofityHeroDied ();
				ac.SetTrigger ("Death");
			} else if (c.collider.CompareTag ("Ground")) {
				playLandSound ();
			}
		}
	}

	public void OnCollisionExit2D (Collision2D c)
	{
		if (c.collider.CompareTag ("Ground")) {
			grounded = false;
		}
	}

	public void OnCollisionStay2D (Collision2D c)
	{
		if (c.collider.CompareTag ("Ground")) {
			grounded = true;
		}

	}

	public void Restart ()
	{
		this.transform.rotation = Quaternion.identity;
		this.transform.position = initialPos;
		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0f;
		ac.SetTrigger ("Alive");
	}

	private void playJumpSound ()
	{
		audioSource.clip = jumpClip;
		audioSource.Play ();
	}

	private void playLandSound ()
	{
		audioSource.clip = landClip;
		audioSource.Play ();
	}
}
