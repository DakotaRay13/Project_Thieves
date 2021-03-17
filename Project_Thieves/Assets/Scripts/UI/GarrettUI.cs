using UnityEngine;
using UnityEngine.UI;

public class GarrettUI : MonoBehaviour
{
    public Player_Garrett garrett;

    public Image healthBar;
    public Image ammoBar;

    public Text healthText;
    public Text ammoText;

    float minPos = -221f;
    float maxPos = 181.5f;

    // Start is called before the first frame update
    void Start()
    {
        garrett = FindObjectOfType<Player_Garrett>();
    }

    // Update is called once per frame
    void Update()
    {
        //healthBar.transform.localScale = new Vector3(Mathf.Clamp((float)garrett.HEALTH / garrett.MAX_HEALTH, 0, 1),
        //    healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        //ammoBar.transform.localScale = new Vector3(Mathf.Clamp((float)garrett.AMMO / garrett.MAX_AMMO, 0, 1),
        //    ammoBar.transform.localScale.y, ammoBar.transform.localScale.z);

        float currentHealth = 1f - Mathf.Clamp((float)garrett.HEALTH / garrett.MAX_HEALTH, 0, 1);
        float currentAmmo = 1f - Mathf.Clamp((float)garrett.AMMO / garrett.MAX_AMMO, 0, 1);

        float newHealthPos = NewBarPos(currentHealth);
        float newAmmoPos = NewBarPos(currentAmmo);

        healthBar.GetComponent<RectTransform>().position = new Vector2(newHealthPos, healthBar.GetComponent<RectTransform>().position.y);
        ammoBar.GetComponent<RectTransform>().position = new Vector2(newAmmoPos, ammoBar.GetComponent<RectTransform>().position.y);

        healthText.text = garrett.HEALTH.ToString();
        ammoText.text = garrett.AMMO.ToString();
    }

    float NewBarPos(float currPercent)
    {
        return maxPos - (currPercent * (maxPos - minPos));
    }
}
