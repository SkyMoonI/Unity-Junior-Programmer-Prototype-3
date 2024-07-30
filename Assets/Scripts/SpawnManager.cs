using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] GameObject obstaclePrefab;
	[SerializeField] Vector3 spawnPos = new Vector3(25, 0, 0);
	[SerializeField] float startDelay = 2;
	[SerializeField] float repeatRate = 2;

	PlayerController playerControllerScript;

	// Start is called before the first frame update
	void Start()
	{
		playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
		InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
	}

	// Update is called once per frame
	void Update()
	{

	}
	void SpawnObstacle()
	{
		if (!playerControllerScript.gameOver)
		{
			Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
		}

	}
}