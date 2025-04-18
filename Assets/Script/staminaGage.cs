using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaGage : MonoBehaviour
{
    Slider slider;
    player player;
    public GameObject pobj;
    float st;
    // Start is called before the first frame update
    void Start()
    {
        slider = transform.GetComponentInChildren<Slider>();
        player = pobj.GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        st = player.stamina;
        slider.value = st;
    }
}
