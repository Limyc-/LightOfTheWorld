using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
	[SerializeField]
	private CameraController cameraFollow;
	[SerializeField]
	private Transform player;
	[SerializeField]
	private PlayerClone playerClonePrefab;
	[SerializeField]
	private Vector2 levelDimensions;

	public PlayerClone Top { get; private set; }
	public PlayerClone Bottom { get; private set; }
	public PlayerClone Left { get; private set; }
	public PlayerClone Right { get; private set; }

	private new Transform transform;
	private List<PlayerClone> clones;
	private Bounds levelBounds;
	private bool playerIsRigidbody = false;

	private void Awake()
	{
		transform = GetComponent<Transform>();
		levelBounds = new Bounds(Vector3.zero, levelDimensions);
	}

	private void Start()
	{
		if (player.rigidbody2D != null || player.rigidbody != null)
		{
			playerIsRigidbody = true;
		}

		transform.position = player.position;

		SetupClones();
	}

	private void Update()
	{
		if (!playerIsRigidbody)
		{
			FollowTarget();
		}
	}

	private void FixedUpdate()
	{
		if (playerIsRigidbody)
		{
			FollowTarget();
		}
	}

	private void FollowTarget()
	{
		transform.position = player.position;
	}

	public void SwapPositions(Vector3 borderPos, Vector3 velocity)
	{
		var vX = velocity.x;
		var vY = velocity.y;

		var pX = player.position.x;
		var pY = player.position.y;

		var bX = borderPos.x;
		var bY = borderPos.y;

		if (bX == 0f)
		{
			if (bY > 0f && vY > 0f && pY >= bY)
			{
				player.position = Bottom.Position;
				cameraFollow.SetPosition("Bottom");
			}
			else if (bY < 0f && vY < 0f && pY <= bY)
			{
				player.position = Top.Position;
				cameraFollow.SetPosition("Top");
			}
		}
		else if (bY == 0f)
		{
			if (bX > 0f && vX > 0f && pX >= bX)
			{
				player.position = Left.Position;
				cameraFollow.SetPosition("Left");
			}
			else if (bX < 0f && vX < 0f && pX <= bX)
			{
				player.position = Right.Position;
				cameraFollow.SetPosition("Right");
			}
		}
	}

	public void ChangeLight(Color color)
	{
		for (int i = 0; i < clones.Count; i++)
		{
			clones[i].SetLightColor(color);
		}
	}

	public void ChangeMaterial(Color color)
	{
		for (int i = 0; i < clones.Count; i++)
		{
			clones[i].SetMaterialColor(color);
		}
	}

	private void SetupClones()
	{
		clones = new List<PlayerClone>(4);
		var pos = transform.position;
		var offset = ((CircleCollider2D)player.collider2D).radius;
		var x = levelDimensions.x;// -offset;
		var y = levelDimensions.y;// -offset;


		Top = CreateClone("CloneTop", pos + new Vector3(0f, y));
		Bottom = CreateClone("CloneBottom", pos + new Vector3(0f, -y));
		Left = CreateClone("CloneLeft", pos + new Vector3(-x, 0f));
		Right = CreateClone("CloneRight", pos + new Vector3(x, 0f));

		clones.Add(Top);
		clones.Add(Bottom);
		clones.Add(Left);
		clones.Add(Right);
	}

	private PlayerClone CreateClone(string name, Vector3 pos)
	{
		var clone = Instantiate(playerClonePrefab, pos, Quaternion.identity) as PlayerClone;
		clone.name = name;
		clone.transform.parent = transform;

		return clone;
	}
}