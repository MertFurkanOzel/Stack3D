using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kupsc : MonoBehaviour
{
    public bool hareket;
    public bool eksenx;
    int index = 0;
    bool eksi = true;
    Color32[] renkler = { new Color32(0, 0, 255, 255), new Color32(255, 0, 0, 255), new Color32(0, 255, 0, 255) };
    private void Start()
    {
        GameObject zemin = GameObject.Find("GController").GetComponent<Gamecontroller>().zemin;
        int küpsayisi = GameObject.Find("GController").GetComponent<Gamecontroller>().küpsayisi;
        index = küpsayisi / 10;
        GetComponent<MeshRenderer>().material.color = Color32.Lerp(zemin.GetComponent<MeshRenderer>().material.color, renkler[index % renkler.Length], 0.033f);
        
    }
    void Update()
    {
        if (transform.position.x <= -2 || transform.position.z <= -2)
            eksi = false;
        else if (transform.position.x >= 2 || transform.position.z >= 2)
            eksi = true;

        if(hareket)
        {
            hareketet();
        }
    }
    void hareketet()
    {
        if(eksenx)
        {
            if(eksi)
            transform.Translate(new Vector3(-2.4f, 0, 0) * Time.deltaTime);
            else
            transform.Translate(new Vector3(2.4f, 0, 0) * Time.deltaTime);
        }
        else
        {
            if(eksi)
            transform.Translate(new Vector3(0, 0, -2.4f) * Time.deltaTime);
            else
            transform.Translate(new Vector3(0, 0, 2.4f) * Time.deltaTime);
        }
        
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
