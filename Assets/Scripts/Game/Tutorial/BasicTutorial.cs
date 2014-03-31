using UnityEngine;
using System.Collections;
using SGUI;

public class BasicTutorial:Singleton<BasicTutorial>, Tutorial {
	public enum TutorialStage {
		None = 0,
		Movement = 1,
		BlockPickup,
		BlockPath,
		BlockDrop
	};

	[SerializeField] private SGUITexture tooltip;

	[SerializeField] private SGUISlowWriteLabel[] labels;

	private TutorialStage stage = TutorialStage.None;

	private bool started;

	void Start() {
		tooltip.Create();

		foreach(SGUISlowWriteLabel label in labels)
			label.Create();
	}

	public void StartTutorial() {
		stage = 0;

		started = true;

		StartStage();
	}

	public void StartStage() {
		tooltip.Activated = true;
		labels[(int)stage].Activated = true;

		StartCoroutine(labels[(int)stage].Write());
	}

	public void NextStage() {
		labels[(int)stage].Activated = false;

		stage++;
	}

	public void StopTutorial() {
		tooltip.Activated = false;
		labels[(int)stage].Activated = false;

		started = false;
	}

	public IEnumerator NextStageOnFinish() {
		while(!labels[(int)stage].FinishedWriting)
			yield return new WaitForSeconds(0.01f);

		NextStage();
		StartStage();
	}

	public TutorialStage Stage {
		get { return stage; }
	}

	public bool Started {
		get { return started; }
	}
}
