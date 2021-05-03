using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
	[SerializeField]
	Transform leftCorner;
	[SerializeField]
	Transform rightCorner;

	public bool isGame;//start on mouse click
	void OnValidate()
	{
		if (leftCorner == null)
			Debug.LogError("leftCorner bad value");
		if (rightCorner == null)
			Debug.LogError("rightCorner bad value");
	}

	void Update()
	{
		if (isGame)
		{
			//translate screen mouse position to game scene mouse position
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
						Input.mousePosition.y, Vector3.Distance(Camera.main.transform.position, transform.position)));

			//cutting and normalizing player pos
			Vector3 pos = transform.position;
			float x = Mathf.Clamp(mousePos.x, leftCorner.position.x + (transform.localScale.x / 2),
				rightCorner.position.x - (transform.localScale.x / 2));
			transform.position = new Vector3(x, pos.y, pos.z);
		}
	}
}
