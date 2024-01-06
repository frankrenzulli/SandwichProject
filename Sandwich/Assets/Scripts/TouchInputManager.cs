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
    [SerializeField] private Transform objectToMove;
    [SerializeField] private Ingredient ingredientScript;
    [SerializeField] AudioSource audioSource;
    public float timer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
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
                        ingredientScript = objectToMove.GetComponent<Ingredient>();
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
        
        if (ingredientScript.CheckIngredientNearObjectToMove(directionJump))
        {
            Undo.Instance.AddState(objectToMove.gameObject, objectToMove.transform.position, objectToMove.transform.rotation.eulerAngles);
            isRotating = true;
            //Domanda: perchè devo dividere per 10?
            targetPosition = objectToMove.position + directionJump + new Vector3(0f,(ingredientScript.GetParentScale(directionJump) * 0.1f),0f);
            targetRotation = desiredRotation;
            objectToMove.DOJump(targetPosition, 1, 1, 0.5f, false);
            objectToMove.DORotate(targetRotation, 0.5f, RotateMode.WorldAxisAdd);
            audioSource.Play();
            
            
        }
        else
        {
            Camera.main.DOShakePosition(.5f, .1f, 50, 2, true);
            return;
        }
        
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


}
