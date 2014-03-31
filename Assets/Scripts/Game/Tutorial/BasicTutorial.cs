using UnityEngine;
using System.Collections;
using SGUI;

public class BasicTutorial:Singleton<BasicTutorial> {
	[SerializeField] private SGUITexture tooltip;

	[SerializeField] private SGUISlowWriteLabel[] labels;

	private int stage = -1;

	void Start() {
		StartCoroutine(WaitForGlobalManager());
	}

	void Update() {
		
	}

	public void StartStage(int stage) {
		if(this.stage >= 0)
			labels[this.stage].Activated = false;

		tooltip.Activated = true;
		labels[stage].Activated = true;

		StartCoroutine(labels[stage].Write());

		this.stage = stage;
	}

	/** Wait until the Global Manager has been loaded */
	private IEnumerator WaitForGlobalManager() {
		while(GameObject.FindGameObjectWithTag("Global Manager") == null)
			yield return new WaitForSeconds(0.3f);

		tooltip.Create();

		foreach(SGUILabel label in labels)
			label.Create();
	}
}
