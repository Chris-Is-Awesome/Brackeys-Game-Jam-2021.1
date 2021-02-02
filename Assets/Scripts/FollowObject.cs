using UnityEngine;

public class FollowObject : MonoBehaviour
{
	[SerializeField] Transform objectToFollow;
	[SerializeField] Vector3 followOffset;
	[SerializeField] bool followX;
	[SerializeField] bool followY;
	[SerializeField] bool followZ;

	private void Awake()
	{
		if (objectToFollow == null) Debug.LogWarning("objectToFollow is null. " + gameObject.name + " won't follow anything.");
		if (!followX && !followY && !followZ) Debug.LogWarning("All follow checks are false. " + gameObject.name + " won't follow anything.");
	}

	private void FixedUpdate()
	{
		Vector3 currentPosition = transform.position;
		Vector3 positionToFollow = objectToFollow.position;
		Vector3 newPosition = positionToFollow - followOffset;

		if (followX) newPosition = new Vector3(newPosition.x, currentPosition.y, currentPosition.z);
		if (followY) newPosition = new Vector3(currentPosition.x, newPosition.y, currentPosition.z);
		if (followZ) newPosition = new Vector3(currentPosition.x, currentPosition.y, newPosition.z);
		transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
	}
}