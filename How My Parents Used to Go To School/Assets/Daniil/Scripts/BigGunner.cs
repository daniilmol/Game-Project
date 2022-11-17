using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigGunner : Enemy
{
    [SerializeField] float range = 7f;
    [SerializeField] float attackSpeed = 0.5f;
    private bool shooting;
    private bool following;
    private bool IsAvailable = true;
    private bool IsResting;
    private bool finishedAbility = false;
    private int abilityTimer;
    private int restTimer;
    private int angleScale;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        abilityTimer = 5;
        restTimer = 2;
        IsResting = false;
        shooting = true;
        angleScale = 0;
        dropChance = 100;
        boss = true;
    }
    void Update()
    {
        agent.updateRotation = false;
        FaceTarget(player.transform.position);
        
        FirstBossBehaviour();
    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f);
    }

    private void FirstBossBehaviour() {
        if(!IsResting && Vector3.Distance(player.transform.position, transform.position) < 50){
            Attack();
        }
    }

    private void Shoot(){
        if(!IsAvailable){
            return;
        }
        if(angleScale == 360){
            angleScale = 0;
        }
        for(int i = 0; i < 10; i++){
            GameObject particle = Instantiate(bullet, new Vector3(transform.position.x, -9f, transform.position.z), Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            particle.GetComponent<Bullet>().SetShooter(gameObject);
            particle.GetComponent<Rigidbody>().AddForce(Quaternion.Euler(angleScale, angleScale, 0) * transform.forward * particle.GetComponent<Bullet>().GetSpeed(), ForceMode.Impulse);
            angleScale += 36;
        }
        StartCoroutine(StartCooldown());
    }

    private void Charge(){
        if(!IsAvailable){
            return;
        }
        agent.destination = player.transform.position;
        StartCoroutine(StartCooldown());
    }

    protected override void Attack() {
        if(shooting){
            Shoot();
            if(!finishedAbility){
                StartCoroutine(StartAbilityCooldown());
            }
        }else{
            Charge();
            if(!finishedAbility){
                StartCoroutine(StartAbilityCooldown());
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(allowed){
                collision.gameObject.GetComponent<PlayerHealth>().takeDamage(2);
                //c.GetComponent<NavMeshAgent>().isStopped = true;
                float force = 50f;
                collision.gameObject.GetComponent<Rigidbody>().AddForce(force * collision.gameObject.transform.forward, ForceMode.Impulse);                    //  Need a on hit decrese health function here, example: decreaseHealth(Gameobject enemy);
                StartCoroutine(StartChargeCooldown());
            }
        }
        if (collision.gameObject.tag == "Bullet")
        {
            //takeDamage(collision.gameObject.GetComponent<Bullet>().GetShooter().GetComponent<EnemyStatContainer>().GetDamage());
            //print("Player took " + collision.gameObject.GetComponent<Bullet>().GetShooter().GetComponent<EnemyStatContainer>().GetDamage());
        }
    }
bool allowed = true;
    

    public IEnumerator StartChargeCooldown(){
        allowed = false;
        yield return new WaitForSeconds(2);
        allowed = true;
    }
    public IEnumerator StartAbilityCooldown(){
        finishedAbility = true;
        yield return new WaitForSeconds(abilityTimer);
        StartCoroutine(StartRestCooldown());
        shooting = !shooting;
        finishedAbility = false;
    }
    public IEnumerator StartRestCooldown(){
        IsResting = true;
        yield return new WaitForSeconds(restTimer);
        IsResting = false;
    }
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(attackSpeed);
        IsAvailable = true;
    }
}
