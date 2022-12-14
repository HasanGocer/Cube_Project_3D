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
                BackTotheMove(MoveToPlayer.Instance.player, this.gameObject);
        }
    }


    public void ItemDown(int taskCount)
    {
        TaskSystem taskSystem = TaskSystem.Instance;

        taskSystem.ObjectCountList[taskCount]--;
        taskSystem.templateImagePos[taskCount].gameObject.GetComponentInChildren<Text>().text = taskSystem.ObjectCountList[taskCount].ToString();
        if (taskSystem.ObjectCountList[taskCount] == 0)
            taskSystem.ObjectBoolList[taskCount] = true;
    }

    private void BackTotheMove(GameObject player, GameObject obj)
    {

        float floatx;
        if (player.transform.position.x > obj.transform.position.x)
            floatx = player.transform.position.x + (player.transform.position.x - obj.transform.position.x);
        else
            floatx = player.transform.position.x - (player.transform.position.x - obj.transform.position.x);

        float floaty;
        if (player.transform.position.y > obj.transform.position.y)
            floaty = player.transform.position.y + (player.transform.position.y - obj.transform.position.y);
        else
            floaty = player.transform.position.y - (player.transform.position.y - obj.transform.position.y);


        float floatz;
        if (player.transform.position.z > obj.transform.position.z)
            floatz = player.transform.position.z + (player.transform.position.z - obj.transform.position.z);
        else
            floatz = player.transform.position.z - (player.transform.position.z - obj.transform.position.z);

        obj.transform.position = Vector3.Lerp(obj.transform.position, new Vector3(floatx, floaty, floatz), 1f);
    }

    public void WinFunc()
    {
        if (TaskSystem.Instance.CheckFinish())
        {
            RandomSystem.Instance.AllObjectClose();
            Buttons.Instance.winPanel.SetActive(true);
            GameManager.Instance.isStart = false;
            //obje patlat
        }
    }
    public void WrongObjectFunc(GameObject obj)
    {
        //obje patlat
        RandomSystem.Instance.ObjectPoolAdd(obj, RandomSystem.Instance.ObjectList);
    }


}
