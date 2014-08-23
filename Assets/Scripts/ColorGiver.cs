using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class ColorGiver : MonoBehaviour
{
	private static string tintColor = "_TintColor";

	private Color32 baseColor;
	private Color32 currentColor;

	private Color ratios;
	private Material mat;

	private void Awake()
	{
		mat = renderer.material;
		baseColor = light.color;
		currentColor = baseColor;

		SetupRatios(mat.GetColor(tintColor));
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (currentColor != Color.clear && other.CompareTag("Player"))
		{
			var player = other.GetComponent<PlayerController>();
			currentColor = player.GiveColor(currentColor);

			if (currentColor == Color.clear)
			{
				Destroy(this.gameObject);
			}
			else
			{
				ChangeMaterialColor(currentColor);
			}
		}
	}

	private void ChangeMaterialColor(Color32 color)
	{
		float currentMax = GetLargestChannel(color) / 255f;

		Color c;
		c.r = ratios.r * currentMax;
		c.g = ratios.g * currentMax;
		c.b = ratios.b * currentMax;
		c.a = ratios.a * currentMax;

		mat.SetColor(tintColor, c);
	}

	private void SetupRatios(Color32 color)
	{
		float max = GetLargestChannel(color);

		ratios = new Color(color.r / max, color.g / max, color.b / max, color.a / max);
	}

	private int GetLargestChannel(Color32 color)
	{
		var list = new List<int>();
		list.Add(color.r);
		list.Add(color.g);
		list.Add(color.b);
		list.Add(color.a);

		return list.OrderByDescending(x => x).First();
	}
}