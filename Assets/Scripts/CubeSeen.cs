using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSeen : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectID objectID = GetComponentInParent<ObjectID>();
            transform.parent.GetChild(objectID.objectID).GetComponent<MeshRenderer>().material = MateraiSystem.Instance.ObjectMateral[GetComponentInParent<ObjectID>().materialCount];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectID objectID = GetComponentInParent<ObjectID>();
            transform.parent.GetChild(objectID.objectID).GetComponent<MeshRenderer>().material = MateraiSystem.Instance.emptyMaterial;
        }
    }
}
