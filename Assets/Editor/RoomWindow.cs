using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class RoomWindow : EditorWindow {

	GameObject itemObject;
	bool itemSelected = false;

	List<GameObject> objectList;
	bool foldList;
	bool foldTexture;
	//For ItemObject;
	RoomObject currentObject;
	Texture currentTexture;
	Editor gameobjectEditor;
	[MenuItem ("Window/RoomEditor")]
	static void Init(){
		//RoomWindow window = (RoomWindow)EditorWindow.GetWindow(typeof(RoomWindow));
	}

	void OnInspectorUpdate(){
		Debug.Log(itemSelected);
		if(Selection.activeGameObject != null){
			if(Selection.activeGameObject.tag == "Item" && !itemSelected){
				itemObject = Selection.activeGameObject.gameObject;
				itemSelected = true;
				currentObject = itemObject.GetComponent<RoomObject>();
				currentTexture = itemObject.renderer.sharedMaterial.mainTexture;
				this.Repaint();
			}else if(Selection.activeGameObject.tag != "Item"){
				itemSelected = false;
				itemObject = null;
				this.Repaint();
			}
		}
	}

	void OnGUI(){
		foldList = EditorGUILayout.Foldout(foldList,"ItemList");
		if(foldList){
			if(objectList.Count != 0){
				for(int i = 0; i < objectList.Count; i++){
					//objectList[i] = (GameObject)EditorGUILayout.ObjectField("THIS",typeof(GameObject),true);
					if(objectList[i] == null){
						objectList.RemoveAt(i);
						break;
					}
					EditorGUILayout.ObjectField((GameObject)objectList[i],typeof(GameObject),true);
				}
			}
		}
		if(itemSelected){
			EditorGUILayout.HelpBox("You are currently editing a Item Object",MessageType.Info);
			DrawSelectedItemScreen();
		}

		if(itemObject != null){
			if(gameobjectEditor == null){
				gameobjectEditor = Editor.CreateEditor(itemObject);
			}
			gameobjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(200,200),EditorStyles.layerMaskField);
		}
	}
	void DrawSelectedItemScreen(){
		GUILayout.Label("Selected Item",EditorStyles.objectFieldThumb);
		EditorGUILayout.HelpBox("Here you can Edit the Room Item you currently have selected",MessageType.Info);
		foldTexture = EditorGUILayout.Foldout(foldTexture,"Texture/Material");
		if(foldTexture){
			currentTexture = (Texture)EditorGUILayout.ObjectField(currentTexture,typeof(Texture),true);
			currentObject.renderer.sharedMaterial.mainTexture = currentTexture;
		}
		GUILayout.Label("",EditorStyles.objectFieldThumb);
	}
}
	
	/*
	}
	void DrawSelectedItemScreen(){

		showItemSelector = EditorGUILayout.InspectorTitlebar(showItemSelector,selectedItem);
		if(showItemSelector && selectedItem){
			
			EditorGUILayout.HelpBox("Here you can Edit the Room Item you currently have selected",MessageType.Info);
			Color selectedItemColor = selectedItemGameObject.renderer.sharedMaterial.color;
			foldTexture = EditorGUILayout.Foldout(foldTexture,"Texture/Material");
			if(foldTexture){
				selectedItemColor = EditorGUILayout.ColorField(selectedItemColor);
				selectedObjectTexture = selectedItemGameObject.renderer.sharedMaterial.mainTexture;
				selectedItemGameObject.renderer.sharedMaterial.mainTexture = (Texture)EditorGUILayout.ObjectField(selectedObjectTexture,typeof(Texture),true);
			}
		}else{
			EditorGUILayout.HelpBox("No Object with RoomObject found. To edit a RoomObject Select a GameObject with a RoomObject Script attached to it",MessageType.Warning);
		}
		if(gameobj != null){
			if(gameobjectEditor == null){
				gameobjectEditor = Editor.CreateEditor(gameobj);
			}
			gameobjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(200,200),EditorStyles.layerMaskField);
		}
		if(selectedItem != null){
			if(GUILayout.Button("Deselect Item")){
				Selection.objects = new GameObject[0];
			}
			if(GUILayout.Button("Destroy Item")){
				Selection.objects = new GameObject[0];
				DestroyImmediate(selectedItemGameObject);
			}
		}
		
		if(selectedItem == null){
			DrawPropsWindow();
		}

	}

	void DrawPropsWindow(){

		GUILayout.Label("Props",EditorStyles.objectFieldThumb);
		EditorGUILayout.BeginHorizontal();
		DrawItemWindowOne();
		DrawItemWindowTwo();
		DrawItemWindowThree();
		EditorGUILayout.EndHorizontal();
		GUILayout.Label("",EditorStyles.objectFieldThumb);

	}
	void DrawItemWindowOne(){

		scrollPos = EditorGUILayout.BeginScrollView(scrollPos,GUILayout.Width(100),GUILayout.Height(150));
		if(GUILayout.Button("Pot")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("Torch")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("Box")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("Corpse")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("BloodStain")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("Chains")){
			CreateItem("Pot");
		}
		EditorGUILayout.EndScrollView();
	}
	void DrawItemWindowTwo(){
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos,GUILayout.Width(100),GUILayout.Height(150));
		if(GUILayout.Button("Spikes")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("Statue")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("Rubble")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("Chain-Floor")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("GargoyleDead")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("WarriorDead")){
			CreateItem("Pot");
		}
		EditorGUILayout.EndScrollView();

	}
	void DrawItemWindowThree(){
		/*
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos,GUILayout.Width(100),GUILayout.Height(150));
		if(GUILayout.Button("EmptyChest")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("BrokenSword")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("Moss")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("Pillar")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("Wall")){
			CreateItem("Pot");
		}
		if(GUILayout.Button("CrackedFloor")){
			CreateItem("Pot");
		}
		EditorGUILayout.EndScrollView();

	}
	void CreateItem(string name){
		GameObject instantiatedItem = Instantiate(Resources.Load("Prefabs/RoomItems/"+name),new Vector3(0,0.5f,0),Quaternion.identity) as GameObject;
		instantiatedItem.name = name;
		instantiatedItem.transform.parent = roomParent.transform;
		Object[] selectedobject = new Object[1];
		selectedobject[0] = instantiatedItem;
		Selection.objects = selectedobject;
		objectList.Add(instantiatedItem);
	}
	void OnInspectorUpdate(){

		this.Repaint();
		if(Selection.activeGameObject){
			if(Selection.activeGameObject.gameObject != previousObject){
				selectedItemGameObject = null;
				gameobj = null;
				//gameobjectEditor = null;
			}
			previousObject = Selection.activeGameObject.gameObject;
			Debug.Log(Selection.activeGameObject.gameObject);
			selectedItem = Selection.activeGameObject.GetComponent<RoomObject>();
			selectedItemGameObject = Selection.activeGameObject.gameObject;
			gameobjectEditor = null;
		gameobj = selectedItemGameObject;
		}else{
			//gameobjectEditor = null;
			//selectedItem = null;
			//gameobj = null;
		}

	}
	*/
