using Newtonsoft.Json;

namespace Kadoy.CrimeNet.Models.Missions {
  public class InnerMissionInfo  {
    [JsonProperty("id")]
    public string Id { get; private set; }
    
    [JsonProperty("name")]
    public string Name { get; private set; }
    
    [JsonProperty("author")]
    public string Author { get; private set; }
    
    [JsonProperty("conditions")]
    public string Conditions { get; private set; }

    [JsonProperty("difficulty")]
    public int Difficulty { get; private set; }
    
    [JsonProperty("difficultyDescription")]
    public string DifficultyDescription { get; private set; }
    
    [JsonProperty("experience")]
    public string Experience { get; private set; }
    
    [JsonProperty("duration")]
    public float Duration { get; private set; }
  }
}