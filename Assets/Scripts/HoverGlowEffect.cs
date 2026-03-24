using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HoverGlowEffect : MonoBehaviour
{
    private XRBaseInteractable _interactable;
    
    [Header("Visual References")]
    [SerializeField] private MeshRenderer visualRenderer;
    
    [Header("Materials")]
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material glowingMaterial;

    private void Awake()
    {
        _interactable = GetComponent<XRBaseInteractable>();
    }

    private void OnEnable()
    {
        _interactable.hoverEntered.AddListener(OnHoverEnter);
        _interactable.hoverExited.AddListener(OnHoverExit);
    }

    private void OnDisable()
    {
        _interactable.hoverEntered.RemoveListener(OnHoverEnter);
        _interactable.hoverExited.RemoveListener(OnHoverExit);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (visualRenderer != null && glowingMaterial != null)
        {
            visualRenderer.material = glowingMaterial;
        }
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        if (visualRenderer != null && defaultMaterial != null)
        {
            visualRenderer.material = defaultMaterial;
        }
    }
}