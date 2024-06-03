using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class PlayerMagneticTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemCollectableBase i = other.transform.GetComponent<ItemCollectableBase>();
        if (i != null)
        {
            i.gameObject.AddComponent<Magnetic>();
        }
    }
}
