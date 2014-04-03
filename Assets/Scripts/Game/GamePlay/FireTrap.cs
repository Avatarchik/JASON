using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour {
	[SerializeField] private GameObject mats;
	[SerializeField] private Color setColor;
	
	[SerializeField] private float time;

	[SerializeField] private bool isEnabled;

	private float colorValue;
	
	void Start() {
		StartCoroutine("ActivateGrill");
	}

	void FixedUpdate(){
		setColor = new Color(colorValue, colorValue, colorValue);
		mats.renderer.material.color = setColor;

		if(isEnabled) {
			if(colorValue <= 1)
				colorValue += 0.01f;
		} else {
			if(colorValue >= 0.2f)
				colorValue -= 0.01f;
		}
	}

	/** Activate the grill */
	private IEnumerator ActivateGrill(){
		while(true){
		yield return new WaitForSeconds(time * Random.Range(0,3));
		collider.enabled = true;
		isEnabled = true;
		yield return new WaitForSeconds(3);
		isEnabled = false;
		collider.enabled = false;
		}
	}
}
