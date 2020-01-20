using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Kurt A Vedros
/// Class that contains all information on the Client.
/// Such as Name and Mutex number
/// </summary>
namespace SystemBuildingDevelopment
{
    public class Client : NamedEntity
    {
        private int _ClientID;
        private string _ClientName;
        private bool _ClientMutex;

        public Client(int ClientID, String ClientName, bool ClientMutex) : base(ClientName)
        {
            _ClientID = ClientID;
            _ClientMutex = ClientMutex;
        }

        public int ClientID
        {
            get
            {
                return _ClientID;
            }
            set
            {
                _ClientID = value;
            }
        }

        public bool ClientMutex
        {
            get
            {
                return _ClientMutex;
            }
            set
            {
                _ClientMutex = value;
            }
        }
    }
}