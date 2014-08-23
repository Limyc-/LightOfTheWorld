using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class ColorTaker : MonoBehaviour
{
	private Color32 color;

	private void Awake()
	{
		color = light.color;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			var player = other.GetComponent<PlayerController>();
			player.TakeColor(color);
		}
	}
}
