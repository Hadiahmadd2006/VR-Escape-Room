using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class NumberCodePanel : MonoBehaviour
{
    [Header("CRITICAL: Element 0 MUST be Button 1, Element 1 MUST be Button 2, etc.")]
    [SerializeField] private XRSimpleInteractable[] digitButtons;
    
    [Header("Correct Code")]
    [SerializeField] private int[] correctCode = { 4, 7, 2 };
    
    [Header("Targets")]
    [SerializeField] private GameObject floorclosed1;
    [SerializeField] private Animator doorAnimator;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonBeep;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;
    
    [Header("Feedback")]
    [SerializeField] private GameObject successUI;
    
    private int[] _playerInput;
    private int _currentIndex;
    private bool _solved;

    private void Awake()
    {
        _playerInput = new int[correctCode.Length];
        _currentIndex = 0;
        if (successUI != null) successUI.SetActive(false);
    }

    private void OnEnable()
    {
        for (int i = 0; i < digitButtons.Length; i++)
        {
            if (digitButtons[i] != null)
            {
                int digitValue = i + 1; 
                digitButtons[i].selectEntered.RemoveAllListeners(); 
                digitButtons[i].selectEntered.AddListener((args) => OnDigitPressed(digitValue));
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < digitButtons.Length; i++)
        {
            if (digitButtons[i] != null)
                digitButtons[i].selectEntered.RemoveAllListeners();
        }
    }

    private void OnDigitPressed(int digit)
    {
        if (_solved) return;
        
        Debug.Log($"[KEYPAD] Button {digit} was pressed.");

        if (audioSource != null && buttonBeep != null) 
            audioSource.PlayOneShot(buttonBeep);

        _playerInput[_currentIndex] = digit;
        _currentIndex++;

        if (_currentIndex >= correctCode.Length) 
        {
            CheckCode();
        }
    }

    private void CheckCode()
    {
        bool isCorrect = true;
        string whatThePlayerTyped = "";

        for (int i = 0; i < correctCode.Length; i++)
        {
            whatThePlayerTyped += _playerInput[i].ToString();
            if (_playerInput[i] != correctCode[i]) 
            { 
                isCorrect = false; 
            }
        }

        Debug.Log($"[KEYPAD] Checking code... You typed: {whatThePlayerTyped}. The answer is: 472.");

        if (isCorrect)
        {
            Debug.Log("[KEYPAD] MATCH! Opening the door.");
            _solved = true;
            
            if (audioSource != null) audioSource.Stop(); 
            
            if (audioSource != null && correctSound != null) 
                audioSource.PlayOneShot(correctSound);
                
            if (floorclosed1 != null) 
                floorclosed1.SetActive(false);
                
            if (doorAnimator != null)
            {
                doorAnimator.SetBool("IsIdle", false);
                doorAnimator.SetTrigger("Open");
            }
            else
            {
                Debug.LogWarning("[KEYPAD] ERROR: The Door Animator is missing in the Inspector!");
            }

            if (successUI != null) successUI.SetActive(true);
            
            if (GameProgressManager.Instance != null)
                GameProgressManager.Instance.SolvePuzzle(2);
        }
        else
        {
            Debug.Log("[KEYPAD] WRONG CODE. Resetting the panel.");
            
            if (audioSource != null) audioSource.Stop();
            
            if (audioSource != null && wrongSound != null) 
                audioSource.PlayOneShot(wrongSound);
                
            _currentIndex = 0;
            _playerInput = new int[correctCode.Length];
        }
    }
}