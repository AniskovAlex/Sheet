//#define LOCAL_TEST

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public static class DataCloudeSave
{

    private static ICloudSaveDataClient _client;

    static bool saving = false;
    static bool overSave = false;
    static string token = "";
    public static async Task Auth()
    {
        if (_client != null) return;
        //await DeleteAll();
        PlayGamesPlatform.Activate();
        await UnityServices.InitializeAsync();
        await AuthGoogle();
        await SignInWithGooglePlayGamesAsync(token);

        _client = CloudSaveService.Instance.Data;
        Debug.Log("It's Done!" + token);
    }

    static Task AuthGoogle()
    {
#if LOCAL_TEST
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
#else
        var tcs = new TaskCompletionSource<object>();
        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                Debug.Log("Login with Google Play games successful.");

                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    token = code;
                    tcs.SetResult(null);
                    // This token serves as an example to be used for SignInWithGooglePlayGames
                });
            }
            else
            {
                //Error = "Failed to retrieve Google play games authorization code";
                Debug.Log("Login Unsuccessful");
                tcs.SetResult(null);
            }

        });
        //Debug.Log("a   " + token);
        return tcs.Task;
#endif
    }

    static async Task SignInWithGooglePlayGamesAsync(string authCode)
    {
        //Debug.Log("b   " + authCode);
        try
        {
            //Debug.Log("c   " + authCode);
            await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    public static bool IsLoaded()
    {
        if (_client != null) return true;
        return false;
    }

    public static void Delete(string key)
    {
        FileSaverAndLoader.DeleteFile(key);
        FileSaverAndLoader.SaveFile(Path.Combine("_time_@"), DateTime.Now.Ticks.ToString());
    }

    public static async Task DeleteAll()
    {
        if (_client != null)
        {
            var keys = await Call(_client.RetrieveAllKeysAsync());
            if (keys != null)
            {
                var tasks = keys.Select(k => _client.ForceDeleteAsync(k)).ToList();
                await Call(Task.WhenAll(tasks));
            }
        }
        FileSaverAndLoader.DeleteFile("_time_@");
        List<(int, string)> characters = await Load<List<(int, string)>>("_characters_");
        foreach ((int, string) x in characters)
            FileSaverAndLoader.DeleteFile("char_Id_" + x.Item1.ToString());
        FileSaverAndLoader.DeleteFile("_characters_");

    }

    public static async Task Save(string key, object value)
    {
        var data = value;
        string teststr = JsonConvert.SerializeObject(data);
        FileSaverAndLoader.SaveFile(key, teststr);
        FileSaverAndLoader.SaveFile("_time_@", DateTime.Now.Ticks.ToString());
    }

    public static async Task SaveToCloud(CloudSaveObj saveObj)
    {
        if (_client == null) await Auth();
        if (saving)
        {
            overSave = true;
            return;
        }
        saveObj.time = DateTime.Now.Ticks;
        saving = true;
        await Call(_client.ForceSaveAsync(new Dictionary<string, object> { { "_characters_", saveObj } }));
        saving = false;
        if (overSave)
        {
            overSave = false;
            await SaveToCloud(saveObj);
        }
    }

    public static async Task<T> Load<T>(string key)
    {
        string str = FileSaverAndLoader.LoadPersistenFile(key);
        if (str == null) return default;
        //Debug.Log("!@#" + str);
        return Deserialize<T>(str);
    }

    public static async Task<CloudSaveObj> LoadFromCloud()
    {
        if (_client == null) await Auth();
        var query = await Call(_client.LoadAsync(new HashSet<string> { "_characters_" }));
        if (query == null) return new CloudSaveObj();
        return query.TryGetValue("_characters_@", out var value) ? Deserialize<CloudSaveObj>(value) : default;
    }

    private static T Deserialize<T>(string input)
    {
        if (typeof(T) == typeof(string)) return (T)(object)input;
        //Debug.Log(JsonConvert.DeserializeObject<T>(input));
        return JsonConvert.DeserializeObject<T>(input);
    }
    private static async Task Call(Task action)
    {
        try
        {
            await action;
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
    }

    private static async Task<T> Call<T>(Task<T> action)
    {
        try
        {
            return await action;
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }

        return default;
    }
}
