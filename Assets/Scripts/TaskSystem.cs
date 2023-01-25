using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskSystem : MonoSingleton<TaskSystem>
{
    public List<int> ObjectTypeList = new List<int>();
    public List<int> ObjectCountList = new List<int>();
    public List<int> ObjectMaterialList = new List<int>();
    public List<bool> ObjectBoolList = new List<bool>();

    public List<Image> templateImagePos = new List<Image>();

    public void TaskStart()
    {
        SelectTaskList(ItemData.Instance.field.taskObjectTypeCount, MateraiSystem.Instance.ObjectMateral.Count, ItemData.Instance.field.ObjectTypeCount, ItemData.Instance.field.taskObjectTypeCount, ObjectMaterialList, ObjectTypeList, ObjectCountList, ObjectBoolList);
    }

    public bool CheckFinish()
    {
        bool isFinish = true;
        for (int i = 0; i < ObjectBoolList.Count; i++)
        {
            if (!ObjectBoolList[i])
                isFinish = false;
        }
        return isFinish;
    }

    public Image CallImage(Image freeImage, int taskCount)
    {
        Image tempImage = freeImage;
        tempImage.sprite = MateraiSystem.Instance.objectTemp2D[ObjectTypeList[taskCount]];
        Material mat = new Material(MateraiSystem.Instance.Mat2D.shader);
        tempImage.material = mat;
        tempImage.material.color = MateraiSystem.Instance.ObjectMateral[ObjectMaterialList[taskCount]].color;
        return tempImage;
    }

    public Image CallWrong(Image freeImage, Sprite freeSprite, Material RedMat)
    {
        Image tempImage = freeImage;
        tempImage.sprite = freeSprite;
        Material mat = new Material(MateraiSystem.Instance.Mat2D.shader);
        tempImage.material = mat;
        tempImage.color = RedMat.color;
        return tempImage;
    }

    private void SelectTaskList(int taskCount, int materialMaxCount, int typeMaxCount, int objectCountMaxCount, List<int> ObjectMaterialList, List<int> ObjectTypeList, List<int> ObjectTypeCountList, List<bool> ObjectBoolList)
    {
        for (int i = 0; i < taskCount; i++)
        {
            ObjectMaterialList.Add(Random.Range(0, materialMaxCount));
            ObjectTypeList.Add(Random.Range(0, typeMaxCount));
            ObjectTypeCountList.Add(Random.Range(1, objectCountMaxCount));
            ObjectBoolList.Add(false);
            templateImagePos[i].gameObject.SetActive(true);
            templateImagePos[i].sprite = MateraiSystem.Instance.objectTemp2D[ObjectTypeList[i]];
            Material mat = new Material(MateraiSystem.Instance.Mat2D.shader);
            templateImagePos[i].material = mat;
            templateImagePos[i].material.color = MateraiSystem.Instance.ObjectMateral[ObjectMaterialList[i]].color;
            templateImagePos[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = ObjectTypeCountList[i].ToString();
        }
    }
}
