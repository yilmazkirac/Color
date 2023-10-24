using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cember : MonoBehaviour
{

    public GameObject _AitOlduguStand;
    public GameObject _AitOlduguCemberSoketi;
    public bool HareketEdebilirmi;
    public string Renk;
    public GameManager _GameManager;

    GameObject HareketPos;
    GameObject GidecegiStand;

    bool Secildi, PosDegistir, SoketOtur, SoketeGeriGit;

    public void HareketEt(string Islem,GameObject Stand=null,GameObject Soket=null,GameObject GidilecekObje=null)
    {
        switch (Islem)
        {
            case "Secim":
                HareketPos=GidilecekObje;
                Secildi=true;
                break;
            case "PozisyonDegis":
                GidecegiStand = Stand;
                _AitOlduguCemberSoketi=Soket;
                HareketPos = GidilecekObje;
                PosDegistir=true;
                break;
            case "SoketeGeriGit":
                SoketeGeriGit = true;
                break;


        }
    }
    private void Update()
    {
        if (Secildi)
        {
            transform.position = Vector3.Lerp(transform.position, HareketPos.transform.position, .2f);
            if (Vector3.Distance(transform.position, HareketPos.transform.position)<.10f)
            {
                Secildi = false;
            }

        
        }
        if (PosDegistir)
        {
            transform.position = Vector3.Lerp(transform.position, HareketPos.transform.position, .2f);
            if (Vector3.Distance(transform.position, HareketPos.transform.position) < .10f)
            {
                PosDegistir = false;
                SoketOtur = true;
            }
        }
        if (SoketOtur)
        {
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSoketi.transform.position, .2f);
            if (Vector3.Distance(transform.position, _AitOlduguCemberSoketi.transform.position) < .10f)
            {
                transform.position = _AitOlduguCemberSoketi.transform.position;

                SoketOtur = false;

                _AitOlduguStand = GidecegiStand;

                if (_AitOlduguStand.GetComponent<Stand>()._Cemberler.Count>1)
                {
                    _AitOlduguStand.GetComponent<Stand>()._Cemberler[^2].GetComponent<Cember>().HareketEdebilirmi = false;
                }
                _GameManager.HareketVar = false;    
            }
        }
        if (SoketeGeriGit)
        {
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSoketi.transform.position, .2f);
            if (Vector3.Distance(transform.position, _AitOlduguCemberSoketi.transform.position) < .10f)
            {
                transform.position = _AitOlduguCemberSoketi.transform.position;
                SoketeGeriGit = false;
                _GameManager.HareketVar = false;
            }
        }
    }
}