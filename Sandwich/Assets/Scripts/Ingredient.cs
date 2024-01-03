using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    float raycastDistance = 2f;
    public bool CheckJump(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            //sparo raycast sul lato che serve per controllare se c'è un ingrediente
            return true;
        }else
        {
            return false;
        }
    }

}
