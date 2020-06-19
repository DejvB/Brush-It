using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading;
using Unity.Jobs;
using Unity.Collections;

public struct SimpleJob : IJobParallelFor
{
    public NativeArray<Color32> cur; // Complete controll of alloc and dealloc
    public NativeArray<Color32> pcur;
    public float Time;
    public int nop;
    public NativeArray<int> per;
    
    public void Execute(int p)
    {
        
        for (int i = 0; i < nop; i++)
        {
            if (pcur[i].Equals(cur[p]))
            {
                per[i] += 1;
            }
        }
    }


}
