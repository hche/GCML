﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using GenericCampaignMasterLib;

namespace GcmlWebService
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Klassennamen "Service1" sowohl im Code als auch in der SVC- und der Konfigurationsdatei ändern.
    public class CampaignMasterService : ICampaignMasterService
    {
        private Dictionary<string, Player> m_playerDic = new Dictionary<string, Player>();
        private Dictionary<string, ICampaignDatabase> m_dictRunningCampaigns = new Dictionary<string, ICampaignDatabase>();
        private string strStorepath = Environment.CurrentDirectory;


        #region ICampaignMasterService Member

        public string getPlayer(string playername)
        {
            throw new NotImplementedException();
        }

        public List<string> getPlayerCampaigns(string playerid)
        {
            throw new NotImplementedException();
        }

        public string getFieldKoord(string campaignid)
        {
            throw new NotImplementedException();
        }

        public string getSektor(string campaignid, string sektorkoord)
        {
            throw new NotImplementedException();
        }

        public List<string> getSektorList(string campaignid)
        {
            throw new NotImplementedException();
        }

        public string getUnit(string campaignid, string unitid)
        {
            throw new NotImplementedException();
        }

        public List<string> getUnitCollisions(string campaignid)
        {
            throw new NotImplementedException();
        }

        public List<string> getCommandsForUnit(string campaignid, string unitid)
        {
            throw new NotImplementedException();
        }

        public string createNewCampaign(string playerid, string fielddimension)
        {
            Player player;
            if (!m_playerDic.Keys.Contains(playerid))
            {
                player = new Player(playerid);
                m_playerDic.Add(playerid, player);
            }
            else
            {
                player = m_playerDic[playerid];
            }

            // Datenbank
            CampaignDatabaseRaptorDb database = new CampaignDatabaseRaptorDb();
            database.CampaignKey = Guid.NewGuid().ToString();
            database.StorePath = strStorepath;
            database.init();

            // Spielfeld
            Field field = new Field( 5, 5 );
            
            // Engine
            CampaignEngine engine = new CampaignEngine(field);
            engine.FieldField.Id = 123;
            engine.addPlayer(player);
            engine.addUnit(player, new DummyUnit(new Random().Next(1000, 9999)), field.getSektorList()[0]);

            // Controller
            CampaignController controller = new CampaignController();
            controller.CampaignDataBase = database;
            controller.campaignEngine = engine;
            controller.CampaignKey = database.CampaignKey;

            m_dictRunningCampaigns.Add(database.CampaignKey, database);

            return database.CampaignKey;
        }

        public void addPlayerToCampaign(string playerid, string campaignid)
        {
            throw new NotImplementedException();
        }

        public void executeCommand(string campaignid, string command)
        {
            throw new NotImplementedException();
        }

        public void addUnitToField(string campaignid, string unit, string targetsektor)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
