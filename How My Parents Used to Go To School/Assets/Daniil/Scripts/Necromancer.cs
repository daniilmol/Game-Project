using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Necromancer : Enemy
{
    [SerializeField] float range = 7f;
    [SerializeField] float attackSpeed = 0.5f;
    [SerializeField] GameObject skeleton;
    private bool following;
    private bool IsAvailable = true;
    private bool IsResting;
    private bool finishedAbility = false;
    private bool attacking;
    private bool hasTeleported;
    private bool firstTimeTeleport;

    private int abilityTimer;
    private int restTimer;
    private int minionCount;
    private int minionSpawnAmount;

    private Vector3 playerLocation;

    private void Start()
    {
        minionCount = 0;
        minionSpawnAmount = 3;
        agent = GetComponent<NavMeshAgent>();
        damage = 3f;
        abilityTimer = 20;
        restTimer = 2;
        IsResting = false;
        finishedAbility = false;
        hasTeleported = false;
        firstTimeTeleport = false;
    }
    void Update()
    {
        agent.updateRotation = false;
        FaceTarget(player.transform.position);

        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > range)
        {
            following = true;
            attacking = false;
        }
        else if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= range)
        {
            attacking = true;
            following = false;
        }
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
        Attack();
    }

    private void Summon(){
        GameObject[] spawnables = GameObject.FindGameObjectsWithTag("Spawnable");
        for(int i = 0; i < minionSpawnAmount; i++){
            int index = Random.Range(0, spawnables.Length);
            Instantiate(skeleton, spawnables[index].transform.position, Quaternion.identity);
            minionCount++;
        }
    }

    private void Invincibility(){
        GetComponent<CapsuleCollider>().enabled = false;
        if(minionCount <= 0){
            Summon();        
        }
    }

    private void Melee(){
        if (following && CheckForPlayerRange())
        {
            agent.SetDestination(player.transform.position);
        }
        else if (attacking)
        {
            if(!IsAvailable){
                return;
            }
            //player.GetComponent<PlayerHealth>().takeDamage(damage)
            print("NECROMANCER ATTACKED");
            StartCoroutine(StartCooldown());
        }
    }

    protected override void Attack() {
        if(!finishedAbility){
            Invincibility();
        }else if(finishedAbility){
            
            Teleport();
            Melee();
        }
    }
    
    private void Teleport(){
        if(!firstTimeTeleport){
            if(!hasTeleported){
                GetComponent<CapsuleCollider>().enabled = true;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
                transform.position = new Vector3(0,0,0);
                playerLocation = new Vector3(player.transform.position.x, -10f, player.transform.position.z);
                StartCoroutine(StartTeleport());
            }
        }
        firstTimeTeleport = true;
        if(hasTeleported){
            transform.position = playerLocation;
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void MinionDead(){
        minionCount--;
        print("ALL MINIONS DEAD");
        if(minionCount == 0){
            StartCoroutine(StartAbilityCooldown());
        }
    }

    public IEnumerator StartAbilityCooldown(){
        finishedAbility = true;
        yield return new WaitForSeconds(abilityTimer);
        finishedAbility = false;
        firstTimeTeleport = false;
    }
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(attackSpeed);
        IsAvailable = true;
    }
    public IEnumerator StartTeleport(){
        hasTeleported = false;
        yield return new WaitForSeconds(2f);
        hasTeleported = true;
    }
}
