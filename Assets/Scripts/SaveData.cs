using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public GameObject CubesParent;

    private List<CubeData> _CubesToSave = new List<CubeData>();
    private List<CubeData> _CubesToLoad = new List<CubeData>();

    [SerializeField] private GameObject _RedCubePrefab;
    [SerializeField] private GameObject _BlueCubePrefab;

    private string _fileStart = "Cube";
    private string _fileEnd = ".txt";

    private bool _EndOfCubes = false;

    private void RetrieveCubeData()
    {
        GameObject cube;
        CubeData cubeData;

        bool Color;
        Vector3 position;
        Vector3 rotation;

        for(int index = 0; index < CubesParent.transform.childCount; index++)
        {
            cube = CubesParent.transform.GetChild(index).gameObject;

            position = cube.transform.position;
            rotation = cube.transform.rotation.eulerAngles;

            Color = cube.GetComponent<color>().cubeColor == color.colors.red;
            cubeData = new CubeData(position.x, position.y, position.z, rotation.z, Color);

            _CubesToSave.Add(cubeData);
        }
    }

    public void SaveIntoJson()
    {
        RetrieveCubeData();
        string json = string.Empty;
        int index = 0;

        foreach (CubeData data in _CubesToSave)
        {
            json = JsonUtility.ToJson(data);
            WriteToFile(index, json);
            index++;
        }
    }

    public void LoadJson()
    {
        int index = 0;

        while (!_EndOfCubes)
        {
            CubeData _newCubeData = new CubeData(0, 0, 0, 0, false);
            string json = ReadFromFile(index);

            if (json != "")
            {
                JsonUtility.FromJsonOverwrite(json, _newCubeData);
                _CubesToLoad.Add(_newCubeData);
                index++;
            }
        }

        SpawnCubesFromLoad();
    }

    private void SpawnCubesFromLoad()
    {

        foreach(CubeData cubeData in _CubesToLoad)
        {
            GameObject cubeToSpawn;
            GameObject newCube;

            cubeToSpawn = cubeData.color ? _RedCubePrefab : _BlueCubePrefab;
            newCube = Instantiate(cubeToSpawn, new Vector3(cubeData.x, cubeData.y, cubeData.z), Quaternion.Euler(0, 0, cubeData.rotation));
            newCube.transform.SetParent(CubesParent.transform);
        }
    }

    private void WriteToFile(int index, string json)
    {
        string path = GetFilePath(index);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string ReadFromFile(int index)
    {
        string path = GetFilePath(index);
        if (File.Exists(path))
        {
            using(StreamReader streamReader = new StreamReader(path))
            {
                string json = streamReader.ReadToEnd();
                return json;
            }
        }
        else
        {
            _EndOfCubes = true;
            return "";
        }
    }

    private string GetFilePath(int index)
    {
        return Application.persistentDataPath + "/" + _fileStart + index.ToString() + _fileEnd;
    } 
}

