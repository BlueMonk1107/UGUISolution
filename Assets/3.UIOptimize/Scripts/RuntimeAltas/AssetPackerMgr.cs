using System;
using System.Collections;
using System.Collections.Generic;
using DaVikingCode.AssetPacker;
using UnityEngine;

public class AssetPackerMgr : MonoBehaviour
{
    public static AssetPackerMgr Instance { get; private set; }
    private Dictionary<string,AssetPacker> _packers = new Dictionary<string, AssetPacker>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GentatorNewAltas(string altasName,Dictionary<string,string> paths,Action complete = null)
    {
        if(paths == null)
            return;
        
        AssetPacker packer = new GameObject(altasName).AddComponent<AssetPacker>();
        packer.transform.SetParent(transform);
        packer.cacheName = altasName;
        foreach (KeyValuePair<string,string> path in paths)
        {
            packer.AddTextureToPack(path.Value,path.Key);
        }
        packer.Process();
        
        packer.OnProcessCompleted.AddListener(() =>
        {
            if (complete != null)
                complete();
        });
        
        _packers.Add(altasName,packer);
    }

    public AssetPacker GetAltas(string altasName)
    {
        if (_packers.ContainsKey(altasName))
        {
            return _packers[altasName];
        }
        else
        {
            Debug.LogError("can not find altas name is "+altasName);
            return null;
        }
    }

    public void ClearAltas(string altasName)
    {
        if (_packers.ContainsKey(altasName))
        {
            AssetPacker packer = _packers[altasName];
            _packers.Remove(altasName);
            Destroy(packer.gameObject);
        }
        else
        {
            Debug.LogError("can not remove altas,because it can not find,name is"+altasName);
            return;
        }
    }
}
