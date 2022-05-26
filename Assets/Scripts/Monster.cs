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
        private void Update()
        {
            if(hitPoints <= 0) Die();
            Move();
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
            if (doorsInRoom.Any(d => d.IsOpen))
            {
                NavMeshAgent agent = GetComponent<NavMeshAgent>();
                agent.SetDestination(target.transform.position);
            }
        }

    }

}