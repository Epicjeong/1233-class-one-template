using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    #region PRIVATE_VARIABLES
    private Vector2 positionCorrection = new Vector2(0, 100);
    #endregion
    #region PUBLIC_REFERENCES
    [SerializeField] private RectTransform targetCanvas;
    [SerializeField] private RectTransform healthBar;
    #endregion
    #region PUBLIC_METHODS
    public void SetHealthBarData()
    {
        healthBar = GetComponent<RectTransform>();
        healthBar.gameObject.SetActive(true);
    }
    public void OnHealthChanged(float healthFill)
    {
        healthBar.GetComponent<Image>().fillAmount = healthFill;
    }
    #endregion
}
