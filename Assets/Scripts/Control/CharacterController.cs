namespace DeathIsOnlyTheBeginning.Controlls
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CharacterController : MonoBehaviour
    {
        [SerializeField] float rotationSpeed;
        [SerializeField] Character character;

        private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            HandleMovement();
            HandleAttack();
        }

        private void HandleMovement()
        {
            HandleWalking();
            HandleRotation();
        }

        private void HandleAttack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (RayCaster.MouseHitObjectWithTag("Monster"))
                {
                    Monster monster = RayCaster.GetMouseHitGameObject().GetComponent<Monster>();
                    character.Attack(monster);
                };
            }
            
        }

        private void HandleWalking()
        {
            animator.SetFloat("Speed", Input.GetAxis("Vertical"));
        }

        private void HandleRotation()
        {
            if (Input.GetAxis("Horizontal") > 0.1)
            {
                this.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
            if (Input.GetAxis("Horizontal") < -0.1)
            {
                this.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            }
        }


    }
}
