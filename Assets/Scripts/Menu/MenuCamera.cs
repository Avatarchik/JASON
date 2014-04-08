using UnityEngine;
using System.Collections;

public class MenuCamera:MonoBehaviour {
	[SerializeField] private float startTransitionDuration;
	[SerializeField] private float transitionDuration;

	[SerializeField] private Component[] menuComponents;

	private IMenu[] menus;

	private IMenu currentMenu;
	private IMenu targetMenu;

	private Vector3 startPosition;
	private Quaternion startRotation;

	private float startTime;

	private bool firstOpen = true;

	void Start() {
		menus = new IMenu[menuComponents.Length];

		for(int i = 0; i < menuComponents.Length; i++)
			menus[i] = menuComponents[i] as IMenu;

		GotoMenu(menus[0]);
	}
	
	void Update() {
		if(currentMenu != targetMenu) {
			float transition = firstOpen ? startTransitionDuration : transitionDuration;

			transform.position = Vector3.Lerp(startPosition, targetMenu.GetCameraTransform().position, (Time.time - startTime) / transition);
			transform.rotation = Quaternion.Lerp(startRotation, targetMenu.GetCameraTransform().rotation, (Time.time - startTime) / transition);
			
			if(transform.position.Equals(targetMenu.GetCameraTransform().position) && transform.rotation.Equals(targetMenu.GetCameraTransform().rotation)) {
				currentMenu = targetMenu;
				firstOpen = false;

				currentMenu.Open();
			}
		}
	}

	/** <summary>Go to the specified menu</summary> 
	 * <param name="menu">The menu to go to</param> */
	public void GotoMenu(IMenu menu) {
		if(currentMenu != null)
			currentMenu.Close();

		targetMenu = menu;

		startTime = Time.time;

		startPosition = transform.position;
		startRotation = transform.rotation;
	}
}
