namespace DeathIsOnlyTheBeginning.Controlls
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CharacterController : MonoBehaviour
    {
        [SerializeField] float rotationSpeed;

        private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }


        void Update()
        {
            animator.SetFloat("Speed", Input.GetAxis("Vertical"));
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
