using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushCube : MonoSingleton<CrushCube>
{
    public int OPCubeCrashCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            GridSystem.Instance.CubeAddObjectPool(other.gameObject);
            StartCoroutine(CallPartical(other.gameObject, other.GetComponent<CubeID>(), OPCubeCrashCount, GridSystem.Instance.particalWaitTime));
        }
    }

    public IEnumerator CallPartical(GameObject obj, CubeID cubeID, int OPCubeCrashCount, float particalWaitTime)
    {
        ObjectPool objectPool = ObjectPool.Instance;
        GameObject partical = objectPool.GetPooledObject(OPCubeCrashCount);
        partical.GetComponent<Renderer>().material = MaterialSystem.Instance.CubeMaterial[cubeID.cubeCount];
        partical.transform.position = obj.transform.position;
        yield return new WaitForSeconds(particalWaitTime);
        objectPool.AddObject(OPCubeCrashCount, partical);
    }
}
