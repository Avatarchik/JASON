using UnityEngine;
using System.Collections;

public class MenuMain:MonoBehaviour, IMenu {
	[SerializeField] private Transform cameraTransform;

	private MenuCamera menuCamera;

	void Start() {
		menuCamera = GetComponent<MenuCamera>();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.W))
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
