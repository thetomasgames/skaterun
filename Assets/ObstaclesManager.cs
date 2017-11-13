using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstaclesManager : MonoBehaviour
{
	System.Random random;
	public Transform createPosition;
	public List<GameObject> prefabs;

	private Dictionary<GameObject,List<GameObject>> avaliables;

	private Manager manager;

	void Start ()
	{
		random = new System.Random (System.Environment.TickCount);
		avaliables = new Dictionary<GameObject,List<GameObject>> ();
		prefabs.ForEach (p => avaliables.Add (p, new List<GameObject> ()));
		manager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<Manager> ();
		InvokeRepeating ("CreateRandomObstacle", 1.5f, 3f);
	}

	public void CreateRandomObstacle ()
	{
		if (manager.playing) {
			if (random.NextDouble () <= 0.9f) {
				GameObject go = GetOrCreateObstacle (prefabs [random.Next (0, prefabs.Count)]);
				go.transform.position = createPosition.position;
			}
		}
	}

	private GameObject GetOrCreateObstacle (GameObject gameObject)
	{
		GameObject go = avaliables [gameObject].Find (obj => !obj.activeSelf);
		if (go == null) {
			go = GameObject.Instantiate (gameObject, transform);
			avaliables [gameObject].Add (go);
		}
		go.SetActive (true);
		return go;
	}

	public void Init ()
	{
		StartCoroutine (generateInSeconds ());
	}

	private IEnumerator generateInSeconds ()
	{
		while (manager.playing) {
			yield return new WaitForSeconds (2);
			CreateRandomObstacle ();
		}
	}

	public void ClearObstacles ()
	{
		StopAllCoroutines ();
		if (avaliables != null) {
			foreach (KeyValuePair<GameObject,List<GameObject>> kv in avaliables) {
				kv.Value.ForEach (g => g.SetActive (false));
			}
		}
	}
}
