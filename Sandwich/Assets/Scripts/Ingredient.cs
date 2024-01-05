using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    Ray ray;
    public bool CheckIngredientNearObjectToMove(Vector3 desiredRayDirection)
    {
        ray = new Ray(transform.position, desiredRayDirection);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1f))
        {
            transform.SetParent(hit.transform.root);
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetParentScale(Vector3 rayDirection)
    {
        Ray ray = new Ray(transform.position, rayDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject parent = hit.collider.gameObject;
            float offsetY = SumLossyScale(parent.transform);
            return offsetY;
        }
        return 0f;
    }
    private float SumLossyScale(Transform parent)
    {
        float offsetY = parent.localScale.y;
        foreach (Transform child in parent)
        {
            offsetY += child.lossyScale.y;
            offsetY += SumLossyScale(child);
        }
        return offsetY;
    }


}
