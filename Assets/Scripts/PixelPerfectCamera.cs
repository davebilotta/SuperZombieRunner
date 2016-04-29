using UnityEngine;
using System.Collections;

public class PixelPerfectCamera : MonoBehaviour {

	public static float pixelsToUnits = 1f;
	public static float scale = 1f; 

	public Vector2 nativeResolution = new Vector2(240,160);

	void Awake() { 
		// This gets called before Start() 
		var camera = GetComponent<Camera>();

		if (camera.orthographic) { 
			scale = Screen.height / nativeResolution.y;
			pixelsToUnits *= scale;
			camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnits;

		}
	}

	/*// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	} */
}
