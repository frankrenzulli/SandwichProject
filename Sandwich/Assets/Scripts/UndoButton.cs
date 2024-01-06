using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    private void Start()
    {
        // Assicurati che il pulsante abbia un listener per l'evento di clic
        Button undoButton = GetComponent<Button>();

        if (undoButton != null)
        {
            undoButton.onClick.AddListener(UndoMoves);
        }
    }

    private void UndoMoves()
    {
        Debug.Log("Undo");
        Undo.Instance.UndoMove();
    }
}