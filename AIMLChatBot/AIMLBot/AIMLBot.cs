using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Data;
using System.Configuration;
using AIMLChatBot.AIMLBot.Utils;
using AIMLChatBot.Conversion;
using Microsoft.AspNetCore.Hosting;
using AIMLChatBot.Enum;
using AIMLChatBot.Helper;
using AIMLChatBot.Models;

namespace AIMLChatBot.AIMLBot
{
    public class AIMLBot
    {
        #region Attributes
        /// <summary>
        /// Language Definition
        /// </summary>
        internal string lang;

        /// <summary>
        /// The last message to be entered into the log (for testing purpose)
        /// </summary>
        public string LastLogMessage = string.Empty;

        /// <summary>
        /// If set to false the input from AIML files will undergo the same normalization process that
        /// user input goes through. If true the bot will assume the AIML is correct. Defaults to true.
        /// </summary>
        public bool TrustAIML = true;

        /// <summary>
        /// The maximum number of characters a "that" element of the path is allowed to be. Anything above 
        /// this length will cause "that" to be "*". This is to avoid having the graphmaster process 
        /// huge "that" elements in the path that might have been caused by the bot reporting third party
        /// data.
        /// </summary>
        public int MaxThatSize = 512;


        //Getter Setter
        /// <summary>
        /// The number of catagories this bot has in its graphmaster "brain"
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Configuration of map path which is not derived from .net standard to .net core 
        /// </summary>
        public static string MapPath
        {
            get
            {
                return (string)AppDomain.CurrentDomain.GetData("ContentRootPath");
            }
        }

        //Dictionary
        #region Dictionary
        /// <summary>
        /// A dictionary object that looks after all the settings associated with this bot
        /// </summary>
        public SettingsDictionary GlobalSettings;

        /// <summary>
        /// A dictionary object that looks after all the messages associated with this bot
        /// </summary>
        public SettingsDictionary MessageSettings;

        /// <summary>
        /// A dictionary object that looks after all the messages associated with this bot
        /// </summary>
        public SettingsDictionary CardinalSettings;


        #endregion

        //Message Information
        #region MessageInformation
        /// <summary>
        /// The default message to display in the event of timeout
        /// </summary>
        public string TimeOutMessage
        {
            get
            {
                return this.MessageSettings.GetSetting("timeoutmessage");
            }
        }

        /// <summary>
        /// The message to show if the user tries to use the bot whilst it is set to not process user input
        /// </summary>
        public string NotAcceptingUserInput
        {
            get
            {
                return this.MessageSettings.GetSetting("notacceptinguserinputmessage");
            }
        }

        /// <summary>
        /// the message to display default greeting information based on language message
        /// </summary>
        public string DefaultGreetingMessage
        {
            get
            {
                return this.MessageSettings.GetSetting("defaultgreetingmessage");
            }
        }

        /// <summary>
        /// The default message for giving information about feedback message
        /// </summary>
        public string FeedbackMessage
        {
            get
            {
                return this.MessageSettings.GetSetting("feedbackmessage");
            }
        }

        /// <summary>
        /// The default message for under maintenance information warning
        /// </summary>
        public string UnderMaintenanceMessage
        {
            get
            {
                return this.MessageSettings.GetSetting("undermaintenancemessage");
            }
        }


        #endregion

        //Path
        #region Path
        public string PathToAIML
        {
            get
            {
                try
                {
                    if (Convert.ToBoolean(StringConversion.ReturnSafeString(AppSetings.SettingLoadFile)))
                    {
                        return Path.Combine(MapPath, AppSetings.SettingFilePath, StringConversion.ReturnSafeString(EnumFile.AIML), String.Format("{0}", lang.Contains(StringConversion.ReturnSafeString(EnumLanguage.en)) ? StringConversion.ReturnSafeString(EnumLanguage.en) : StringConversion.ReturnSafeString(EnumLanguage.id)));
                    }
                    else
                    {
                        return Path.Combine(MapPath, this.CardinalSettings.GetSetting("aimldirectory"), String.Format("{0}", lang.Contains(StringConversion.ReturnSafeString(EnumLanguage.en)) ? StringConversion.ReturnSafeString(EnumLanguage.en) : StringConversion.ReturnSafeString(EnumLanguage.id)));
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }
        public string PathToLogs
        {
            get
            {
                try
                {
                    if (Convert.ToBoolean(StringConversion.ReturnSafeString(AppSetings.SettingLoadFile)))
                    {
                        return Path.Combine(MapPath, AppSetings.SettingFilePath, StringConversion.ReturnSafeString(EnumFile.Logs));
                    }
                    else
                    {
                        return Path.Combine(MapPath, this.CardinalSettings.GetSetting("logdirectory"));
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        #endregion

        /// <summary>
        /// Load setting status 
        /// </summary>
        public LoadSettingStatus LoadSettingStatus;

        /// <summary>
        /// Flag to denote if the bot is writing messages to its logs
        /// </summary>
        public bool IsLogging
        {
            get
            {
                string islogging = this.GlobalSettings.GetSetting("islogging");
                if (islogging.ToLower() == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// How big to let the log buffer get before writing to disk
        /// </summary>
        private int MaxLogBufferSize
        {
            get
            {
                return Convert.ToInt32(this.GlobalSettings.GetSetting("maxlogbuffersize"));
            }
        }

        /// <summary>
        /// The maximum amount of time a request should take (in milliseconds)
        /// </summary>
        public double TimeOut
        {
            get
            {
                return Convert.ToDouble(this.GlobalSettings.GetSetting("timeout"));
            }
        }


        /// <summary>
        /// An List<> containing the tokens used to split the input into sentences during the 
        /// normalization process
        /// </summary>
        public List<string> Splitters = new List<string>();

        /// <summary>
        /// A buffer to hold log messages to be written out to the log file when a max size is reached
        /// </summary>
        private List<string> LogBuffer = new List<string>();
        #endregion

        public AppSetings AppSetings;


        #region Delegates

        #endregion

        #region Events

        #endregion

        ///==========================================================================================   Main Process   ==========================================================================================   


        public AIMLBot()
        {
            this.setup();
        }

        private void setup()
        {
            this.GlobalSettings = new SettingsDictionary(this);
            this.MessageSettings = new SettingsDictionary(this);
            this.CardinalSettings = new SettingsDictionary(this);
            this.LoadSettingStatus = new LoadSettingStatus(this);
            this.AppSetings = new AppSetings();
        }

        #region LoadGeneralSettings
        /// <summary>
        /// Loads settings based upon the default location of the Settings.xml file
        /// </summary>
        public void loadSettings()
        {
            string path = String.Empty;

            try
            {
                if (AppSetings != null)
                {
                    if (Convert.ToBoolean(StringConversion.ReturnSafeString(AppSetings.SettingLoadFile)))
                    {
                        //load general settings
                        LoadSettingStatus.LoadModelName = StringConversion.ReturnSafeString(EnumFile.Config);
                        path = Path.Combine(MapPath, Path.Combine(StringConversion.ReturnSafeString(AppSetings.SettingFilePath),"GeneralSettings.xml"));
                        this.loadSettings(path);

                        LoadSettingStatus.LoadModelName = StringConversion.ReturnSafeString(EnumFile.Message);
                        path = Path.Combine(MapPath, Path.Combine(StringConversion.ReturnSafeString(AppSetings.SettingFilePath), StringConversion.ReturnSafeString(EnumFile.Config), LoadSettingStatus.LoadModelName, String.Format("{0}.xml", lang.Contains(StringConversion.ReturnSafeString(EnumLanguage.en)) ? StringConversion.ReturnSafeString(EnumLanguage.en) : StringConversion.ReturnSafeString(EnumLanguage.id))));
                        this.loadMessageSettings(path);

                        LoadSettingStatus.LoadModelStatus = true;
                    }
                    else
                    {
                        this.loadSettingsFromDatabase();
                    }
                }
                else
                {
                    this.LoadSettingStatus.LoadModelStatus = false;
                    this.LoadSettingStatus.LoadModelMessage = String.Format(lang.Contains(StringConversion.ReturnSafeString(EnumLanguage.en)) ? ErrorHelper.ERR900000EN : ErrorHelper.ERR900000ID);
                }
            }
            catch (Exception)
            {
                this.LoadSettingStatus.LoadModelStatus = false;
                this.LoadSettingStatus.LoadModelMessage = String.Format(lang.Contains(StringConversion.ReturnSafeString(EnumLanguage.en)) ? ErrorHelper.ERR900000EN : ErrorHelper.ERR900000ID);
            }

        }
        /// <summary>
        /// Load settings and configuration from various xml files referenced for the default meesage passed
        /// </summary>
        /// <param name="pathToSettings"></param>
        public void loadSettings(string pathToSettings)
        {
            this.GlobalSettings.LoadSettings(pathToSettings);

            if (!this.GlobalSettings.ContainsSetting("version"))
            {
                this.GlobalSettings.AddSetting("version", ConfigurationManager.AppSettings["Version"].ToString());
            }
            if (!this.GlobalSettings.ContainsSetting("name"))
            {
                this.GlobalSettings.AddSetting("name", "Virtual Assistant");
            }
            if (!this.GlobalSettings.ContainsSetting("alias"))
            {
                this.GlobalSettings.AddSetting("alias", "ChatBot");
            }
            if (!this.GlobalSettings.ContainsSetting("author"))
            {
                this.GlobalSettings.AddSetting("author", "Faris Fajar Muhammad");
            }
            if (!this.GlobalSettings.ContainsSetting("location"))
            {
                this.GlobalSettings.AddSetting("location", "Jakarta, Indonesia");
            }
            if (!this.GlobalSettings.ContainsSetting("gender"))
            {
                this.GlobalSettings.AddSetting("gender", "-1");
            }
            if (!this.GlobalSettings.ContainsSetting("culture"))
            {
                this.GlobalSettings.AddSetting("culture", "en-us");
            }
            if (!this.GlobalSettings.ContainsSetting("islogging"))
            {
                this.GlobalSettings.AddSetting("islogging", "false");
            }
            if (!this.GlobalSettings.ContainsSetting("timeout"))
            {
                this.GlobalSettings.AddSetting("timeout", "150000");
            }
            if (!this.GlobalSettings.ContainsSetting("maxlogbuffersize"))
            {
                this.GlobalSettings.AddSetting("maxlogbuffersize", "64");
            }
            if (!this.GlobalSettings.ContainsSetting("stripperregex"))
            {
                this.GlobalSettings.AddSetting("stripperregex", "[^0-9a-zA-Z]");
            }
        }

        public void loadMessageSettings(string pathToSettings)
        {
            this.MessageSettings.LoadSettings(pathToSettings);

            if (!this.MessageSettings.ContainsSetting("timeoutmessage"))
            {
                this.MessageSettings.AddSetting("timeoutmessage", lang.Contains(StringConversion.ReturnSafeString(EnumLanguage.en)) ? MessageHelper.MSG403001EN : MessageHelper.MSG403001ID);
            }
            if (!this.MessageSettings.ContainsSetting("notacceptinguserinputmessage"))
            {
                this.MessageSettings.AddSetting("notacceptinguserinputmessage", lang.Contains(StringConversion.ReturnSafeString(EnumLanguage.en)) ? MessageHelper.MSG403002EN : MessageHelper.MSG403002ID);
            }
            if (!this.MessageSettings.ContainsSetting("undermaintenancemessage"))
            {
                this.MessageSettings.AddSetting("undermaintenancemessage", lang.Contains(StringConversion.ReturnSafeString(EnumLanguage.en)) ? MessageHelper.MSG503001EN : MessageHelper.MSG503001ID);
            }
            if (!this.MessageSettings.ContainsSetting("feedbackmessage"))
            {
                this.MessageSettings.AddSetting("feedbackmessage", lang.Contains(StringConversion.ReturnSafeString(EnumLanguage.en)) ? MessageHelper.MSG200011EN : MessageHelper.MSG200011ID);
            }
            if (!this.MessageSettings.ContainsSetting("defaultgreetingmessage"))
            {
                this.MessageSettings.AddSetting("defaultgreetingmessage", lang.Contains(StringConversion.ReturnSafeString(EnumLanguage.en)) ? MessageHelper.MSG200010EN : MessageHelper.MSG200010ID);
            }
            if (!this.MessageSettings.ContainsSetting("unknownmessage"))
            {
                this.MessageSettings.AddSetting("unknownmessage", lang.Contains(StringConversion.ReturnSafeString(EnumLanguage.en)) ? MessageHelper.MSG200013EN : MessageHelper.MSG200013EN);
            }
            if (!this.MessageSettings.ContainsSetting("askingresolvemessage"))
            {
                this.MessageSettings.AddSetting("askingresolvemessage", lang.Contains(StringConversion.ReturnSafeString(EnumLanguage.en)) ? MessageHelper.MSG200012EN : MessageHelper.MSG200012EN);
            }
        }

        /// <summary>
        /// Loads Settings and configuration info from various xml files referenced for the default message passed in the final arguments based on respective language 
        /// Also generates some default messages if such values have not been set by setting files
        /// </summary>       
        public void loadSettingsFromDatabase()
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Loads the splitters for this bot from the supplied config file (or sets up some safe defaults)
        /// </summary>
        /// <param name="pathToSplitters">Path to the config file</param>
        private void loadSplitters(string pathToSplitters)
        {
            FileInfo splittersFile = new FileInfo(pathToSplitters);
            if (splittersFile.Exists)
            {
                XmlDocument splittersXmlDoc = new XmlDocument();
                using (var fs = new FileStream(splittersFile.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    splittersXmlDoc.Load(fs);
                }
                // the XML should have an XML declaration like this:
                // <?xml version="1.0" encoding="utf-8" ?> 
                // followed by a <root> tag with children of the form:
                // <item value="value"/>
                if (splittersXmlDoc.ChildNodes.Count == 2)
                {
                    if (splittersXmlDoc.LastChild.HasChildNodes)
                    {
                        foreach (XmlNode myNode in splittersXmlDoc.LastChild.ChildNodes)
                        {
                            if ((myNode.Name == "item") & (myNode.Attributes.Count == 1))
                            {
                                string value = myNode.Attributes["value"].Value;
                                this.Splitters.Add(value);
                            }
                        }
                    }
                }
            }
            if (this.Splitters.Count == 0)
            {
                // we don't have any splitters, so lets make do with these...
                this.Splitters.Add(".");
                this.Splitters.Add("!");
                this.Splitters.Add("?");
                this.Splitters.Add(";");
            }
        }

        #endregion

        #region LoadAIMLFromFiles
        public void loadAIMLFromFiles()
        {
            AIMLLoader loader = new AIMLLoader(this);
            loader.LoadAIML();
        }

        #endregion
    }
}
