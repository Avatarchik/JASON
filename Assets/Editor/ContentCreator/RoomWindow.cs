
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RoomWindow:EditorWindow {
	public const string roomsPath = "Editor/Room Editor/Rooms/";
	public const string propsPath = "Editor/Room Editor/Props/";
	public const string enemiesPath = "Editor/Room Editor/Enemies/";

	private List<string> roomNames;
	private List<string> propNames;
	private List<string> enemyNames;
	
	private string roomName;
	private string roomType;

	private bool itemSelected;
	private bool editorSelected;

	private GameObject room;
	private List<GameObject> props;

	[MenuItem("Window/Room Editor")]
	static void Init() {
		RoomWindow window = (RoomWindow)EditorWindow.GetWindow(typeof(RoomWindow));

		window.title = "Room Editor";

		window.Show();
		window.Focus();
	}

	void OnInspectorUpdate() {
		switch(GetSelectedGameObjectTag()) {
		case "Room Editor Prop":
			itemSelected = true;
			editorSelected = false;
			Repaint();
			break;
		case "Room Editor":
			itemSelected = false;
			editorSelected = true;
			Repaint();
			break;
		}
	}

	void OnGUI() {
		if(roomNames == null || propNames == null || enemyNames == null)
			Initialize();

		if(itemSelected) {
			DrawItemGUI();
		} else if(editorSelected) {
			DrawRoomGUI();
		} else {
			GUILayout.Label("No valid Game Object selected");
		}
	}

	/** (Re)Load the rooms, props and enemies arrays */
	private void Initialize() {
		roomNames = LoadAssets(roomsPath);
		propNames = LoadAssets(propsPath);
		enemyNames = LoadAssets(enemiesPath);
	}

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

	/** Draw the GUI if an item is selected */
	private void DrawItemGUI() {

	}

	/** Draw the GUI if the Room Editor is selected */
	private void DrawRoomGUI() {
		roomName = EditorGUILayout.TextField("Room Name", roomName);

		HandleRooms();
		HandleProps();
		HandleEnemies();

		GUILayout.Label("Options", EditorStyles.boldLabel);

		if(GUILayout.Button("Create Room"));
			//CreateRoomPrefab();

		if(GUILayout.Button("Reload Assets"))
			Initialize();
	}

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

	private string DrawList(List<string> list) {
		EditorGUILayout.BeginVertical();
		
		foreach(string listItem in list)
			if(GUILayout.Button(listItem))
				return listItem;
		
		EditorGUILayout.EndVertical();

		return "";
	}

	private void CreateRoom(string name) {
		DestroyImmediate(room);

		room = Instantiate(Resources.Load(roomsPath + name), Vector3.zero, Quaternion.identity) as GameObject;
		room.name = name + " Room";
		room.tag = "Room Editor Room";
	}

	private void CreateOther(string path, string name) {
		if(room == null) {
			Debug.LogError("You need to create a room first");
			return;
		}

		GameObject prop = Instantiate(Resources.Load(path + name), Vector3.zero, Quaternion.identity) as GameObject;
		Object[] selectedobject = new Object[] {prop};

		prop.name = "Prop " + name;
		prop.transform.parent = room.transform;
		
		props.Add(prop);

		Selection.objects = selectedobject;
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

	/*

	private enum RoomType{
		Default,
		Hall,
		Corner,
		TRoom,
		Plus,
		BigRoom
	}

	private List<GameObject> objectList = new List<GameObject>();

	private GameObject roomPreview;
	private GameObject itemObject;
	private GameObject roomObject;

	private Editor gameObjectEditor;

	private Texture currentTexture;

	private RoomObject currentObject;
	private RoomType roomtype;

	private string currentRoomPreview;
	private string roomId;

	private bool itemSelected;
	private bool roomSelected;
	private bool foldTexture;
	private bool foldList;

	[MenuItem ("Window/Room Editor")]
	static void Init(){
		RoomWindow roomWindow = (RoomWindow)EditorWindow.GetWindow(typeof(RoomWindow));

		roomWindow.title = "Room Editor";

		roomWindow.Show();
		roomWindow.Focus();
	}
	
	void OnInspectorUpdate() {
		if(Selection.activeGameObject != null){
			if(Selection.activeGameObject.tag == "Item" && !itemSelected) {
				itemObject = Selection.activeGameObject.gameObject;
				itemSelected = true;
				currentObject = itemObject.GetComponent<RoomObject>();
				currentTexture = itemObject.renderer.sharedMaterial.mainTexture;
				this.Repaint();
			} else if(Selection.activeGameObject.tag != "Item") {
				itemSelected = false;
				itemObject = null;
				this.Repaint();
			}

			if(Selection.activeGameObject.tag == "Room" && !roomSelected) {
				roomObject = Selection.activeGameObject.gameObject;
				roomSelected = true;
				this.Repaint();
			} else if(Selection.activeGameObject.tag != "Room") {
				roomSelected = false;
				roomObject = null;
				this.Repaint();
			}
		}
	}
	
	void OnGUI(){
		if(roomSelected) {
			EditorGUILayout.HelpBox("Here you can add content to the room", MessageType.Info);
			EditorGUILayout.HelpBox("Make sure you've selected the correct room type and check if the prefab doesn't already exist!", MessageType.Warning);

			roomId = EditorGUILayout.TextField("Room ID", roomId);
			roomtype = (RoomType)EditorGUILayout.EnumPopup("Room Type", roomtype);

			if(roomtype.ToString() != currentRoomPreview){
				currentRoomPreview = roomtype.ToString();
				CreateRoom(currentRoomPreview);
			}

			if(GUILayout.Button("Create"))
				CreatePrefab(roomId, roomtype);

			DrawPropsWindow();
			DrawEnemiesWindow();
		}
		
		foldList = EditorGUILayout.Foldout(foldList, "Item List");

		if(foldList) {
			if(objectList.Count != 0) {
				for(int i = 0; i < objectList.Count; i++) {
					if(objectList[i] == null) {
						objectList.RemoveAt(i);
						break;
					}

					EditorGUILayout.ObjectField((GameObject)objectList[i], typeof(GameObject), true);
				}
			}
		}

		if(itemSelected) {
			EditorGUILayout.HelpBox("You are currently editing a Item Object", MessageType.Info);
			DrawSelectedItemScreen();
		}
	}

	/** Draw the props window *
	private void DrawPropsWindow(){
		string[] list1 = new string[] {"Barrel", "Chest", "Gold Pile", "Pillar", "Stone Block", "Tomb"};
		string[] list2 = new string[] {"Torch", "Vase", "Vase Broken", "Wine Keg"};

		GUILayout.Label("Props", EditorStyles.objectFieldThumb);

		EditorGUILayout.BeginHorizontal();
			DrawPropList(list1);
			DrawPropList(list2);
		EditorGUILayout.EndHorizontal();
	}

	private void DrawEnemiesWindow() {
		string[] list1 = new string[] {"Normal Warrior"};

		GUILayout.Label("Enemies", EditorStyles.objectFieldThumb);

		EditorGUILayout.BeginHorizontal();
			DrawPropList(list1);
		EditorGUILayout.EndHorizontal();
	}

	/** Draw the props list *
	private void DrawPropList(string[] items){
		EditorGUILayout.BeginVertical();
			for(int i = 0; i < items.Length; i++){
				if(GUILayout.Button(items[i].ToString())){
					CreateItem(items[i].ToString());
				}
			}
		EditorGUILayout.EndVertical();
	}

	/** Draw the selected item screen *
	private void DrawSelectedItemScreen(){
		GUILayout.Label("Selected Item", EditorStyles.objectFieldThumb);
		EditorGUILayout.HelpBox("Here you can Edit the Room Item you currently have selected", MessageType.Info);

		foldTexture = EditorGUILayout.Foldout(foldTexture, "Texture/Material");

		if(foldTexture){
			currentTexture = (Texture)EditorGUILayout.ObjectField(currentTexture, typeof(Texture), true);
			currentObject.renderer.sharedMaterial.mainTexture = currentTexture;
		}
		
		if(itemObject != null) {
			if(gameObjectEditor == null)
				gameObjectEditor = Editor.CreateEditor(itemObject);

			gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(200, 200), EditorStyles.layerMaskField);
		}
		
		if(GUILayout.Button("Deselect Item"))
			Selection.objects = new GameObject[0];

		if(GUILayout.Button("Destroy Item")){
			Selection.objects = new GameObject[0];
			DestroyImmediate(currentObject);
		}
	}

	/** Create a room *
	private void CreateRoom(string name){
		DestroyImmediate(roomPreview);
		GameObject instantiatedItem = Instantiate(Resources.Load("Prefabs/Rooms/Template/" + name),new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
		instantiatedItem.name = name;
		instantiatedItem.tag = "Room";
		roomPreview = instantiatedItem;
	}

	/** Create an item *
	private void CreateItem(string name){
		if(Resources.Load("Prefabs/Room Items/" + name) == null) {
			Debug.LogError("Prefabs/Room Items/" + name + " doesn't exist");
			return;
		}

		GameObject instantiatedItem = Instantiate(Resources.Load("Prefabs/Editor Items/" + name), new Vector3(0, 1, 0), Quaternion.identity) as GameObject;

		instantiatedItem.name = name;
		instantiatedItem.transform.parent = roomObject.transform;

		Object[] selectedobject = new Object[] {instantiatedItem};
		Selection.objects = selectedobject;
		
		objectList.Add(instantiatedItem);
	}

	/** Create a prefab *
	private void CreatePrefab(string id, RoomType type){
		for(int i = 0; i < objectList.Count; i++){
			GameObject child = objectList[i].transform.GetChild(0).gameObject;
			DestroyImmediate(child);
		}
		Debug.Log("Saved " + type.ToString() + "/" + id);
		Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/Rooms/Content/" + type.ToString() + "/" + id + ".prefab");
		PrefabUtility.ReplacePrefab(Selection.activeGameObject, prefab);
		AssetDatabase.Refresh();
		for(int i = 0; i < objectList.Count; i++){
			DestroyImmediate(objectList[i]);
		}
	}*/
}