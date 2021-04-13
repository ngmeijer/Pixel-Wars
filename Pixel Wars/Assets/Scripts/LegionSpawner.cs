using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BATTLE_MODE
{
    PASSIVE,
    MIDDLEGROUND,
    AGGRESSIVE
};


public class LegionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Transform spawnedUnitsParent;
    [SerializeField] private Transform spawnpointsParent;
    [SerializeField] [Range(0.5f, 10f)] private float respawnRate = 1f;
    private List<Transform> listOfSpawnpoints = new List<Transform>();

    private int currentAmountOfUnits;
    [SerializeField] private int maxAmountOfUnits;

    private void Start()
    {
        int amountOfSpawnpoints = spawnpointsParent.childCount;

        for (int i = 0; i < amountOfSpawnpoints; i++)
        {
            listOfSpawnpoints.Add(spawnpointsParent.GetChild(i));
        }

        StartCoroutine(spawnNewUnit());
    }

    private IEnumerator spawnNewUnit()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnRate);

            if (currentAmountOfUnits < maxAmountOfUnits)
            {
                //Convert to object pool
                int randomIndex = Random.Range(0, listOfSpawnpoints.Count - 1);
                Instantiate(unitPrefab, listOfSpawnpoints[randomIndex].position, Quaternion.identity, spawnedUnitsParent);
                currentAmountOfUnits++;
            }
        }
    }
}