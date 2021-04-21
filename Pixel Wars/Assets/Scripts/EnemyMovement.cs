using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MovementHandler
{
    private void Start()
    {
        LegionSpawner spawner = GetComponentInParent<LegionSpawner>();
        spawner.OnUnitSpawn += addUnitToAgents;
    }

    private void addUnitToAgents(GameObject pUnit)
    {
        
    }
}
