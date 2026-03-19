using UnityEngine;
public class PuzzleGateCheck : MonoBehaviour
{
    [Header("Gate Settings")]
    [SerializeField] private int requiredFloor = 1;
    [SerializeField] private GameObject warningUI;
    private void Awake() { if (warningUI != null) warningUI.SetActive(false); }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!GameProgressManager.Instance.IsFloorComplete(requiredFloor) && warningUI != null)
            warningUI.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (warningUI != null) warningUI.SetActive(false);
    }
}