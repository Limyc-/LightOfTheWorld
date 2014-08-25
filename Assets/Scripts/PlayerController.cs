using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	private static string tintColor = "_TintColor";

	[SerializeField]
	private new Renderer renderer;
	[SerializeField]
	private new Light light;
	[SerializeField]
	private Color32 color = new Color32(255, 255, 255, 0);
	[SerializeField]
	private float moveSpeed = 7f;
	[SerializeField]
	private float rotationSpeed = 1f;
	[SerializeField]
	private CloneController cloneController;
	[SerializeField]
	private GameManager gameManager;

	private new Transform transform;
	private new Rigidbody2D rigidbody2D;
	private Material mat;
	private Vector2 keyInput;
	private Vector3 mouseInput;
	private bool isRunning = true;
	private Color32 baseColor = new Color32(128, 128, 128, 128);

	public Light Light
	{
		get { return light; }
	}


	private void Awake()
	{
		transform = GetComponent<Transform>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		mat = renderer.material;

		light.color = color;
	}

	private void Start()
	{
		ChangeMaterialColor(Color.clear);
	}

	private void Update()
	{
		if (isRunning)
		{
			if (light.color == Color.clear && gameManager.SunIsLit())
			{
				gameManager.EndGame();
				isRunning = false;
			}
			else
			{
				keyInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
				mouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse ScrollWheel"));

				if (Input.GetKey(KeyCode.Alpha1))
				{
					color = Color.clear;
					SetColors();
				}

				if (Input.GetKey(KeyCode.Alpha2))
				{
					color = Color.white;
					SetColors();
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

	}

	private void FixedUpdate()
	{
		rigidbody2D.velocity = transform.up * keyInput.y * moveSpeed;
		Rotate();
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
				c.g -= 1;
			}

			SetColors();
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

			SetColors();
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

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Border"))
		{
			cloneController.SwapPositions(other.transform.position, rigidbody2D.velocity);
		}
	}

	private void SetColors()
	{
		light.color = color;
		ChangeMaterialColor(color);
		cloneController.ChangeLight(color);
	}

	private void ChangeMaterialColor(Color color)
	{
		Color c = baseColor;
		c.r += color.r / 2f;
		c.g += color.g / 2f;
		c.b += color.b / 2f;

		mat.SetColor(tintColor, c);
	}
}