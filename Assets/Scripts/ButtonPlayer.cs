using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlayer : MonoBehaviour
{
    public Slider audioBar;
    
    public void StartBar()
    {
        audioBar = gameObject.AddComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
