using UnityEngine;

public class Loose : MonoBehaviour
{
    [SerializeField] private GameObject defeatScreen;
    private void Start()
    {
        foreach (PlayerStats c in PlayerStatsManager.Instance.Characters)
        {
            c.OnDeath.AddListener(CheckAlivePlayers);
        }
    }

    private void CheckAlivePlayers()
    {
        foreach (PlayerStats c in PlayerStatsManager.Instance.Characters)
        {
            if (c.CurrentHealth > 0)
            {
                return;
            }
        }

        Defeat();
    }

    private void Defeat()
    {
        defeatScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
