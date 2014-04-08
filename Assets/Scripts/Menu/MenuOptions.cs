using UnityEngine;
using System.Collections;

public class MenuOptions:MonoBehaviour, IMenu {
	[SerializeField] private Transform cameraTransform;

	private MenuCamera menuCamera;

	void Start() {
		menuCamera = GetComponent<MenuCamera>();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Q))
			menuCamera.GotoMenu(this);
	}

	public void Open() {

	}

	public void Close() {

	}

	public Transform GetCameraTransform() {
		return cameraTransform;
	}
}
