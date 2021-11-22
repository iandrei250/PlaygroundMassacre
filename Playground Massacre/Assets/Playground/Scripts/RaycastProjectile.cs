using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastProjectile : Projectile
{
    public override void Launch(){
        base.Launch();
        RaycastHit hit ;
        if(Physics.Raycast(transform.position, transform.forward, out hit)){
            IDamage[] damageTakers = hit.collider.GetComponentsInParent<IDamage>();
            foreach(var taker in damageTakers){

                taker.TakeDamage(weapon, this, hit.point);

            }
        }
    }
}
