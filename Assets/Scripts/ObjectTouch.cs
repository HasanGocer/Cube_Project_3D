using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hide"))
        {
            ObjectID objectID = other.GetComponent<ObjectID>();
            TaskSystem taskSystem = TaskSystem.Instance;
            bool trueObject = false;

            for (int i = 0; i < TaskSystem.Instance.ObjectMaterialList.Count; i++)
            {
                if (objectID.objectID == taskSystem.ObjectTypeList[i] && objectID.materialCount == taskSystem.ObjectMaterialList[i])
                {
                    ItemDown(i);
                    trueObject = true;
                    WinFunc();
                }
            }

            if (!trueObject)
                WrongObjectFunc(other.gameObject);
        }
    }

    private void ItemDown(int taskCount)
    {
        TaskSystem taskSystem = TaskSystem.Instance;

        taskSystem.ObjectCountList[taskCount]--;
        if (taskSystem.ObjectCountList[taskCount] == 0)
            taskSystem.ObjectBoolList[taskCount] = true;
    }

    private void WinFunc()
    {
        if (TaskSystem.Instance.CheckFinish())
        {
            Buttons.Instance.winPanel.SetActive(true);
            GameManager.Instance.isStart = false;
            //obje patlat
        }
    }
    private void WrongObjectFunc(GameObject obj)
    {
        //obje patlat
        RandomSystem.Instance.ObjectPoolAdd(obj, RandomSystem.Instance.ObjectList);
    }
}
