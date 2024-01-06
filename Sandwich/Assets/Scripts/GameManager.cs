using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public GameObject winPanel;
    public bool gamewin;
    [SerializeField] GameObject sandwich;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    public void GetSandwich(GameObject Sandwich)
    {
        sandwich = Sandwich;
        if (sandwich.tag == "Bread" && sandwich.transform.GetChild(sandwich.transform.childCount - 1).tag == "Bread")
        {
            gamewin = true;
            StartCoroutine(WinGameWithDelay(2f));
        }
    }

    private IEnumerator WinGameWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        WinGame();
    }

    public void WinGame()
    {
        
        winPanel.SetActive(true);
        
    }
    public void CloseGame()
    {

        Application.Quit();
    }
}