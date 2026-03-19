using UnityEngine;
public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance { get; private set; }
    public bool Puzzle1Complete { get; private set; }
    public bool Puzzle2Complete { get; private set; }
    public bool Puzzle3Complete { get; private set; }
    public bool Puzzle4Complete { get; private set; }
    public bool Puzzle5Complete { get; private set; }
    public bool Puzzle6Complete { get; private set; }
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip puzzleSolvedSound;
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    public void SolvePuzzle(int puzzleNumber)
    {
        switch (puzzleNumber)
        {
            case 1: Puzzle1Complete = true; break;
            case 2: Puzzle2Complete = true; break;
            case 3: Puzzle3Complete = true; break;
            case 4: Puzzle4Complete = true; break;
            case 5: Puzzle5Complete = true; break;
            case 6: Puzzle6Complete = true; break;
        }
        Debug.Log($"Puzzle {puzzleNumber} solved!");
        if (audioSource != null && puzzleSolvedSound != null)
            audioSource.PlayOneShot(puzzleSolvedSound);
    }
    public bool IsFloorComplete(int floor)
    {
        switch (floor)
        {
            case 1: return Puzzle1Complete && Puzzle2Complete;
            case 2: return Puzzle3Complete && Puzzle4Complete;
            default: return false;
        }
    }
}