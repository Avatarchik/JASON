using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ItemCreator:EditorWindow {
	private Item item = new Item();
	private ItemEquipable itemEquipable = new ItemEquipable();
	private ItemPower itemPower = new ItemPower();
	private ItemSpecial itemSpecial = new ItemSpecial();
	private ItemWeapon itemWeapon = new ItemWeapon();

	[MenuItem("Window/Item Manager")]
	static void Init() {
		ItemCreator window = (ItemCreator)EditorWindow.GetWindow(typeof(ItemCreator));
	}
	
	void OnEnable() {
		itemEquipable.stats = new ItemEquipable.ItemEquipableStats();
		itemWeapon.stats = new ItemEquipable.ItemEquipableStats();
	}
	
	void OnGUI() {
		GUILayout.Label("General Settings", EditorStyles.boldLabel);
		
		DrawGeneralSettings();
	}
	
	private void DrawGeneralSettings() {
		item.itemType = (Item.ItemType)EditorGUILayout.EnumPopup("Item Type", item.itemType);
		item.itemName = EditorGUILayout.TextField("Item Name", item.itemName);
		
		switch(item.itemType) {
		case Item.ItemType.Equipable:
			DrawEquipableSettings();
			break;
		case Item.ItemType.Power:
			DrawPowerSettings();
			break;
		case Item.ItemType.Special:
			DrawSpecialSettings();
			break;
		}
	}
	
	private void DrawEquipableSettings() {
		GUILayout.Label("Equipable Settings", EditorStyles.boldLabel);
	
		itemEquipable.equipableType = (ItemEquipable.EquipableType)EditorGUILayout.EnumPopup("Equipable Type", itemEquipable.equipableType);
		itemEquipable.element = (ItemEquipable.EquipableElement)EditorGUILayout.EnumPopup("Element", itemEquipable.element);
		itemEquipable.model = (GameObject)EditorGUILayout.ObjectField("Model", itemEquipable.model, typeof(GameObject), false);
		itemEquipable.stats.speed = EditorGUILayout.IntField("Speed", itemEquipable.stats.speed);
		itemEquipable.stats.defence = EditorGUILayout.IntField("Defence", itemEquipable.stats.defence);
		itemEquipable.stats.storePrice = EditorGUILayout.IntField("Store Price", itemEquipable.stats.storePrice);
		
		if(itemEquipable.equipableType == ItemEquipable.EquipableType.Weapon) {
			DrawWeaponSettings();
		} else {
			//if(GUILayout.Button("Create"))
				// Add item
		}
	}
	
	private void DrawWeaponSettings() {
		GUILayout.Label("Weapon Settings", EditorStyles.boldLabel);
		
		itemWeapon.weaponType = (ItemWeapon.WeaponType)EditorGUILayout.EnumPopup("Weapon Type", itemWeapon.weaponType);
		itemWeapon.stats.damage = EditorGUILayout.IntField("Damage", itemWeapon.stats.damage);
		
		//if(GUILayout.Button("Create"))
			// Add item
	}
	
	private void DrawPowerSettings() {
		GUILayout.Label("Power Settings", EditorStyles.boldLabel);
		
		itemPower.powerType = (ItemPower.PowerType)EditorGUILayout.EnumPopup("Power Type", itemPower.powerType);
		itemPower.time = EditorGUILayout.IntField(itemPower.powerType + " Time", itemPower.time);
		
		//if(GUILayout.Button("Create"))
			// Add item
	}
	
	private void DrawSpecialSettings() {
		GUILayout.Label("Special Settings", EditorStyles.boldLabel);
		
		itemSpecial.id = EditorGUILayout.IntField("ID", itemSpecial.id);
		
		//if(GUILayout.Button("Create"))
			// Add item
	}
}
