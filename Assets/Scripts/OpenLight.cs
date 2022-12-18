using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLight : MonoBehaviour
{
    [SerializeField] GameObject lightGO;
    [SerializeField] Rigidbody rb;
    Touch touch;

    public IEnumerator LightIsThere()
    {
        while (GameManager.Instance.isStart)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        Vector3 worldFromMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
                        Vector3 direction = worldFromMousePos - Camera.main.transform.position;
                        RaycastHit hit;
                        if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
                        {
                            lightGO.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, lightGO.transform.position.z);
                        }
                        break;

                }
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
    }
}
