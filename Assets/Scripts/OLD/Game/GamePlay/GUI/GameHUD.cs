using UnityEngine;
using System;
using System.Collections;
using SGUI;

public class GameHUD:GUIBehaviour {
	private enum Bars {
		Health = 0,
		Damage = 1,
		Shield = 2
	}

	[SerializeField] private SGUITexture[] innerBars;
	[SerializeField] private SGUITexture outerBar;

	private Old_Player player;

	private Bars activeBar;

	private Rect originalBarWidth;

	private int hitTimer;

	private bool playerInstanceFound;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Old_Player>();

		if(player == null)
			throw new System.NullReferenceException("No Game Object found in the scene with the 'Player' tag");

		originalBarWidth = innerBars[0].Bounds;

		outerBar.Create();

		foreach(SGUITexture texture in innerBars)
			texture.Create();
	}
	
	void Update() {
		if(player.Hit) {
			SwitchBar(Bars.Damage);
		} else if(player.PlayerCombat.Defending) {
			SwitchBar(Bars.Shield);
		} else {
			SwitchBar(Bars.Health);
		}

		Rect bounds = originalBarWidth;
		bounds.width = bounds.width / PlayerData.Instance.InitialHealth * PlayerData.Instance.Health;

		innerBars[(int)activeBar].Bounds = bounds;

		if(player.PlayerCombat.Attacking) {
			hitTimer -= 10;
		} else {
			hitTimer = 300;
		}
	}

	/** Switch the active inner bar texture */
	private void SwitchBar(Bars newBar) {
		if(activeBar == newBar)
			return;

		innerBars[(int)activeBar].Activated = false;
		innerBars[(int)newBar].Activated = true;

		activeBar = newBar;
	}

	/** Get the outer bar SGUI Texture */
	public SGUITexture Outerbar {
		get { return outerBar; }
	}

	/** Get the inner bar SGUI Texture */
	public SGUITexture[] Innerbars {
		get { return innerBars; }
	}
}