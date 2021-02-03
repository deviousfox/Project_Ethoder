using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class WorldState : MonoBehaviour
{
    private static List<SicretObj> sicrets;
    public static WeaponChanger WeaponChanger { get; private set; }
    public static AmmoCounter AmmoCounter { get; private set; }
    public static PlayerStats PlayerStats { get; private set; }

    private void Awake()
    {
        PlayerDelegate.OnPlayerSpawnetEventHandler += SetPlayerData;
        GetAllSicrets();
        OnSicretFindEvent += PrintSicretsStats;
    }

    public void SetPlayerData(Transform tr)
    {
        AmmoCounter = tr.GetComponentInChildren<AmmoCounter>();
        WeaponChanger = tr.GetComponentInChildren<WeaponChanger>();
        PlayerStats = tr.GetComponentInChildren<PlayerStats>();
    }

    #region Sicrets

    public delegate void OnSicretFind();
    public static event OnSicretFind OnSicretFindEvent;

    public void GetAllSicrets()
    {
        sicrets = FindObjectsOfType<SicretObj>().ToList();
        foreach (var sicret in sicrets)
        {
            sicret.InstanceId = sicret.GetInstanceID();
        }
    }

    public string GetSicretStats()
    {
        int FindSicrets = 0, AllSicrets = 0;
        foreach (var sicret in sicrets)
        {
            if (sicret.IsFind)
            {
                FindSicrets++;
            }
            AllSicrets++;
        }
        return $"{FindSicrets} / {AllSicrets}";
    }

    public static void SetSicretState(int id)
    {
        print(id);
        foreach (var sicret in sicrets)
        {
            if (sicret.InstanceId == id)
            {
                sicret.IsFind = true;
                OnSicretFindEvent();
                break;
            }
        }
    }
    #region SicretsDebug

    public void PrintSicretsStats()
    {
        print(GetSicretStats());
    }

    #endregion

    #endregion
}
