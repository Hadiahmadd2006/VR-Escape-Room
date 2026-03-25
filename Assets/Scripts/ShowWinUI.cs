using UnityEngine;

public class ShowWinUI : MonoBehaviour
{
    [SerializeField] private GameObject winUI;

    public void Show()
    {
        if (winUI != null) winUI.SetActive(true);
        Debug.Log("GAME FINISHED: Victory achieved.");
    }
}