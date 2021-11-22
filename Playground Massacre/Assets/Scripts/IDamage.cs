using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void TakeDamage(Weapon weapon, Projectile projectile, Vector3 hitPos);
}
