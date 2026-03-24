using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ReactorCoreManager : MonoBehaviour
{
    [Header("The Sockets")]
    [SerializeField] private XRSocketInteractor socket1;
    [SerializeField] private XRSocketInteractor socket2;
    [SerializeField] private XRSocketInteractor socket3;
    [SerializeField] private XRSocketInteractor socket4;

    [Header("The Correct Blocks (The Answer Key)")]
    [SerializeField] private XRGrabInteractable correctBlock1;
    [SerializeField] private XRGrabInteractable correctBlock2;
    [SerializeField] private XRGrabInteractable correctBlock3;
    [SerializeField] private XRGrabInteractable correctBlock4;

    [Header("Hidden Completion Objects")]
    [SerializeField] private GameObject physicalButton; 
    [SerializeField] private GameObject successSpotlight; 

    [Header("Win State UI")]
    [SerializeField] private GameObject winUI; 

    private bool _combinationCorrect = false;

    void Start()
    {
        if (physicalButton) physicalButton.SetActive(false);
        if (successSpotlight) successSpotlight.SetActive(false);
        if (winUI) winUI.SetActive(false);
    }

    public void CheckBlockCombination()
    {
        if (_combinationCorrect) return;

        if (IsCorrect(socket1, correctBlock1) &&
            IsCorrect(socket2, correctBlock2) &&
            IsCorrect(socket3, correctBlock3) &&
            IsCorrect(socket4, correctBlock4))
        {
            _combinationCorrect = true;
            Debug.Log("Blocks Correct! Reveal the final button.");
            
            if (physicalButton) physicalButton.SetActive(true);
            if (successSpotlight) successSpotlight.SetActive(true);
        }
    }

    public void TriggerFinalWin()
    {
        if (!_combinationCorrect) return;

        if (winUI) winUI.SetActive(true);

        Debug.Log("GAME OVER: Victory achieved.");
    }

    private bool IsCorrect(XRSocketInteractor socket, XRGrabInteractable expectedBlock)
    {
        return socket.hasSelection && socket.firstInteractableSelected != null && 
               socket.firstInteractableSelected.transform.gameObject == expectedBlock.gameObject;
    }
}