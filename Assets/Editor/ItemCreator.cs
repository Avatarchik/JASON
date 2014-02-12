using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class ItemCreator:EditorWindow {
	private Item item = new Item();
	private ItemEquipable itemEquipable = new ItemEquipable();
	private ItemPower itemPower = new ItemPower();
	private ItemSpecial itemSpecial = new ItemSpecial();
	private ItemWeapon itemWeapon = new ItemWeapon();
	
	private ItemManager itemManager;
	private ItemList itemList;
	
	private string addedName;
	private bool added;

	private GameObject lastObjectSelected;
	
	void OnEnable() {
		added = false;
		itemEquipable.Stats = new ItemEquipable.ItemEquipableStats();
		itemWeapon.Stats = new ItemEquipable.ItemEquipableStats();
	}
	
	void OnGUI() {
		if(Selection.activeGameObject == null || (Selection.activeGameObject != lastObjectSelected && lastObjectSelected != null)) {
			Debug.LogError("You need to have a Game Object with the ItemList component selected");
			return;
		} else if(itemList == null) {
			itemList = (Selection.activeGameObject).GetComponent<ItemList>();
		}

		if(added)
			EditorGUILayout.HelpBox("Item '" + addedName + "' added!", MessageType.Info);
	
		GUILayout.Label("General Settings", EditorStyles.boldLabel);
		DrawGeneralSettings();

		lastObjectSelected = Selection.activeGameObject;
	}
	
	private void DrawGeneralSettings() {
		item.Type = (Item.ItemType)EditorGUILayout.EnumPopup("Item Type", item.Type);
		item.Name = EditorGUILayout.TextField("Item Name", item.Name);
		
		switch(item.Type) {
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

		itemEquipable.TypeEquipable = (ItemEquipable.EquipableType)EditorGUILayout.EnumPopup("Equipable Type", itemEquipable.TypeEquipable);
		itemEquipable.Element = (ItemEquipable.EquipableElement)EditorGUILayout.EnumPopup("Element", itemEquipable.Element);
		itemEquipable.Model = (GameObject)EditorGUILayout.ObjectField("Model", itemEquipable.Model, typeof(GameObject), false);
		itemEquipable.Stats.Speed = EditorGUILayout.IntField("Speed", itemEquipable.Stats.Speed);
		itemEquipable.Stats.Defence = EditorGUILayout.IntField("Defence", itemEquipable.Stats.Defence);
		itemEquipable.Stats.StorePrice = EditorGUILayout.IntField("Store Price", itemEquipable.Stats.StorePrice);
		
		if(itemEquipable.TypeEquipable == ItemEquipable.EquipableType.Weapon) {
			DrawWeaponSettings();
		} else {
			if(GUILayout.Button("Create")) {
				if(VerifyEquipable()) {
					itemList.EquipableItems.Add(new ItemEquipable(item.Name, itemEquipable.TypeEquipable, itemEquipable.Element, itemEquipable.Stats, itemEquipable.Model));
					Reset();
				}
			}
		}
	}
	
	private void DrawWeaponSettings() {
		GUILayout.Label("Weapon Settings", EditorStyles.boldLabel);
		
		itemWeapon.TypeWeapon = (ItemWeapon.WeaponType)EditorGUILayout.EnumPopup("Weapon Type", itemWeapon.TypeWeapon);
		itemWeapon.Stats.Damage = EditorGUILayout.IntField("Damage", itemWeapon.Stats.Damage);
		
		if(GUILayout.Button("Create")) {
			if(VerifyWeapon()) {
				itemList.WeaponItems.Add(new ItemWeapon(item.Name, itemEquipable.Element, itemEquipable.Stats, itemEquipable.Model, itemWeapon.TypeWeapon));
				Reset();
			}
		}
	}
	
	private void DrawPowerSettings() {
		GUILayout.Label("Power Settings", EditorStyles.boldLabel);
		
		itemPower.TypePower = (ItemPower.PowerType)EditorGUILayout.EnumPopup("Power Type", itemPower.TypePower);
		itemPower.Time = EditorGUILayout.IntField(itemPower.TypePower + " Time", itemPower.Time);
		
		if(GUILayout.Button("Create")) {
			if(VerifyPower()) {
				itemList.PowerItems.Add(new ItemPower(item.Name, itemPower.TypePower, itemPower.Time));
				Reset();
			}
		}
	}
	
	private void DrawSpecialSettings() {
		GUILayout.Label("Special Settings", EditorStyles.boldLabel);
		
		itemSpecial.Id = EditorGUILayout.IntField("ID", itemSpecial.Id);
		
		if(GUILayout.Button("Create")) {
			if(VerifySpecial()) {
				itemList.SpecialItems.Add(new ItemSpecial(item.Name, itemSpecial.Id));
				Reset();
			}
		}
	}
	
	private bool VerifyItem() {
		bool passed = true;
	
		if(item.Name == "" || item.Name == null) {
			passed = false;
		} else {
			foreach(Item item2 in itemList.EquipableItems)
				if(item.Name.Equals(item2.Name))
					passed = false;

			foreach(Item item2 in itemList.WeaponItems)
				if(item.Name.Equals(item2.Name))
					passed = false;

			foreach(Item item2 in itemList.PowerItems)
				if(item.Name.Equals(item2.Name))
					passed = false;

			foreach(Item item2 in itemList.SpecialItems)
				if(item.Name.Equals(item2.Name))
					passed = false;
		}
			
		return passed;	
	}
	
	private bool VerifyEquipable() {
		bool passed = VerifyItem();
		
		if(itemEquipable.Model == null)
			passed = false;
			
		return passed;
	}
	
	private bool VerifyWeapon() {
		return VerifyEquipable();
	}
	
	private bool VerifyPower() {
		bool passed = VerifyItem();
		
		if(itemPower.Time == 0)
			passed = false;
		
		return passed;
	}
	
	private bool VerifySpecial() {
		return VerifyItem();
	}

	private void Reset() {
		if(itemManager != null)
			itemManager.Repaint();

		if(itemList != null) {
			UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/Items/Item Manager.prefab");
			PrefabUtility.ReplacePrefab(Selection.activeGameObject, prefab);
			AssetDatabase.Refresh();
		}
		
		addedName = item.Name;
		added = true;
	
		item = new Item();
		itemEquipable = new ItemEquipable();
		itemPower = new ItemPower();
		itemSpecial = new ItemSpecial();
		itemWeapon = new ItemWeapon();
	
		itemEquipable.Stats = new ItemEquipable.ItemEquipableStats();
		itemWeapon.Stats = new ItemEquipable.ItemEquipableStats();
	}
	
	public void SetItemManager(ItemManager itemManager) {
		this.itemManager = itemManager;
	}

	public void EditItem<T>(T t) {
		if(t.GetType() == typeof(ItemEquipable)) {
			EditEquipable((ItemEquipable)Convert.ChangeType(t, typeof(ItemEquipable)));
		} else if(t.GetType() == typeof(ItemWeapon)) {
			EditWeapon((ItemWeapon)Convert.ChangeType(t, typeof(ItemWeapon)));
		} else if(t.GetType() == typeof(ItemPower)) {
			EditPower((ItemPower)Convert.ChangeType(t, typeof(ItemPower)));
		} else if(t.GetType() == typeof(ItemSpecial)) {
			EditSpecial((ItemSpecial)Convert.ChangeType(t, typeof(ItemSpecial)));
		}
	}

	private void EditItem(Item item) {
		this.item.Name = item.Name;
		this.item.Type = item.Type;
	}

	private void EditEquipable(ItemEquipable item) {
		EditItem(item);

		this.itemEquipable.TypeEquipable = item.TypeEquipable;
		this.itemEquipable.Element = item.Element;
		this.itemEquipable.Model = item.Model;
		this.itemEquipable.Stats = item.Stats;
	}

	private void EditWeapon(ItemWeapon item) {
		EditItem(item);
		EditEquipable (item);

		this.itemWeapon.TypeWeapon = item.TypeWeapon;
	}

	private void EditPower(ItemPower item) {
		EditItem(item);

		this.itemPower.TypePower = item.TypePower;
		this.itemPower.Time = item.Time;
	}

	private void EditSpecial(ItemSpecial item) {
		EditItem(item);

		this.itemSpecial.Id = item.Id;
	}
}