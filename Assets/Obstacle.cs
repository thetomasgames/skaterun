using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

	private Rigidbody2D rb;
	private Manager manager;
	private float minX = -20;


	void Start ()
	{
		this.rb = GetComponent<Rigidbody2D> ();	
		this.manager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<Manager> ();
	}


	void FixedUpdate ()
	{
		if (transform.position.x < minX) {
			this.gameObject.SetActive (false);
		}
		rb.velocity = manager.getObstaclesVelocity ();
	}


}
