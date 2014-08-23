using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Light))]
public class ColorTaker : MonoBehaviour
{
	private static string tintColor = "_TintColor";

	private Color32 baseColor;
	private Color32 currentColor;

	private Material mat;
	private float maxRange;

	private void Awake()
	{
		mat = renderer.material;
		baseColor = light.color;
		currentColor = baseColor;

		maxRange = light.range;
		light.range = 0f;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			var player = other.GetComponent<PlayerController>();
			currentColor = player.TakeColor(currentColor);

			float currentMax = GetLargestChannel(currentColor) / 255f;
			light.color = currentColor;
			light.range = Mathf.Pow(maxRange, currentMax);

			ChangeMaterialColor(currentMax);
		}
	}

	private void ChangeMaterialColor(float largestChannel)
	{
		Color c = currentColor;
		c.a = largestChannel / 2f;

		mat.SetColor(tintColor, c);
	}

	private int GetLargestChannel(Color32 color)
	{
		var list = new List<int>();
		list.Add(color.r);
		list.Add(color.g);
		list.Add(color.b);

		return list.OrderByDescending(x => x).First();
	}
}
