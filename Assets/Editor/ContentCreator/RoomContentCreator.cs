using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RoomContentCreator:EditorWindow {
	public const string savePath = "Editor/Content Creator/Saved Rooms/";
	public const string roomsPath = "Editor/Content Creator/Rooms/";
	public const string propsPath = "Editor/Content Creator/Props/";
	public const string enemiesPath = "Editor/Content Creator/Enemies/";

	private List<string> roomNames;
	private List<string> propNames;
	private List<string> enemyNames;
	
	private string roomName;
	private string roomType;

	private GameObject room;
	private List<GameObject> props;

	[MenuItem("Content Creator/Room Content Creator")]
	static void Init() {
		RoomContentCreator window = (RoomContentCreator)EditorWindow.GetWindow(typeof(RoomContentCreator));

		window.title = "Room Content Creator";

		window.Show();
		window.Focus();
	}

	void OnGUI() {
		if(roomNames == null || propNames == null || enemyNames == null)
			Initialize();

		HandleRooms();
		
		if(room != null) {
			HandleProps();
			HandleEnemies();
		}
		
		GUILayout.Label("Options", EditorStyles.boldLabel);
		
		roomName = EditorGUILayout.TextField("Room Name", roomName);
		
		if(room != null && roomName != "")
			if(GUILayout.Button("Create Room"))
				SaveRoom();
		
		if(GUILayout.Button("Reload Assets"))
			Initialize();
	}

	/** (Re)Load the rooms, props and enemies arrays */
	private void Initialize() {
		roomNames = LoadAssets(roomsPath);
		propNames = LoadAssets(propsPath);
		enemyNames = LoadAssets(enemiesPath);
		
		if(props == null)
			props = new List<GameObject>();
	}

	/** Handle the room buttons */
	private void HandleRooms() {
		GUILayout.Label("Available Room Types", EditorStyles.boldLabel);
		string selectedRoom;

		if(roomNames.Count > 1) {
			selectedRoom = DrawSeperatedList(roomNames);
		} else {
			selectedRoom = DrawList(roomNames);
		}
		
		if(selectedRoom != null && selectedRoom != "")
			CreateRoom(selectedRoom);
	}

	/** Handle the prop buttons */
	private void HandleProps() {
		GUILayout.Label("Available Props", EditorStyles.boldLabel);
		string selectedProp;
		
		if(propNames.Count > 1) {
			selectedProp = DrawSeperatedList(propNames);
		} else {
			selectedProp = DrawList(propNames);
		}
		
		if(selectedProp != null && selectedProp != "")
			CreateOther(propsPath, selectedProp);
	}
	
	/** Handle the enemies buttons */
	private void HandleEnemies() {
		GUILayout.Label("Available Enemies", EditorStyles.boldLabel);
		string selectedEnemy;
		
		if(enemyNames.Count > 1) {
			selectedEnemy = DrawSeperatedList(enemyNames);
		} else {
			selectedEnemy = DrawList(enemyNames);
		}
		
		if(selectedEnemy != null && selectedEnemy != "")
			CreateOther(enemiesPath, selectedEnemy);
	}

	/** Draw a list with 2 collumns */
	private string DrawSeperatedList(List<string> list) {
		List<string> row1 = new List<string>();
		List<string> row2 = new List<string>();

		string selected = "";

		for(int i = 0; i < list.Count / 2; i++)
			row1.Add(list[i]);

		for(int i = list.Count / 2; i < list.Count; i++)
			row2.Add(list[i]);

		EditorGUILayout.BeginHorizontal();
			string selectedRow1 = DrawList(row1);

			selected = selectedRow1 == "" ? DrawList(row2) : selectedRow1;
		EditorGUILayout.EndHorizontal();

		return selected;
	}

	/** Draw a list */
	private string DrawList(List<string> list) {
		EditorGUILayout.BeginVertical();
		
		foreach(string listItem in list)
			if(GUILayout.Button(listItem))
				return listItem;
		
		EditorGUILayout.EndVertical();

		return "";
	}

	/** Create a room */
	private void CreateRoom(string name) {
		DestroyImmediate(room);

		room = Instantiate(Resources.Load(roomsPath + name), Vector3.zero, Quaternion.identity) as GameObject;
		room.name = name;
		
		roomType = name;
	}

	/** Create a prop or an enemy */
	private void CreateOther(string path, string name) {
		GameObject go = Instantiate(Resources.Load(path + name), Vector3.zero, Quaternion.identity) as GameObject;
		Object[] selectedobject = new Object[] {go};
		Transform parent = GameObject.Find("Props").transform;
		
		if(parent.parent == room.transform)
			go.transform.parent = parent;

		go.name = name;
		
		props.Add(go);

		Selection.objects = selectedobject;
	}
	
	/** Save the room to a prefab */
	private void SaveRoom() {
		if(Resources.Load("Assets/Resources/" + savePath + roomType + "/" + roomName + ".prefab")) {
			Debug.LogError("That name is unavailable");
			return;
		}
	
		Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/" + savePath + roomType + "/" + roomName + ".prefab");
		GameObject props = GameObject.Find("Props");
		PrefabUtility.ReplacePrefab(props, prefab);
		
		DestroyImmediate(room);
		
		Debug.Log("Room created! Saved as " + savePath + roomType + "/" + roomName + ".prefab");
	}

	/** Load all the Game Objects in the specified path */
	private List<string> LoadAssets(string path) {
		List<string> result = new List<string>();

		Object[] objects = Resources.LoadAll(path, typeof(GameObject));

		if(objects.Length == 0)
			return result;

		foreach(Object o in objects) {
			GameObject go = (GameObject)o;
			result.Add(go.name);
		}

		return result;
	}

	/** Return the tag of the selected game object */
	private string GetSelectedGameObjectTag() {
		if(Selection.activeGameObject != null)
			return Selection.activeGameObject.tag;
		
		return "";
	}
}