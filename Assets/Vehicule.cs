using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Vehicule : MonoBehaviour
{
	[SerializeField]
	float Speed = 2f;

	// Update is called once per frame
	void Update()
	{
		transform.Translate(Vector3.left * Time.deltaTime * Speed);

		if (transform.localPosition.x < -8)
		{
			transform.localPosition = new Vector3(8, 0, 0);
		}
	}
}
