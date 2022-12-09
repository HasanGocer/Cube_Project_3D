using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hide"))
        {
            if (other.GetComponent<ObjectID>().objectID == 0)
                WinFunc();
            else
                WrongObjectFunc(other.gameObject);
        }
    }

    private void WinFunc()
    {
        Buttons.Instance.winPanel.SetActive(true);
        GameManager.Instance.isStart = false;
        //obje patlat
    }
    private void WrongObjectFunc(GameObject obj)
    {
        //obje patlat
        RandomSystem.Instance.ObjectPoolAdd(obj, RandomSystem.Instance.ObjectList);
    }
}
