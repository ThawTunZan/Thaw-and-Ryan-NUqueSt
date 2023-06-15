using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Unity.VisualScripting.FullSerializer;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public Story globalVariablesStory;

    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        GameData data = DataPersistenceManager.instance.gameData;
        // create the story
        globalVariablesStory = new Story(loadGlobalsJSON.text);

        if (!string.IsNullOrEmpty(data.story))
        {
            Debug.Log("Loading JSON data: " + data.story);
            try
            {
                
                globalVariablesStory.state.LoadJson(data.story);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error loading JSON data: " + e.Message);
            }
            Debug.Log("JSON data loaded successfully.");
        }
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialised global dialogue variable: " + name + " = " + value);
        }
    }

    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story) 
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    public void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }
    public string saveVariables()
    {
        if (globalVariablesStory != null)
        {
            VariablesToStory(globalVariablesStory);
            Debug.Log(JsonUtility.ToJson(globalVariablesStory));
            return globalVariablesStory.state.ToJson();
        }
        else { return null; }
    }
}
