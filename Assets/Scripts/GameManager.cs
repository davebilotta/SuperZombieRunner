using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public Text continueText;
	private float blinkTime = 0f;
	private bool blink;  // whether it needs to blink or not

	private bool gameStarted;
	private TimeManager timeManager;
	private GameObject player;

	// want to reposition floor towards bottom of screen no matter what resolution is 
	private GameObject floor;
	// want to reference spawner so we can turn it off when game first begins
	private Spawner spawner;

	public Text scoreText;
	private float elapsedTime = 0f;
	private float bestTime = 0f;
	private bool beatBestTime;

	void Awake() { 
		// could just assign these by dragging in Unity as well. 
		floor = GameObject.Find("Foreground");
		spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
		timeManager = GetComponent<TimeManager>();
	}


	// Use this for initialization
	void Start () {
		var floorHeight = floor.transform.localScale.y;

		var pos = floor.transform.position;

		pos.x = 0;
		pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnits) / 2) + floorHeight / 2;

		floor.transform.position = pos;

		spawner.active = false;

		//ResetGame();

		continueText.text = "PRESS ANY BUTTON TO START";

		Time.timeScale = 0;

		bestTime = PlayerPrefs.GetFloat("BestTime");
	}
	
	// Update is called once per frame
	void Update () {
		// check to see if player has gone off screen 

		if (!gameStarted && (Time.timeScale == 0)) { 
			if (Input.anyKeyDown) { 
				timeManager.ManipulateTime(1f,1f);
				ResetGame();
			}
		}

		// can't use Time.deltaTime because when game first starts timeScale is 0 so deltaTime is also 0 

		if (!gameStarted) { 
			blinkTime += 1;


			if (blinkTime % 40 == 0) { 
				blink = !blink; // toggle
			}
			continueText.canvasRenderer.SetAlpha(blink ? 0f : 1f);

			var textColor = beatBestTime ? "#FF0" : "#FFF";

			//scoreText.text = "TIME: " + FormatTime(elapsedTime) + "\nBEST: " + FormatTime(bestTime);

			scoreText.text = "TIME: " + FormatTime(elapsedTime) + "\n<color=" + textColor + ">BEST: " + FormatTime(bestTime) + "</color>";


		} 
		else { 
			elapsedTime += Time.deltaTime;
			// when game is running, only display elapsedTime
			scoreText.text = "TIME: " + FormatTime(elapsedTime);
		}
	
	}

	void OnPlayerKilled() { 
		spawner.active = false;

		var playerDestroyScript = player.GetComponent<DestroyOffScreen>();
		playerDestroyScript.DestroyCallback -= OnPlayerKilled; // unlink

		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

		timeManager.ManipulateTime(0f,5.5f);

		gameStarted = false;

		continueText.text = "PRESS ANY BUTTON TO RESTART";

		if ( elapsedTime > bestTime) { 
			bestTime = elapsedTime;
			beatBestTime = true;

			// save values into unity (similar to cookies?)
			PlayerPrefs.SetFloat("BestTime",bestTime);
		}
	}

	void ResetGame() { 
		// will reset game when player goes off screen
		spawner.active = true;

	//	player = GameObjectUtil.Instantiate(playerPrefab,new Vector3(0,(Screen.height / PixelPerfectCamera.pixelsToUnits) /2   ,0));

		//player = GameObjectUtil.Instantiate(playerPrefab,new Vector3(0,100 ,0));

		player = GameObjectUtil.Instantiate(playerPrefab,new Vector3(0,(Screen.height / PixelPerfectCamera.pixelsToUnits) /2 + 100,0));

		var playerDestroyScript = player.GetComponent<DestroyOffScreen>();
		playerDestroyScript.DestroyCallback += OnPlayerKilled;

		gameStarted = true;

		continueText.canvasRenderer.SetAlpha(0f);

		elapsedTime = 0f;
		beatBestTime = false;
	}

	public string FormatTime(float time) { 
		//return time.ToString();
		TimeSpan t = TimeSpan.FromSeconds(time);
		return string.Format("{0:D2}:{1:D2}",t.Minutes, t.Seconds);

	}
}
