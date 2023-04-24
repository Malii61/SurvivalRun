using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fill;
    public void UpdateHealthBar(int health, int maxHealth)
    {
        fill.fillAmount = (float)health / maxHealth;
        gameObject.SetActive(fill.fillAmount != 1);
    }
}
