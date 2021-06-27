using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.IO;


namespace PixelDefense.Engine
{
    public class Save
    {
        protected string gameName, baseFolder, backupFolder, backupPath;

        protected bool loadingID = true;

        protected XDocument saveFile;



        public Save(string GAMENAME)
        {
            this.gameName = GAMENAME;

            //heroLevel = 1;
            //LoadGame();

            this.backupFolder = "bzaxcyk";
            this.backupPath = "bath";
            this.baseFolder = Globals.appDataFilePath + "\\" + gameName + "";

            CreateBaseFolders();

        }


        public void CreateBaseFolders()
        {
            CreateFolder(Globals.appDataFilePath + "\\" + gameName + "");
            CreateFolder(Globals.appDataFilePath + "\\" + gameName + "\\XML");
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
        }





        public virtual void DeleteFile(string PATH)
        {
            File.Delete(PATH);
        }

        #region Getting XML Files

        public XDocument GetFile(string FILE)// throws null pointer exception 
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
