using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour, IDamage
{
    const string Run = "Run";
    const string Crouch = "Crouch";
    const string Shoot = "Shoot";

    [SerializeField] private float startingHealth;
    [SerializeField] private float minTimeUnderCover;
    [SerializeField] private float maxTimeUnderCover;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float damage;
    [SerializeField] private int minShotsToTake;
    [SerializeField] private int maxShotsToTake;

    [Range(0,100)]
    [SerializeField] private float shootingAcc;
    [SerializeField] private Transform shootingPosition;
    [SerializeField] private ParticleSystem bloodFX;

    private bool isShooting;
    private int currentShotsTaken;
    private int currentMaxShotsToTake;
    private NavMeshAgent agent;
    private Player player;
    private Transform occupiedCoverSpot;
    private Animator animator;

    private float _health;
    public float health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = Mathf.Clamp(value, 0, startingHealth);
        }
    }


    private void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator.SetTrigger(Run);
        _health = startingHealth;
    }

    private void Update() {
        if(agent.isStopped == false && (transform.position - occupiedCoverSpot.position).sqrMagnitude <= 0.1f){
            agent.isStopped = true;
            StartCoroutine(InitializeShooting());
        }
        if(isShooting){
            FacePlayer();
        }
    }
    public void Init(Player player, Transform coverSpot){
        occupiedCoverSpot = coverSpot;
        this.player = player;
        GoToCover();
    }

    public void ShootEv(){
        bool hitPlayer = UnityEngine.Random.Range(0,100) < shootingAcc ;
        if(hitPlayer){
            RaycastHit hit;
            Vector3 direction = player.GetHeadPosition() - shootingPosition.position;
            if(Physics.Raycast(shootingPosition.position, direction, out hit))
            {
                Player player = hit.collider.GetComponentInParent<Player>();
                if(player){
                    player.TakeDamage(damage);
                }
            }
        }
        currentShotsTaken++;
        if(currentShotsTaken >= currentMaxShotsToTake){
            StartCoroutine(InitializeShooting());
        }
    }

    private IEnumerator InitializeShooting(){
        HideBehindCover();
        yield return new WaitForSeconds(0.5f);
        StartShooting();
    }
    private void FacePlayer(){
        Vector3 direction = player.GetHeadPosition();
        direction.y = transform.position.y;
        direction.x = transform.position.x - 90;
        Quaternion rotation = Quaternion.LookRotation(direction);
        // rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
    }

    private void HideBehindCover(){
        animator.SetTrigger(Crouch);
        currentMaxShotsToTake = UnityEngine.Random.Range(minShotsToTake, maxShotsToTake);
        currentShotsTaken = 0;

        animator.SetTrigger(Shoot);
    }

    private void StartShooting() {
        isShooting = true;
    }


    public void GoToCover(){
        agent.isStopped = false;
        agent.SetDestination(occupiedCoverSpot.position);
    }
    
    public void TakeDamage(Weapon weapon, Projectile projectile, Vector3 hitPos){
        health -= weapon.GetDamage();
        if(health <= 0) Destroy(gameObject);

       ParticleSystem effect = Instantiate(bloodFX, hitPos, Quaternion.LookRotation(weapon.transform.position - hitPos));
       effect.Stop();
       effect.Play();
    }
}
