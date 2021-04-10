using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public GameObject endLevelObject;

    public void OnDestroy()
    {
        endLevelObject.SetActive(true);
    }
}
