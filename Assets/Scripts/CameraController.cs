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

	[SerializeField]
	private Transform top;
	[SerializeField]
	private Transform bottom;
	[SerializeField]
	private Transform left;
	[SerializeField]
	private Transform right;
	[SerializeField]
	private Transform topLeft;
	[SerializeField]
	private Transform bottomLeft;
	[SerializeField]
	private Transform topRight;
	[SerializeField]
	private Transform bottomRight;

	private new Transform transform;
	private new Rigidbody2D rigidbody2D;
	private Transform mainCamera;
	private Vector3 velocity = Vector3.zero;
	private float averageDeltaTime;
	private float lastframe = 0f;
	private float currentframe = 0f;
	private float delta = 0f;

	private void Awake()
	{
		transform = GetComponent<Transform>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		mainCamera = Camera.main.transform;
	}

	private void Start()
	{
		var pos = target.position;
		pos.z = -cameraDistance;
		rigidbody2D.MovePosition(pos);
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
		mainCamera.rotation = rot;
		top.rotation = rot;
		bottom.rotation = rot;
		left.rotation = rot;
		right.rotation = rot;
	}

	private void FollowTarget()
	{
		averageDeltaTime = (Time.deltaTime + Time.smoothDeltaTime + delta) * 0.33333f;

		if (target != null)
		{
			var destination = target.position;
			destination.z = -cameraDistance;
			var pos = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime, Mathf.Infinity, averageDeltaTime);
			transform.position = pos;
		}
	}

	public void SetPosition(string direction)
	{
		Vector3 pos = transform.position;

		switch (direction)
		{
			case "Top":
				pos = top.position;
				break;
			case "Bottom":
				pos = bottom.position;
				break;
			case "Left":
				pos = left.position;
				break;
			case "Right":
				pos = right.position;
				break;
			default:
				Debug.LogError("Could not swap with camera '" + direction + "'");
				break;
		}

		transform.position = pos;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Border"))
		{
			Debug.Log("SetCameraState = true");
			SetCameraState(other.transform.position, true);

		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Border"))
		{
			Debug.Log("SetCameraState = false");
			SetCameraState(other.transform.position, false);
		}
	}

	private void SetCameraState(Vector3 pos, bool isEnabled)
	{
		if (pos.y == 0f)
		{
			if (pos.x > 0f)
			{
				left.gameObject.SetActive(isEnabled);

				if (isEnabled)
				{
					if (bottom.gameObject.activeInHierarchy)
					{
						bottomLeft.gameObject.SetActive(isEnabled);
					}
					else if (top.gameObject.activeInHierarchy)
					{
						topLeft.gameObject.SetActive(isEnabled);
					}
				}
				else
				{
					bottomLeft.gameObject.SetActive(isEnabled);
					topLeft.gameObject.SetActive(isEnabled);
				}
			}
			else if (pos.x < 0f)
			{
				right.gameObject.SetActive(isEnabled);

				if (isEnabled)
				{
					if (bottom.gameObject.activeInHierarchy)
					{
						bottomRight.gameObject.SetActive(isEnabled);
					}
					else if (top.gameObject.activeInHierarchy)
					{
						topRight.gameObject.SetActive(isEnabled);
					}
				}
				else
				{
					bottomRight.gameObject.SetActive(isEnabled);
					topRight.gameObject.SetActive(isEnabled);
				}
			}
		}
		else
		{
			if (pos.y > 0f)
			{
				bottom.gameObject.SetActive(isEnabled);

				if (isEnabled)
				{
					if (left.gameObject.activeInHierarchy)
					{
						bottomLeft.gameObject.SetActive(isEnabled);
					}
					else if (right.gameObject.activeInHierarchy)
					{
						bottomRight.gameObject.SetActive(isEnabled);
					}
				}
				else
				{
					bottomLeft.gameObject.SetActive(isEnabled);
					bottomRight.gameObject.SetActive(isEnabled);
				}
			}
			else if (pos.y < 0f)
			{
				top.gameObject.SetActive(isEnabled);

				if (isEnabled)
				{
					if (left.gameObject.activeInHierarchy)
					{
						topLeft.gameObject.SetActive(isEnabled);
					}
					else if (right.gameObject.activeInHierarchy)
					{
						topRight.gameObject.SetActive(isEnabled);
					}
				}
				else
				{
					bottomLeft.gameObject.SetActive(isEnabled);
					bottomRight.gameObject.SetActive(isEnabled);
				}
			}
		}
	}

}