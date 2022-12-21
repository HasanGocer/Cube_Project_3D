using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLight : MonoSingleton<OpenLight>
{
    [SerializeField] GameObject lightGO;
    [SerializeField] Rigidbody rb;
    [SerializeField] BoxCollider moveCollider;
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
                        moveCollider.enabled = true;
                        Vector3 worldFromMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
                        Vector3 direction = worldFromMousePos - Camera.main.transform.position;
                        RaycastHit hit;
                        if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
                        {
                            Debug.Log(hit.transform.position);
                            lightGO.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, lightGO.transform.position.z);
                        }
                        break;
                    case TouchPhase.Ended:
                        moveCollider.enabled = false;
                        break;
                }
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
    }
}
