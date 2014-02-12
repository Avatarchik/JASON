using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ItemManager:EditorWindow {
	private ItemList itemList;

	private Object lastObjectSelected;

	[MenuItem("Window/Item Manager")]
	static void Init() {
		ItemManager window = (ItemManager)EditorWindow.GetWindow(typeof(ItemManager));
		window.title = "Item Manager";

		window.Show();
		window.Focus();
	}
	
	void OnGUI() {
		if(Selection.activeGameObject == null || (Selection.activeGameObject != lastObjectSelected && lastObjectSelected != null)) {
			GUILayout.Label("No Game Object with an ItemList component selected");
			return;
		} else if(itemList == null) {
			itemList = (Selection.activeGameObject).GetComponent<ItemList>();

			if(itemList == null) {
				GUILayout.Label("Can't find an ItemList component on the selected Game Object");
				return;
			}
		}
	
		if(GUILayout.Button("Add items"))
			CreateItemCreator();

		GUILayout.Label("Equipable Items", EditorStyles.boldLabel);
		DrawEquipable();
		DrawWeapon();

		GUILayout.Label("Powers", EditorStyles.boldLabel);
		DrawPower();

		GUILayout.Label("Special Items", EditorStyles.boldLabel);
		DrawSpecial();

		lastObjectSelected = Selection.activeGameObject;
	}
	
	private void DrawEquipable() {
		foreach(ItemEquipable item in itemList.EquipableItems) {
			Texture2D preview = AssetPreview.GetAssetPreview(item.Model);
		
			EditorGUILayout.BeginHorizontal();
				GUILayout.Label(preview, GUILayout.MaxWidth(60f), GUILayout.MaxHeight(60f));

				EditorGUILayout.BeginVertical();
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label(item.Name, EditorStyles.boldLabel, GUILayout.Width(225f));

						if(GUILayout.Button("Edit", GUILayout.Width(150f))) {
							ItemCreator itemCreator = CreateItemCreator();
							itemCreator.EditEquipable(item);
						}
					EditorGUILayout.EndHorizontal();

					GUILayout.Label(item.Element.ToString() + " " + item.TypeEquipable.ToString(), GUILayout.Width(225f));
					
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label("Speed: " + item.Stats.Speed.ToString(), GUILayout.Width(100f));
						GUILayout.Label("Defence: " + item.Stats.Defence.ToString(), GUILayout.Width(100f));
						GUILayout.Label("Damage: " + item.Stats.Damage.ToString(), GUILayout.Width(100f));
						GUILayout.Label("Store price: " + item.Stats.StorePrice.ToString(), GUILayout.Width(100f));
					EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
	}
	
	private void DrawWeapon() {
		foreach(ItemWeapon item in itemList.WeaponItems) {
			Texture2D preview = AssetPreview.GetAssetPreview(item.Model);
			
			EditorGUILayout.BeginHorizontal();
				GUILayout.Label(preview, GUILayout.MaxWidth(60f), GUILayout.MaxHeight(60f));
				
				EditorGUILayout.BeginVertical();
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label(item.Name, EditorStyles.boldLabel, GUILayout.Width(225f));
						
						if(GUILayout.Button("Edit", GUILayout.Width(150f))) {
							ItemCreator itemCreator = CreateItemCreator();
							itemCreator.EditWeapon(item);
						}
					EditorGUILayout.EndHorizontal();

					GUILayout.Label(item.Element.ToString() + " " + item.TypeEquipable.ToString() + " - " + item.TypeWeapon.ToString(), GUILayout.Width(225f));
					
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label("Speed: " + item.Stats.Speed.ToString(), GUILayout.Width(100f));
						GUILayout.Label("Defence: " + item.Stats.Defence.ToString(), GUILayout.Width(100f));
						GUILayout.Label("Damage: " + item.Stats.Damage.ToString(), GUILayout.Width(100f));
						GUILayout.Label("Store price: " + item.Stats.StorePrice.ToString(), GUILayout.Width(100f));
					EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
	}
		
	private void DrawPower() {
		GUIStyle style = new GUIStyle();
		style.margin = new RectOffset(65, 0, 0, 0);

		foreach(ItemPower item in itemList.PowerItems) {
			EditorGUILayout.BeginHorizontal(style);
				EditorGUILayout.BeginVertical();
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label(item.Name, EditorStyles.boldLabel, GUILayout.Width(225f));

						if(GUILayout.Button("Edit", GUILayout.Width(150f))) {
							ItemCreator itemCreator = CreateItemCreator();
							itemCreator.EditPower(item);
						}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
						GUILayout.Label("Type: " + item.TypePower.ToString(), GUILayout.Width(205f));
						GUILayout.Label("Time: " + item.Time.ToString(), GUILayout.Width(100f));
					EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
	}
	
	private void DrawSpecial() {
		GUIStyle style = new GUIStyle();
		style.margin = new RectOffset(65, 0, 0, 0);
		
		foreach(ItemSpecial item in itemList.SpecialItems) {
			EditorGUILayout.BeginHorizontal(style);
				EditorGUILayout.BeginVertical();
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label(item.Name, EditorStyles.boldLabel, GUILayout.Width(225f));
						
						if(GUILayout.Button("Edit", GUILayout.Width(150f))) {
							ItemCreator itemCreator = CreateItemCreator();
							itemCreator.EditSpecial(item);
						}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
						GUILayout.Label("ID: " + item.Id.ToString(), GUILayout.Width(100f));
					EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
	}

	private ItemCreator CreateItemCreator() {
		ItemCreator itemCreator = (ItemCreator)EditorWindow.GetWindow(typeof(ItemCreator));

		itemCreator.title = "Item Creator";
		itemCreator.SetItemManager(this);
		
		itemCreator.Show();
		itemCreator.Focus();

		return itemCreator;
	}
}
