﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class GamePath
    {
        public const string DATA = "data";
        public const string MODS = "mods";

        public static string GetBasename(string path)
        {
            return Path.GetFileName(path.TrimEnd(Path.DirectorySeparatorChar));
        }

        public static string GetGamePath()
        {
            return Directory.GetCurrentDirectory();
        }

        public static string GetDataPath()
        {
            return Path.Combine(GetGamePath(), DATA);
        }

        public static string GetModsPath()
        {
            return Path.Combine(GetGamePath(), MODS);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Given path but starting after /mods/ModName/ </returns>
        public static string RemoveModPath(string path)
        {
            return path.Substring(path.IndexOf(Path.DirectorySeparatorChar, path.IndexOf("mods" + Path.DirectorySeparatorChar) + 5));
        }

        public static string RemoveParlessPath(string path)
        {
            path = path.Replace(".parless", "");

            return path.Substring(path.IndexOf("data" + Path.DirectorySeparatorChar) + 4);
        }

        public static string GetDataPathFrom(string path)
        {
            if (path.Contains(".parless"))
            {
                // Preserve .parless in path instead of removing it
                return path.Substring(path.IndexOf("data" + Path.DirectorySeparatorChar) + 4);
            }

            return RemoveModPath(path);
        }

        public static string GetModPathFromDataPath(string mod, string path)
        {
            return Path.Combine(GetModsPath(), mod, path.TrimStart(Path.DirectorySeparatorChar));
        }

        public static bool FileExistsInData(string path)
        {
            return File.Exists(Path.Combine(GetDataPath(), path.TrimStart(Path.DirectorySeparatorChar)));
        }

        public static bool DirectoryExistsInData(string path)
        {
            return Directory.Exists(Path.Combine(GetDataPath(), path.TrimStart(Path.DirectorySeparatorChar)));
        }

        public static bool ExistsInDataAsPar(string path)
        {
            if (path.Contains(".parless"))
            {
                // Remove ".parless"
                return FileExistsInData(RemoveParlessPath(path) + ".par");
            }

            // Add ".par"
            return FileExistsInData(RemoveModPath(path) + ".par");
        }

        public static Game GetGame()
        {
            foreach (string file in Directory.GetFiles(GetGamePath(), "*.exe"))
            {
                if (Enum.TryParse(Path.GetFileNameWithoutExtension(file), out Game game))
                {
                    return game;
                }
            }

            return Game.Unsupported;
        }
    }
}
