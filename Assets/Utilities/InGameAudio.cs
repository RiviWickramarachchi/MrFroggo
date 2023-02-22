using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameAudio : MonoBehaviour
{
    [SerializeField] AudioSource btnClick;
    public void ButtonClickSound() {
        btnClick.Play();
    }
}
