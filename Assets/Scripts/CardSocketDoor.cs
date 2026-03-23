using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CardSocketDoor : MonoBehaviour
{
    private XRSocketInteractor _socketInteractor;
    
    [Header("Door Settings")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private int puzzleNumberToSolve = 1; 
    
    [Header("Targets")]
    [SerializeField] private GameObject blockerToRemove; 
    
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip cardInsertSound;

    private void Awake()
    {
        _socketInteractor = GetComponent<XRSocketInteractor>();
        
        if (_socketInteractor == null)
        {
            Debug.LogError("CardSocketDoor script is missing an XRSocketInteractor on the same GameObject!", this);
        }
    }

    private void OnEnable()
    {
        if (_socketInteractor != null)
        {
            _socketInteractor.selectEntered.AddListener(OnCardInserted);
        }
    }

    private void OnDisable()
    {
        if (_socketInteractor != null)
        {
            _socketInteractor.selectEntered.RemoveListener(OnCardInserted);
        }
    }

    private void OnCardInserted(SelectEnterEventArgs args)
    {
        if (audioSource != null && cardInsertSound != null)
            audioSource.PlayOneShot(cardInsertSound);
            
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("IsIdle", false);
            doorAnimator.SetTrigger("Open");
        }

        if (blockerToRemove != null)
        {
            blockerToRemove.SetActive(false);
        }
        
        Debug.Log($"Keycard inserted! Puzzle {puzzleNumberToSolve} solved.");
        
        if (GameProgressManager.Instance != null)
        {
            GameProgressManager.Instance.SolvePuzzle(puzzleNumberToSolve);
        }
        else
        {
            Debug.LogError("Wait! There is no GameProgressManager in the scene to record this puzzle!");
        }
        
        if (_socketInteractor != null)
        {
            _socketInteractor.selectEntered.RemoveListener(OnCardInserted);
        }
    }
}