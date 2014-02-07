using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ItemCreator:EditorWindow {
	[MenuItem("Window/Item Adder")]
	static void Init() {
		ItemCreator window = (ItemCreator)EditorWindow.GetWindow(typeof(ItemCreator));
	}

	private Item.ItemType itemType;
	private string itemName;	
	void OnGUI() {
		GUILayout.Label("General Settings", EditorStyles.boldLabel);

		itemType = (Item.ItemType)EditorGUILayout.EnumPopup("Item Type", itemType);
		itemName = EditorGUILayout.TextField("Item Name", itemName);

		switch(itemType) {
		case Item.ItemType.Equipable:
			DrawEquipable();
			break;
		case Item.ItemType.Weapon:
			DrawWeapon();
			break;
		case Item.ItemType.Power:
			DrawPower();
			break;
		case Item.ItemType.Special:
			DrawSpecial();
			break;
		}
	}

	private ItemEquipable.EquipableType equipableType;
	private GameObject equipableModel;
	private int equipableDefence;
	private int equipableSpeed;
	private int equipableStorePrice;
	private bool equipableStoreObject;
	private void DrawEquipable() {
		GUILayout.Label("Equipable Settings", EditorStyles.boldLabel);

		equipableStoreObject = EditorGUILayout.Toggle("Store Object", equipableStoreObject);
		equipableModel = (GameObject)EditorGUILayout.ObjectField("Model", equipableModel, typeof(GameObject), false);
		equipableDefence = EditorGUILayout.IntField("Defence", equipableDefence);
		equipableSpeed = EditorGUILayout.IntField("Speed", equipableSpeed);

		if(equipableStoreObject) {
			equipableStorePrice = EditorGUILayout.IntField("Store Price", equipableSpeed);
		}

		if(GUILayout.Button("Create"))
			CreateEquipable();
	}

	private ItemWeapon.WeaponType weaponType;
	private int weaponDamage;
	private int weaponSpeed;
	private void DrawWeapon() {
		GUILayout.Label("Weapon Settings", EditorStyles.boldLabel);

		weaponType = (ItemWeapon.WeaponType)EditorGUILayout.EnumPopup ("Weapon Type", weaponType);
		equipableStoreObject = EditorGUILayout.Toggle("Store Object", equipableStoreObject);
		equipableModel = (GameObject)EditorGUILayout.ObjectField("Model", equipableModel, typeof(GameObject), false);
		weaponDamage = EditorGUILayout.IntField("Damage", weaponDamage);
		weaponSpeed = EditorGUILayout.IntField("Speed", weaponSpeed);

		if(equipableStoreObject) {
			equipableStorePrice = EditorGUILayout.IntField("Store Price", equipableSpeed);
		}

		if(GUILayout.Button("Create"))
			CreateWeapon();
	}

	private ItemPower.PowerType powerType;
	private int buffTime;
	private void DrawPower() {
		GUILayout.Label("Power Settings", EditorStyles.boldLabel);
		
		powerType = (ItemPower.PowerType)EditorGUILayout.EnumPopup("Power Type", powerType);
		buffTime = EditorGUILayout.IntField(powerType + " Time", buffTime);

		if(GUILayout.Button("Create"))
			CreatePower();
	}

	private int specialId;
	private void DrawSpecial() {
		GUILayout.Label("Special Settings", EditorStyles.boldLabel);

		specialId = EditorGUILayout.IntField("ID", specialId);

		if(GUILayout.Button("Create"))
			CreateSpecial();
	}

	private void CreateEquipable() {
		ItemEquipable item = new ItemEquipable(equipableType, equipableModel, equipableStoreObject ? equipableStorePrice : 0);

		ItemList.Add(item);
	}

	private void CreateWeapon() {
		
	}

	private void CreatePower() {
		
	}

	private void CreateSpecial() {
		
	}
}
