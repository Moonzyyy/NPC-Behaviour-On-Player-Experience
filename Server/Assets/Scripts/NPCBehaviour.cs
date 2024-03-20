using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;


public abstract class NPCBehaviour : NetworkBehaviour
{
    
    protected NavMeshAgent agent;
    
    private List<Transform> pathArray;
    
    protected List<string> conversationList = new List<string>();
    
    bool isInConversation = false;
    
    protected bool playerTaskFinished = false;
 
    protected bool hasInteractedAlready = false;

    [StoreData]
    public Dictionary<string, int> playerDictionary = new Dictionary<string, int>();
    [StoreData]
    public int currentEmotion = 0;


    protected int CurrentEmotion {
        get => currentEmotion; 
        set
        {
            currentEmotion = Math.Clamp(value, -5, 5);
        }
    }


    public void PlayerMakeNPCHappier(int amount)
    {
        try
        {
            playerDictionary[PlayerInteraction.Username] = Math.Min(playerDictionary[PlayerInteraction.Username] + Math.Abs(amount), 5);
            CurrentEmotion++;
        }
        catch (Exception e) { Debug.LogError("Player not in dictionary when changing value: " + "\n" + e.Message); }
    }

    public void PlayerMakeNPCSadder(int amount) 
    {
        try
        {
            playerDictionary[PlayerInteraction.Username] = Math.Max(playerDictionary[PlayerInteraction.Username] + -Math.Abs(amount), -5);
            CurrentEmotion--;
        }
        catch (Exception e) { Debug.LogError("Player not in dictionary when changing value: " + "\n" + e.Message); }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Transform paths = transform.parent.Find("Paths");
        if (paths == null || agent == null) return;
        pathArray = paths.GetComponentsInChildren<Transform>().ToList();
        pathArray.RemoveAt(0);
        
    }

    int currentPath = 0;
    private void Update()
    {
        if (agent == null)
        {
            return;
        }
        else 
        {
            if (isInConversation)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
            agent.SetDestination(pathArray[currentPath].position);
        }
        if (Vector3.Distance(new Vector3(pathArray[currentPath].position.x, 0, pathArray[currentPath].position.z), new Vector3(transform.position.x, 0 , transform.position.z)) < 1)
        {
            if (currentPath == pathArray.Count - 1) currentPath = 0;
            else currentPath++; 
        }
    }

    public virtual void OnClientDisconnected(ulong clientID)
    {
        Debug.Log("Client has disconnected... Calling OnClientDisconnected on script: " + name);
        hasInteractedAlready = false;
        playerTaskFinished = false;
        isInConversation = false;
    }


    public void EndOfConversation() 
    { 
        isInConversation = false;
        Debug.Log("End of conversation");
    }


    public virtual NPCBehaviour StartConversation()
    {
        isInConversation = true;
        conversationList.Clear();
        if (-5 == currentEmotion || currentEmotion == -4)
        {
            Debug.Log("NPC is Miserable.");
            Miserable();
        }
        else if (-3 == currentEmotion || currentEmotion == -2)
        {
            Debug.Log("NPC is Unhappy.");
            UnHappy();
        }
        else if (-1 <= currentEmotion && currentEmotion <= 1)
        {
            Debug.Log("NPC is Neutral.");
            Neutral();
        }
        else if (2 == currentEmotion || currentEmotion == 3)
        {
            Debug.Log("NPC is Happy.");
            Happy();
        }
        else if (4 == currentEmotion || currentEmotion == 5) 
        {
            Debug.Log("NPC is Transcanded.");
            Transcended();
        }
        hasInteractedAlready = true;
        if (!playerDictionary.ContainsKey(PlayerInteraction.Username)) playerDictionary[PlayerInteraction.Username] =  0;
        return this;
    }

    protected virtual void Start()
    {
        LoadFile();
    }

    protected virtual void OnApplicationQuit()
    {
        WriteToFile();
    }

    private void WriteToFile()
    {
        try 
        {

            FieldInfo[] storeDataFields = GetType().GetFields()
          .Where(field => field.IsDefined(typeof(StoreDataAttribute), inherit: true))
          .ToArray();
            Dictionary<string, object> dataToStore = new Dictionary<string, object>();
            foreach (FieldInfo field in storeDataFields) 
            { 
                dataToStore.Add(field.Name, field.GetValue(this));
            }
            string json = JsonConvert.SerializeObject(dataToStore, Formatting.Indented);
            Debug.Log("Now writing to file.");
            File.WriteAllText(this.name + ".json", json);
            Debug.Log("NPC Saved");
        } 
        catch (Exception e)
        {
            Debug.LogError("Error Saving: " + e.ToString());
        }
    }

    private void LoadFile() 
    {
        try
        {
            if (File.Exists(this.name + ".json"))
            {
                Debug.Log("File Found for: " + this.name);
                string json = File.ReadAllText(this.name + ".json");
                Debug.Log($"Json found for: {this.name}... Here is the json data {json}");
                Dictionary<string, object> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                Debug.Log("Values found and put in dictionary: " + jsonDict.Count);
                foreach (var kvp in jsonDict)
                {
                    Debug.Log($"Trying to set data for: {this.name} " + kvp.Key + ": " + kvp.Value);
                    FieldInfo fieldInfo = GetType().GetField(kvp.Key, BindingFlags.Public | BindingFlags.Instance);
                    if (fieldInfo != null)
                    {
                        Debug.Log("Setting Value! : " + kvp.Key + " to " + kvp.Value);
                        var type = TypeDescriptor.GetConverter(fieldInfo.FieldType);
                        if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                        {

                            var dictionary = JsonConvert.DeserializeObject(kvp.Value.ToString(), fieldInfo.FieldType);
                            fieldInfo.SetValue(this, dictionary);
                        }
                        else
                        {
                            fieldInfo.SetValue(this, type.ConvertFromInvariantString(kvp.Value.ToString()));
                        }
                    }
                    else
                    {
                        Debug.Log("Value not found! : " + kvp.Key);
                    }
                }
            }
            else
            {
                Debug.Log("NPC file does not exist, creating file.");
                Debug.Log(this.name + ".json");
                File.Create(this.name + ".json");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error Loading: " + e.ToString());
        }
    }

    public List<string> ConversationList =>  conversationList;

    #region conversationLogic
    private void Miserable() 
    {
        switch (playerDictionary.ContainsKey(PlayerInteraction.Username))
        {
            case false:
                Miserable_New();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= -4:
                Miserable_PlayerMiserable();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= -2:
                Miserable_PlayerUnhappy();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 1:
                Miserable_PlayerNeutral();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 3:
                Miserable_PlayerHappy();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 5:
                Miserable_PlayerTranscendent();
                break;
        }
    }
    private void UnHappy() 
    {
        switch (playerDictionary.ContainsKey(PlayerInteraction.Username))
        {
            case false:
                Unhappy_New();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= -4:
                Unhappy_PlayerMiserable();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= -2:
                Unhappy_PlayerUnhappy();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 1:
                Unhappy_PlayerNeutral();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 3:
                Unhappy_PlayerHappy();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 5:
                Unhappy_PlayerTranscendent();
                break;
        }
    }
    private void Neutral()
    {
        switch (playerDictionary.ContainsKey(PlayerInteraction.Username))
        {
            case false:
                Neutral_New();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= -4:
                Neutral_PlayerMiserable();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= -2:
                Neutral_PlayerUnhappy();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 1:
                Neutral_PlayerNeutral();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 3:
                Neutral_PlayerHappy();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 5:
                Neutral_PlayerTranscendent();
                break;
        }
    }
    private void Happy()
    {
        switch (playerDictionary.ContainsKey(PlayerInteraction.Username))
        {
            case false:
                Happy_New();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= -4:
                Happy_PlayerMiserable();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= -2:
                Happy_PlayerUnhappy();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 1:
                Happy_PlayerNeutral();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 3:
                Happy_PlayerHappy();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 5:
                Happy_PlayerTranscendent();
                break;
        }
    }
    private void Transcended() 
    {
        switch (playerDictionary.ContainsKey(PlayerInteraction.Username))
        {
            case false:
                Transcendent_New();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= -4:
                Transcendent_PlayerMiserable();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= -2:
                Transcendent_PlayerUnhappy();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 1:
                Transcendent_PlayerNeutral();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 3:
                Transcendent_PlayerHappy();
                break;
            case true when playerDictionary[PlayerInteraction.Username] <= 5:
                Transcendent_PlayerTranscendent();
                break;
        }
    }
    #endregion

    #region abstractMethodsForInheritance
    //Edit these in subclass by using string and adding to a list

    /*
     * Miserable
     */
    protected abstract void Miserable_New();
    protected abstract void Miserable_PlayerMiserable();
    protected abstract void Miserable_PlayerUnhappy();
    protected abstract void Miserable_PlayerNeutral();
    protected abstract void Miserable_PlayerHappy();
    protected abstract void Miserable_PlayerTranscendent();
    /*
     * Unhappy
     */

    protected abstract void Unhappy_New();
    protected abstract void Unhappy_PlayerMiserable();
    protected abstract void Unhappy_PlayerUnhappy();
    protected abstract void Unhappy_PlayerNeutral();
    protected abstract void Unhappy_PlayerHappy();
    protected abstract void Unhappy_PlayerTranscendent();

    /*
     * Neutral
     */
    protected abstract void Neutral_New();
    protected abstract void Neutral_PlayerMiserable();
    protected abstract void Neutral_PlayerUnhappy();
    protected abstract void Neutral_PlayerNeutral();
    protected abstract void Neutral_PlayerHappy();
    protected abstract void Neutral_PlayerTranscendent();

    /*
     * Happy
     */
    protected abstract void Happy_New();
    protected abstract void Happy_PlayerMiserable();
    protected abstract void Happy_PlayerUnhappy();
    protected abstract void Happy_PlayerNeutral();
    protected abstract void Happy_PlayerHappy();
    protected abstract void Happy_PlayerTranscendent();
    /*
     * Transcendent
     */
    protected abstract void Transcendent_New();
    protected abstract void Transcendent_PlayerMiserable();
    protected abstract void Transcendent_PlayerUnhappy();
    protected abstract void Transcendent_PlayerNeutral();
    protected abstract void Transcendent_PlayerHappy();
    protected abstract void Transcendent_PlayerTranscendent();
    #endregion


}
