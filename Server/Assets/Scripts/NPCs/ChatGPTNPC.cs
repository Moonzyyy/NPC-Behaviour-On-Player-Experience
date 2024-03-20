using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using System.Diagnostics;


public class ChatGPTNPC : MonoBehaviour, IonClientDisconnected
{
    [StoreData]
    public Dictionary<string, int> playerDictionary = new Dictionary<string, int>();
    [StoreData]
    public int currentEmotion = 0;

    public List<string> chatGPTConversationList = new List<string>();
    protected bool hasInteractedAlready = false;

    string currentPlayerInteracting;


    void Awake()
    {
        currentPlayerInteracting = "";
        chatGPTConversationList.Clear();
    }

    protected int CurrentEmotion
    {
        get => currentEmotion;
        set
        {
            currentEmotion = Math.Clamp(value, -5, 5);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        { 
            StartCoroutine(ChatGPT(null));
        }
    }

    public void CallChatGPTFunction(PlayerInteraction playerInteraction) 
    {
        if (chatGPTConversationList.Count > 1)
        {
            if (!playerDictionary.ContainsKey(PlayerInteraction.Username)) 
            {
                playerDictionary.Add(PlayerInteraction.Username, 0);
            }
        }
        StartCoroutine(ChatGPT(playerInteraction));
    }

    IEnumerator ChatGPT(PlayerInteraction playerInteraction) 
    {
            currentPlayerInteracting = PlayerInteraction.Username;
            string chatGPTContent = "In my game you are the chef of a village who cooks for the village daily. Your name is Gordon. You take a lot of pride in your work and love to share your idea of food with people. There are other people in the village such as:" +
            "\r\n- Flowey, the old man who thinks about his flower. He loves your food.\r\n- The Doctor, who always orders your food.\r\n- Para, who doesn't talk much but seems to love your food." +
            "\nIn this game, you can't mechanically give anyone food." +
            "\nIn this game emotions range from -5 being miserable to 5 being super happy." +
            $"\nYour current emotion is {currentEmotion}.";

            if (playerDictionary.ContainsKey(PlayerInteraction.Username))
            {
                chatGPTContent += $"The Adventurer {PlayerInteraction.Username} talks to you, your emotion towards them is {playerDictionary[PlayerInteraction.Username]}. What will you say? Just talk as and for Gordon (yourself) and nothing else.";
            }
            else
            {
                chatGPTContent += "\nAn adventurer talks to you, it is your first time meeting them. What will you say? Just talk as and for Gordon (yourself) and nothing else.";
            }
            chatGPTContent += "\nIf it seems like the adventurer has said nothing, that means the adventurer is speechless. If that happens just say something, don't give any kind of information about the adventurer.";
            string conversations = string.Join(" ", chatGPTConversationList.ToArray());

            UnityEngine.Debug.Log("Key Press Down happened.");
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "python3";
            startInfo.Arguments = "ChatGPT.py " + chatGPTContent + conversations; // Pass the name of your Python script
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true; // Set to true to prevent the console window from popping up


            // Start the process
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            while (!process.HasExited)
            {
                yield return null;
            }

            // Read the output from the Python script
            string output = process.StandardOutput.ReadToEnd();
            if (playerInteraction != null) playerInteraction.SendChatGPTText(output);
            chatGPTConversationList.Add("\nGordon: " + output);
            // Display the output
            UnityEngine.Debug.Log("Output from Python script: " + output);
            yield return null;
    }

    IEnumerator ChatGPTPlayerOpinion()
    {

        string chatGPTContent = "You are a villager in my game, an adventurer has had a conversation with you.";

        chatGPTContent += string.Join(" ", chatGPTConversationList.ToArray());
        chatGPTContent += "\n\nWould you say the conversation was Good or Bad? Only say Good or Bad, don't say anything else.";


        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "python";
        startInfo.Arguments = "ChatGPT.py " + chatGPTContent; // Pass the name of your Python script
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true; // Set to true to prevent the console window from popping up


        // Start the process
        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        while (!process.HasExited)
        {
            yield return null;
        }

        // Read the output from the Python script
        string output = process.StandardOutput.ReadToEnd();
        if (output.Contains("Bad"))
        {
            PlayerMakeNPCSadder(1);
        }
        else 
        {
            PlayerMakeNPCHappier(1);
        }
        
        // Display the output
        UnityEngine.Debug.Log("Output from Python script: " + output);

        string filePath = "conversation.txt";

        // Write strings to the text file
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            foreach (string str in chatGPTConversationList)
            {
                writer.WriteLine(str);
            }

            writer.WriteLine("--------------------------------------------------------------------------------------------------------------------------");
        }

       

        chatGPTConversationList.Clear();
        yield return null;
    }



    protected virtual void Start()
    {
        if (!File.Exists("conversation.txt"))
        {
            File.Create("conversation.txt");
        }
        LoadFile();
    }

    protected virtual void OnApplicationQuit()
    {
        WriteToFile();
    }


    public void OnClientDisconnected(ulong clientID)
    {
        UnityEngine.Debug.Log("Client has disconnected! Calling CHATGPTNPC onClientDisconnected.");
        if (chatGPTConversationList.Count > 1)
        {
            UnityEngine.Debug.Log("CHATGPTNPC start coroutine has been called.");
            try
            {
                StartCoroutine(ChatGPTPlayerOpinion());
            }
            catch (Exception e) 
            {
                chatGPTConversationList.Clear();
            }
        }
        else 
        {
            chatGPTConversationList.Clear();
        }
    }

    public void PlayerMakeNPCHappier(int amount)
    {
        try
        {
            playerDictionary[currentPlayerInteracting] = Math.Min(playerDictionary[currentPlayerInteracting] + Math.Abs(amount), 5);
            CurrentEmotion++;
        }
        catch (Exception e) { UnityEngine.Debug.LogError("Player not in dictionary when changing value: " + "\n" + e.Message); }
    }

    public void PlayerMakeNPCSadder(int amount)
    {
        try
        {
            playerDictionary[currentPlayerInteracting] = Math.Max(playerDictionary[currentPlayerInteracting] + -Math.Abs(amount), -5);
            CurrentEmotion--;
        }
        catch (Exception e) { UnityEngine.Debug.LogError("Player not in dictionary when changing value: " + "\n" + e.Message); }
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
            UnityEngine.Debug.Log("Now writing to file.");
            File.WriteAllText(this.name + ".json", json);
            UnityEngine.Debug.Log("NPC Saved");
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Error Saving: " + e.ToString());
        }
    }

    private void LoadFile()
    {
        try
        {
            if (File.Exists(this.name + ".json"))
            {
                UnityEngine.Debug.Log("File Found for: " + this.name);
                string json = File.ReadAllText(this.name + ".json");
                UnityEngine.Debug.Log($"Json found for: {this.name}... Here is the json data {json}");
                Dictionary<string, object> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                UnityEngine.Debug.Log("Values found and put in dictionary: " + jsonDict.Count);
                foreach (var kvp in jsonDict)
                {
                    UnityEngine.Debug.Log($"Trying to set data for: {this.name} " + kvp.Key + ": " + kvp.Value);
                    FieldInfo fieldInfo = GetType().GetField(kvp.Key, BindingFlags.Public | BindingFlags.Instance);
                    if (fieldInfo != null)
                    {
                        UnityEngine.Debug.Log("Setting Value! : " + kvp.Key + " to " + kvp.Value);
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
                        UnityEngine.Debug.Log("Value not found! : " + kvp.Key);
                    }
                }
            }
            else
            {
                UnityEngine.Debug.Log("NPC file does not exist, creating file.");
                UnityEngine.Debug.Log(this.name + ".json");
                File.Create(this.name + ".json");
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Error Loading: " + e.ToString());
        }
    }
}
