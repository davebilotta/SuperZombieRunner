using UnityEngine;
using System.Collections;

public class DestroyOffScreen : MonoBehaviour {

	public delegate void OnDestroy();
	public event OnDestroy DestroyCallback;

	public float offset = 16f; // how far off-screen it needs to be in order to get destroyed

	private bool offScreen;    // flag that object is off-screen and needs to be destroyed

	private float offScreenX = 0;

	private Rigidbody2D body2d;

	void Awake() { 
		body2d = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		// Width / pixelsToUnits = actual width of screen before we cut it in half

		offScreenX = (Screen.width / PixelPerfectCamera.pixelsToUnits) / 2 + offset;
	}
	
	// Update is called once per frame
	void Update () {
		// get position of object
		var posX = transform.position.x;

		// keep track of actual velocity so we know which direction object is facing
		var dirX = body2d.velocity.x;

		// absolute value of x position 
		if (Mathf.Abs(posX) > offScreenX) {
			// test direction to see if we're going off left or right 

			if (dirX < 0 && posX < -offScreenX) {
				offScreen = true;
			}
			else if (dirX > 0 && posX > offScreenX) {
				offScreen = true;
			}
		}
		else { 
			offScreen = false;
		}

		if (offScreen) { 
			OnOutOfBounds();
		}
	}

	public void OnOutOfBounds() { 
		// handles object going offscreen 

		// Current offScreen = false line is future-proofing for when we reuse this object later
		offScreen = false;

		GameObjectUtil.Destroy(gameObject);

		if (DestroyCallback != null) {
			DestroyCallback(); // call this as if it were a method
		}
	}
}
