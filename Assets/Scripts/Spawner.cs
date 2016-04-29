﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;
	public float delay = 2.0f;
	public bool active = true;

	public Vector2 delayRange = new Vector2(1,2); // using Vector2 not for x,y but for min and max (Strange but whatever)

	// Use this for initialization
	void Start () {
		ResetDelay();
		StartCoroutine(EnemyGenerator());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator EnemyGenerator() { 
		yield return new WaitForSeconds(delay);

		if (active) { 
			var newTransform = transform;

			//Instantiate(prefabs[Random.Range(0,prefabs.Length)],newTransform.position,Quaternion.identity);
			GameObjectUtil.Instantiate(prefabs[Random.Range(0,prefabs.Length)],newTransform.position);
			ResetDelay();
		
		}

		StartCoroutine(EnemyGenerator());
	}

	void ResetDelay() { 
		delay = Random.Range(delayRange.x,delayRange.y);
	}
}