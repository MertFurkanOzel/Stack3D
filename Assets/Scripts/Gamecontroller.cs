using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Gamecontroller : MonoBehaviour
{
    GameObject küp;
    public GameObject zemin;
    //-----------------
    public int küpsayisi;
    float tolerans;
    bool eksen;
    int score = 0;
    bool ilk = true;
    //-----------------
    Vector3 zeminpos;
    Vector3 zeminscale;
    //-----------------
    public Camera cam;
    public AudioSource normal;
    public AudioSource perfect;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI scoretext2;
    private void Awake()
    {
        zemin = GameObject.Find("Zemin0");
        Time.timeScale = 0;
    }
    
    void Start()
    {
        küpsayisi = 1;
        küpbul();
        zeminpos = zemin.transform.position;
        zeminscale = new Vector3(1.5f, 1, 1.5f);
        tolere();
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            if (ilk)
            {
                ilk = false;
                Time.timeScale = 1;
                küp.GetComponent<Kupsc>().hareket = true;
            }
            else
            {
                if (küp == null)
                {
                    küpbul();
                }
                küp.GetComponent<Kupsc>().hareket = false;
                slice();
            }

        }

    }
    void küpbul()
    {
        küp = GameObject.Find("Küp");
        eksen = küp.GetComponent<Kupsc>().eksenx;

    }
    void zeminayarla(GameObject go)
    {
        zemin = go;
        zeminpos = go.transform.position;
        zeminscale = go.transform.localScale;
    }
    void slice()
    {
        if (eksen)
        {
            if (Mathf.Abs(küp.transform.position.x - zeminpos.x) <= tolerans)
                küp.transform.position = new Vector3(zeminpos.x, küp.transform.position.y, zeminpos.z);
            if (küp.transform.position.x > zeminpos.x && küp.transform.position.x < zeminscale.x + zeminpos.x)
            {
                float scale = zeminscale.x - (küp.transform.position.x - zeminpos.x);
                //GameObject birinciparca = Instantiate(küp, new Vector3(küp.transform.position.x / 2,küp.transform.position.y, zeminpos.z), Quaternion.identity);
                GameObject birinciparca = Instantiate(küp, new Vector3(zeminpos.x + zeminscale.x / 2 - scale / 2, küp.transform.position.y, zeminpos.z), Quaternion.identity);
                birinciparca.transform.localScale = new Vector3(scale, 0.2f, küp.transform.localScale.z);
                GameObject ikinciparca = Instantiate(küp, new Vector3(zeminpos.x + zeminscale.x / 2 + scale / 2, küp.transform.position.y, zeminpos.z), Quaternion.identity);
                ikinciparca.transform.localScale = new Vector3(zeminscale.x - scale, 0.2f, küp.transform.localScale.z);
                ortakkisimlar(birinciparca, ikinciparca);
                ikinciparca.transform.GetComponent<Rigidbody>().AddForce(0, 5, -10);
            }
            else if (küp.transform.position.x < zeminpos.x && küp.transform.position.x > -zeminscale.x + zeminpos.x)
            {
                float scale = zeminscale.x - (zeminpos.x - küp.transform.position.x);
                GameObject birinciparca = Instantiate(küp, new Vector3(zeminpos.x - zeminscale.x / 2 + scale / 2, küp.transform.position.y, zeminpos.z), Quaternion.identity);
                birinciparca.transform.localScale = new Vector3(scale, 0.2f, küp.transform.localScale.z);
                GameObject ikinciparca = Instantiate(küp, new Vector3(zeminpos.x - zeminscale.x / 2 - scale / 2, küp.transform.position.y, zeminpos.z), Quaternion.identity);
                ikinciparca.transform.localScale = new Vector3(zeminscale.x - scale, 0.2f, küp.transform.localScale.z);
                ortakkisimlar(birinciparca, ikinciparca);
                ikinciparca.transform.GetComponent<Rigidbody>().AddForce(0, 5, 10);
            }

            else if (küp.transform.position.x == zeminpos.x)
            {
                perfect.Play();
                küp.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                küp.GetComponent<Kupsc>().hareket = false;
                zeminayarla(küp);
                küp.name = "zemin";
                küpolustur();
                scoreupdate(2);
            }
            else
            {
                gameover();
            }

        }
        else
        {
            if (Mathf.Abs(küp.transform.position.z - zeminpos.z) <= tolerans)
                küp.transform.position = new Vector3(zeminpos.x, küp.transform.position.y, zeminpos.z);
            if (küp.transform.position.z > zeminpos.z && küp.transform.position.z < zeminscale.z + zeminpos.z)
            {
                //GameObject birinciparca = Instantiate(küp, new Vector3(0, küp.transform.position.y, küp.transform.position.z / 2), Quaternion.identity);

                float scale = zeminscale.z - (küp.transform.position.z - zeminpos.z);
                GameObject birinciparca = Instantiate(küp, new Vector3(zeminpos.x, küp.transform.position.y, zeminpos.z + zeminscale.z / 2 - scale / 2), Quaternion.identity);
                birinciparca.transform.localScale = new Vector3(küp.transform.localScale.x, 0.2f, scale);
                GameObject ikinciparca = Instantiate(küp, new Vector3(zeminpos.x, küp.transform.position.y, zeminpos.z + zeminscale.z / 2 + scale / 2), Quaternion.identity);
                ikinciparca.transform.localScale = new Vector3(küp.transform.localScale.x, 0.2f, zeminscale.z - scale);
                ortakkisimlar(birinciparca, ikinciparca);
                ikinciparca.GetComponent<Rigidbody>().AddForce(0, 5, 10);
       }

            else if (küp.transform.position.z < zeminpos.z && küp.transform.position.z > -zeminscale.z + zeminpos.z)
            {
                float scale = zeminscale.z - (zeminpos.z - küp.transform.position.z);
                GameObject birinciparca = Instantiate(küp, new Vector3(zeminpos.x, küp.transform.position.y, zeminpos.z - zeminscale.z / 2 + scale / 2), Quaternion.identity);
                birinciparca.transform.localScale = new Vector3(küp.transform.localScale.x, 0.2f, scale);
                GameObject ikinciparca = Instantiate(küp, new Vector3(zeminpos.x, küp.transform.position.y, zeminpos.z - zeminscale.z / 2 - scale / 2), Quaternion.identity);
                ikinciparca.transform.localScale = new Vector3(küp.transform.localScale.x, 0.2f, zeminscale.z - scale);
                ortakkisimlar(birinciparca, ikinciparca);
                ikinciparca.transform.GetComponent<Rigidbody>().AddForce(0, 5, -10);
            }
            else if (küp.transform.position.z == zeminpos.z)
            {

                perfect.Play();
                küp.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                küp.GetComponent<Kupsc>().hareket = false;
                zeminayarla(küp);
                küp.name = "zemin";
                küpolustur();
                scoreupdate(2);
            }
            else
            {
                gameover();
            }
        }
    }
    void ortakkisimlar(GameObject bir, GameObject iki)
    {

        normal.Play();
        Rigidbody rb1 = bir.GetComponent<Rigidbody>();
        Rigidbody rb2 = iki.GetComponent<Rigidbody>();
        rb1.constraints = RigidbodyConstraints.FreezeAll;
        rb2.useGravity = true;
        rb2.constraints = RigidbodyConstraints.None;
        rb2.mass = 0.1f;
        Destroy(küp);
        Destroy(iki, 2f);
        zeminayarla(bir);
        bir.name = "zemin";
        küpolustur();
        scoreupdate(1);
    }
    void gameover()
    {
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene(1);
    }
    void küpolustur()
    {

        GameObject yeniküp;
        küpsayisi++;
        tolere();
        cam.transform.position = new Vector3(cam.transform.position.x, 6.3f + küpsayisi * .2f, cam.transform.position.z);

        if (eksen)
        {
            yeniküp = Instantiate(zemin, new Vector3(zeminpos.x, ((küpsayisi - 1) * 0.2f) + .1f, 2), Quaternion.identity);
            yeniküp.GetComponent<Rigidbody>().useGravity = false;
            yeniküp.GetComponent<Kupsc>().eksenx = false;
        }

        else
        {
            yeniküp = Instantiate(zemin, new Vector3(2, ((küpsayisi - 1) * 0.2f) + .1f, zeminpos.z), Quaternion.identity);
            yeniküp.GetComponent<Rigidbody>().useGravity = false;
            yeniküp.GetComponent<Kupsc>().eksenx = true;
        }
        yeniküp.GetComponent<Kupsc>().hareket = true;
        yeniküp.name = "Küp";
        küpbul();
    }
    void tolere()
    {
        if (eksen)
            tolerans = küp.transform.localScale.x / 20f;
        else
            tolerans = küp.transform.localScale.z / 20f;

    }
   
    void scoreupdate(int sayi)
    {
        scoretext2.text = "+" + sayi.ToString();
        score += sayi;
        scoretext.text = score.ToString();
        Invoke("gizle", 0.4f);
    }
    void gizle()
    {
        scoretext2.text = "";
    }
}
