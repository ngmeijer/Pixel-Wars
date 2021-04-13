using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LegionController : MonoBehaviour
{
    [SerializeField] private PixelUnit armyProperties;
    [SerializeField] private List<GameObject> listOfPixels = new List<GameObject>();

    private List<LegionController> listOfEnemyUnits = new List<LegionController>();
    private float attackTimer;

    public void TakeDamage(out bool pHasDied)
    {
        int randomIndex = Random.Range(0, listOfPixels.Count);
        Destroy(listOfPixels[randomIndex]);
        listOfPixels[randomIndex] = null;
        listOfPixels.RemoveAt(randomIndex);

        pHasDied = checkDeathConditions();
    }

    private bool checkDeathConditions()
    {
        if (listOfPixels.Count <= 0)
        {
            Destroy(this);
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider pOther)
    {
        if (pOther.gameObject.CompareTag("EnemyUnit"))
        {
            if (pOther.gameObject.TryGetComponent(out LegionController controller))
            {
                listOfEnemyUnits.Add(controller);
            }
        }
    }

    private void OnTriggerStay(Collider pOther)
    {
        if (pOther.gameObject.CompareTag("EnemyUnit"))
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= armyProperties.AttackSpeed)
            {
                if (listOfEnemyUnits.Count <= 0)
                    return;

                for (int i = 0; i < listOfEnemyUnits.Count; i++)
                {
                    bool hasKilledUnit = false;

                    if (listOfEnemyUnits[i] != null)
                        listOfEnemyUnits[i].TakeDamage(out hasKilledUnit);

                    if (listOfEnemyUnits[i] == null || hasKilledUnit)
                        listOfEnemyUnits.RemoveAt(i);
                }

                attackTimer = 0;
            }
        }
    }

    private void OnTriggerExit(Collider pOther)
    {
        if (pOther.gameObject.CompareTag("EnemyUnit"))
        {
        }
    }
}