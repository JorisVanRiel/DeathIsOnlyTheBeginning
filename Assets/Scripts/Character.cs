namespace DeathIsOnlyTheBeginning
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Character : MonoBehaviour
    {
        [SerializeField] float timeToLife = 300;
        [SerializeField] int hitPoints = 100;

        public int HitPoints { get { return this.hitPoints; } }

        private void Update()
        {
            timeToLife -= Time.deltaTime;
            if (timeToLife <= 0) Die();
            if (hitPoints <= 0) Die();
        }

        private void Die()
        {
            GameObject.Destroy(this.gameObject);
        }

        public void ReceiveDamage(int amount)
        {
            hitPoints -= amount;
        }
    }
}
