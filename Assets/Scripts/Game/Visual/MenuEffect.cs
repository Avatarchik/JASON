using UnityEngine;
using System.Collections;

public class MenuEffect : MonoBehaviour {
	public GameObject cameraRotation;
	public Material[] materials;
	private bool fire;
	
	public Material[] normalMaterials;
	public Material[] fireMaterials;
	private float blendfloat;
	// Use this for initialization
	void Start () {
		StartCoroutine(SwitchTextures());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		cameraRotation.transform.Rotate(Vector3.up / 22);
		if(fire){
			for(int i = 0; i < materials.Length; i++){
				if(materials[i].GetFloat("_Blend") <= 1)
					materials[i].SetFloat("_Blend",materials[i].GetFloat("_Blend") +0.01f);
				normalMaterials[i].color = new Color(1,1,1,-materials[i].GetFloat("_Blend") + 1);
			}
			for(int i = 0; i < fireMaterials.Length; i++){
				fireMaterials[i].color = new Color(1,1,1,materials[1].GetFloat("_Blend"));
			}
		}else{
			for(int i = 0; i < materials.Length; i++){
				if(materials[i].GetFloat("_Blend") >= 0)
					materials[i].SetFloat("_Blend",materials[i].GetFloat("_Blend") -0.01f);
				normalMaterials[i].color = new Color(1,1,1,-materials[i].GetFloat("_Blend") + 1);
			}
			for(int i = 0; i < fireMaterials.Length; i++){
				fireMaterials[i].color = new Color(1,1,1,materials[1].GetFloat("_Blend"));
			}
		}
	}

	/**Wait to swap the Textures of Menu */
	IEnumerator SwitchTextures(){
		while(true){
			yield return new WaitForSeconds(10);
			if(fire){
				fire = false;
			}else{
				fire = true;
			}
		}
	}
}
