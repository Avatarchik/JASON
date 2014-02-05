using UnityEngine;
using System.Collections;

public class RoomViewer : MonoBehaviour {

	public enum RoomType{
		Default,
		Hall,
		Corner,
		BigRoom
	}
	public RoomType roomtype;
	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		switch(roomtype){
			case RoomType.Default:
			DrawDefaultRoom();
			break;
			case RoomType.Hall:
			DrawHallRoom();
			break;
			case RoomType.Corner:
			DrawCornerRoom();
			break;
			case RoomType.BigRoom:
			DrawBigRoom();
			break;
		}
	}

	void DrawDefaultRoom(){
		Gizmos.DrawWireCube(new Vector3(0,2f,0),new Vector3(20,5,20));
	}
	void DrawHallRoom(){
		Gizmos.DrawWireCube(new Vector3(0,2f,0),new Vector3(20,5,20));
		Gizmos.DrawWireCube(new Vector3(20,2f,0),new Vector3(20,5,20));
	}
	void DrawCornerRoom(){
		Gizmos.DrawWireCube(new Vector3(0,2f,0),new Vector3(20,5,20));
		Gizmos.DrawWireCube(new Vector3(20,2f,0),new Vector3(20,5,20));
		Gizmos.DrawWireCube(new Vector3(20,2f,20),new Vector3(20,5,20));
	}
	void DrawBigRoom(){
		Gizmos.DrawWireCube(new Vector3(0,2f,0),new Vector3(40,5,40));

	}
}
