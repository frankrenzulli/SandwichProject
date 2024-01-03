using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TouchInputManager : MonoBehaviour
{
    private Touch touch;
    private Vector2 startTouchPosition, endTouchPosition;

    private Vector3 targetPosition;

    private Vector3 targetRotation;

    public bool isRotating = false;

    private Transform objectToMove;

    public float timer;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        objectToMove = hit.transform.root;
                    }

                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;

                    MovingIngredient();
                    break;

                case TouchPhase.Canceled:
                    break;

                default:
                    break;
            }
        }

        Timer();
    }
    public void MovingIngredient()
    {
        float x = endTouchPosition.x - startTouchPosition.x;
        float y = endTouchPosition.y - startTouchPosition.y;

        if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
        {
            Debug.Log("Tappato fratmo");
            return;
        }

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {

            if (x > 0)
            {
                //right                           
                if (!isRotating)
                {
                    FlipIngredient(Vector3.right, new Vector3(objectToMove.rotation.x, objectToMove.rotation.y, (objectToMove.rotation.z - 180f)));
                }
            }
            else if (x < 0)
            {
                //left                           
                if (!isRotating)
                {
                    FlipIngredient(Vector3.left, new Vector3(objectToMove.rotation.x, objectToMove.rotation.y, (objectToMove.rotation.z + 180f)));
                }
            }
        }
        else if (y > 0)
        {
            //up
            if (!isRotating)
            {
                FlipIngredient(Vector3.forward, new Vector3((objectToMove.rotation.x + 180f), objectToMove.rotation.y, objectToMove.rotation.z));
            }
        }
        else if (y < 0)
        {
            //down
            if (!isRotating)
            {
                FlipIngredient(Vector3.back, new Vector3((objectToMove.rotation.x - 180f), objectToMove.rotation.y, objectToMove.rotation.z));
            }
        }
    }
    public void FlipIngredient(Vector3 directionJump, Vector3 desiredRotation)
    {
        if (CheckIngredientByObjectToMove(directionJump))
        {
            isRotating = true;
            Debug.Log(isRotating);
            targetPosition = objectToMove.position + directionJump;
            targetRotation = desiredRotation;
            objectToMove.DOJump(targetPosition, 1, 1, 0.5f, false);
            objectToMove.DORotate(targetRotation, 0.5f, RotateMode.WorldAxisAdd);
        }
        else return;
    }
    public void Timer()
    {
        if (isRotating)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isRotating = false;
                timer = 0.5f;
            }
        }
    }
    
    public bool CheckIngredientByObjectToMove(Vector3 desiredRayDirection)
    {
        Ray ray = new Ray(objectToMove.position, desiredRayDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1f))
        {
            return true;
        }
        else return false;
    }
    
}
