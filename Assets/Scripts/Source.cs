using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour {

    [SerializeField]
    private int m_Amount;

    private int m_FreeSlot;

    [SerializeField]
    private List<Village> m_Connected;
    private List<int> m_ConnectedAmount;


    [SerializeField]
    private LineType m_Type;

    private void Start()
    {
        m_FreeSlot = m_Amount;
        m_Connected = new List<Village>();
        m_ConnectedAmount = new List<int>();
    }

    public void Update()
    {
        m_FreeSlot = m_Amount;

        for(int i = 0; i < m_Connected.Count; i++)
        {
            int maxToProvide = 0;
            for(int amount = 0; amount < m_ConnectedAmount[i]; amount++)
            {
                if(m_FreeSlot > 0)
                {
                    maxToProvide++;
                    m_FreeSlot--;
                }
            }

            switch (m_Type)
            {
                case LineType.Water:
                    m_Connected[i].water = maxToProvide;
                    break;
                case LineType.Electricity:
                    m_Connected[i].energy = maxToProvide;
                    break;
            }
        }
    }

    public int max
    {
        get { return m_Amount; }
    }

    public int amount
    {
        get { return m_FreeSlot; }
        set { m_Amount = value; }
    }

    public LineType type
    {
        get { return m_Type; }
    }

    public void AddVillage(Village village, int amount)
    {
        if (!m_Connected.Contains(village))
        {
            m_Connected.Add(village);
            m_ConnectedAmount.Add(amount);
        }
    }

    public void RemoveVillage(Village village)
    {
        if (m_Connected.Contains(village))
        {
            int i = m_Connected.IndexOf(village);

            m_Connected.RemoveAt(i);
            m_ConnectedAmount.RemoveAt(i);
        }
    }

}
