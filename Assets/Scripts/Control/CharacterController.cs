namespace DeathIsOnlyTheBeginning.Controlls
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CharacterController : MonoBehaviour
    {
        [SerializeField] float speed;

        // Update is called once per frame
        void Update()
        {
            this.transform.position= new Vector3(
                this.transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed,
                this.transform.position.y,
                this.transform.position.z + Input.GetAxis("Vertical") * Time.deltaTime * speed);
        }
    }
}
