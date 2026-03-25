using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class FusePuzzleManager : MonoBehaviour
{
    [Header("Sockets (assign exact XRSocketInteractors)")]
    [SerializeField] private XRSocketInteractor redSocket;
    [SerializeField] private XRSocketInteractor blueSocket;
    [SerializeField] private XRSocketInteractor greenSocket;

    [Header("Targets")]
    [SerializeField] private GameObject floor3ReactorPuzzle;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip insertSound;
    [SerializeField] private AudioClip removeSound;
    [SerializeField] private AudioClip solvedSound;

    private bool _redPlaced;
    private bool _bluePlaced;
    private bool _greenPlaced;
    private bool _solved;

    private UnityAction<SelectEnterEventArgs> _redEnter;
    private UnityAction<SelectExitEventArgs> _redExit;
    private UnityAction<SelectEnterEventArgs> _blueEnter;
    private UnityAction<SelectExitEventArgs> _blueExit;
    private UnityAction<SelectEnterEventArgs> _greenEnter;
    private UnityAction<SelectExitEventArgs> _greenExit;

    private void Awake()
    {
        _redPlaced = false;
        _bluePlaced = false;
        _greenPlaced = false;
        _solved = false;

        if (floor3ReactorPuzzle != null)
            floor3ReactorPuzzle.SetActive(false);

        _redEnter = (args) => HandleFuseInserted(Color.Red, args);
        _redExit = (args) => HandleFuseRemoved(Color.Red, args);
        _blueEnter = (args) => HandleFuseInserted(Color.Blue, args);
        _blueExit = (args) => HandleFuseRemoved(Color.Blue, args);
        _greenEnter = (args) => HandleFuseInserted(Color.Green, args);
        _greenExit = (args) => HandleFuseRemoved(Color.Green, args);
    }

    private void OnEnable()
    {
        if (redSocket != null)
        {
            redSocket.selectEntered.AddListener(_redEnter);
            redSocket.selectExited.AddListener(_redExit);
        }
        if (blueSocket != null)
        {
            blueSocket.selectEntered.AddListener(_blueEnter);
            blueSocket.selectExited.AddListener(_blueExit);
        }
        if (greenSocket != null)
        {
            greenSocket.selectEntered.AddListener(_greenEnter);
            greenSocket.selectExited.AddListener(_greenExit);
        }
    }

    private void OnDisable()
    {
        if (redSocket != null)
        {
            redSocket.selectEntered.RemoveListener(_redEnter);
            redSocket.selectExited.RemoveListener(_redExit);
        }
        if (blueSocket != null)
        {
            blueSocket.selectEntered.RemoveListener(_blueEnter);
            blueSocket.selectExited.RemoveListener(_blueExit);
        }
        if (greenSocket != null)
        {
            greenSocket.selectEntered.RemoveListener(_greenEnter);
            greenSocket.selectExited.RemoveListener(_greenExit);
        }
    }

    private enum Color { Red, Blue, Green }

    private void HandleFuseInserted(Color color, SelectEnterEventArgs args)
    {
        if (_solved) return;

        switch (color)
        {
            case Color.Red: _redPlaced = true; break;
            case Color.Blue: _bluePlaced = true; break;
            case Color.Green: _greenPlaced = true; break;
        }

        if (audioSource != null && insertSound != null) audioSource.PlayOneShot(insertSound);

        CheckAllFuses();
    }

    private void HandleFuseRemoved(Color color, SelectExitEventArgs args)
    {
        if (_solved) return;

        switch (color)
        {
            case Color.Red: _redPlaced = false; break;
            case Color.Blue: _bluePlaced = false; break;
            case Color.Green: _greenPlaced = false; break;
        }

        if (audioSource != null && removeSound != null) audioSource.PlayOneShot(removeSound);
    }

    private void CheckAllFuses()
    {
        if (_solved) return;

        if (_redPlaced && _bluePlaced && _greenPlaced)
        {
            _solved = true;

            if (audioSource != null && solvedSound != null) audioSource.PlayOneShot(solvedSound);

            if (floor3ReactorPuzzle != null) floor3ReactorPuzzle.SetActive(true);

            if (GameProgressManager.Instance != null) GameProgressManager.Instance.SolvePuzzle(4);
        }
    }
}