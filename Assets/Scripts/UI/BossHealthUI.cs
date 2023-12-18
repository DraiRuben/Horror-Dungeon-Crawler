using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{

    public Image HealthBarBoss;
    public static BossHealthUI Instance;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        HealthBarBoss.fillAmount = 1;
    }

    public void BossUpdateFill(float _fillAmount)
    {
        HealthBarBoss.fillAmount = _fillAmount;
        gameObject.SetActive(_fillAmount > 0);
    }

}
