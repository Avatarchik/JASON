using UnityEngine;
using System.Collections;
using SGUI;

public class MenuMain:GUIBehaviour {
	public GameObject cameraRotation;
	public SGUITextureButton[] buttons;
	public Material[] materials;
    private bool fire;

	public Material[] normalMaterials;
	public Material[] fireMaterials;
	private float blendfloat;
	void Start() {
		StartCoroutine(WaitForGlobalManager());
		StartCoroutine(SwitchTextures());
	}
	void FixedUpdate(){
		DoMenuEffects();
	}
	protected override void OnGUI() {
		base.OnGUI();

		if(buttons[0].Click) {
			buttons[0].Destroy();

			Application.LoadLevel("Game");
		}
	}

	/**Animates the Menu Scenery */
	void DoMenuEffects(){
		cameraRotation.transform.Rotate(Vector3.up / 2);
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
		Debug.Log(fire);
		yield return new WaitForSeconds(10);
		if(fire){
			fire = false;
		}else{
			fire = true;
		}
		Debug.Log(fire);
		}
	}

	/** Wait until the Global Manager has been loaded */
	private IEnumerator WaitForGlobalManager() {
		while(GameObject.FindGameObjectWithTag("Global Manager") == null)
			yield return new WaitForSeconds(0.3f);

		foreach(SGUITextureButton button in buttons)
			button.Create();
	}
}
