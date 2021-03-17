using UnityEngine;
using UnityEngine.UI;

public class AlexUI : MonoBehaviour
{
    public Player_Alex alex;
    public Image healthBar;
    public Image staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        alex = FindObjectOfType<Player_Alex>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(alex.HEALTH / alex.MAX_HEALTH, 0, 1),
            healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

}
