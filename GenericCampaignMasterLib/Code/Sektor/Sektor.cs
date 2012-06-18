﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericCampaignMasterLib
{
    [Serializable()]
    public class Sektor : IEquatable<Sektor>
    {
        public Sektor()
        {
            m_objSektorType = new clsSektorType_base();
        }


        public Sektor(string sektorId) 
        {
            m_strID = sektorId;
            m_objSektorType = new clsSektorType_base();
        }
        public Sektor(string sektorId, clsSektorType_base objSektorType) 
        {
            m_strID = sektorId;
            m_objSektorType = objSektorType; 
        }
        

        private string m_strID;
		private List<IUnit> m_ListUnits = new List<IUnit>();

        private clsSektorType_base m_objSektorType;

        public string Id 
        { 
            get 
        { 
                return m_strID; 
            }
            set
            {
                m_strID = value;
            }
        }

		public List<IUnit> ListUnits 
        { 
            get 
            {
                if (m_ListUnits == null) { m_ListUnits = new List<IUnit>(); }
                return m_ListUnits; 
            }
        }

        public delegate void UnitEnteringHandler(object sender, SektorEventArgs args);
        public delegate void UnitLeavingHandler(object sender, SektorEventArgs args);

        public event UnitEnteringHandler onUnitEnteredSektor;
        public event UnitLeavingHandler onUnitLeftSektor;

        public string strUniqueID { get { return (this.objSektorKoord != null) ? this.objSektorKoord.uniqueIDstr():"" ; } }

        public clsSektorKoordinaten objSektorKoord { get; set; } 

        

		public void addUnit (IUnit unit)
		{
			m_ListUnits.Add (unit);

            if(onUnitEnteredSektor != null)
                onUnitEnteredSektor(this, new SektorEventArgs(unit, this));
		}
		
		public void removeUnit (IUnit unit)
		{
			m_ListUnits.Remove(unit);

            if(onUnitLeftSektor != null)
                onUnitLeftSektor(this, new SektorEventArgs(unit, this));
		}

        #region IEquatable<Sektor> Member
        
		public bool Equals(Sektor other)
        {
            if (other.Id == this.Id)
                return true;
            else
                return false;
        }

        #endregion
        
        public int intMoveCost { get { return m_objSektorType.intMoveCost; } }
    }
}
