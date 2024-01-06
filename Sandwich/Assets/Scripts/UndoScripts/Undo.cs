using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Undo : MonoBehaviour
{
    private static Undo instance;

    public List<GameObject> objectToUndo = new List<GameObject>();
    public List<Vector3> positionToUndo = new List<Vector3>();
    public List<Vector3> rotationToUndo = new List<Vector3>();

    public int index = 0;


    public static Undo Instance
    {
        get
        {
            if (instance == null)
            {
                // Se l'istanza non esiste, crea una nuova istanza
                GameObject go = new GameObject("UndoManager");
                instance = go.AddComponent<Undo>();
            }
            return instance;
        }
    }

    public void AddState(GameObject objectToRecord, Vector3 positionToRecord, Vector3 RotationToRecord)
    {
        objectToUndo.Add(objectToRecord);
        positionToUndo.Add(positionToRecord);
        rotationToUndo.Add(RotationToRecord);
    }

    public void UndoMove()
    {
        if (GameManager.Instance.gamewin == false)
        {
            index = objectToUndo.Count - 1;

            objectToUndo[index].transform.parent = null;
            objectToUndo[index].gameObject.transform.DOJump(positionToUndo[index], 1, 1, 0.5f, false);
            objectToUndo[index].gameObject.transform.DORotate(rotationToUndo[index], 0.5f, RotateMode.WorldAxisAdd);


            objectToUndo.RemoveAt(index);
            positionToUndo.RemoveAt(index);
            rotationToUndo.RemoveAt(index);
        }
    }

}
