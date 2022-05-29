namespace DeathIsOnlyTheBeginning
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class Character : MonoBehaviour
    {        
        [SerializeField] float timeToLife = 300;
        [SerializeField] int hitPoints = 100;
        [SerializeField] float timeBetweenAttacks = 0.5f;
        [SerializeField] CharacterSheet characterSheet;

        private float timeOfLastAttack;
        public  UnityEvent CharacterDied = new UnityEvent();  
        
        [field: SerializeField] public int AttackRange { get; set; }

        public int HitPoints { get { return this.hitPoints; } }

        private void Update()
        {
            timeToLife -= Time.deltaTime;
            if (timeToLife <= 0) Die();
            if (hitPoints <= 0) Die();
        }

        private void Die()
        {
            if (CharacterDied != null) CharacterDied.Invoke();
            GameObject.Destroy(this.gameObject);
        }

        public void ReceiveDamage(int amount)
        {
            hitPoints -= amount;
        }

        public void Attack(Monster monster)
        {
            if (CanAttack(monster))
            {
                monster.ReceiveDamage(characterSheet.Attack);
                timeOfLastAttack = Time.time;
            }

        }

        public bool CanAttack(Monster monster)
        {
            if(Time.time - timeOfLastAttack < timeBetweenAttacks) return false;
            if (Vector3.Distance(transform.position, monster.transform.position) > AttackRange) return false;
            return true;
        }

        internal void ReceiveXp(int xpValue)
        {
            characterSheet.AddExperiencePoints(xpValue);
        }
    }
}
