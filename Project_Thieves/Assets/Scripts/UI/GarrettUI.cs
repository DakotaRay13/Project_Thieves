using UnityEngine;
using UnityEngine.UI;

public class GarrettUI : MonoBehaviour
{
    public Player_Garrett garrett;
    public Image healthBar;
    public Image ammoBar;

    // Start is called before the first frame update
    void Start()
    {
        garrett = FindObjectOfType<Player_Garrett>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(garrett.HEALTH / garrett.MAX_HEALTH, 0, 1),
            healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        ammoBar.transform.localScale = new Vector3(Mathf.Clamp(garrett.AMMO / garrett.MAX_AMMO, 0, 1),
            ammoBar.transform.localScale.y, ammoBar.transform.localScale.z);
    }

}
