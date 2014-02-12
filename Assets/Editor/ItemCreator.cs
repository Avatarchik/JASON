using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class ItemCreator:EditorWindow {
	private List<HelpBox> helpBoxes;

	private Item item = new Item();
	private ItemEquipable itemEquipable = new ItemEquipable();
	private ItemPower itemPower = new ItemPower();
	private ItemSpecial itemSpecial = new ItemSpecial();
	private ItemWeapon itemWeapon = new ItemWeapon();
	
	private ItemManager itemManager;
	private ItemList itemList;

	private GameObject lastObjectSelected;

	private int editArrayId;
	
	void OnEnable() {
		itemEquipable.Stats = new ItemEquipable.ItemEquipableStats();
		itemWeapon.Stats = new ItemEquipable.ItemEquipableStats();

		helpBoxes = new List<HelpBox>();

		editArrayId = -1;
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

		foreach(HelpBox helpBox in helpBoxes)
			helpBox.Render();
	
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
					Reset(item);
				}
			}

			if(editArrayId >= 0)
				if(GUILayout.Button("Delete")) {
					itemList.EquipableItems.RemoveAt(editArrayId);

					if(itemManager != null)
						itemManager.Repaint();

					editArrayId = -1;
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
				Reset(item);
			}
		}

		if(editArrayId >= 0)
			if(GUILayout.Button("Delete")) {
				itemList.WeaponItems.RemoveAt(editArrayId);

				if(itemManager != null)
					itemManager.Repaint();

				editArrayId = -1;
			}
	}
	
	private void DrawPowerSettings() {
		GUILayout.Label("Power Settings", EditorStyles.boldLabel);
		
		itemPower.TypePower = (ItemPower.PowerType)EditorGUILayout.EnumPopup("Power Type", itemPower.TypePower);
		itemPower.Time = EditorGUILayout.IntField(itemPower.TypePower + " Time", itemPower.Time);
		
		if(GUILayout.Button("Create")) {
			if(VerifyPower()) {
				itemList.PowerItems.Add(new ItemPower(item.Name, itemPower.TypePower, itemPower.Time));
				Reset(item);
			}
		}

		if(editArrayId >= 0)
			if(GUILayout.Button("Delete")) {
				itemList.PowerItems.RemoveAt(editArrayId);
			
				if(itemManager != null)
					itemManager.Repaint();

				editArrayId = -1;
			}
	}
	
	private void DrawSpecialSettings() {
		GUILayout.Label("Special Settings", EditorStyles.boldLabel);
		
		itemSpecial.Id = EditorGUILayout.IntField("ID", itemSpecial.Id);
		
		if(GUILayout.Button("Create")) {
			if(VerifySpecial()) {
				itemList.SpecialItems.Add(new ItemSpecial(item.Name, itemSpecial.Id));
				Reset(item);
			}
		}

		if(editArrayId >= 0)
			if(GUILayout.Button("Delete")) {
				itemList.SpecialItems.RemoveAt(editArrayId);
			
				if(itemManager != null)
					itemManager.Repaint();

				editArrayId = -1;
			}
	}
	
	private bool VerifyItem() {
		helpBoxes.Clear();

		bool passed = true;
	
		if(item.Name == "" || item.Name == null) {
			helpBoxes.Add(new HelpBox("An item needs a name!", MessageType.Error));
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

			if(!passed)
				helpBoxes.Add(new HelpBox("An item with this name already exists", MessageType.Error));
		}
			
		return passed;	
	}
	
	private bool VerifyEquipable() {
		bool passed = VerifyItem();
		
		if(itemEquipable.Model == null) {
			helpBoxes.Add(new HelpBox("An equipable item needs a model!", MessageType.Error));
			passed = false;
		}
			
		return passed;
	}
	
	private bool VerifyWeapon() {
		return VerifyEquipable();
	}
	
	private bool VerifyPower() {
		bool passed = VerifyItem();
		
		if(itemPower.Time == 0) {
			helpBoxes.Add(new HelpBox("A power needs a time", MessageType.Error));
			passed = false;
		}
		
		return passed;
	}
	
	private bool VerifySpecial() {
		return VerifyItem();
	}

	public void Reset(Item added) {
		if(itemManager != null)
			itemManager.Repaint();

		if(itemList != null) {
			UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/Items/Item Manager.prefab");
			PrefabUtility.ReplacePrefab(Selection.activeGameObject, prefab);
			AssetDatabase.Refresh();
		}

		if(added != null)
			helpBoxes.Add(new HelpBox("Item '" + item.Name + "' added!", MessageType.Info));
	
		item = new Item();
		itemEquipable = new ItemEquipable();
		itemPower = new ItemPower();
		itemSpecial = new ItemSpecial();
		itemWeapon = new ItemWeapon();
	
		itemEquipable.Stats = new ItemEquipable.ItemEquipableStats();
		itemWeapon.Stats = new ItemEquipable.ItemEquipableStats();

		editArrayId = -1;
	}

	public void SetItemManager(ItemManager itemManager) {
		this.itemManager = itemManager;
	}

	private void EditItem(Item item) {
		this.item = item;
	}

	public void EditEquipable(ItemEquipable item) {
		EditItem(item);

		this.itemEquipable = item;

		if(itemList != null)
			editArrayId = itemList.EquipableItems.IndexOf(item);
	}

	public void EditWeapon(ItemWeapon item) {
		EditItem(item);
		EditEquipable(item);

		this.itemWeapon = item;

		if(itemList != null)
			editArrayId = itemList.WeaponItems.IndexOf(item);
	}

	public void EditPower(ItemPower item) {
		EditItem(item);

		this.itemPower = item;

		if(itemList != null)
			editArrayId = itemList.PowerItems.IndexOf(item);
	}

	public void EditSpecial(ItemSpecial item) {
		EditItem(item);

		this.itemSpecial = item;

		if(itemList != null)
			editArrayId = itemList.SpecialItems.IndexOf(item);
	}
}