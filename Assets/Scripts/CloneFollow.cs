using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneFollow : MonoBehaviour
{
	[SerializeField]
	private Transform target;
	[SerializeField]
	private bool targetIsRigidbody = true;

	private new Transform transform;

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
		if (!targetIsRigidbody)
		{
			FollowTarget();
		}
	}

	private void FixedUpdate()
	{
		if (targetIsRigidbody)
		{
			FollowTarget();
		}
	}

	private void FollowTarget()
	{
		transform.position = target.position;
	}
}