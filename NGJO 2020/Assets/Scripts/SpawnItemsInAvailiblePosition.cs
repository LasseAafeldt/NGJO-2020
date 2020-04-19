using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemsInAvailiblePosition : MonoBehaviour {
	[SerializeField] private int NumItemsToSpawn = 10;
	public GameObject[] itemsToSpawn;

	ItemSpawnPositions spawnPoints;

	public List<Transform> availableSpawnPoints;

	private void Awake() {
		spawnPoints = GetComponent<ItemSpawnPositions>();
	}

	private void Start() {
		availableSpawnPoints = new List<Transform>(spawnPoints.GetAllSpawnPositions());
		SpawnItems();
	}

	void SpawnItems() {
		for (int i = 0; i < NumItemsToSpawn; i++) {
			Vector3 placeToSpawn = SelectAvailableSpawnPoint().position;
			Instantiate(itemsToSpawn[Random.Range(0, itemsToSpawn.Length)], placeToSpawn, Quaternion.identity);
		}
	}

	Transform SelectAvailableSpawnPoint() {
		//Debug.Log("Selecting...");
		Transform chosenTransform;
		int index = Random.Range(0, availableSpawnPoints.Count);

		chosenTransform = availableSpawnPoints[index];
		//Debug.Log("Removed: " + chosenTransform);
		availableSpawnPoints.Remove(chosenTransform);

		return chosenTransform;
	}
}
