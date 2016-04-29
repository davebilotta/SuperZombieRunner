using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IRecycle { 

	void Restart();
	void Shutdown();
}

public class RecycleGameObject : MonoBehaviour {

	private List<IRecycle> recycleComponents;

	void Awake() {
		// Get all components attached to the gameobject we're on 
		var components = GetComponents<MonoBehaviour>();
		recycleComponents = new List<IRecycle>();

		foreach (var component in components) {
			// determine if component implements IRecycle interface
			if (component is IRecycle) {
				recycleComponents.Add(component as IRecycle);
			}
		}

		//Debug.Log(name + " Found " + recycleComponents.Count + " Components");
	}

	public void Restart() { 
		gameObject.SetActive(true);

		// Now we're not just restarting one object, we're restarting any of its children that implement the IRecycle interfact
		foreach (var component in recycleComponents) {
			component.Restart();
		}
	}

	public void Shutdown() { 
		gameObject.SetActive(false);

		foreach (var component in recycleComponents) { 
			component.Shutdown();
		}
	}

}
