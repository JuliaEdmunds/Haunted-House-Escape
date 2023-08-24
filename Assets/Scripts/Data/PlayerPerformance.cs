using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerPerformance : MonoBehaviour
{
    private float m_ElapsedTime;

    void Update()
    {
        m_ElapsedTime += Time.deltaTime;
    }
}

