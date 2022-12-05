using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DrawAndFollow : MonoBehaviour
{
    Rigidbody rb;
    public float timeForNextRay;
    float timer = 0;
    bool touchPlane;
    bool touchStartedOnPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        touchStartedOnPlayer = false;
    }

    private void OnMouseDown()
    {
        touchStartedOnPlayer = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && timer > timeForNextRay && touchStartedOnPlayer)
        {
            Vector3 worldFromMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
            Vector3 direction = worldFromMousePos - Camera.main.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
            {
                touchPlane = true;
                Vector3 pos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                transform.DOMove(pos, timer).SetEase(Ease.InOutSine);
                timer = 0;
            }
        }

        if (Input.GetMouseButtonUp(0) && touchPlane == true)
        {
            touchStartedOnPlayer = false;
        }
    }
}
