namespace DeathIsOnlyTheBeginning
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Monster : MonoBehaviour
    {
        [SerializeField] int hitPoints;
        [SerializeField] Item lootItem;

        private void Update()
        {
            if(hitPoints <= 0)
            {
                Die();
            }
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
    }

}