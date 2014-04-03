using UnityEngine;
using System.Collections;

public class CutsceneDoor:MonoBehaviour {
	[SerializeField] private Animator cutscene;

	private bool fireAnimation;
	
	void Start() {
		if(GameData.Instance.fireDungeonCleared) {
			cutscene.SetInteger("SceneNumber", 2);
			StartCoroutine("TimeToCredits");
		} else {
			cutscene.SetInteger("SceneNumber",1);
			StartCoroutine("TimeToFire");
		}
	}

	/** Delay before the fire dungeon is loaded */
	private IEnumerator TimeToFire() {
		yield return new WaitForSeconds(6);

		cutscene.enabled = false;

		Application.LoadLevel("Fire Dungeon");
	}

	/** Delay before the credits scene is loaded */
	private IEnumerator TimeToCredits() {
		yield return new WaitForSeconds(15);

		cutscene.enabled = false;

		Application.LoadLevel("Credits");
	}

}
