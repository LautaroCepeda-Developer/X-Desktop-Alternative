using System.IO;
using System.Threading.Tasks;
using Tweetinvi;

namespace X_Desktop_Alternative.Models;
using Newtonsoft.Json;
using Tweetinvi.Models;
public class TokensModel
{

    public string? ConsumerKey { get; set; }
    public string? ConsumerSecret { get; set; }
    public string? AccessToken { get; set; }
    public string? AccessSecret { get; set;}
    public bool HasTokensSet { get; set; }
    public TwitterCredentials? TwitterCredentials { get; set; }


    public void SetTokens(string consumerKey, string consumerSecret, string accessToken, string accessSecret) // Set and save the tokens
    {
        // Reads the json
        string json = File.ReadAllText("tokens.json");
        
        // Deserializing the json
        dynamic? tokens = JsonConvert.DeserializeObject(json);

        // Saving and updating the tokens in the json
        tokens.ConsumerKey = consumerKey;
        tokens.ConsumerSecret = consumerSecret;
        tokens.AccessToken = accessToken;
        tokens.AccessSecret = accessSecret;
        tokens.HasTokensSet = true;

        ConsumerKey = consumerKey;
        ConsumerSecret = consumerSecret;
        AccessToken = accessToken;
        AccessSecret = accessSecret;
        HasTokensSet = true;

        // Serializing the updated json
        string updatedJson = JsonConvert.SerializeObject(tokens, Formatting.Indented);

        // Saving the json with the updated values
        File.WriteAllText("tokens.json",updatedJson);
    }

    public void GetTokensFromJson() // Gets the saved tokens and updates the GLOBAL var
    {
        // Reads the json
        string json = File.ReadAllText("tokens.json");

        // Deserializing Json
        dynamic? tokens = JsonConvert.DeserializeObject(json);

        // Updating the GLOBAL vars
        ConsumerKey = tokens.ConsumerKey;
        ConsumerSecret = tokens.ConsumerSecret;
        AccessToken = tokens.AccessToken;
        AccessSecret = tokens.AccessSecret;
        HasTokensSet = tokens.HasTokensSet;
    }

    public void WasTokensSetted() // Check if the tokens have already been set and updates the GLOBAL var
    {
        // Reads the Json
        string json = File.ReadAllText("tokens.json");
        
        // Deserializing the Json
        dynamic? tokens = JsonConvert.DeserializeObject(json);

        // Getting the value of Json and updating the GLOBAL var
        HasTokensSet = tokens.HasTokensSet;
    }

    public void SetTwitterCredentials() // Sets the GLOBAL twitter credentials
    {
        // Verifying that tokens are not null
        if (ConsumerKey == null || ConsumerSecret == null || AccessToken == null || AccessSecret == null) return;
        
        // Setting the credentials
        TwitterCredentials = new TwitterCredentials(ConsumerKey, ConsumerSecret, AccessToken, AccessSecret);
        
        // Setting the GLOBAL var
        HasTokensSet = true;
    }


    

}