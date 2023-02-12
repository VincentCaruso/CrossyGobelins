using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class Row : MonoBehaviour
{

	[SerializeField]
	List<GameObject> Obstacles;

	[SerializeField]
	int NumObstacles = 3;

	[SerializeField]
	int MaxX = 7;

	public float baseZPosition = -1000;
	List<int> randomPos = new List<int>();

	private void Start()
	{
		PickRandomNumbers(NumObstacles, MaxX);
		for (int i = 0; i < randomPos.Count; i++)
		{
			GameObject go = Instantiate(Obstacles[Random.Range(0, Obstacles.Count)], transform);
			go.transform.localPosition = new Vector3(randomPos[i], 0, 0);
		}
	}

	public void MoveBackward(float speed)
	{
		if (baseZPosition == -1000)
		{
			baseZPosition = transform.position.z;
		}

		baseZPosition--;

		transform.DOMoveZ(baseZPosition, speed);
	}

	void PickRandomNumbers(int numValues, int range)
	{
		for (int i = 0; i < numValues; i++)
		{
			PickRandomNumber(range);
		}
	}

	void PickRandomNumber(int range)
	{
		int rand = Random.Range(-range, range + 1);

		if (randomPos.Contains(rand))
		{
			PickRandomNumber(range);
        }
        else
        {
			randomPos.Add(rand);
        }
	}
}
