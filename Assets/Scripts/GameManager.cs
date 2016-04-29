using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;

	private GameObject player;

	// want to reposition floor towards bottom of screen no matter what resolution is 
	private GameObject floor;

	// want to reference spawner so we can turn it off when game first begins
	private Spawner spawner;

	void Awake() { 
		// could just assign these by dragging in Unity as well. 
		floor = GameObject.Find("Foreground");
		spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
	}


	// Use this for initialization
	void Start () {
		var floorHeight = floor.transform.localScale.y;

		var pos = floor.transform.position;

		pos.x = 0;
		pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnits) / 2) + floorHeight / 2;

		floor.transform.position = pos;

		spawner.active = false;

		ResetGame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPlayerKilled() { 
		spawner.active = false;

		var playerDestroyScript = player.GetComponent<DestroyOffScreen>();
		playerDestroyScript.DestroyCallback -= OnPlayerKilled; // unlink

		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

	}

	void ResetGame() { 
		// will reset game when player goes off screen
		spawner.active = true;

		player = GameObjectUtil.Instantiate(playerPrefab,new Vector3(0,(Screen.height / PixelPerfectCamera.pixelsToUnits) /2 ,0));

		var playerDestroyScript = player.GetComponent<DestroyOffScreen>();
		playerDestroyScript.DestroyCallback += OnPlayerKilled;
	}
}
