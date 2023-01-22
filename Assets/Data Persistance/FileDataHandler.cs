using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class FileDataHandler {
   private string dataDirPath = "";
   private string fileName = "";

   public FileDataHandler(string dataDirPath, string fileName) {
        this.dataDirPath = dataDirPath;
        this.fileName = fileName;
   }

   public GameData Load() {
        //Path.Combine()  is used to combine file path names. Caters to multiple OS path seperators
        string fullPath = Path.Combine(dataDirPath, fileName);

        GameData loadedData = null;
        if(File.Exists(fullPath)) {
            try {
                //Load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath,FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //De-serialize data from JSON back into C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                Debug.Log("Load Successful");
            }

            catch(Exception e) {
                Debug.Log("Error Occurred Trying to Load Data : "+fullPath + "\n"+ e);
            }
        }
        return loadedData;
    }

   public void Save(GameData data) {
        //Path.Combine()  is used to combine file path names. Caters to multiple OS path seperators
        string fullPath = Path.Combine(dataDirPath, fileName);
        try {
            //create directory for the file to be written to if the file does not exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize C# Game data object to JSON
            string dataToStore = JsonUtility.ToJson(data,true);

            //write the serialized data to a file 
            using (FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                using ( StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
            Debug.Log("Save Successful");

        }
        catch(Exception e) {
            Debug.Log("Error Occurred Trying to Save Data to file : "+fullPath + "\n"+ e);
        }
   }


}
