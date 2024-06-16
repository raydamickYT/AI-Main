using System.Collections.Generic;
using UnityEngine;

public class GlobalBlackboard
{
    //eerste keer dat ik een lock gebruikt
    /// <summary>
    /// korte uitleg: een lock statement zorgt ervoor dat maar een thread tegelijk een object kan gebruiken
    /// als een andere thread hem dan probeert te gebruiken moet hij wachten totdat _lock wordt vrijgegeven.
    /// Dit zorgt voor meer voorspelbare code als meerdere threads tegelijk deze instance proberen te accessen, want alles moet via 1 thread
    /// </summary>
    private static readonly object _lock = new object();
    private static GlobalBlackboard _instance;
    public string AttackingPlayerStr = "AttackingPlayer", IsChasingPlayerStr = "ChasingPlayer";
    private Dictionary<string, object> dictionary = new Dictionary<string, object>();
    private Dictionary<string, Vector3> aiPositions = new Dictionary<string, Vector3>();

    public static GlobalBlackboard Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new GlobalBlackboard();
                }
                return _instance;
            }
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
    public bool ClearData(string key)
    {
        //checks if key is in dictionary
        if (dictionary.ContainsKey(key))
        {
            //succes: return true
            dictionary.Remove(key);
            return true;
        }
        return false;
    }
}
