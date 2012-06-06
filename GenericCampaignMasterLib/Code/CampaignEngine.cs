using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GenericCampaignMasterLib.Code.Unit;
using GenericCampaignMasterLib.Code;

namespace GenericCampaignMasterLib
{
    [Serializable()]
    public class CampaignEngine
    {
     
        #region " Properties && Felder "
        private Dictionary<int, Player> m_Players = null;
        public Dictionary<int, Player> dicPlayers
        {
            get
            {
                return m_Players;
            }
        }

        private Field m_FieldField;

        public Field FieldField
        {
            get { return m_FieldField; }
            set { m_FieldField = value; }
        }

        #endregion

        // Todo: Methode soll nur die Units zurückliefern die aktivierbar sind
		// Auf die Liste aller Units des Players kann über die List-Property zugegriffen werden.
        public List<IUnit> getActiveUnitsForPlayer(Player p)
        {
			return p.ListUnits;
        }



        private clsMoveFactory m_objMovFactory;
       private List<ICommand>m_lisReadyCommands;

       public List<ICommand> getCommandsForUnit(IUnit u)
		{
            List<ICommand> listRawCommands = u.getCommands();           // Unfertige Commands von der Unit - Enthalten keine Position-/Zielsektoren
            m_lisReadyCommands = new List<ICommand>();                   // Liste mit vollständigen Commands - wird zurückgeliefert.
        	
			foreach (ICommand cmdRaw in listRawCommands)
			{
				if (cmdRaw.GetType() == typeof(Move))
				{
                    m_objMovFactory = new clsMoveFactory(u, FieldField);
                    m_objMovFactory.onNewMoveCommand += new clsMoveFactory.delNewMoveCommand(m_objMovFactory_onNewMoveCommand);
                    m_objMovFactory.onNewStatus += new clsMoveFactory.delNewStatus(m_objMovFactory_onNewStatus);
                    m_objMovFactory.go();
				}
			}
			
			return m_lisReadyCommands;
        }

        //public delegate void delStatus(string strText);
        public event Field.delStatus onEngineStatus;

        void m_objMovFactory_onNewStatus(string strStatus)
        {
            if (this.onEngineStatus != null)
                this.onEngineStatus(strStatus);
        }

        void m_objMovFactory_onNewMoveCommand(Move readyCmd)
        {
            m_lisReadyCommands.Add(readyCmd);
        }

        
        /// <summary>
        /// Erstmal zum Testen: Einheit liefert i.d.R. nur ein Command für Move.
        /// Liefert nur eine Collection aus möglichen Moves.
        /// </summary>
        public List<Move> getDefaultMoveCommandsForUnit(IUnit u)
        {
            List<Move> listMoves = new List<Move>();
            List<ICommand> cmds = getCommandsForUnit(u);
			foreach (ICommand c in cmds)
			{
                if (c.GetType() == typeof(Move))
                    listMoves.Add((Move)c);
			}

            return listMoves;
        }

        public List<Sektor> getViewableSectorsForUnit(IUnit u)
        {
            /*List<Sektor> sektorList = FieldField.objSektorCollection.getSektorList();
            return sektorList;
*/
            return null;
        }

        public Sektor getSektorContainingUnit(IUnit u)
        {
            return FieldField.getSektorForUnit(u);
            
        }

        public Player getUnitOwner(IUnit unit)
        {
            var owner = (from p in m_Players.Values
                         where p.ListUnits.Contains(unit)
                         select p).First();

            return owner as Player;
        }


        #region " Uuitfaktory "
        
        public void addUnit(int intPlayerID, IUnit newUnit)
        {
            m_Players[intPlayerID].ListUnits.Add(newUnit);
        }

        #endregion
        public Player addPlayer(Player objNewPlayer)
        {
            if (m_Players == null) { m_Players = new Dictionary<int, Player>(); }

            if (m_Players.ContainsKey(objNewPlayer.Id))
            {
                throw new Exception_Engine_Player("PlayerID ist bereits vergeben!");
            }
            
            m_Players.Add(objNewPlayer.Id, objNewPlayer);
            
            return objNewPlayer;
        }

        public void setPlayerList(List<Player> list)
        {
            foreach (Player aktPlayer in list)
            {
                addPlayer(aktPlayer);
            }
        }


        
    }
}
