using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RoomWindow:EditorWindow {
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

	/** Draw the props window */
	private void DrawPropsWindow(){
		string[] list1 = new string[] {"Pot", "Pillar", "Chest", "Tomb", "Moss", "BrokenPot"};
		string[] list2 = new string[] {"Torch", "Statue", "BrkSword", "Table", "Chair", "Anvil"};
		string[] list3 = new string[] {"Furnace", "Fountain", "Tree", "Bush", "Saw", "Crystal"};

		GUILayout.Label("Props", EditorStyles.objectFieldThumb);

		EditorGUILayout.BeginHorizontal();
			DrawPropList(list1);
			DrawPropList(list2);
			DrawPropList(list3);
		EditorGUILayout.EndHorizontal();

		GUILayout.Label("", EditorStyles.objectFieldThumb);
	}

	/** Draw the props list */
	private void DrawPropList(string[] items){
		EditorGUILayout.BeginVertical();
			for(int i = 0; i < items.Length; i++){
				if(GUILayout.Button(items[i].ToString())){
					CreateItem(items[i].ToString());
				}
			}
		EditorGUILayout.EndVertical();
	}

	/** Draw the selected item screen */
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

		GUILayout.Label("", EditorStyles.objectFieldThumb);
	}

	/** Create a room */
	private void CreateRoom(string name){
		DestroyImmediate(roomPreview);
		GameObject instantiatedItem = Instantiate(Resources.Load("Prefabs/Rooms/Template/" + name),new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
		instantiatedItem.name = name;
		instantiatedItem.tag = "Room";
		roomPreview = instantiatedItem;
	}

	/** Create an item */
	private void CreateItem(string name){
		if(Resources.Load("Prefabs/RoomItems/" + name) == null) {
			Debug.LogError("Prefabs/RoomItems/" + name + " doesn't exist");
			return;
		}

		GameObject instantiatedItem = Instantiate(Resources.Load("Prefabs/EditorItems/" + name), new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
		Object[] selectedobject = new Object[1];

		instantiatedItem.name = name;
		instantiatedItem.transform.parent = roomObject.transform;
		
		selectedobject[0] = instantiatedItem;
		Selection.objects = selectedobject;
		
		objectList.Add(instantiatedItem);
	}

	/** Create a prefab */
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
	}
}