using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
	[SerializeField]
	float speed;
	[SerializeField]
	float addSpeed;
	[SerializeField]
	float maxSpeed;
	[SerializeField]
	[Tooltip("change direction depending on the distance from the center of the player's platform")]
	float playerRotCoef;

	Vector3 currDir;
	Rigidbody rigBody;

	private void OnValidate()
	{
		if (speed <= 0)
			Debug.LogError("speed bad value");
		if (addSpeed <= 0)
			Debug.LogError("addSpeed bad value");
		if (maxSpeed <= 0)
			Debug.LogError("maxSpeed bad value");
		if (playerRotCoef <= 0)
			Debug.LogError("playerRotCoef bad value");
	}

	private void Awake()
	{
		//create random start direction
		currDir = Vector3.Normalize(new Vector3(Random.Range(-1.0f, 1.0f), 0, 1) + Vector3.forward);
		rigBody = this.GetComponent<Rigidbody>();

	}

	public void StartBall()
	{
		rigBody.velocity = currDir * speed;

	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			GetBonk(collision.contacts[0].point);
			GameController.Instance.PlayerBonkCounter++;
		}
		if (collision.gameObject.layer == LayerMask.NameToLayer("LoseWall"))
		{
			GameController.Instance.LoseBonkCounter++;
		}

		//if ball moving in only one direction, fixing this direction for more action
		if (rigBody.velocity.normalized.x < 0.1f && rigBody.velocity.normalized.x > -0.1f)
			rigBody.velocity = new Vector3(rigBody.velocity.x * 5, rigBody.velocity.y, rigBody.velocity.z);
		if (rigBody.velocity.normalized.z < 0.1f && rigBody.velocity.normalized.z > -0.1f)
			rigBody.velocity = new Vector3(rigBody.velocity.x, rigBody.velocity.y, rigBody.velocity.z * 5);
		rigBody.velocity.Normalize();
	}

	void Update()
	{
		//rotate ball
		rigBody.AddTorque(Vector3.Cross(-rigBody.velocity, Vector3.up).normalized);
	}

	void GetBonk(Vector3 point)
	{
		speed += addSpeed;
		if (speed > maxSpeed)
			speed = maxSpeed;

		Vector3 norm = rigBody.velocity.normalized;
		//change direction depending on the distance from the center of the player's platform
		norm.x *= Mathf.Abs((GameController.Instance.PlayerPos - point).x * playerRotCoef);

		norm.Normalize();
		rigBody.velocity = norm * speed;

	}
}
