using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTouch : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (transform.GetChild(transform.childCount - 1).GetComponent<CubeSeen>().seen)
        {
            ObjectID objectID = GetComponent<ObjectID>();
            TaskSystem taskSystem = TaskSystem.Instance;
            bool trueObject = false;

            for (int i = 0; i < TaskSystem.Instance.ObjectMaterialList.Count; i++)
            {
                if (objectID.objectID == taskSystem.ObjectTypeList[i] && objectID.materialCount == taskSystem.ObjectMaterialList[i] && GetComponent<BoxCollider>().enabled)
                {
                    GetComponent<BoxCollider>().enabled = false;
                    StartCoroutine(AddedObject.Instance.StartSlalom(taskSystem.ObjectTypeList[i], taskSystem.ObjectMaterialList[i], i, this));
                    trueObject = true;
                }
            }

            if (!trueObject)
                WrongObjectFunc(this.gameObject);
        }
    }


    public void ItemDown(int taskCount)
    {
        print(1);
        TaskSystem taskSystem = TaskSystem.Instance;

        taskSystem.ObjectCountList[taskCount]--;
        taskSystem.templateImagePos[taskCount].gameObject.GetComponentInChildren<Text>().text = taskSystem.ObjectCountList[taskCount].ToString();
        if (taskSystem.ObjectCountList[taskCount] == 0)
            taskSystem.ObjectBoolList[taskCount] = true;
    }

    public void WinFunc()
    {
        print(2);
        if (TaskSystem.Instance.CheckFinish())
        {
            Buttons.Instance.winPanel.SetActive(true);
            GameManager.Instance.isStart = false;
            //obje patlat
        }
    }
    public void WrongObjectFunc(GameObject obj)
    {
        print(3);
        //obje patlat
        RandomSystem.Instance.ObjectPoolAdd(obj, RandomSystem.Instance.ObjectList);
    }


}
