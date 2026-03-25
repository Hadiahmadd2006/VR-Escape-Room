using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class FreezeRotationOnSocket : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socket;

    private UnityAction<SelectEnterEventArgs> _onEnter;
    private UnityAction<SelectExitEventArgs> _onExit;

    private void Awake()
    {
        if (socket == null) socket = GetComponent<XRSocketInteractor>();
        _onEnter = (args) =>
        {
            if (args.interactableObject == null) return;
            var rb = args.interactableObject.transform.GetComponent<Rigidbody>();
            if (rb != null) rb.constraints |= RigidbodyConstraints.FreezeRotation;
        };
        _onExit = (args) =>
        {
            if (args.interactableObject == null) return;
            var rb = args.interactableObject.transform.GetComponent<Rigidbody>();
            if (rb != null) rb.constraints &= ~RigidbodyConstraints.FreezeRotation;
        };
    }

    private void OnEnable()
    {
        if (socket != null)
        {
            socket.selectEntered.AddListener(_onEnter);
            socket.selectExited.AddListener(_onExit);
        }
    }

    private void OnDisable()
    {
        if (socket != null)
        {
            socket.selectEntered.RemoveListener(_onEnter);
            socket.selectExited.RemoveListener(_onExit);
        }
    }
}