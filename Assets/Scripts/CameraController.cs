using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Transform cameraSpot;
	[SerializeField] private float smooth;

	[Header("Зум")]
	[SerializeField]
	float zoomSpeed;
	[SerializeField]
	float zoomInOutValue = 1;
	[SerializeField]
	Camera controlledCamera;
	Vector2 f0start = new Vector2();
	Vector2 f1start = new Vector2();
	private void LateUpdate()
	{
		ZoomFromMobile();
		controlledCamera.orthographicSize = Mathf.Clamp(controlledCamera.orthographicSize, 0.5f, 50);
		var target = cameraSpot.position;
		target.z = transform.position.z;
		transform.position = Vector3.Lerp(transform.position, target, smooth * Time.deltaTime);
	}

	public void ZoomIn()
	{
		controlledCamera.orthographicSize -= zoomInOutValue;
	}

	public void ZoomOut()
	{
		controlledCamera.orthographicSize += zoomInOutValue;
	}

	private void ZoomFromMobile()
	{
		if (Input.touchCount < 2)
		{
			f0start = Vector2.zero;
			f1start = Vector2.zero;
			return;
		}

		if (Input.touchCount == 2 && f0start == Vector2.zero && f1start == Vector2.zero)
		{
			f0start = Input.GetTouch(0).position;
			f1start = Input.GetTouch(1).position;
		}

		Vector2 f0position = Input.GetTouch(0).position;
		Vector2 f1position = Input.GetTouch(1).position;

		float dir = Math.Sign(
			Vector2.Distance(f1start, f0start) - Vector2.Distance(f0position, f1position));
		controlledCamera.orthographicSize += dir * Time.deltaTime * zoomSpeed;
	}
}