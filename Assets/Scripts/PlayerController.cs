using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private new Light light;
	[SerializeField]
	private Color32 color = new Color32(255, 255, 255, 0);
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

		light.color = color;
	}

	private void Update()
	{
		keyInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		mouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse ScrollWheel"));

		Rotate();

		if (Input.GetKey(KeyCode.Alpha1))
		{
			color = Color.clear;
			light.color = color;
		}

		if (Input.GetKey(KeyCode.Alpha2))
		{
			color = Color.white;
			light.color = color;
		}
	}

	private void FixedUpdate()
	{
		rigidbody2D.velocity = transform.up * keyInput.y * moveSpeed;
	}

	public Color32 GiveColor(Color32 c)
	{
		if (color != Color.white)
		{
			if (c.r > 0 && color.r < 255)
			{
				color.r += 1;
				c.r -= 1;
			}

			if (c.b > 0 && color.b < 255)
			{
				color.b += 1;
				c.b -= 1;
			}

			if (c.g > 0 && color.g < 255)
			{
				color.g += 1;
				c.b -= 1;
			}

			light.color = color;
		}

		return c;
	}

	public Color32 TakeColor(Color32 c)
	{
		if (color != Color.clear)
		{
			if (c.r < 255 && color.r > 0)
			{
				color.r -= 1;
				c.r += 1;
			}

			if (c.b < 255 && color.b > 0)
			{
				color.b -= 1;
				c.b += 1;
			}

			if (c.g < 255 && color.g > 0)
			{
				color.g -= 1;
				c.g += 1;
			}

			light.color = color;
		}

		return c;
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