using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Rifle : Weapon
{
    [SerializeField] private float fireRate;
    private Projectile projectile;

    private WaitForSeconds wait;

    protected override void Awake() {
        base.Awake();
        projectile = GetComponentInChildren<Projectile>();
    }

    private void Start() {
        wait = new WaitForSeconds(1/fireRate);
        projectile.Init(this);
    }

    protected override void StartShoot(XRBaseInteractor interactor){
        base.StartShoot(interactor);
        StartCoroutine(ShootingCoroutine());
    }

    private IEnumerator ShootingCoroutine(){
        while(true){
            Shoot();
            yield return wait;
        }
    }

    override protected void Shoot(){
        base.Shoot();
        projectile.Launch();
    }

    protected override void StopShooting(XRBaseInteractor interactor){
        base.StopShooting(interactor);
        StopAllCoroutines();
    }
}
