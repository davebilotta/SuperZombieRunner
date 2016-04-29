using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	public RecycleGameObject prefab;

	// list - can dynamically add or remove 
	private List<RecycleGameObject> poolInstances = new List<RecycleGameObject>();

	private RecycleGameObject CreateInstance(Vector3 position) {
		// this is the only place in game where we instantiate directly instead of routing through GameObjectUtil class 

		var clone = GameObject.Instantiate(prefab);
		clone.transform.position = position;
		clone.transform.parent = transform;

		poolInstances.Add(clone);

		return clone;
	}

	public RecycleGameObject NextObject(Vector3 position) { 
		RecycleGameObject instance = null;

		foreach (var go in poolInstances) {
			// test each to see if it's set to false
			if (go.gameObject.activeSelf != true) {
				instance = go;
				instance.transform.position = position;
			}
		}

		// If we haven't found one that is inactive, create a new one 
		if (instance == null) { 
			instance = CreateInstance(position);
		}

		instance.Restart();
		return instance;
	}
}
