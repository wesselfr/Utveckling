using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour {

    [SerializeField]
    private GenericPoints[] m_WaterPoints, m_ElectricityPoints;

    [SerializeField]
    private bool m_Happy;
    [SerializeField]
    private bool m_HasWater = false;
    [SerializeField]
    private bool m_HasElectricty = false;

    [SerializeField]
    private int m_WaterNeeded, m_EnergyNeeded;

    private int m_Energy;
    private int m_Water;

    [SerializeField]
    private GameObject m_DoneParticle, m_WaterParticle, m_ElectricityParticle;

    //private Source m_VillageWaterSource;
    //private Source m_VillageEnergySouce;

    // Use this for initialization
    void Start () {
        m_DoneParticle.SetActive(false);
        m_WaterParticle.SetActive(false);
        m_ElectricityParticle.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        CheckConnections();

        foreach(GenericPoints point in m_WaterPoints)
        {
            //if(point.isTransporting == true && m_Water == m_WaterNeeded)
            //{
            //    m_HasWater = true;
            //}

            //if(point.source != null)
            //{
            //    m_VillageWaterSource = point.source;
            //}
            //else
            //{
            //    point.source = m_VillageWaterSource;
            //}

            if (m_HasWater)
            {
                point.isTransporting = true;
            }
            else if (!m_HasWater)
            {
                point.isTransporting = false;
                point.isProvided = false;
            }
        }

        foreach (GenericPoints point in m_ElectricityPoints)
        {
            //if (point.isTransporting == true && m_Energy == m_EnergyNeeded)
            //{
            //    m_HasElectricty = true;
            //}

            //if(point.source != null)
            //{
            //    m_VillageEnergySouce = point.source;
            //}
            //else
            //{
            //    point.source = m_VillageEnergySouce;
            //}

            if (m_HasElectricty)
            {
                point.isTransporting = true;
            }
            else if (!m_HasElectricty)
            {
                point.isTransporting = false;
                point.isProvided = false;
            }
        }

        //if (m_VillageWaterSource != null)
        //{
        //    m_VillageWaterSource.AddVillage(this, m_WaterNeeded);
        //}
        //if (m_VillageEnergySouce != null)
        //{
        //    m_VillageEnergySouce.AddVillage(this, m_EnergyNeeded);
        //}

        if(m_HasWater && m_HasElectricty)
        {
            m_Happy = true;
        }
        else
        {
            m_Happy = false;
        }

        m_DoneParticle.SetActive(m_Happy);
        if (m_Happy)
        {
            m_ElectricityParticle.SetActive(false);
            m_WaterParticle.SetActive(false);
        }
        else
        {
            if (m_HasWater)
            {
                m_WaterParticle.SetActive(true);
                m_ElectricityParticle.SetActive(false);
            }
            else if (m_HasElectricty)
            {
                m_WaterParticle.SetActive(false);
                m_ElectricityParticle.SetActive(true);
            }
            
            if(!m_HasWater && !m_HasElectricty)
            {
                m_WaterParticle.SetActive(false);
                m_ElectricityParticle.SetActive(false);
            }
        }


    }

    public void CheckConnections()
    {

        bool water = false;
        m_Water = 0;
        for(int i = 0; i < m_WaterPoints.Length; i++)
        {
            if(m_WaterPoints[i].isConnected && m_WaterPoints[i].isTransporting)
            {
                m_Water++;
            }
            
            if(m_WaterPoints[i].source != null)
            {
                //m_VillageWaterSource = m_WaterPoints[i].source;
            }
        }
        if(m_Water >= m_WaterNeeded)
        {
            water = true;
        }

        m_HasWater = water;


        bool electricity = false;
        m_Energy = 0;
        for (int i = 0; i < m_ElectricityPoints.Length; i++)
        {
            if (m_ElectricityPoints[i].isConnected && m_ElectricityPoints[i].isTransporting)
            {
                m_Energy++;
            }

            if(m_ElectricityPoints[i].source != null)
            {
                //m_VillageEnergySouce = m_ElectricityPoints[i].source;
            }
        }

        if(m_Energy >= m_EnergyNeeded)
        {
            electricity = true;
        }

        m_HasElectricty = electricity;


    }

    public bool CheckObjectives()
    {
        bool isProvided = true;
        if(!m_HasWater || !m_HasElectricty)
        {
            isProvided = false;
        }
        return isProvided;
    }

    public void DisconnectAll()
    {
        foreach(GenericPoints point in m_WaterPoints)
        {
            point.isTransporting = false;
        }
        foreach(GenericPoints point in m_ElectricityPoints)
        {
            point.isTransporting = false;
        }
    }

    public int water
    {
        get { return m_Water; }
        set { m_Water = value; }
    }
    
    public int energy
    {
        get { return m_Energy; }
        set { m_Energy = value; }
    }

    public bool happy
    {
        get { return m_Happy; }
    }

}
