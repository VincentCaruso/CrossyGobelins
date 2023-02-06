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

	private void Start()
	{
		List<int> randPosition = PickRandomNumbers(NumObstacles, MaxX);
		print(randPosition);
		for (int i = 0; i < randPosition.Count; i++)
		{
			GameObject go = Instantiate(Obstacles[Random.Range(0, Obstacles.Count)], transform);
			go.transform.localPosition = new Vector3(randPosition[i], 0, 0);
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

	List<int> PickRandomNumbers(int numValues, int range)
	{
		List<int> result = new List<int>();

		for (int i = 0; i < numValues; i++)
		{
			result.Add(PickRandomNumber(result, range));
		}

		return result;
	}

	int PickRandomNumber(List<int> tab, int range)
	{
		int rand = Random.Range(-range, range + 1);

		if (tab.Contains(rand))
		{
			PickRandomNumber(tab, range);
		}

		return rand;
	}
}
