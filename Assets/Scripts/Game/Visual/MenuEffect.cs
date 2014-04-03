using UnityEngine;
using System.Collections;

public class MenuEffect:MonoBehaviour {
	[SerializeField] private GameObject cameraRotation;
	[SerializeField] private Material[] materials;

	[SerializeField] private Material[] normalMaterials;
	[SerializeField] private Material[] fireMaterials;

	private float blendfloat;

	private bool fire;

	void Start() {
		StartCoroutine(SwitchTextures());
	}
	
	void FixedUpdate() {
		cameraRotation.transform.Rotate(Vector3.up / 22);

		if(fire) {
			for(int i = 0; i < materials.Length; i++) {
				if(materials[i].GetFloat("_Blend") <= 1)
					materials[i].SetFloat("_Blend", materials[i].GetFloat("_Blend") + 0.01f);

				normalMaterials[i].color = new Color(1, 1, 1, -materials[i].GetFloat("_Blend") + 1);
			}

			for(int i = 0; i < fireMaterials.Length; i++)
				fireMaterials[i].color = new Color(1, 1, 1, materials[1].GetFloat("_Blend"));
		} else {
			for(int i = 0; i < materials.Length; i++) {
				if(materials[i].GetFloat("_Blend") >= 0)
					materials[i].SetFloat("_Blend", materials[i].GetFloat("_Blend") - 0.01f);

				normalMaterials[i].color = new Color(1, 1, 1, -materials[i].GetFloat("_Blend") + 1);
			}

			for(int i = 0; i < fireMaterials.Length; i++)
				fireMaterials[i].color = new Color(1, 1, 1, materials[1].GetFloat("_Blend"));
		}
	}

	/** Swap the textures after a delay */
	private IEnumerator SwitchTextures(){
		while(true) {
			yield return new WaitForSeconds(10);

			fire = !fire;
		}
	}
}
