using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeSeen : MonoBehaviour
{
    public bool seen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectID objectID = GetComponentInParent<ObjectID>();
            transform.parent.GetChild(objectID.objectID).GetComponent<MeshRenderer>().material = MateraiSystem.Instance.ObjectMateral[objectID.materialCount];
            seen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectID objectID = GetComponentInParent<ObjectID>();
            transform.parent.GetChild(objectID.objectID).GetComponent<MeshRenderer>().material = MateraiSystem.Instance.emptyMaterial;
            seen = false;
        }
    }


}
