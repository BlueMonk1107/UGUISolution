using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPoolMgr
{
    private Dictionary<string, CardPool> _pools;

    public CardPoolMgr(Transform root)
    {
        _pools = new Dictionary<string, CardPool>();

        Transform parent = new GameObject("Cache").AddComponent<RectTransform>();
        parent.SetParent(root);
        parent.gameObject.SetActive(false);

        AddPool(CardType.MagicCard.ToString(), parent, typeof (MagicCard), typeof (DragNormalCard),typeof(NormalCardAreaAction));

        AddPool(CardType.MinionCard.ToString(), parent, typeof(MinionCard), typeof(DragNormalCard), typeof(NormalCardAreaAction));

        AddPool(SizeType.MiniCard.ToString(), parent, typeof(MiniCard), typeof(DragMiniCard), typeof(MiniCardAreaAction));
    }

    private void AddPool(string prefabName,Transform parent,params Type[] component)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        _pools[prefabName] = new CardPool(parent, prefab.transform, component);
    }

    public Transform Spwan(string type, Transform parent)
    {
        if (_pools.ContainsKey(type))
        {
            return _pools[type].Spawn(parent);
        }
        else
        {
            Debug.LogError(type+"找不到对应池");
            return null;
        }
    }

    public void Despwan(string type, Transform card)
    {
        if (_pools.ContainsKey(type))
        {
            _pools[type].Despwan(card);
        }
    }
}
