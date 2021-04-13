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
    public ArmyUnit armyProperties;
    [SerializeField] private Transform spawnedUnitsParent;
    [SerializeField] private Transform spawnpointsParent;
    private List<Transform> listOfSpawnpoints = new List<Transform>();

    private int currentAmountOfUnits;

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
            yield return new WaitForSeconds(armyProperties.RespawnRate);

            if (currentAmountOfUnits < armyProperties.MaxAmountOfUnits)
            {
                //TODO Convert to object pool!
                int randomIndex = Random.Range(0, listOfSpawnpoints.Count - 1);
                Instantiate(armyProperties.LegionPrefab, listOfSpawnpoints[randomIndex].position, Quaternion.identity, spawnedUnitsParent);
                currentAmountOfUnits++;
            }
        }
    }
}