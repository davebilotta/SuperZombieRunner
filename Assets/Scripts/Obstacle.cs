using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour, IRecycle {

	public Sprite[] sprites;

	public Vector2 colliderOffset = Vector2.zero;

	public void Restart() { 
		var renderer = GetComponent<SpriteRenderer>();

		// set a random sprite on the renderer itself - every time we restart script, we'll 
		// get a random sprite from the array
		renderer.sprite = sprites[Random.Range(0,sprites.Length)];

		// need to resize the collider so it's not the size of the prefab (in this case the desk,
		// which is bigger than many of our sprites)
		var collider = GetComponent<BoxCollider2D>();
		//collider.size = new Vector2(renderer.sprite.texture.width, renderer.sprite.texture.height);
		//collider.size = renderer.bounds.size;
		var size = renderer.bounds.size;

		size.y += colliderOffset.y;

		collider.size = size; 
		collider.offset = new Vector2(-colliderOffset.x, collider.size.y/2 - colliderOffset.y);

	}

	public void Shutdown() { 
	}
}
