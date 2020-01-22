using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FreeCameraController : MonoBehaviour
{
    public float zoomSpeed = 5;
    public float cameraHeight = 36;
    public float moveSpeed = 5;
    public GameObject target;
    private Camera controlledCamera;

	[Range(0, 10)]
	Vector2 f0start = new Vector2();
	Vector2 f1start = new Vector2();

	const float MobileSensivity = 0.2f;
	void Start()
    {
		controlledCamera = Camera.main;
    }

	// Update is called once per frame
	void Update()
	{
	//d	Zoom();
		OtherInputs();
		MoveCamera();
	}

	private void OtherInputs()
	{
		if (Input.GetKeyDown(KeyCode.R))
			ResetCamera();
	}

	private void Zoom()
	{
		controlledCamera.orthographicSize -= Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
		ZoomFromMobile();
		if (controlledCamera.orthographicSize < 5)
			controlledCamera.orthographicSize = 5;
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

	private void MoveCamera()
	{
		MoveFromMobile();
		if (Input.GetKey(KeyCode.W))
			FreeAndMoveCamera(-transform.forward);

		if (Input.GetKey(KeyCode.S))
			FreeAndMoveCamera(transform.forward);

		if (Input.GetKey(KeyCode.A))
			FreeAndMoveCamera(-transform.right);

		if (Input.GetKey(KeyCode.D))
			FreeAndMoveCamera(transform.right);
	}

	private void MoveFromMobile()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
				return;
		}

		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
			FreeAndMoveCamera(-Input.GetTouch(0).deltaPosition * MobileSensivity);
	}

	private void FreeAndMoveCamera(Vector3 direction)
	{
		controlledCamera.transform.parent = null;
		controlledCamera.transform.Translate(direction * moveSpeed * Time.deltaTime);
	}

	public void ResetCamera()
    {
        controlledCamera.transform.parent = target.transform;
		controlledCamera.orthographicSize = 80;
        controlledCamera.transform.position = 
			new Vector3(target.transform.position.x, cameraHeight, target.transform.position.z);
    }
}
