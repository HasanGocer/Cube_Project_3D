using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AddedObject : MonoSingleton<AddedObject>
{
    public void StartSlalom(int taskCount, ObjectTouch objectTouch)
    {
        ViewTaskSystem.Instance.CallCheckedTask(taskCount);
        objectTouch.ItemDown(taskCount);
        objectTouch.WinFunc();
        objectTouch.WrongObjectFunc(objectTouch.gameObject);
    }
}
