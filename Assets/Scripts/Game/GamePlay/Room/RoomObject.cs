using UnityEngine;
using System.Collections;

public class RoomObject:MonoBehaviour {
	void Start(){
		GameObject contentItem = Instantiate(Resources.Load("Prefabs/RoomItems/" + transform.name),transform.position, transform.rotation) as GameObject;
		contentItem.transform.parent = this.transform;
	}
}
