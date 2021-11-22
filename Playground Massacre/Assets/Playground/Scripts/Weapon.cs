using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Weapon : MonoBehaviour
{   
     [SerializeField] protected float shootingForce;
     [SerializeField] protected Transform bulletSpawn;
     [SerializeField] private float recoilForce;
     [SerializeField] private float damage;

     private Rigidbody rigidbody;
     private XRGrabInteractable interactableWeapon;

     protected virtual void Awake()  {
         interactableWeapon = GetComponent<XRGrabInteractable>();
         rigidbody = GetComponent<Rigidbody>();
        
         WeaponEvents();
     }

     private void WeaponEvents(){
         interactableWeapon.onSelectEntered.AddListener(PickGun);
         interactableWeapon.onSelectExited.AddListener(DropGun);
         interactableWeapon.onActivate.AddListener(StartShoot);
         interactableWeapon.onDeactivate.AddListener(StopShooting);
     }

     private void PickGun(XRBaseInteractor interactor){
        interactor.GetComponent<HideMeshRenderer>().HideMesh();
        
     }
     protected virtual void DropGun(XRBaseInteractor interactor){
        interactor.GetComponent<HideMeshRenderer>().ShowMesh();
     }
     protected virtual void StartShoot(XRBaseInteractor interactor){

     }
     protected virtual void StopShooting(XRBaseInteractor interactor){

     }

     protected virtual void Shoot(){
         ApplyRecoil();
     }

     private void ApplyRecoil(){
         rigidbody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
     }

     public float GetShootingForce(){
         return shootingForce;
     }

     public float GetDamage(){
         return damage;
     }
}
