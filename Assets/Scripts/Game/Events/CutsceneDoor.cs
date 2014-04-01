using UnityEngine;
using System.Collections;

public class CutsceneDoor : MonoBehaviour {
	private bool fireAnimation;
	public Animator cutscene;
	// Use this for initialization
	void Start () {
		fireAnimation = GameData.Instance.fireDungeonCleared;
		if(fireAnimation){
			cutscene.SetInteger("SceneNumber",2);
			StartCoroutine("TimeToCredits");
		}else{
			cutscene.SetInteger("SceneNumber",1);
			StartCoroutine("TimeToFire");
		}
	}
	IEnumerator TimeToFire(){
		yield return new WaitForSeconds(6);
		cutscene.enabled = false;
		Application.LoadLevel("Fire Dungeon");
	}
	IEnumerator TimeToCredits(){
		yield return new WaitForSeconds(12);
		cutscene.enabled = false;
		Application.LoadLevel("Credits");
	}

}
