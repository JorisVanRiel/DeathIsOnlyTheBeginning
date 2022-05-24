namespace DeathIsOnlyTheBeginning
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Character : MonoBehaviour
    {
        [SerializeField] float timeToLife = 300;

        private void Update()
        {
            timeToLife -= Time.deltaTime;
            if(timeToLife <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
