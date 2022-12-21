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
                    case TouchPhase.Began:

                        break;

                    case TouchPhase.Moved:
                        //moveCollider.enabled = true;
                        Vector3 worldFromMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
                        Vector3 direction = (worldFromMousePos - Camera.main.transform.position);
                        RaycastHit hit;
                        if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 50f))
                        {
                            print(hit.transform.name);
                            GameObject newWayPoint = new GameObject("WayPoint");
                            newWayPoint.transform.position = new Vector3(hit.point.x, hit.point.y, lightGO.transform.position.z);
                            lightGO.transform.position = newWayPoint.transform.position;
                            Debug.DrawLine(Camera.main.transform.position, direction, Color.red, 1f);
                        }
                        break;
                    case TouchPhase.Ended:
                        //moveCollider.enabled = false;
                        break;
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return null;
        }
    }
}
