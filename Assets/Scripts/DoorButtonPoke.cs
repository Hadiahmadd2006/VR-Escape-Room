using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorButtonPoke : MonoBehaviour
{
    private XRSimpleInteractable _simpleInteractable;

    [Header("Door Settings")]
    [SerializeField] private Animator doorAnimator;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonPressSound;

    private void Awake()
    {
        _simpleInteractable = GetComponent<XRSimpleInteractable>();
    }

    private void OnEnable()
    {
        _simpleInteractable.selectEntered.AddListener(OnButtonPressed);
    }

    private void OnDisable()
    {
        _simpleInteractable.selectEntered.RemoveListener(OnButtonPressed);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        if (audioSource != null && buttonPressSound != null)
        {
            audioSource.PlayOneShot(buttonPressSound);
        }

        doorAnimator.SetTrigger("Open");

        Debug.Log("Door button pressed! Door opening...");

        _simpleInteractable.selectEntered.RemoveListener(OnButtonPressed);
    }
}