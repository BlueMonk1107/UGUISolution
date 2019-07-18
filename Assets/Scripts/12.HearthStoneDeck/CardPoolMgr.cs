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

        string prefabName = CardType.MagicCard.ToString();
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        _pools[prefabName] = (new CardPool(prefab, parent, typeof(MagicCard),typeof(DragNormalCard)));

        prefabName = CardType.MinionCard.ToString();
        prefab = Resources.Load<GameObject>(prefabName);
        _pools[prefabName] = new CardPool(prefab, parent, typeof(MinionCard), typeof(DragNormalCard));

        prefabName = SizeType.MiniCard.ToString();
        prefab = Resources.Load<GameObject>(prefabName);
        _pools[prefabName] = new CardPool(prefab, parent, typeof(MiniCard), typeof(DragMiniCard));
    }

    public Transform Spawn(string type,Transform parent)
    {
        return _pools[type].Spawn(parent);
    }

    public void Despawn(string type,Transform cardTrans)
    {
        _pools[type].Despawn(cardTrans);
    }
}

public class CardPool
{
    private List<Transform> _inactiveList;
    private List<Transform> _activeList;
    private Transform _parent;
    private GameObject _prefab;
    private Type[] _component;

    public CardPool(GameObject prefab, Transform parent,params Type[] component)
    {
        _parent = parent;
        _prefab = prefab;
        _component = component;
        _inactiveList = new List<Transform>();
        _activeList = new List<Transform>();
    }

    private Transform SpawnNew()
    {
        GameObject go = UnityEngine.Object.Instantiate(_prefab, _parent);
        foreach (Type type in _component)
        {
            go.AddComponent(type);
        }
        return go.transform;
    }

    public Transform Spawn(Transform parent)
    {
        Transform card = null;
        if (_inactiveList.Count > 0)
        {
            card = _inactiveList[0];
            _inactiveList.Remove(card);
        }
        else
        {
            card = SpawnNew();
        }

        card.SetParent(parent);
        _activeList.Add(card);
        return card;
    }

    public void Despawn(Transform card)
    {
        if (_activeList.Contains(card))
        {
            card.transform.SetParent(_parent);
            _activeList.Remove(card);
            _inactiveList.Add(card);
        }
    }
}
