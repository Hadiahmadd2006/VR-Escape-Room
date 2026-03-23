using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ColorSequencePuzzle : MonoBehaviour
{
    [Header("Color Buttons (Red=0, Blue=1, Green=2, Yellow=3)")]
    [SerializeField] private XRSimpleInteractable redButton;
    [SerializeField] private XRSimpleInteractable blueButton;
    [SerializeField] private XRSimpleInteractable greenButton;
    [SerializeField] private XRSimpleInteractable yellowButton;

    [Header("Correct Sequence")]
    [SerializeField] private int[] correctSequence = { 0, 2, 1, 3, 0 };

    [Header("Targets")]
    [SerializeField] private Animator doorAnimator; 

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;

    [Header("Feedback")]
    [SerializeField] private GameObject successUI;

    private int _currentStep;
    private bool _solved;

    private void Awake()
    {
        _currentStep = 0;
        if (successUI != null) successUI.SetActive(false);
    }

    private void OnEnable()
    {
        if (redButton != null) redButton.selectEntered.AddListener((a) => OnColorPressed(0));
        if (blueButton != null) blueButton.selectEntered.AddListener((a) => OnColorPressed(1));
        if (greenButton != null) greenButton.selectEntered.AddListener((a) => OnColorPressed(2));
        if (yellowButton != null) yellowButton.selectEntered.AddListener((a) => OnColorPressed(3));
    }

    private void OnDisable()
    {
        if (redButton != null) redButton.selectEntered.RemoveAllListeners();
        if (blueButton != null) blueButton.selectEntered.RemoveAllListeners();
        if (greenButton != null) greenButton.selectEntered.RemoveAllListeners();
        if (yellowButton != null) yellowButton.selectEntered.RemoveAllListeners();
    }

    private void OnColorPressed(int colorIndex)
    {
        if (_solved) return;
        
        if (audioSource != null && buttonSound != null) audioSource.PlayOneShot(buttonSound);
        
        if (correctSequence[_currentStep] == colorIndex)
        {
            _currentStep++;
            
            if (_currentStep >= correctSequence.Length)
            {
                _solved = true;
                if (audioSource != null && correctSound != null) audioSource.PlayOneShot(correctSound);
                
                if (doorAnimator != null)
                {
                    doorAnimator.SetBool("IsIdle", false);
                    doorAnimator.SetTrigger("Open");
                }

                if (successUI != null) successUI.SetActive(true);
                
                if (GameProgressManager.Instance != null)
                    GameProgressManager.Instance.SolvePuzzle(3);
            }
        }
        else
        {
            _currentStep = 0;
            if (audioSource != null && wrongSound != null) audioSource.PlayOneShot(wrongSound);
        }
    }
}