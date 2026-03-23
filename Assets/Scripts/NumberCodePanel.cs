using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class NumberCodePanel : MonoBehaviour
{
    [Header("Button References (assign in order 1-9)")]
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
            int digitValue = i + 1;
            digitButtons[i].selectEntered.AddListener((args) => OnDigitPressed(digitValue));
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < digitButtons.Length; i++)
            digitButtons[i].selectEntered.RemoveAllListeners();
    }
    private void OnDigitPressed(int digit)
    {
        if (_solved) return;
        if (audioSource != null && buttonBeep != null) audioSource.PlayOneShot(buttonBeep);
        _playerInput[_currentIndex] = digit;
        _currentIndex++;
        if (_currentIndex >= correctCode.Length) CheckCode();
    }
    private void CheckCode()
    {
        bool correct = true;
        for (int i = 0; i < correctCode.Length; i++)
            if (_playerInput[i] != correctCode[i]) { correct = false; break; }
        if (correct)
        {
            _solved = true;
            if (audioSource != null && correctSound != null) audioSource.PlayOneShot(correctSound);
            if (floorclosed1 != null) floorclosed1.SetActive(false);
            if (doorAnimator != null)
            {
                doorAnimator.SetBool("IsIdle", false);
                doorAnimator.SetTrigger("Open");
            }
            if (successUI != null) successUI.SetActive(true);
            GameProgressManager.Instance.SolvePuzzle(2);
        }
        else
        {
            if (audioSource != null && wrongSound != null) audioSource.PlayOneShot(wrongSound);
            _currentIndex = 0;
            _playerInput = new int[correctCode.Length];
        }
    }
}