using UnityEngine;

public class ReactorWinOnEnable : MonoBehaviour
{
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject winButton;

    private bool _triggered;

    private void OnEnable()
    {
        if (_triggered) return;

        if (winUI != null) winUI.SetActive(true);
        if (winButton != null) winButton.SetActive(true);

        Debug.Log("GAME FINISHED: Victory achieved.");
        _triggered = true;
    }
}