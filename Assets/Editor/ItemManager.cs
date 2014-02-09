using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ItemManager:EditorWindow {
	[MenuItem("Window/Item Manager")]
	static void Init() {
		ItemManager window = (ItemManager)EditorWindow.GetWindow(typeof(ItemManager));
		window.title = "Item Manager";
	}
	
	void OnGUI() {
		List<ItemEquipable> equipable = new List<ItemEquipable>();
		List<ItemWeapon> weapon = new List<ItemWeapon>();
		List<ItemPower> power = new List<ItemPower>();
		List<ItemSpecial> special = new List<ItemSpecial>();
	
		if(GUILayout.Button("Add items")) {
			ItemCreator itemCreator = (ItemCreator)EditorWindow.GetWindow(typeof(ItemCreator));
			itemCreator.title = "Item Creator";
			itemCreator.SetItemManager(this);
		}
		
		foreach(Item item in ItemList.items) {
			if(item.GetType() == typeof(ItemEquipable)) {
				equipable.Add(item as ItemEquipable);
			} else if(item.GetType() == typeof(ItemWeapon)) {
				weapon.Add(item as ItemWeapon);
			} else if(item.GetType() == typeof(ItemPower)) {
				power.Add(item as ItemPower);
			} else if(item.GetType() == typeof(ItemSpecial)) {
				special.Add(item as ItemSpecial);
			}
		}
		
		DrawEquipable(equipable);
		DrawWeapon(weapon);
		DrawPower(power);
		DrawSpecial(special);
	}
	
	private void DrawEquipable(List<ItemEquipable> equipable) {
		foreach(ItemEquipable item in equipable) {
			Texture2D preview = AssetPreview.GetAssetPreview(item.model);
		
			EditorGUILayout.BeginHorizontal();
				GUILayout.Label(preview, GUILayout.MaxWidth(60f), GUILayout.MaxHeight(60f));
				
				EditorGUILayout.BeginVertical();
					GUILayout.Label(item.itemName, EditorStyles.boldLabel);
					GUILayout.Label(item.element.ToString() + " " + item.equipableType.ToString());
					
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label("Speed: " + item.stats.speed.ToString());
						GUILayout.Label("Defence: " + item.stats.defence.ToString());
						GUILayout.Label("Damage: " + item.stats.damage.ToString());
						GUILayout.Label("Store price: " + item.stats.storePrice.ToString());
					EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
	}
	
	private void DrawWeapon(List<ItemWeapon> weapon) {
		foreach(ItemWeapon item in weapon) {
			Texture2D preview = AssetPreview.GetAssetPreview(item.model);
			
			EditorGUILayout.BeginHorizontal();
				GUILayout.Label(preview, GUILayout.MaxWidth(60f), GUILayout.MaxHeight(60f));
				
				EditorGUILayout.BeginVertical();
					GUILayout.Label(item.itemName, EditorStyles.boldLabel);
					GUILayout.Label(item.element.ToString() + " " + item.equipableType.ToString() + " - " + item.weaponType.ToString());
					
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label("Speed: " + item.stats.speed.ToString());
						GUILayout.Label("Defence: " + item.stats.defence.ToString());
						GUILayout.Label("Damage: " + item.stats.damage.ToString());
						GUILayout.Label("Store price: " + item.stats.storePrice.ToString());
					EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
	}
		
	private void DrawPower(List<ItemPower> power) {
		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.margin = new RectOffset(68, 0, 0, 0);
		
		foreach(ItemPower item in power) {
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.BeginVertical(style);
					GUILayout.Label(item.itemName, EditorStyles.boldLabel);
				
					EditorGUILayout.BeginHorizontal();	
						GUILayout.Label("Type: " + item.powerType.ToString());
						GUILayout.Label("Time: " + item.time.ToString());
					EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
	}
	
	private void DrawSpecial(List<ItemSpecial> special) {
		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.margin = new RectOffset(68, 0, 0, 0);
		
		foreach(ItemSpecial item in special) {
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.BeginVertical(style);
					GUILayout.Label(item.itemName, EditorStyles.boldLabel);
				
					EditorGUILayout.BeginHorizontal();
						GUILayout.Label("Type: " + item.id.ToString());
					EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
	}
}
