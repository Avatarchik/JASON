using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class ItemCreator:EditorWindow {
	private List<HelpBox> helpBoxes;
	
	private GameObject selectedGameObject;
	private ItemList itemList;
	
	private bool updated;
	
	void OnEnable() {
		helpBoxes = new List<HelpBox>();
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
		Item.item.Type = (Item.ItemType)EditorGUILayout.EnumPopup("Item Type", Item.item.Type);
		Item.item.Name = EditorGUILayout.TextField("Item Name", Item.item.Name);
		
		switch(Item.item.Type) {
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
		
		ItemEquipable.equipable.TypeEquipable = (ItemEquipable.EquipableType)EditorGUILayout.EnumPopup("Equipable Type", ItemEquipable.equipable.TypeEquipable);
		ItemEquipable.equipable.Element = (ItemEquipable.EquipableElement)EditorGUILayout.EnumPopup("Element", ItemEquipable.equipable.Element);
		ItemEquipable.equipable.Model = (GameObject)EditorGUILayout.ObjectField("Model", ItemEquipable.equipable.Model, typeof(GameObject), false);
		ItemEquipable.equipable.Stats.Speed = EditorGUILayout.IntField("Speed", ItemEquipable.equipable.Stats.Speed);
		ItemEquipable.equipable.Stats.Defence = EditorGUILayout.IntField("Defence", ItemEquipable.equipable.Stats.Defence);
		ItemEquipable.equipable.Stats.StorePrice = EditorGUILayout.IntField("Store Price", ItemEquipable.equipable.Stats.StorePrice);
		
		if(ItemEquipable.equipable.TypeEquipable == ItemEquipable.EquipableType.Weapon) {
			DrawWeaponSettings();
		} else {
			if(GUILayout.Button("Create"))
				CreateEquipable();
		}
	}
	
	/** Draw the weapon item settings */
	private void DrawWeaponSettings() {
		GUILayout.Label("Weapon Settings", EditorStyles.boldLabel);
		
		ItemWeapon.weapon.TypeWeapon = (ItemWeapon.WeaponType)EditorGUILayout.EnumPopup("Weapon Type", ItemWeapon.weapon.TypeWeapon);
		ItemWeapon.weapon.Stats.Damage = EditorGUILayout.IntField("Damage", ItemWeapon.equipable.Stats.Damage);
		
		if(GUILayout.Button("Create"))
			CreateWeapon();
	}
	
	/** Draw the power item settings */
	private void DrawPowerSettings() {
		GUILayout.Label("Power Settings", EditorStyles.boldLabel);
		
		ItemPower.power.TypePower = (ItemPower.PowerType)EditorGUILayout.EnumPopup("Power Type", ItemPower.power.TypePower);
		ItemPower.power.Time = EditorGUILayout.IntField(ItemPower.power.TypePower + " Time", ItemPower.power.Time);
		
		if(GUILayout.Button("Create"))
			CreatePower();
	}
	
	/** Draw the special item settings */
	private void DrawSpecialSettings() {
		GUILayout.Label("Special Settings", EditorStyles.boldLabel);
		
		ItemSpecial.special.Id = EditorGUILayout.IntField("ID", ItemSpecial.special.Id);
		
		if(GUILayout.Button("Create"))
			CreateSpecial();
	}
	
	/** Verify an item */
	private bool VerifyItem() {
		bool passed = true;
		
		helpBoxes.Clear();
		
		if(Item.item.Name == "" || Item.item.Name == null) {
			helpBoxes.Add(new HelpBox("An item needs a name!", MessageType.Error));
			passed = false;
		} else {
			foreach(Item item2 in itemList.EquipableItems)
				if(Item.item.Name.Equals(item2.Name))
					passed = false;
			
			foreach(Item item2 in itemList.WeaponItems)
				if(Item.item.Name.Equals(item2.Name))
					passed = false;
			
			foreach(Item item2 in itemList.PowerItems)
				if(Item.item.Name.Equals(item2.Name))
					passed = false;
			
			foreach(Item item2 in itemList.SpecialItems)
				if(Item.item.Name.Equals(item2.Name))
					passed = false;
			
			if(!passed)
				helpBoxes.Add(new HelpBox("An item with this name already exists", MessageType.Error));
		}
		
		return passed;	
	}
	
	/** Try to create an equipable item */
	private void CreateEquipable() {
		bool passed = VerifyItem();
		
		if(ItemEquipable.equipable.Model == null) {
			helpBoxes.Add(new HelpBox("An equipable item needs a model!", MessageType.Error));
			passed = false;
		}
		
		if(passed) {
			itemList.EquipableItems.Add(new ItemEquipable(Item.item.Name, ItemEquipable.equipable.TypeEquipable, ItemEquipable.equipable.Element, ItemEquipable.equipable.Stats, ItemEquipable.equipable.Model));
			ItemCreated();
		}
	}
	
	/** Try to create a weapon */
	private void CreateWeapon() {
		bool passed = VerifyItem();
		
		if(ItemEquipable.equipable.Model == null) {
			helpBoxes.Add(new HelpBox("An equipable item needs a model!", MessageType.Error));
			passed = false;
		}
		
		if(passed) {
			itemList.WeaponItems.Add(new ItemWeapon(Item.item.Name, ItemEquipable.equipable.Element, ItemEquipable.equipable.Stats, ItemEquipable.equipable.Model, ItemWeapon.weapon.TypeWeapon));
			ItemCreated();
		}
	}
	
	/** Try to create a power item */
	private void CreatePower() {
		bool passed = VerifyItem();
		
		if(ItemPower.power.Time == 0) {
			helpBoxes.Add(new HelpBox("A power needs a time", MessageType.Error));
			passed = false;
		}
		
		if(passed) {
			itemList.PowerItems.Add(new ItemPower(Item.item.Name, ItemPower.power.TypePower, ItemPower.power.Time));
			ItemCreated();
		}
	}
	
	/** Try to create a special item */
	private void CreateSpecial() {
		bool passed = VerifyItem();
	
		if(passed) {
			itemList.SpecialItems.Add(new ItemSpecial(Item.item.Name, ItemSpecial.special.Id));
			ItemCreated();
		}
	}
	
	/** Replace the prefab when an item is created */
	private void ItemCreated() {
		UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/Items/Item Manager.prefab");
		PrefabUtility.ReplacePrefab(Selection.activeGameObject, prefab);
		AssetDatabase.Refresh();
		
		helpBoxes.Add(new HelpBox("Item '" + Item.item.Name + "' added!", MessageType.Info));
		
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