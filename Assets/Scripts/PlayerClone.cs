using UnityEngine;
using System.Collections;

public class PlayerClone : MonoBehaviour
{
	private static string tintColor = "_TintColor";

	public Vector3 Position
	{
		get { return transform.position; }
	}

	private new Transform transform;
	private new Light light;
	private Material mat;

	private void Awake()
	{
		transform = GetComponent<Transform>();
		light = GetComponent<Light>();
		mat = GetComponent<Renderer>().material;
	}

	public void SetLightColor(Color color)
	{
		light.color = color;
	}

	public void SetMaterialColor(Color color)
	{
		mat.SetColor(tintColor, color);
	}
}
