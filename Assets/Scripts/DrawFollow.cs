using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody), typeof(LineRenderer))]

public class DrawFollow : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] LineRenderer lr;
    public float timeForNextRay;
    public List<GameObject> wayPoints;
    float timer = 0;
    int wayIndex;
    bool move;
    bool touchPlane;
    bool touchStartedOnPlayer;
    private bool touch;
    Touch touchFeyz;

    void Start()
    {
        lr.enabled = false;
        wayIndex = 1;
        move = false;
        touchStartedOnPlayer = false;
    }

    private void OnMouseDown()
    {
        rb.isKinematic = false;
        lr.enabled = true;
        touchStartedOnPlayer = true;
        lr.positionCount = 1;
        lr.SetPosition(0, transform.position);
        touch = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.touchCount > 0 && GameManager.Instance.isStart)
        {

            touchFeyz = Input.GetTouch(0);
            switch (touchFeyz.phase)
            {
                case TouchPhase.Moved:
                    {
                        if (timer > timeForNextRay && touchStartedOnPlayer && touch)
                        {
                            Vector3 worldFromMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
                            Vector3 direction = worldFromMousePos - Camera.main.transform.position;
                            RaycastHit hit;
                            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
                            {
                                touchPlane = true;
                                GameObject newWayPoint = new GameObject("WayPoint");
                                newWayPoint.transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                                wayPoints.Add(newWayPoint);
                                lr.positionCount = wayIndex + 1;
                                lr.SetPosition(wayIndex, newWayPoint.transform.position);
                                timer = 0;
                                wayIndex++;
                            }
                        }
                        break;
                    }
                case TouchPhase.Ended:
                    {
                        if (touchPlane == true)
                        {
                            touchStartedOnPlayer = false;
                            move = true;
                            lr.enabled = false;
                        }
                        break;
                    }
            }
        }

        if (move && GameManager.Instance.isStart)
        {
            StartCoroutine(FinishMove());
        }
    }

    IEnumerator FinishMove()
    {
        move = false;
        for (int currentWaypoint = 0; currentWaypoint < wayPoints.Count;)
        {
            transform.DOMove(wayPoints[currentWaypoint].transform.position, 0.05f);
            yield return new WaitForSeconds(0.1f);
            currentWaypoint++;

            if (currentWaypoint == wayPoints.Count)
            {
                foreach (var item in wayPoints)
                    Destroy(item);

                wayPoints.Clear();
                wayIndex = 1;
                currentWaypoint = 0;
            }
            touch = false;
        }
        rb.isKinematic = true;
    }
}
