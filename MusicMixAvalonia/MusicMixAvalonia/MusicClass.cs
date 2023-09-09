using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMixAvalonia
{
    public class MusicClass
    {
        ObservableCollection<string> folders = new ObservableCollection<string>();
        string? folderMix;

        public MusicClass(ObservableCollection<string> current_folders, string current_mix_folder) 
        {
            folderMix = current_mix_folder;
            folders = current_folders;
        }

        public void Mix() 
        {
            int size = 0;

            List<string[]> allFolders = new List<string[]>();
            List<int> all_id = new List<int>();

            foreach (var folder in folders) 
            {
                if (!Directory.Exists(folder)) 
                {
                    return;
                }

                string[] currentFolder = Directory.GetFiles(folder);

                size += currentFolder.Length;
                allFolders.Add(currentFolder);
                all_id.Add(0);
            }

            int id_past = -1;

            List<string[]> currentFolders;

            for (int i = 0; i<size; i++) 
            {
                currentFolders = new List<string[]>(allFolders);

                int rand_id;

                if (currentFolders.Count > 1)
                {
                    Random rand = new Random();
                    rand_id = rand.Next(0, currentFolders.Count);
                }
                else 
                {
                    rand_id = 0;
                }

                File.Copy(currentFolders[rand_id][all_id[rand_id]], folderMix + @"\" + i + ".mp3");
                all_id[rand_id]++;

                if (all_id[rand_id] == currentFolders[rand_id].Length) 
                {
                    allFolders.Remove(currentFolders[rand_id]);
                    all_id.RemoveAt(rand_id);
                }
            }
        }
    }
}