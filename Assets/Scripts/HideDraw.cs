using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDraw : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    float timeForNextRay;
    float timer = 0;
    bool touchPlane;
    public bool touchStartedOnPlayer;
    Touch touch;

    void Start()
    {
        touchStartedOnPlayer = false;
        rb.isKinematic = true;
    }

    private void OnMouseDown()
    {
        rb.isKinematic = false;
        touchStartedOnPlayer = true;
        touchPlane = true;
        StartCoroutine(Draw());
    }

    private IEnumerator Draw()
    {
        while (touchStartedOnPlayer)
        {
            timer += Time.deltaTime;
            if (Input.touchCount > 0 && GameManager.Instance.isStart)
            {
                touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        if (timer > timeForNextRay && touchStartedOnPlayer)
                        {
                            Vector3 worldFromMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
                            Vector3 direction = worldFromMousePos - Camera.main.transform.position;
                            RaycastHit hit;
                            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
                            {
                                touchPlane = true;
                                Vector3 pos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                                transform.position = Vector3.Slerp(this.transform.position, pos, 10f);
                                timer = 0;
                            }
                        }
                        break;

                    case TouchPhase.Ended:
                        if (touchPlane)
                        {
                            touchStartedOnPlayer = false;
                            touchPlane = false;
                            rb.isKinematic = true;
                        }
                        break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
