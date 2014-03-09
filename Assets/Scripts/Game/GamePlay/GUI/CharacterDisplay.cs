using UnityEngine;
using System.Collections;

public class CharacterDisplay : GUIBehaviour {
	[SerializeField]private Texture[] iconHelmet;
	[SerializeField]private Texture[] iconChest;
	[SerializeField]private Texture[] iconLegs;
	[SerializeField]private Texture[] iconSword;
	[SerializeField]private Texture[] iconShield;
	[SerializeField]private Texture[] iconQuality;
	[SerializeField]private Texture[] statsIcons;

	[SerializeField]private Texture background;
	[SerializeField]private Texture playerBackground;
	private bool windowOpen;
	private bool isonCharacter = true;
	private int slidePosition;
	[SerializeField]private int depth;

	[SerializeField]private GUIStyle textStyle;
	[SerializeField]private GUIStyle statsStyle;

	private int equipmentNum;
	private Object[] currentEquipment;

	private ItemEquipable[] selectedItems = new ItemEquipable[2];
	[SerializeField]private Texture[] selectedTextures;
	// Use this for initialization
	void Start () {
	
	}
	void FixedUpdate(){
		if(isonCharacter){
			if(slidePosition < 0){
				slidePosition += 16;
			}else{
				slidePosition = 0;
			}
		}else{
			if(slidePosition > -1700){
				slidePosition -= 16;
			}else{
				slidePosition = -1700;
			}
		}
	}
	void OnGUI(){
		GUI.depth = -1;
		base.OnGUI();
		if(GUI.Button(new Rect(0,600,400,400),"Menu")){
			if(windowOpen){
				windowOpen = false;
			}else{
				windowOpen = true;
			}
		}

		if(windowOpen){
			DrawCharacterWindow();
		}
	}

	void DrawCharacterWindow(){
		//Character Window
		GUI.BeginGroup(new Rect(slidePosition,0,1920,1080));
			GUI.DrawTexture(new Rect(0,0,1920,1080),background);

			GUI.BeginGroup(new Rect(30,100,900,1200));
			//Character Preview Window
				GUI.DrawTexture(new Rect(0,0,600,900),playerBackground);	
					DrawSelectionButton(background,new Vector2(600,0),0);
					DrawSelectionButton(background,new Vector2(600,180),1);	
					DrawSelectionButton(background,new Vector2(600,360),2);
					DrawSelectionButton(background,new Vector2(600,540),3);	
					DrawSelectionButton(background,new Vector2(600,720),4);	
				GUI.EndGroup();
			
			ItemWindow(new Vector2(850,100),0);
			//ItemWindow(new Vector2(850,600),1);
			
			if(GUI.Button(new Rect(1700,200,220,900),background)){
				if(isonCharacter){
				isonCharacter = false;
				}else{
				isonCharacter = true;
				}
			}
			if(GUI.Button(new Rect(1700,0,220,200),background)){
			windowOpen = false;
			}
			GUI.EndGroup();
	}
	void DrawSelectionButton(Texture icon,Vector2 pos,int num){
		if(GUI.Button(new Rect(pos.x,pos.y,150,180),icon)){
				equipmentNum = num;
			selectedItems[0] = Inventory.Instance.GetEquipable((Inventory.EquipableType)equipmentNum,0);
			selectedItems[1] = Inventory.Instance.GetEquipable((Inventory.EquipableType)equipmentNum,1);
		}
	}

	void ItemWindow(Vector2 pos,int num){
		GUI.BeginGroup(new Rect(pos.x,pos.y,700,400));
			GUI.DrawTexture(new Rect(0,0,700,400),background);

			if(selectedItems[0] !=null){
			ItemProperties(0);
			}
		GUI.EndGroup();
	}

	void ItemProperties(int arraynum){
			textStyle.fontSize = 80;
			GUI.Label(new Rect(200,5,300,100),"" + selectedItems[0].data.itemName,textStyle);	
			textStyle.fontSize = 60;
			textStyle.wordWrap = true;
			GUI.Label(new Rect(10,100,680,100),"*" + selectedItems[0].data.itemDescription,textStyle);	
			DrawItemIcon(new Vector2(0,250),0);
			DrawStatsIcon(new Vector2(150,250),selectedItems[0].data.stats.damage,0);
			DrawStatsIcon(new Vector2(300,250),selectedItems[0].data.stats.defence,1);
			textStyle.wordWrap = false;
	}
	void DrawStatsIcon(Vector2 pos,int displayValue,int textureNum){
		GUI.DrawTexture(new Rect(pos.x,pos.y,150,150),statsIcons[textureNum]);
		GUI.Label(new Rect(pos.x,pos.y,150,150),""+displayValue,statsStyle);
	}
	void DrawItemIcon(Vector2 pos,int selectednum){
		Texture textr = GetItemIcon(selectedItems[selectednum].data.equipableType,(int)selectedItems[selectednum].data.element);
		GUI.DrawTexture(new Rect(pos.x,pos.y,150,150),iconQuality[(int)selectedItems[selectednum].data.rarity]);
		GUI.DrawTexture(new Rect(pos.x,pos.y,150,150),textr);
	}
	Texture GetItemIcon(EquipableData.EquipableType type, int index) {
		Debug.Log(index);
		switch(type) {
		case EquipableData.EquipableType.Helmet:
			return iconHelmet[index];
		case EquipableData.EquipableType.Chest:
			return iconChest[index];
		case EquipableData.EquipableType.Legs:
			return iconLegs[index];
		case EquipableData.EquipableType.Shield:
			return iconShield[index];
		default:
			return null;
		}
	}
}
