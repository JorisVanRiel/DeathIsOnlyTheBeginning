namespace DeathIsOnlyTheBeginning
{
    using DeathIsOnlyTheBeginning.Controlls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.AI;

    public class Monster : MonoBehaviour
    {

        [SerializeField] int monsterLevel = 1;

        [SerializeField] int hitPoints;
        [SerializeField] Item lootItem;
        [SerializeField] Character player;
        [SerializeReference] List<DoorController> doorsInRoom;
        [SerializeField] int attackStrength;
        [SerializeField] float attackTime;
        [SerializeField] float attackDistance;
        [SerializeField] int xpValue;
        


        [SerializeField] Room room;

        private float timeOflastAttack;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            GameObject.FindObjectOfType<GameManager>().CharacterRespawn.AddListener(SetCharacter);
            this.doorsInRoom = room.Doors;
            hitPoints *= monsterLevel;
            attackStrength *= monsterLevel * monsterLevel;
            xpValue *= monsterLevel;
        }

        private void Update()
        {
            if(hitPoints <= 0) Die();
            Move();
            Attack();
        }

        private void Attack()
        {
            if (!CanAttakcTarget()) return;

            player.ReceiveDamage(attackStrength);
            timeOflastAttack = Time.time;
            audioSource.Play();
        }

        private bool CanAttakcTarget()
        {
            if (timeOflastAttack + attackTime > Time.time) return false;
            if (player == null) return false;
            if(Vector3.Distance(transform.position, player.transform.position) > attackDistance) return false;
            return true;
        }

        public int HitPoints { get { return hitPoints;  } }

        public void ReceiveDamage(int amount)
        {
            hitPoints -= amount;
        }

        private void Die()
        {
            GameObject.Instantiate(lootItem);
            player.ReceiveXp(xpValue);
            lootItem.transform.position = transform.position;
            this.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        private void Move()
        {
            if (!doorsInRoom.Any(d => d.IsOpen)) return;
            this.transform.LookAt(player.transform.position);

            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(player.transform.position);
            
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("Speed", speed);
        }

        private void SetCharacter(Character character)
        {
           player = character;
        }

    }

}