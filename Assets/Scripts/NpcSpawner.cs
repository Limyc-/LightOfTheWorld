using UnityEngine;
using System.Collections;

public class NpcSpawner : MonoBehaviour
{
	[SerializeField]
	private NpcController npcPrefab;
	[SerializeField]
	private float spawnScale = 1f;
	[SerializeField]
	private float totalSpawns = 300f;
	[SerializeField]
	private Vector2 levelDimensions;

	private new Transform transform;
	private Rect bounds;


	// Use this for initialization
	void Awake()
	{
		transform = GetComponent<Transform>();

		var x = levelDimensions.x;
		var y = levelDimensions.y;
		bounds = new Rect(-(x / 2f), -(y / 2f), x, y);
	}

	public void Init(float scale)
	{
		this.spawnScale = scale;
	}

	void Start()
	{
		for (int i = 0; i < totalSpawns; i++)
		{
			var npc = Instantiate(npcPrefab, RandomInsideBounds(0f), Quaternion.AngleAxis(Random.value * 360f, Vector3.forward)) as NpcController;
			npc.Bounds = bounds;
			npc.transform.localScale = Vector3.one * spawnScale;
			npc.transform.parent = transform;
		}
	}

	public Vector3 RandomInsideBounds(float z)
	{
		float x = Random.Range(bounds.xMin, bounds.xMax);
		float y = Random.Range(bounds.yMin, bounds.yMax);

		return new Vector3(x, y, z);
	}
}
