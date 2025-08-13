using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    public UIManager UIManager;

    private int totalGems;
    private int gemsLeftToCollect = 0;

    public static GameManager Instance;
    //public AudioManager AudioManager;
    public InputManager InputManager { get; private set; }

    [Header("Dynamic Game Object")]
    [SerializeField] private GameObject bossDoor;
    [SerializeField] private PlayerBehavior player;
    [SerializeField] private BossBehaviour boss;
    [SerializeField] private BossFightTrigger bossFightTrigger;

    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;

        totalGems = FindObjectsOfType<CollectableGem>().Length;
        gemsLeftToCollect = totalGems;

        UIManager.UpdateGemsLeftText(totalGems, gemsLeftToCollect);
        InputManager = new InputManager();

        bossFightTrigger.OnPlayerEnterBossFight += ActivateBossBehaviour;
    }

    public void UpdateGemsLeft()
    {
        gemsLeftToCollect--;
        UIManager.UpdateGemsLeftText(totalGems, gemsLeftToCollect);
        CheckAllGemsCollected();
    }

    private void CheckAllGemsCollected()
    {
        if(gemsLeftToCollect <= 0)
        {
            //bossDoor.SetActive(false);
            Destroy(bossDoor);
        }
    }

    private void ActivateBossBehaviour()
    {
        boss.StartChasing();
    }

    public PlayerBehavior GetPlayer()
    {
        return player;
    }
}
