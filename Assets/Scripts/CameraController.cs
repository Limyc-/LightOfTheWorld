using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private Transform target;
	[SerializeField]
	private float cameraDistance = 10f;
	[SerializeField]
	private float dampTime = 0.15f;
	[SerializeField]
	private bool targetIsRigidbody = true;

	private Transform Main;
	public Transform Top;
	public Transform Bottom;
	public Transform Left;
	public Transform Right;

	private new Transform transform;

	private Vector3 velocity = Vector3.zero;
	private float averageDeltaTime;
	private float lastframe = 0f;
	private float currentframe = 0f;
	private float delta = 0f;

	private void Awake()
	{
		transform = GetComponent<Transform>();
		Main = Camera.main.transform;
	}

	private void Start()
	{
		var pos = target.position;
		pos.z = -cameraDistance;
		transform.position = pos;
	}

	private void Update()
	{
		//calc my delta time
		currentframe = Time.realtimeSinceStartup;
		delta = currentframe - lastframe;
		lastframe = currentframe;
	}

	private void FixedUpdate()
	{
		if (targetIsRigidbody)
		{
			FollowTarget();
		}

	}

	private void LateUpdate()
	{
		if (!targetIsRigidbody)
		{
			FollowTarget();
		}

		RotateCameras(target.rotation);
	}

	private void RotateCameras(Quaternion rot)
	{
		Main.rotation = rot;
		Top.rotation = rot;
		Bottom.rotation = rot;
		Left.rotation = rot;
		Right.rotation = rot;
	}

	private void FollowTarget()
	{
		averageDeltaTime = (Time.deltaTime + Time.smoothDeltaTime + delta) * 0.33333f;

		if (target != null)
		{
			var destination = target.position;
			destination.z = -cameraDistance;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime, Mathf.Infinity, averageDeltaTime);
		}
	}

	public void SetPosition(string direction)
	{
		switch (direction)
		{
			case "Top":
				transform.position = Top.position;
				break;
			case "Bottom":
				transform.position = Bottom.position;
				break;
			case "Left":
				transform.position = Left.position;
				break;
			case "Right":
				transform.position = Right.position;
				break;
			default:
				break;
		}

		Debug.Log("Swap " + direction);
	}

}