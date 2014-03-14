using UnityEngine;
using System.Collections;

public class Arrow:MonoBehaviour {
	[SerializeField] Shader alphaShader;

	private Shader normalShader;

	private Renderer model;

	private bool isTraveling;
	private bool fadeOut;

	void Start() {
		model = transform.GetChild(0).renderer;
		normalShader = renderer.material.shader;
	}

	void FixedUpdate () {
		if(isTraveling)
			transform.Translate(Vector3.forward * 0.8f);
	}

	void LateUpdate() {
		if(fadeOut) {
			Color newColor = model.material.color;

			newColor.a -= 0.01f;

			model.material.color = newColor;
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		isTraveling = false;
		collider.enabled = false;

		transform.parent = collision.transform;

		if(collision.gameObject.name == "Player")
			collision.gameObject.GetComponent<Player>().Damage(1, 0);

		StartFade();
	}

	/** Fire the arrow from the specified arrow trap */
	public void Fire(ArrowTrap arrowTrap) {
		transform.position = arrowTrap.transform.position;
		transform.rotation = arrowTrap.transform.rotation;
		
		isTraveling = true;
		collider.enabled = true;
		
		transform.parent = arrowTrap.transform;

		ResetFade();
	}

	/** Start fading out the arrow */
	private void StartFade() {
		model.material.shader = alphaShader;

		fadeOut = true;
	}

	/** Reset the fade of the arrow */
	private void ResetFade() {
		if(normalShader != null) {
			model.material.shader = normalShader;
			
			Color newColor = model.material.color;
			
			newColor.a = 1.0f;
			
			model.material.color = newColor;

			fadeOut = false;
		}
	}
}