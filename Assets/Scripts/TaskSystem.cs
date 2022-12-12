using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSystem : MonoSingleton<TaskSystem>
{
    public List<int> ObjectTypeList = new List<int>();
    public List<int> ObjectMaterialList = new List<int>();

    public void TaskStart()
    {
        SelectTaskList(ItemData.Instance.field.taskObjectCount, MateraiSystem.Instance.ObjectMateral.Count, ItemData.Instance.field.ObjectTypeCount, ObjectMaterialList, ObjectTypeList);
    }

    private void SelectTaskList(int taskCount, int materialMaxCount, int typeMaxCount, List<int> ObjectMaterialList, List<int> ObjectTypeList)
    {
        for (int i = 0; i < taskCount; i++)
        {
            ObjectMaterialList.Add(Random.Range(0, materialMaxCount));
            ObjectTypeList.Add(Random.Range(0, typeMaxCount));
        }
    }
}
