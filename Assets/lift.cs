using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lift : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 posA;
    private Vector3 posB;
    
    public Transform startPosition;
    public Transform endPosition;
    public float lerpTime = 1f;
    private float currentLerpTime;
    private bool isLerping = true;
    private bool isGoingToEndPosition = true;

    void Update()
    {
        if (isLerping)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float perc = currentLerpTime / lerpTime;
            if (isGoingToEndPosition)
            {
                transform.position = Vector3.Lerp(startPosition.position, endPosition.position, perc);
            }
            else
            {
                transform.position = Vector3.Lerp(endPosition.position, startPosition.position, perc);
            }

            if (perc == 1)
            {
                isLerping = false;
                isGoingToEndPosition = !isGoingToEndPosition;
            }
        }
    }
    void Start()
    {

        
        
        
    }

    // Update is called once per frame
    /*void Update()
    {
        if (transform.position.y == -3f)
        {
            transform.position = Vector3.Lerp(posA,posB,20*Time.deltaTime);
        }
        else if (transform.position.y == 3f)
        {
            transform.position = Vector3.Lerp(posB,posA,20*Time.deltaTime);
        }
        
    }*/
}
