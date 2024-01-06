using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class Data
{
    public Vector3 position;
    public Vector3 rotation;
    public GameObject parent;
    public Data(Vector3 v1, Vector3 v2, GameObject v3)
    {
        position = v1;
        rotation = v2;
        parent = v3;
    }
}
public class UndoSystem : MonoBehaviour
{
    public Stack<Data> previousState = new Stack<Data>();
    public List<Data> dati = new List<Data>();

    public void AddDataToStack(Vector3 position, Vector3 rotation, GameObject parent)
    {
        previousState.Push(new Data(position, rotation, parent));
        Debug.Log(dati);
    }

    
}