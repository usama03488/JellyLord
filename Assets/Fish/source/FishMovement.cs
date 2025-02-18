using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    Animator Fishanimator;
    public Transform centerPoint;
    public float radius;
    public float speed;
    public float rotationSpeed;
    public float waitTime;
    private float timeSinceReachedTarget;
    Vector3 targetPosition;

    private void Start()
    {
        Fishanimator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        Fishanimator.SetBool("IsSwimming",true);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            timeSinceReachedTarget += Time.deltaTime;
            if (timeSinceReachedTarget >= waitTime)
            {
                timeSinceReachedTarget = 0f;
                Vector2 randomPoint = Random.insideUnitCircle * radius;
                targetPosition = centerPoint.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //head.rotation = Quaternion.Slerp(head.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }


    }
}
