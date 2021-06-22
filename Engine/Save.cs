using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.IO;


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
                return XDocument.Load(Globals.appDataFilePath + "\\" + "PixelDefense" + "\\" + "XML" + "\\" + FILE);
               
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





    }
}
