using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	public void ManipulateTime(float newTime, float duration) {  
		if (Time.timeScale == 0) { 
		// make a little faster so everything can start executing as we go back to normal time scale 
			Time.timeScale = 0.1f;
		}

		StartCoroutine(FadeTo(newTime,duration));
	}

	IEnumerator FadeTo(float value, float time) { 
		for (float t = 0f; t < 1f; t += Time.deltaTime / time) { 
		
			Time.timeScale = Mathf.Lerp(Time.timeScale, value, t);

			// don't have scenario where time is so close to 0 so we don't reach it, or it takes too long to scale down 
			// if it's close enough to 0, just set it to 0

			if (Mathf.Abs(value - Time.timeScale) < .01) {
				Time.timeScale = value;
				return false;
			}

			yield return null;
		}
	}
}
