using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOffsetVelocity : MonoBehaviour
{
	private Manager manager;
	private Rigidbody2D rb;
	private SpriteRenderer sr;

	public float multiplier = 1f;

	private float length;


	// Use this for initialization
	void Start ()
	{
		this.sr = GetComponent<SpriteRenderer> ();
		this.rb = GetComponent<Rigidbody2D> ();

		this.length = sr.size.x / 2;//14.2f;
		this.manager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<Manager> ();

	}

	void Update ()
	{
		if (transform.position.x < -length / 2) {
			resetPosition ();
		}
	}

	void FixedUpdate ()
	{
		this.rb.velocity = this.manager.getObstaclesVelocity () * multiplier;
	}

	private void resetPosition ()
	{
		transform.position += new Vector3 (length, 0, 0);
	}
}
