using UnityEngine;
using UnityEngine.UI;

public class AlexUI : MonoBehaviour
{
    public Player_Alex alex;
    public Image healthBar;
    public Image staminaBar;

    float minPos = -221f;
    float maxPos = 181.5f;

    // Start is called before the first frame update
    void Start()
    {
        alex = FindObjectOfType<Player_Alex>();
    }

    // Update is called once per frame
    void Update()
    {
        //healthBar.transform.localScale = new Vector3(Mathf.Clamp((float)alex.HEALTH / alex.MAX_HEALTH, 0, 1),
        //healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        float currentHealth = 1f - Mathf.Clamp((float)alex.HEALTH / alex.MAX_HEALTH, 0, 1);
        float currentStamina = 1f - Mathf.Clamp((float)alex.STAMINA / alex.MAX_STAMINA, 0, 1);

        float newHealthPos = NewBarPos(currentHealth);
        float newStaminaPos = NewBarPos(currentStamina);

        healthBar.GetComponent<RectTransform>().position = new Vector2(newHealthPos, healthBar.GetComponent<RectTransform>().position.y);
        staminaBar.GetComponent<RectTransform>().position = new Vector2(newStaminaPos, staminaBar.GetComponent<RectTransform>().position.y);
    }

    float NewBarPos(float currPercent)
    {
        return maxPos - (currPercent * (maxPos - minPos));
    }
}
