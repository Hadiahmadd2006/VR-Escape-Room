using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CardSocketDoor : MonoBehaviour
{
    private XRSocketInteractor _socketInteractor;

    [Header("Door Settings")]
    [SerializeField] private Animator doorAnimator;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip cardInsertSound;

    private void Awake()
    {
        _socketInteractor = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        _socketInteractor.selectEntered.AddListener(OnCardInserted);
    }

    private void OnDisable()
    {
        _socketInteractor.selectEntered.RemoveListener(OnCardInserted);
    }

    private void OnCardInserted(SelectEnterEventArgs args)
    {
        if (audioSource != null && cardInsertSound != null)
        {
            audioSource.PlayOneShot(cardInsertSound);
        }

        doorAnimator.SetTrigger("Open");

        Debug.Log("Keycard inserted! Door opening...");

        _socketInteractor.selectEntered.RemoveListener(OnCardInserted);
    }
}