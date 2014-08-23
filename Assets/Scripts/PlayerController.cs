using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private new Light light;
	[SerializeField]
	private float moveSpeed = 7f;
	[SerializeField]
	private float rotationSpeed = 1f;

	private new Transform transform;
	private new Rigidbody2D rigidbody2D;


	private Vector2 keyInput;
	private Vector3 mouseInput;

	private void Awake()
	{
		transform = GetComponent<Transform>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		light = GetComponent<Light>();
	}

	private void Update()
	{
		keyInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		mouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse ScrollWheel"));

		Rotate();
	}

	private void FixedUpdate()
	{
		rigidbody2D.velocity = transform.up * keyInput.y * moveSpeed;
	}

	private void Rotate()
	{
		if (keyInput.x != 0f)
		{
			var rot = Quaternion.LookRotation(transform.forward, transform.right * keyInput.x);
			transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
		}
	}
}