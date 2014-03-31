using UnityEngine;
using System.Collections;

public class TutorialManager:MonoBehaviour {
	public enum Tutorial {
		Basic
	}

	[SerializeField] private Tutorial tutorial;

	void Start() {
		BasicTutorial.Instance.StartTutorial();
	}
}
