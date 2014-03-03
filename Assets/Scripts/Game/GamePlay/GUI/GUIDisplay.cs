using UnityEngine;
using System;
using System.Collections;

public class GUIDisplay:MonoBehaviour {
	[SerializeField] private Texture healthBorder;
	[SerializeField] private Texture healthBar;
	[SerializeField] private Texture damageOverlay;
	[SerializeField] private Texture attackIndicator;
	[SerializeField] private Player player;

	private bool isOnDelay;
	private bool showing;

	private int timershit;
	private int barLength;

	private Enemy selectedEnemy;
	void FixedUpdate(){
		selectedEnemy = player.playerCombat.currentEnemy;
		barLength = 300 / player.data.maxHealth * player.data.health;
		if(player.playerCombat.attacking){
			timershit -= 10;
		}else{
			timershit = 300;
		}

	}
	void OnGUI(){
		if(selectedEnemy != null){
			DrawEnemyHealthDisplay();
		}
		DrawPlayerHealthDisplay();
	}
	void DrawEnemyHealthDisplay(){
		GUI.BeginGroup(new Rect(400,0,300,25));
			GUI.DrawTexture(new Rect(0,0,300 / selectedEnemy.data.maxHealth * selectedEnemy.data.health,25),healthBar);
			GUI.DrawTexture(new Rect(0,0,300,25),healthBorder);
		//if(player.isHit){
		//	GUI.DrawTexture(new Rect(0,0,barLength,25),damageOverlay);
		//}
		//GUI.DrawTexture(new Rect(0,0,timershit,25),attackIndicator);
		GUI.EndGroup();
	}
	void DrawPlayerHealthDisplay(){
		GUI.BeginGroup(new Rect(0,0,300,25));
			GUI.DrawTexture(new Rect(0,0,barLength,25),healthBar);
			GUI.DrawTexture(new Rect(0,0,300,25),healthBorder);
			if(player.isHit){
				GUI.DrawTexture(new Rect(0,0,barLength,25),damageOverlay);
			}
			GUI.DrawTexture(new Rect(0,0,timershit,25),attackIndicator);
		GUI.EndGroup();
	}
}