using UnityEngine;
using System.Collections;

public class AnimatedUVs : MonoBehaviour {
		
	public float xScrollSpeed = 0.02f;
	public float yScrollSpeed = 0.02f;
		
	void Update()
	{
		float xOffset = Time.time * xScrollSpeed;
		float yOffset = Time.time * yScrollSpeed;
		Debug.Log(yOffset);
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(yOffset % 1, xOffset % 1);
	}
}