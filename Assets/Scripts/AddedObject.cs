using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AddedObject : MonoSingleton<AddedObject>
{
    [SerializeField] private GameObject _TempPos;
    [SerializeField] private float _slalomWaitTime;
    [SerializeField] private int _OPSpriteCount;
    [SerializeField] private GameObject _tempParent;

    public IEnumerator StartSlalom(int ID, int MaterialCount, int taskCount, ObjectTouch objectTouch)
    {
        objectTouch.ItemDown(taskCount);
        objectTouch.WinFunc();
        objectTouch.WrongObjectFunc(objectTouch.gameObject);

        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPSpriteCount);
        obj.transform.SetParent(_tempParent.transform);
        Material mat = new Material(MateraiSystem.Instance.Mat2D.shader);
        Image ýmage = obj.GetComponent<Image>();
        ýmage.sprite = MateraiSystem.Instance.objectTemp2D[ID];
        ýmage.material = mat;
        ýmage.material.color = MateraiSystem.Instance.ObjectMateral[MaterialCount].color;
        Vector2 worldFromMousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        obj.transform.position = worldFromMousePos;
        obj.transform.DOMove(_TempPos.transform.position, _slalomWaitTime);
        yield return new WaitForSeconds(_slalomWaitTime);
        ObjectPool.Instance.AddObject(_OPSpriteCount, obj);
        TaskSystem taskSystem = TaskSystem.Instance;
        taskSystem.templateImagePos[taskCount].gameObject.GetComponentInChildren<Text>().text = taskSystem.ObjectCountList[taskCount].ToString();
    }
}
