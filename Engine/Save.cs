using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PixelDefense.Engine
{
    public class Save
    {
        public string gameName, baseFolder, backupFolder, backupPath;

        public bool loadingID = true;

        public XDocument saveFile;



        public Save(string GAMENAME)
        {
            gameName = GAMENAME;
            //heroLevel = 1;

            //LoadGame();
            backupFolder = "bzaxcyk";
            backupPath = "bath";

            baseFolder = Globals.appDataFilePath + "\\" + gameName + "";

            CreateBaseFolders();

        }


        public void CreateBaseFolders()
        {
            CreateFolder(Globals.appDataFilePath + "\\" + gameName + "");
            CreateFolder(Globals.appDataFilePath + "\\" + gameName + "\\XML");
           // CreateFolder(Globals.appDataFilePath + "\\" + gameName + "\\XML\\SavedGames");
        }

        public void CreateFolder(string s)
        {
            DirectoryInfo CreateSiteDirectory = new DirectoryInfo(s);
            if (!CreateSiteDirectory.Exists)
            {
                CreateSiteDirectory.Create();
            }
        }

        public virtual bool CheckIfFileExists(string PATH)
        {
            bool fileExists;

            fileExists = File.Exists(Globals.appDataFilePath + "\\" + gameName + "\\" + PATH);


            return fileExists;
            //return true;
        }





        public virtual void DeleteFile(string PATH)
        {
            File.Delete(PATH);
        }

        #region Getting XML Files

        public XDocument GetFile(string FILE)
        {
            if (CheckIfFileExists(FILE))
            {
                return XDocument.Load(Globals.appDataFilePath + "\\" + gameName + "\\" + FILE);
               
            }

            return null;
        }



        #endregion

        public virtual XDocument LoadFile(string FILEPATH, bool CHECKKEY = true)
        {
            XDocument xml = GetFile(FILEPATH);


            return xml;
        }


        public virtual void HandleSaveFormates(XDocument xml, string PATH)
        {
            //Console.WriteLine(Globals.appDataFilePath + "\\" + gameName + "\\" + "XML\\Settings.xml");
            xml.Save(Globals.appDataFilePath + "\\" + gameName + "\\XML\\" + PATH);


        }


        #region Converting to Binary and back

        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }

            return Encoding.ASCII.GetString(byteList.ToArray());
        }
        #endregion



    }
}
