using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBehaviour : MonoBehaviour
{
    public void DealDamage(Collider target)
    {
        print(target.name);
    }
}
