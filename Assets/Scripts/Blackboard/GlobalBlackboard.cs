using System.Collections.Generic;
using UnityEngine;

public class GlobalBlackboard
{
    private static GlobalBlackboard _instance;
    public string AttackingPlayerStr = "AttackingPlayer", IsChasingPlayerStr = "ChasingPlayer";
    private Dictionary<string, object> dictionary = new Dictionary<string, object>();
    private Dictionary<string, Vector3> aiPositions = new Dictionary<string, Vector3>();

    public static GlobalBlackboard Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GlobalBlackboard();
            }
            return _instance;
        }
    }

    // Register an AI agent
    public void RegisterAI(string aiID, Vector3 position)
    {
        aiPositions[aiID] = position;
    }

    // Unregister an AI agent
    public void UnregisterAI(string aiID)
    {
        aiPositions.Remove(aiID);
    }

    // Get the position of a specific AI agent
    public Vector3 GetAIPosition(string aiID)
    {
        if (aiPositions.TryGetValue(aiID, out var position))
        {
            return position;
        }
        return Vector3.zero; // Or some default value indicating no position found
    }

    // Update the position of a specific AI agent
    public void UpdateAIPosition(string aiID, Vector3 position)
    {
        if (aiPositions.ContainsKey(aiID))
        {
            aiPositions[aiID] = position;
        }
    }

    public T GetVariable<T>(string name)
    {
        if (dictionary.ContainsKey(name))
        {
            return (T)dictionary[name];
        }
        return default(T);
    }
    public void SetVariable<T>(string name, T variable)
    {
        // UnityEngine.Debug.Log(name + variable.ToString());
        dictionary[name] = variable;
    }
}
