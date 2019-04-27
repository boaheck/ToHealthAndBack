using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelSnap : MonoBehaviour
{
    public int ppu = 16;
    void LateUpdate(){
        Vector3 pPos = transform.parent.position;
        transform.position = new Vector3(Mathf.Round(pPos.x*ppu)/ppu, Mathf.Round(pPos.y*ppu)/ppu, transform.position.z);
    }
}
