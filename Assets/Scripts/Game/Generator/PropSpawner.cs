using UnityEngine;
using System.Collections;

public class PropSpawner:MonoBehaviour {
	public const string propsPath = "Prefabs/Props/";
	public const string enemiesPath = "Prefabs/Enemies/";
	
	public enum PropType {
		Prop,
		Enemy
	}
	
	public PropType propType;

	void Start() {
		string path = "";
	
		switch(propType) {
		case PropType.Prop:
			path = propsPath;
			break;
		case PropType.Enemy:
			path = enemiesPath;
			break;
		}
		
		Vector3 position = transform.position;
		position.y += 0.5f;
	
		GameObject prop = Instantiate(Resources.Load(path + transform.name), position, transform.rotation) as GameObject;
		prop.transform.parent = transform.parent;
		
		Destroy(gameObject);
	}
}
