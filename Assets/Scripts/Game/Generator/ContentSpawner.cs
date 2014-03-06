using UnityEngine;
using System.Collections;

public class ContentSpawner:MonoBehaviour {
	void Start() {
		StartCoroutine("WaitForLevelGenerator");
	}

	private IEnumerator WaitForLevelGenerator(){
		GameObject content = null;
		Object selected = null;

		yield return new WaitForSeconds(1);

		switch(gameObject.name){
		case "Default":
			selected = LevelGenerator.Instance.RandomDefault();
			break;
		case "Hall":
			selected = LevelGenerator.Instance.RandomHall();
			break;
		case "Corner":
			selected = LevelGenerator.Instance.RandomCorner();
			break;
		case "T":
			selected = LevelGenerator.Instance.RandomT();
			break;
		case "Plus":
			selected = LevelGenerator.Instance.RandomPlus();
			break;
		case "Big":
			selected = LevelGenerator.Instance.RandomBig();
			break;
		}
		
		if(selected == null)
			throw new System.NullReferenceException("There's no content for the room type " + gameObject.name);
		
		content = Instantiate(selected, transform.position, transform.rotation) as GameObject;
		content.transform.parent = transform;
		content.transform.rotation = transform.rotation;
	}
}