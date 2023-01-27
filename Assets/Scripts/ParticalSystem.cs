using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystem : MonoBehaviour
{
    [SerializeField] int _OPTrueParticalCount, _OPWrongParticalCount;
    [SerializeField] int _trueParticalTime, _wrongParticalTime;

    public IEnumerator CallTruePartical(GameObject pos)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPTrueParticalCount);
        obj.transform.position = pos.transform.position;
        yield return new WaitForSeconds(_trueParticalTime);
        ObjectPool.Instance.AddObject(_OPTrueParticalCount, obj);
    }

    public IEnumerator CallWrongPartical(GameObject pos)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPWrongParticalCount);
        obj.transform.position = pos.transform.position;
        yield return new WaitForSeconds(_wrongParticalTime);
        ObjectPool.Instance.AddObject(_OPWrongParticalCount, obj);
    }
}
