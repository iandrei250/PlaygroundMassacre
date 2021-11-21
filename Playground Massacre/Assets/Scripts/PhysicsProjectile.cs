using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsProjectile : Projectile
{
   [SerializeField] private float lifetime;
   private Rigidbody rigidBody;
   private void Awake() {
       rigidBody = GetComponent<Rigidbody>();
   }

   public override void Init(Weapon weapon){
       base.Init(weapon);
       Destroy(gameObject,lifetime);
   }

  public override void Launch(){
      base.Launch();
      rigidBody.AddRelativeForce(Vector3.forward * weapon.GetShootingForce(), ForceMode.Impulse);
  }
}
