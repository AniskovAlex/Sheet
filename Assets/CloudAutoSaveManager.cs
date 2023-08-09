using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class CloudAutoSaveManager : MonoBehaviour
{

    static CloudAutoSaveManager instance;
    CloudSaveObj cloudSave = new CloudSaveObj();
    public bool init = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public static CloudAutoSaveManager GetInstance()
    {
        if (instance == null)
            return Instantiate(Resources.Load<CloudAutoSaveManager>("CloudAutoSave"));
        return instance;
    }

    public void AutoSave()
    {
        SyncSaves();
        Debug.Log("Autosave!");
    }

    /*private void OnApplicationPause(bool pause)
    {
        if (!init) return;
        SyncSaves();
        Debug.Log("Autosave!");
    }*/

    private void OnApplicationQuit()
    {
        SyncSaves();
        Debug.Log("Autosave!");
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SyncSaves();
            Debug.Log("Autosave!");
        }
    }


    public async Task SyncSaves(List<(int, string)> characters)
    {
        long localFileTime = await DataCloudeSave.Load<long>("_time_@");
        CloudSaveObj cloudFile = await DataCloudeSave.LoadFromCloud();
        if (cloudFile != null)
            Debug.Log("Begin sync: local time - " + localFileTime + "| cloud time - " + cloudFile.time);
        if (cloudFile != null&& localFileTime == cloudFile.time)
        {
            Debug.Log("Sync no needed");
            return;
        }
        if ((cloudFile != null && localFileTime > cloudFile.time) || cloudFile == null)
        {
            Debug.Log("Sync to cloud");
            //characters = await DataCloudeSave.Load<List<(int, string)>>("_characters_");
            // метод со[ранения локального файла в облако
            CloudSaveObj cloudSaveObj = new CloudSaveObj();
            foreach ((int, string) x in characters)
                cloudSaveObj.characters.Add(await DataCloudeSave.Load<Character>("char_Id_" + x.Item1.ToString()));
            cloudSaveObj.time = localFileTime;
            await DataCloudeSave.SaveToCloud(cloudSaveObj);
            cloudSave = cloudSaveObj;
            Debug.Log("End sync to cloud");
        }
        {
            Debug.Log("Sync to local");
            //метод загрузки файла из облака в локальный файл
            characters.Clear();
            foreach (Character x in cloudFile.characters)
            {
                characters.Add((x._id, x._name));
                await DataCloudeSave.Save("char_Id_" + x._id.ToString(), x);
            }
            Debug.Log("End sync to local");
        }
        Debug.Log("Saved");
    }

    public async Task SyncSaves()
    {
        long localFileTime = await DataCloudeSave.Load<long>("_time_@");
        CloudSaveObj cloudFile = await DataCloudeSave.LoadFromCloud();
        if (localFileTime == cloudFile.time) return;
        List<(int, string)> characters = new List<(int, string)>();
        if (cloudFile != null)
            Debug.Log("Begin sync: local time - " + localFileTime + "| cloud time - " + cloudFile.time);
        if (cloudFile != null && localFileTime == cloudFile.time)
        {
            Debug.Log("Sync no needed");
            return;
        }
        if ((cloudFile != null && localFileTime > cloudFile.time) || cloudFile == null)
        {
            Debug.Log("Sync to cloud");
            characters = await DataCloudeSave.Load<List<(int, string)>>("_characters_");
            // метод со[ранения локального файла в облако
            CloudSaveObj cloudSaveObj = new CloudSaveObj();
            foreach ((int, string) x in characters)
                cloudSaveObj.characters.Add(await DataCloudeSave.Load<Character>("char_Id_" + x.Item1.ToString()));
            cloudSaveObj.time = localFileTime;
            await DataCloudeSave.SaveToCloud(cloudSaveObj);
            cloudSave = cloudSaveObj;
            Debug.Log("End sync to cloud");
        }
        else
        {
            Debug.Log("Sync to local");
            //метод загрузки файла из облака в локальный файл
            foreach (Character x in cloudFile.characters)
            {
                await DataCloudeSave.Save("char_Id_" + x._id.ToString(), x);
            }
            Debug.Log("End sync to local");
        }
    }

    public CloudSaveObj GetCloudSave()
    {
        return cloudSave;
    }
}
