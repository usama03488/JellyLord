using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightAdjust : MonoBehaviour
{
   public void AdjustHeight()
   {
      transform.position = Vector3.Lerp(transform.position,
         new Vector3(transform.position.x, 1.13f, transform.position.z), 0.2f);
   }
}
