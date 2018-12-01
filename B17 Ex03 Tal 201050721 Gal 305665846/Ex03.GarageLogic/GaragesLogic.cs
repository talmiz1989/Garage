using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GaragesLogic
    {
        public Dictionary<string, Client> m_clientCollection = new Dictionary<string, Client>();//create a new dictionary that holds the client records
        public Dictionary<string, Client> ClientCollection
        {
            get { return m_clientCollection; }
            set { m_clientCollection = value; }
        }

        public void TryChangingStatus(string io_LicenseNumber)
        {
            Client ownerDetails;
            if(ClientCollection.TryGetValue(io_LicenseNumber, out ownerDetails)==true)
            {
                ownerDetails.vehicleStatus = Client.eVehicleStatus.Inprocess;//change clients vehicle status

            }
            else
            {
                throw new ArgumentException();
            }
        }
        public void AddClient(string i_key,Client i_value)
        {
            m_clientCollection.Add(i_key, i_value);                        //add new client to the garages dictionary
        }
        public bool IsVehicleTruck(ref string io_doesUseElectric,string io_VehicleType)
        {
            bool isVehicleTrck;
            if(io_VehicleType.CompareTo("Truck")==0)
            {
                isVehicleTrck = true;
                io_doesUseElectric = "N";
            }
            else
            {
                isVehicleTrck = false;
            }
            return isVehicleTrck;
        }
        public void CreateNewVehicleAndInsertToGarageInputChecker(string io_vehicleType,ref bool io_isElectric,string io_doesUseElectric)
        {
            if(io_vehicleType.CompareTo("Truck") != 0)// if the vehicle is not a truck
            {
                if(io_doesUseElectric == "Y")// if the vehicle has an electric engine
                {
                    io_isElectric = true;
                }
                else if(io_doesUseElectric == "N")                                //if the vehicle has an gasoline engine
                {
                    io_isElectric = false;
                }
            }
            else                                   //if the vehicle is a truck then the truck has a gasoline engine
            {
                io_isElectric = false;
            }
        }
        public bool CheckIfUserWantsFilter(char i_usersChoise, ref List<Client.eVehicleStatus> io_statusList)
        {
            bool doesUserWantsToFilter;

            if(i_usersChoise == 'Y')
            {
                doesUserWantsToFilter = true;
            }
            else if(i_usersChoise == 'N')
            {
                io_statusList.Add(Client.eVehicleStatus.Fixed);
                io_statusList.Add(Client.eVehicleStatus.Inprocess);
                io_statusList.Add(Client.eVehicleStatus.Payed);
                doesUserWantsToFilter = false;
            }
            else
            {
                throw new ArgumentException();
            }
            return doesUserWantsToFilter;
        }
        public void CheckIfUserWantsToFilterPayed(char i_usersChoise, ref List<Client.eVehicleStatus> io_statusList)
        {
            if(i_usersChoise == 'Y')
            {
                io_statusList.Add(Client.eVehicleStatus.Payed);
            }
            else if(i_usersChoise != 'N')
            {
                throw new ArgumentException();
            }          
        }
        public void CheckIfUserWantsToFilterFixed(char i_usersChoise, ref List<Client.eVehicleStatus> io_statusList)
        {
            if(i_usersChoise == 'Y')
            {
                io_statusList.Add(Client.eVehicleStatus.Fixed);
            }
            else if(i_usersChoise != 'N')
            {
                throw new ArgumentException();
            }
        }
        
        public void CheckIfUserWantsToFilterInprocess(char i_usersChoise, ref List<Client.eVehicleStatus> io_statusList)
        {
            if(i_usersChoise == 'Y')
            {
                io_statusList.Add(Client.eVehicleStatus.Inprocess);
            }
            else if(i_usersChoise != 'N')
            {
                throw new ArgumentException();
            }
        }

        public  List<string> GetLicenseNumbers(List<Client.eVehicleStatus> i_statusList)
        {
            List<string> resList = new List<string>();

            if(i_statusList.Capacity != 0)
            {
                foreach(KeyValuePair<string, Client> client in ClientCollection)
                {
                    foreach(Client.eVehicleStatus status in i_statusList)
                    {
                        if(client.Value.vehicleStatus == status)
                        {
                            resList.Add("status " + status.ToString() + " : " + client.Value.clientVehicle.licenseNumber);
                        }
                    }
                }
            }
            else
            {
                resList.Add("There are no Vehicels matches the search.");
            }
            return resList;
        }
        public  bool ChangeVehicelsStatus(string i_licensePlate, Client.eVehicleStatus i_status)
        {
            Client client;
            bool doesClientExist = true;

            if(ClientCollection.TryGetValue(i_licensePlate, out client) == true)
            {
                client.vehicleStatus = i_status;
                doesClientExist = true;
            }
            else
            {
                doesClientExist = false;
            }
            return doesClientExist;
        }
        public void TryToInflate(string i_plateNumber)
        {
            Client ownerDetails;

            if(ClientCollection.TryGetValue(i_plateNumber, out ownerDetails) == true)// if client already exit in the system
            {
                ownerDetails.InflateAllWheels();
            }
            else
            {
                throw new ArgumentException();

            }
        }
        public Client TryGetClient(string i_licensePlate,ref bool io_isSearchSuccsesful)
        {
            Client client;

            if(ClientCollection.TryGetValue(i_licensePlate, out client) == true)
            {
                io_isSearchSuccsesful = true;
            }
            else
            {
                io_isSearchSuccsesful = false;
            }
            return client;
        }

        public static void CheckerForYesNoQuestions(string i_userInput)
        {
            if(i_userInput != "Y" && i_userInput != "N")
            {
                throw new FormatException();
            }
        }
    }
}
