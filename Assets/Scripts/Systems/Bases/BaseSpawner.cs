using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{
    protected const int MAX_DISTANCE = 10;
    protected const int MIN_DISTANCE = -10;

    public abstract void Spawn(Unit unit);
}
