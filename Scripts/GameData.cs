using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Godot;

public partial class GameData : Node
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string PoliticalParty { get; set; }
    public int PoliticalPoints { get; set; }
    public int InitialNameRecognition { get; set; }
    public List<HistoryEntry> History { get; set; } = new List<HistoryEntry>();
    public int NetApproval { get; set; }
    public string Province { get; set; }
    public string CensusDivision { get; set; }

    public void SaveToFile(string saveFileName)
    {
        string savePath = $"user://{saveFileName}.json";

        // Create a save data object
        var saveData = new GameDataSave
        {
            FirstName = this.FirstName,
            LastName = this.LastName,
            Age = this.Age,
            PoliticalParty = this.PoliticalParty,
            PoliticalPoints = this.PoliticalPoints,
            InitialNameRecognition = this.InitialNameRecognition,
            History = this.History,
            NetApproval = this.NetApproval,
            Province = this.Province,
            CensusDivision = this.CensusDivision
        };

        // Serialize the save data
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(saveData, options);

        // Write JSON to file
        using var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Write);
        file.StoreString(json);

        GD.Print($"Game saved to {savePath}");
    }

    public void LoadFromFile(string saveFileName)
    {
        string savePath = $"user://{saveFileName}.json";

        if (!FileAccess.FileExists(savePath))
        {
            GD.PrintErr($"Save file not found: {savePath}");
            return;
        }

        // Read JSON from file
        using var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Read);
        string jsonString = file.GetAsText();

        // Deserialize into the save data object
        var saveData = JsonSerializer.Deserialize<GameDataSave>(jsonString);

        if (saveData == null)
        {
            GD.PrintErr("Failed to deserialize JSON into GameDataSave.");
            return;
        }

        // Copy data back into GameData
        FirstName = saveData.FirstName;
        LastName = saveData.LastName;
        Age = saveData.Age;
        PoliticalParty = saveData.PoliticalParty;
        PoliticalPoints = saveData.PoliticalPoints;
        InitialNameRecognition = saveData.InitialNameRecognition;
        History = saveData.History;
        NetApproval = saveData.NetApproval;
        Province = saveData.Province;
        CensusDivision = saveData.CensusDivision;

        GD.Print($"Game loaded from {savePath}");
    }
}

// Save Data Class for Serialization
public class GameDataSave
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string PoliticalParty { get; set; }
    public int PoliticalPoints { get; set; }
    public int InitialNameRecognition { get; set; }
    public List<HistoryEntry> History { get; set; }
    public int NetApproval { get; set; }
    public string Province { get; set; }
    public string CensusDivision { get; set; }
}

// History Entry Class
public class HistoryEntry
{
    public string HistoryTitle { get; set; }
    public int StartYear { get; set; }
    public int EndYear { get; set; }
}
