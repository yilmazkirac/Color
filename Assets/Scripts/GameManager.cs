using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject SeciliObje;
    GameObject SeciliStand;
    Cember _Cember;
    public bool HareketVar;
    public GameObject PanelKazandin;
    public GameObject PanelOyun;

    public int HedefStandSayisi;
    int TamamlananStandSayisi;

    public void StandTamamladi()
    {
        TamamlananStandSayisi++;
        if (TamamlananStandSayisi==HedefStandSayisi)
        {
            PanelKazandin.SetActive(true);
            PanelOyun.SetActive(false);
        }
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hit,100f))
            {
                if (hit.collider!=null&&hit.collider.CompareTag("Stand"))
                {
                    if (SeciliObje!=null&& SeciliStand != hit.collider.gameObject)
                    {
                        Stand _Stand=hit.collider.GetComponent<Stand>();
                        if (_Stand._Cemberler.Count != 4 && _Stand._Cemberler.Count != 0)
                        {

                            if (_Cember.Renk == _Stand._Cemberler[^1].GetComponent<Cember>().Renk)
                            {
                                SeciliStand.GetComponent<Stand>().SoketDegistirme(SeciliObje);
                                _Cember.HareketEt("PozisyonDegis", hit.collider.gameObject, _Stand.MusaitSoketiVer(), _Stand.HareketPos);
                                _Stand.BosOlanSoket++;
                                _Stand._Cemberler.Add(SeciliObje);
                                _Stand.CemberleriKontrolEt();
                                SeciliObje = null;
                                SeciliStand = null;
                             
                            }
                            else
                            {
                                _Cember.HareketEt("SoketeGeriGit");
                                SeciliObje = null;
                                SeciliStand = null;
                            }
                            
                        }
                        else if (_Stand._Cemberler.Count==0)
                        {
                            SeciliStand.GetComponent<Stand>().SoketDegistirme(SeciliObje);
                            _Cember.HareketEt("PozisyonDegis", hit.collider.gameObject, _Stand.MusaitSoketiVer(), _Stand.HareketPos);
                            _Stand.BosOlanSoket++;
                            _Stand._Cemberler.Add(SeciliObje);
                            _Stand.CemberleriKontrolEt();
                            SeciliObje = null;
                            SeciliStand = null;
                        }
                        else
                        {
                            _Cember.HareketEt("SoketeGeriGit");
                            SeciliObje = null;
                            SeciliStand = null;
                        }

                 
                    }
                    else if (SeciliStand==hit.collider.gameObject)
                    {
                        _Cember.HareketEt("SoketeGeriGit");
                        SeciliObje = null;
                        SeciliStand = null;
                    }
                    else
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>();

                        if (_Stand._Cemberler.Count != 0)
                        {
                            SeciliObje = _Stand.EnUstCember();

                            _Cember = SeciliObje.GetComponent<Cember>();
                            HareketVar = true;
                        }
                   

                        if (_Cember.HareketEdebilirmi)
                        {
                            _Cember.HareketEt("Secim",null,null,_Cember._AitOlduguStand.GetComponent<Stand>().HareketPos);
                            SeciliStand = _Cember._AitOlduguStand;
                        }
                    }
                }
            }
        }
    }
}
