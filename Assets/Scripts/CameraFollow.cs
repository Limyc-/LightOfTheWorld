using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	private Transform target;
	[SerializeField]
	private float cameraDistance = 10f;
	[SerializeField]
	private float dampTime = 0.15f;
	[SerializeField]
	private bool targetIsRigidbody = true;

	private new Transform transform;

	private Vector3 velocity = Vector3.zero;
	private float averageDeltaTime;
	private float lastframe = 0f;
	private float currentframe = 0f;
	private float delta = 0f;
	private void Awake()
	{
		transform = GetComponent<Transform>();
	}

	private void Start()
	{
		transform.position = target.position;
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

		transform.rotation = target.rotation;
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

}