using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ItemCreator:EditorWindow {
	private Item item = new Item();
	private ItemEquipable itemEquipable = new ItemEquipable();
	private ItemPower itemPower = new ItemPower();
	private ItemSpecial itemSpecial = new ItemSpecial();
	private ItemWeapon itemWeapon = new ItemWeapon();
	
	private ItemManager itemManager;
	
	private string addedName;
	private bool added;
	
	void OnEnable() {
		added = false;
		itemEquipable.stats = new ItemEquipable.ItemEquipableStats();
		itemWeapon.stats = new ItemEquipable.ItemEquipableStats();
	}
	
	void OnGUI() {
		if(added)
			EditorGUILayout.HelpBox("Item '" + addedName + "' added!", MessageType.Info);
	
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
			if(GUILayout.Button("Create")) {
				if(VerifyEquipable()) {
					ItemList.items.Add(new ItemEquipable(item.itemName, itemEquipable.equipableType, itemEquipable.element, itemEquipable.stats, itemEquipable.model));
					Reset();
				}
			}
		}
	}
	
	private void DrawWeaponSettings() {
		GUILayout.Label("Weapon Settings", EditorStyles.boldLabel);
		
		itemWeapon.weaponType = (ItemWeapon.WeaponType)EditorGUILayout.EnumPopup("Weapon Type", itemWeapon.weaponType);
		itemWeapon.stats.damage = EditorGUILayout.IntField("Damage", itemWeapon.stats.damage);
		
		if(GUILayout.Button("Create")) {
			if(VerifyWeapon()) {
				ItemList.items.Add(new ItemWeapon(item.itemName, itemEquipable.element, itemEquipable.stats, itemEquipable.model, itemWeapon.weaponType));
				Reset();
			}
		}
	}
	
	private void DrawPowerSettings() {
		GUILayout.Label("Power Settings", EditorStyles.boldLabel);
		
		itemPower.powerType = (ItemPower.PowerType)EditorGUILayout.EnumPopup("Power Type", itemPower.powerType);
		itemPower.time = EditorGUILayout.IntField(itemPower.powerType + " Time", itemPower.time);
		
		if(GUILayout.Button("Create")) {
			if(VerifyPower()) {
				ItemList.items.Add(new ItemPower(item.itemName, itemPower.powerType, itemPower.time));
				Reset();
			}
		}
	}
	
	private void DrawSpecialSettings() {
		GUILayout.Label("Special Settings", EditorStyles.boldLabel);
		
		itemSpecial.id = EditorGUILayout.IntField("ID", itemSpecial.id);
		
		if(GUILayout.Button("Create")) {
			if(VerifySpecial()) {
				ItemList.items.Add(new ItemSpecial(item.itemName, itemSpecial.id));
				Reset();
			}
		}
	}
	
	private bool VerifyItem() {
		bool passed = true;
	
		if(item.itemName == "" || item.itemName == null) {
			passed = false;
		} else {
			foreach(Item item2 in ItemList.items)
				if(item.itemName.Equals(item2.itemName))
					passed = false;
		}
			
		return passed;	
	}
	
	private bool VerifyEquipable() {
		bool passed = VerifyItem();
		
		if(itemEquipable.model == null)
			passed = false;
			
		return passed;
	}
	
	private bool VerifyWeapon() {
		return VerifyEquipable();
	}
	
	private bool VerifyPower() {
		bool passed = VerifyItem();
		
		if(itemPower.time == 0)
			passed = false;
		
		return passed;
	}
	
	private bool VerifySpecial() {
		return VerifyItem();
	}
	
	private void Reset() {
		if(itemManager != null)
			itemManager.Repaint();
	
		addedName = item.itemName;
		added = true;
	
		item = new Item();
		itemEquipable = new ItemEquipable();
		itemPower = new ItemPower();
		itemSpecial = new ItemSpecial();
		itemWeapon = new ItemWeapon();
	
		itemEquipable.stats = new ItemEquipable.ItemEquipableStats();
		itemWeapon.stats = new ItemEquipable.ItemEquipableStats();
	}
	
	public void SetItemManager(ItemManager itemManager) {
		this.itemManager = itemManager;
	}
}