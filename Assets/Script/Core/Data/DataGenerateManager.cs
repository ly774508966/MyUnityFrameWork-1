﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DataGenerateManager<T> where T : DataGenerateBase, new()
{
    static Dictionary<string, T> s_dict = new Dictionary<string, T>();

    static bool s_isInit = false;

    public static T GetData(string key) 
    {
        if (key == null)
        {
            throw new Exception("DataGenerateManager GetData key is Null !");
        }

        //清理缓存
        if (!s_isInit)
        {
            s_isInit = true;
            GlobalEvent.AddEvent(MemoryEvent.FreeHeapMemory, CleanCatch);
        }

        if (s_dict.ContainsKey(key))
        {
            return s_dict[key];
        }
        else
        {
            T data = new T();
            data.LoadData(key);
            s_dict.Add(key,data);
            return data;
        }
    }

    public static void CleanCatch(params object[] objs)
    {
        s_dict.Clear();
    }
}


public abstract class DataGenerateBase
{
    public virtual void LoadData(string key)
    {

    }
}

