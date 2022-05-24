namespace DeathIsOnlyTheBeginning
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Monster : MonoBehaviour
    {
        [SerializeField] int hitPoints;

        private void Update()
        {
            if(hitPoints <= 0)
            {
                Die();
            }
        }

        public double HitPoints { get { return hitPoints;  } }

        public void ReceiveDamage(int amount)
        {
            hitPoints -= amount;
        }

        private void Die()
        {
            GameObject.Destroy(gameObject);
        }
    }

}