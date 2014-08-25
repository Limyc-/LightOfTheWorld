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
	private float maxRange;

	public Color32 CurrenColor
	{
		get { return currentColor; }
	}
	public int Max { get; private set; }

	private void Awake()
	{
		mat = renderer.material;
		baseColor = light.color;
		currentColor = baseColor;
		maxRange = light.range;

		SetupRatios(mat.GetColor(tintColor));
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			var player = other.GetComponent<PlayerController>();
			currentColor = player.GiveColor(currentColor);

			if (currentColor == Color.clear)
			{
				Destroy(this.gameObject);
			}
			else
			{
				float currentMax = GetLargestChannel(currentColor) / 255f;
				light.color = currentColor;
				light.range = Mathf.Pow(maxRange, currentMax);
				ChangeMaterialColor(currentMax);
			}
		}
	}

	private void ChangeMaterialColor(float largestChannel)
	{
		Color c;
		c.r = ratios.r * largestChannel;
		c.g = ratios.g * largestChannel;
		c.b = ratios.b * largestChannel;
		c.a = largestChannel / 2f;

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

		Max = list.OrderByDescending(x => x).First();
		return Max;
	}
}