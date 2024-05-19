using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Itens
{
    public class ItemCollectableBase : MonoBehaviour
    {
        public ItemType itemType;

        public string compareTag = "Player";
        public ParticleSystem particleSystem1;
        public float timeToHide = 0.5f;
        public GameObject graphicItem;

        public Collider collider;

        //public Collider2D colliderBase;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            //if (particleSystem1 != null) particleSystem1.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if(collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }


        protected virtual void Collect()
        {
            if (collider != null) collider.enabled = false;
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideObject", timeToHide);
            //gameObject.SetActive(false);
            OnCollect();
        }
        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (particleSystem1 != null) particleSystem1.Play();
            if (audioSource != null) audioSource.Play();
            ItemManager.Instance.AddByType(itemType);
            //colliderBase.enabled = false;
        }
    }
}