using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorController : InteractObj
{
    public float ElevatorSpeed;
    public float WaitTime;
    public float EndPosition;
    public Transform StartPoint;
    public Transform ElevatorPlatform;
    public UnityEvent UpCompliteEvent;
    public UnityEvent StartUpEvent;
    public UnityEvent DownCompliteEvent;
    Transform EndPoint;
    
    private bool isDown;
    private float tempTimer;
   
    private void Start()
    {
        GameObject tempObject = new GameObject
        {
            name = "EndPoint"
        };
        tempObject.transform.SetParent(transform);
        EndPoint = tempObject.transform;
        EndPoint.position = new Vector3(StartPoint.position.x, StartPoint.position.y + EndPosition, StartPoint.position.z);
    }

    public override void OnInteractObject()
    {
        CanInteract = true;
    }

    public void StartMoveUp()
    {
        if (!isDown && CanInteract)
        {
            StartCoroutine(ElevatorUp());
        }
    }

    public IEnumerator ElevatorUp()
    {
        StartUpEvent?.Invoke();
        tempTimer = WaitTime;
        StopCoroutine(ElevatorDown());

        while (ElevatorPlatform.position.y < EndPoint.position.y-0.05f)
        {
            ElevatorPlatform.position = Vector3.Lerp(ElevatorPlatform.position, EndPoint.position, ElevatorSpeed * Time.deltaTime);
            yield return null;
        }

        while (tempTimer >0)
        {
            UpCompliteEvent?.Invoke();
            tempTimer -= Time.deltaTime;
            yield return null;
        }

        StartCoroutine(ElevatorDown());
        yield break;
    }

    public IEnumerator ElevatorDown()
    {
        isDown = true;
        while (StartPoint.position.y + 0.05f < ElevatorPlatform.position.y )
        {
            ElevatorPlatform.position = Vector3.Lerp(ElevatorPlatform.position, StartPoint.position, ElevatorSpeed * Time.deltaTime);
            yield return null;
        }
        DownCompliteEvent?.Invoke();
        isDown = false;
        yield break;
    }
    



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (StartPoint!= null && EndPoint != null)
        {
            Gizmos.DrawLine(StartPoint.position, EndPoint.position);
        }
        Gizmos.color = Color.green;
        if (StartPoint != null)
        {
            Gizmos.DrawSphere(StartPoint.position, 0.3f);
        }
        Gizmos.DrawSphere(new Vector3(StartPoint.position.x, StartPoint.position.y + EndPosition, StartPoint.position.z), 0.3f);
        Gizmos.color = new Color(1,0, 0.8411183f, 0.24f);
        //Gizmos.DrawCube(
        //    new Vector3(StartPoint.position.x, StartPoint.position.y + EndPosition/2, StartPoint.position.z),
        //    new Vector3(ElevatorPlatform.GetComponent<MeshFilter>().sharedMesh.bounds.size.x* ElevatorPlatform.localScale.z* 0.95f,
        //    EndPosition,
        //    ElevatorPlatform.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * ElevatorPlatform.localScale.z* 0.95f));
    }
}
