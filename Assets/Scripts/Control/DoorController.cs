namespace DeathIsOnlyTheBeginning.Controlls
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DoorController : MonoBehaviour
    {
        private bool characterIsNear = false;
        private bool isOpen = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)) this.Open();
        }

        public void Open()
        {
            if (characterIsNear && !isOpen)
            {
                gameObject.transform.position += new Vector3(0, 3, 0);
                isOpen = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Character>() != null)
            {
                characterIsNear = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Character>() != null)
            {
                characterIsNear = false;
            }
        }
    }
}
