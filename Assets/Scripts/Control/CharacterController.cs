namespace DeathIsOnlyTheBeginning.Controlls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CharacterController : MonoBehaviour
    {
        [SerializeField] float rotationSpeed;
        [SerializeField] Character character;
        [SerializeField] GameObject explosion;



        private Animator animator;
        private AudioSource audioSource;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            HandleMovement();
            HandleAttack();
        }

        private void HandleMovement()
        {
            Vector3 velocity = GetInputVector();
            if (velocity.magnitude > 0)
            {
                HandleWalking(velocity);
                HandleRotation(velocity);
            }
        }

        private Vector3 GetInputVector()
        {
            return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); 
        }

        private void HandleAttack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (RayCaster.MouseHitObjectWithTag("Monster"))
                {
                    Monster monster = RayCaster.GetMouseHitGameObject().GetComponent<Monster>();
                    if (character.CanAttack(monster))
                    {
                        character.Attack(monster);
                        animator.SetTrigger("Attack");
                        Instantiate(explosion).transform.position = monster.transform.position;
                        audioSource.Play();
                    }
                };
            }
            
        }

        private void HandleWalking(Vector3 velocity)
        {
            animator.SetFloat("Speed", velocity.magnitude);
        }

        private void HandleRotation(Vector3 velocity)
        {
            Quaternion rotation = Quaternion.LookRotation(velocity, Vector3.up);
            transform.rotation = rotation;
        }


    }
}
