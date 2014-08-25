using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Transform player;
	public NpcSpawner spawnerPrefab;
	public Camera mainCamera;
	public ColorGiver[] stars = new ColorGiver[3];
	public ColorTaker sun;

	private Transform cameraTransform;
	private bool hasEnded = false;
	private float start;

	private void Awake()
	{
		cameraTransform = mainCamera.transform;
	}

	public void Update()
	{
		if (hasEnded)
		{
			float d = (Time.time - start) * 0.1f;
			float frac = d / 24;
			mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 24, frac);
			cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, Quaternion.LookRotation(Vector3.forward, Vector3.up), frac);
		}
	}

	public void EndGame()
	{
		NpcSpawner spawner;
		var pos = Vector3.zero;
		var rot = Quaternion.identity;

		pos.z = 20f;
		spawner = Instantiate(spawnerPrefab, pos, rot) as NpcSpawner;
		spawner.Init(0.6f);

		pos.z = 30f;
		spawner = Instantiate(spawnerPrefab, pos, rot) as NpcSpawner;
		spawner.Init(0.25f);

		pos.z = 40f;
		spawner = Instantiate(spawnerPrefab, pos, rot) as NpcSpawner;
		spawner.Init(0.1f);

		hasEnded = true;
		start = Time.time;
	}

	public bool SunIsLit()
	{
		bool isLit = true;

		foreach (var star in stars)
		{
			if (star.Max >= 255)
			{
				isLit = false;
			}
		}

		if (isLit)
		{
			var c = sun.CurrenColor;

			if ((c.r + c.g + c.b) / 3f < 128f)
			{
				isLit = false;
			}
		}

		return isLit;
	}
}