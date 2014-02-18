using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class ItemCreator:EditorWindow {
	private List<HelpBox> helpBoxes;
	
	private GameObject selectedGameObject;
	private ItemList itemList;

	private Item item;
	private EquipableData equipable;
	private WeaponData weapon;
	private PowerData power;
	private SpecialData special;

	private bool updated;
	
	void OnEnable() {
		helpBoxes = new List<HelpBox>();

		item = new Item();

		equipable = new EquipableData();
		weapon = new WeaponData();
		power = new PowerData();
		special = new SpecialData();
	}
	
	void OnGUI() {
		if(!CheckForItemList())
			return;
		
		foreach(HelpBox helpBox in helpBoxes)
			helpBox.Render();
		
		DrawGeneralSettings();
	}
	
	/** Draw general item settings */
	private void DrawGeneralSettings() {
		GUILayout.Label("General Settings", EditorStyles.boldLabel);

		item.itemType = (Item.ItemType)EditorGUILayout.EnumPopup("Item Type", item.itemType);
		item.itemName = EditorGUILayout.TextField("Item Name", item.itemName);
		item.itemDescription = EditorGUILayout.TextField("Item Description", item.itemDescription);
		
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
	
	/** Draw the equipable item settings */
	private void DrawEquipableSettings() {
		GUILayout.Label("Equipable Settings", EditorStyles.boldLabel);

		equipable.equipableType = (EquipableData.EquipableType)EditorGUILayout.EnumPopup("Type", equipable.equipableType);
		equipable.element = (EquipableData.EquipableElement)EditorGUILayout.EnumPopup("Element", equipable.element);
		equipable.rarity = (EquipableData.EquipableRarity)EditorGUILayout.EnumPopup("Rarity", equipable.rarity);
		equipable.model = (GameObject)EditorGUILayout.ObjectField("Model", equipable.model, typeof(GameObject), false);

		equipable.stats.speed = EditorGUILayout.IntField("Speed", equipable.stats.speed);
		equipable.stats.defence = EditorGUILayout.IntField("Defence", equipable.stats.defence);
		equipable.stats.storePrice = EditorGUILayout.IntField("Store Price", equipable.stats.storePrice);
		
		if(equipable.equipableType == EquipableData.EquipableType.Weapon) {
			DrawWeaponSettings();
		} else {
			if(GUILayout.Button("Create"))
				CreateEquipable();
		}
	}
	
	/** Draw the weapon item settings */
	private void DrawWeaponSettings() {
		GUILayout.Label("Weapon Settings", EditorStyles.boldLabel);

		weapon.weaponType = (WeaponData.WeaponType)EditorGUILayout.EnumPopup("Weapon Type", weapon.weaponType);
		weapon.stats.damage = EditorGUILayout.IntField("Damage", weapon.stats.damage);

		if(GUILayout.Button("Create"))
			CreateWeapon();
	}
	
	/** Draw the power item settings */
	private void DrawPowerSettings() {
		GUILayout.Label("Power Settings", EditorStyles.boldLabel);

		power.powerType = (PowerData.PowerType)EditorGUILayout.EnumPopup("Type", power.powerType);
		power.time = EditorGUILayout.IntField(power.powerType.ToString() + " Time", power.time);
		
		if(GUILayout.Button("Create"))
			CreatePower();
	}
	
	/** Draw the special item settings */
	private void DrawSpecialSettings() {
		GUILayout.Label("Special Settings", EditorStyles.boldLabel);

		special.id = EditorGUILayout.IntField("ID", special.id);
		
		if(GUILayout.Button("Create"))
			CreateSpecial();
	}
	
	/** Verify an item */
	private bool VerifyItem() {
		bool passed = true;
		
		helpBoxes.Clear();
		
		if(item.itemName == "" || item.itemName == null) {
			helpBoxes.Add(new HelpBox("The item needs a name!", MessageType.Error));
			passed = false;
		} else {
			foreach(EquipableData item2 in itemList.EquipableItems)
				if(item.itemName.Equals(item2.itemName))
					passed = false;
			
			foreach(WeaponData item2 in itemList.WeaponItems)
				if(item.itemName.Equals(item2.itemName))
					passed = false;
			
			foreach(PowerData item2 in itemList.PowerItems)
				if(item.itemName.Equals(item2.itemName))
					passed = false;
			
			foreach(SpecialData item2 in itemList.SpecialItems)
				if(item.itemName.Equals(item2.itemName))
					passed = false;
			
			if(!passed)
				helpBoxes.Add(new HelpBox("An item with this name already exists", MessageType.Error));
		}

		if(item.itemDescription == "" || item.itemDescription == null) {
			helpBoxes.Add(new HelpBox("The item needs a description!", MessageType.Error));
			passed = false;
		}
		
		return passed;	
	}
	
	/** Try to create an equipable item */
	private void CreateEquipable() {
		bool passed = VerifyItem();

		if(passed) {
			EquipableData itemData = equipable;

			itemData.itemType = Item.ItemType.Equipable;
			itemData.itemName = item.itemName;
			itemData.itemDescription = item.itemDescription;

			itemList.EquipableItems.Add(itemData);
			ItemCreated();
		}
	}
	
	/** Try to create a weapon */
	private void CreateWeapon() {
		bool passed = VerifyItem();
		
		if(passed) {
			WeaponData itemData = weapon;

			itemData.itemType = Item.ItemType.Equipable;
			itemData.itemName = item.itemName;
			itemData.itemDescription = item.itemDescription;

			itemList.WeaponItems.Add(itemData);
			ItemCreated();
		}
	}
	
	/** Try to create a power item */
	private void CreatePower() {
		bool passed = VerifyItem();
		
		if(power.time == 0) {
			helpBoxes.Add(new HelpBox("The power item needs a time", MessageType.Error));
			passed = false;
		}
		
		if(passed) {
			PowerData itemData = power;

			itemData.itemType = Item.ItemType.Power;
			itemData.itemName = item.itemName;
			itemData.itemDescription = item.itemDescription;

			itemList.PowerItems.Add(itemData);
			ItemCreated();
		}
	}
	
	/** Try to create a special item */
	private void CreateSpecial() {
		bool passed = VerifyItem();
	
		if(passed) {
			SpecialData itemData = special;

			itemData.itemType = Item.ItemType.Special;
			itemData.itemName = item.itemName;
			itemData.itemDescription = item.itemDescription;

			itemList.SpecialItems.Add(itemData);
			ItemCreated();
		}
	}
	
	/** Replace the prefab when an item is created */
	private void ItemCreated() {
		UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/Items/Item Manager.prefab");
		PrefabUtility.ReplacePrefab(Selection.activeGameObject, prefab);
		AssetDatabase.Refresh();
		
		helpBoxes.Add(new HelpBox("Item '" + item.itemName + "' added!", MessageType.Info));
		
		updated = true;
	}
	
	/** Check if the selected Game Object has an ItemList component attached */
	private bool CheckForItemList() {
		bool found = true;
	
		if(Selection.activeGameObject == null || (Selection.activeGameObject != selectedGameObject && selectedGameObject != null)) {
			GUILayout.Label("No Game Object with an ItemList component selected");
			found = false;
		} else if(itemList == null) {
			itemList = (Selection.activeGameObject).GetComponent<ItemList>();
			
			if(itemList == null) {
				GUILayout.Label("Can't find an ItemList component on the selected Game Object");
				found = false;
			}
		}
		
		selectedGameObject = Selection.activeGameObject;
		
		return found;
	}
	
	public bool Updated { get { return updated; } }
}