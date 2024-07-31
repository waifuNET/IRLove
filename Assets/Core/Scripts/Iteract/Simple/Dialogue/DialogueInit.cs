using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueInit : MonoBehaviour
{
    public void InitializeDialogueData(string file_name)
    {
        string path = Path.GetDirectoryName(file_name);

        List<string> lines = new List<string>();
        using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
        {
            string line = string.Empty;
            while((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                lines.Add(line);
            }
        }
    }

    
}
