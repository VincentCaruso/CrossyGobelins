using System;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class RowGenerator : MonoBehaviour
{
	[SerializeField]
	GameObject RowGrassPrefab;

	[SerializeField]
	GameObject RowRoadPrefab;

	[SerializeField]
	float Speed = 0.3f;

	[SerializeField]
	int MaxNumRows = 10;

	List<GameObject> CurrentRows = new List<GameObject>();

	float currentPosition = 0f;

	// Start is called before the first frame update
	void Start()
	{
		GenerateRows();
	}

	private void GenerateRows()
	{
		for (int i = 0; i < MaxNumRows; i++)
		{
			GameObject go = Instantiate(RowGrassPrefab, transform);
			go.transform.localPosition = new Vector3(0, 0, i);

			CurrentRows.Add(go);
		}
	}

	public void GoForward()
	{
		//Détruit la première ligne et l'enlève de la liste des lignes
		GameObject toDestroy = CurrentRows[0];
		CurrentRows.RemoveAt(0);
		Destroy(toDestroy);

		//Créé une nouvelle ligne et la place tout au bout
		GameObject go = Instantiate(GetRandomRow(), transform);
		go.transform.localPosition = new Vector3(0, 0, MaxNumRows);
		CurrentRows.Add(go);

		//Déplace toutes les lignes d'une case en arrière
		for (int i = 0; i < MaxNumRows; i++)
		{
			GameObject row = CurrentRows[i];

			row.GetComponent<Row>().MoveBackward(Speed);
		}
	}

	GameObject GetRandomRow()
	{
		int random = UnityEngine.Random.Range(0, 2);

		if (random == 1)
		{
			return RowRoadPrefab;
		}

		return RowGrassPrefab;
	}
}
