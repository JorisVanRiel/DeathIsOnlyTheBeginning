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
        [SerializeField] int hitPoints;
        [SerializeField] Item lootItem;
        [SerializeField] Character target;
        [SerializeReference] List<DoorController> doorsInRoom;
        [SerializeField] int attackStrength;
        [SerializeField] float attackTime;
        [SerializeField] float attackDistance;

        private float timeOflastAttack;

        private void Update()
        {
            if(hitPoints <= 0) Die();
            Move();
            Attack();
        }

        private void Attack()
        {
            if (!CanAttakcTarget()) return;

            target.ReceiveDamage(attackStrength);
            timeOflastAttack = Time.time;
        }

        private bool CanAttakcTarget()
        {
            if (timeOflastAttack + attackTime > Time.time) return false;
            if (target == null) return false;
            if(Vector3.Distance(transform.position, target.transform.position) > attackDistance) return false;
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
            lootItem.transform.position = transform.position;
            GameObject.Destroy(gameObject);
        }

        private void Move()
        {

            if (!doorsInRoom.Any(d => d.IsOpen)) return;
            
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(target.transform.position);
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("Speed", speed);



        }

    }

}