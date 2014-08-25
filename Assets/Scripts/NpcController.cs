using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
	[SerializeField]
	private float maxSpeed = 2f;

	private new Transform transform;
	private new Rigidbody2D rigidbody2D;
	private float speed;
	//private Color32 color;

	public Rect Bounds
	{
		get;
		set;
	}

	private void Awake()
	{
		transform = GetComponent<Transform>();
		rigidbody2D = GetComponent<Rigidbody2D>();

		renderer.material.SetColor("_Diffuse", Colors.RandomColor());
		speed = Random.Range(1f, maxSpeed);
	}

	private void FixedUpdate()
	{
		//ChangeColor(color);
		var p = transform.position + transform.up * speed * Time.deltaTime;

		if (p.x > Bounds.xMax)
		{
			p.x -= Bounds.width;
		}
		else if (p.x < Bounds.xMin)
		{
			p.x += Bounds.width;
		}

		if (p.y > Bounds.yMax)
		{
			p.y -= Bounds.height;
		}
		else if (p.y < Bounds.yMin)
		{
			p.y += Bounds.height;
		}

		rigidbody2D.MovePosition(p);
	}
}