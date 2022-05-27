namespace DeathIsOnlyTheBeginning.Controlls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UnityEngine;

    public static class RayCaster
    {
        public static RaycastHit GetMouseHit()
        {
            Camera camera = GameObject.FindObjectOfType<Camera>();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            return hit;
        }

        public static GameObject GetMouseHitGameObject()
        {
            return GetMouseHit().collider.gameObject;
        }

        public static bool MouseHitsAny()
        {
            return GetMouseHit().collider != null;
        }

        public static bool MouseHitObjectWithTag(string tag)
        {
            RaycastHit hit = RayCaster.GetMouseHit();
            if (hit.collider == null) return false;
            return hit.collider.CompareTag(tag);
        }

        public static bool MouseHits(GameObject gameObject)
        {
            return GetMouseHit().collider.gameObject == gameObject;
        }

        public static bool MouseHitsContains(GameObject gameObject)
        {

            return GetAllMouseHits().Any(c => c.collider.gameObject == gameObject);
        }

        public static RaycastHit[] GetAllMouseHits()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            return hits;
        }
    }
}