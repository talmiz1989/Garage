using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;
using System.Threading;
namespace Ex03.ConsoleUI
{
    public class Garage
    {
        private bool m_IsFirstClient = true;// a boolen variable that holds if the dictionar is empty
        private GaragesLogic m_logic = new GaragesLogic();  //Member that holds all the logical calcs and functions

        public GaragesLogic Logic
        {
            get{ return m_logic; }
            set{ m_logic = value; }
        }
        public bool IsFirstClient
        {
            get{return m_IsFirstClient; }
            set{ m_IsFirstClient = value; }
        }
        public void InsertVehicleToGarage()// this method checks if the vehicle has been in the garage in the past and enter it to the garage
        {
            string plateNumber;

            if(IsFirstClient == false)// if the dictionary is not empty 
            {
                Console.WriteLine("Please enter your vehicels license plate number: ");
                plateNumber = Console.ReadLine();
                try
                {
                    Logic.TryChangingStatus(plateNumber);// if client already exit in the system
                }
                catch(ArgumentException )
                {
                    Console.WriteLine(@"The license number: {0} does not match a vehicle in the grage.", plateNumber);
                    Console.WriteLine("Please answer this questions to enter your vehicle to our garage:");
                    Thread.Sleep(2500);
                    Console.Clear();
                    CreateNewVehicleAndInsertToGarage();
                }                
            }
            else                                                                 // if vehicle does not exist then creat a new vehicle
            {
                Console.WriteLine("Please answer this questions to enter your vehicle to our garage:");
                Thread.Sleep(2500);
                Console.Clear();
                CreateNewVehicleAndInsertToGarage();
                IsFirstClient = false;
            }
        }
        public void CreateNewVehicleAndInsertToGarage()// this method creates a new client and a new vehicle and enter it to the garage
        {
            string VehicleType = "a";
            bool isElectric = false;
            Client newClient;
            string doesUseElectric = " ";
            bool isValidInput = false;

            List<string> VehicleFutureDataMembers = new List<string>();// a list that will contain the future data members of the client and vehicle
            List<string> questionsForUser = new List<string>();// a list that will contain the questions to ask the user

            while (isValidInput != true)
            {
                try
                {
                    Console.Write(@"Please enter the vehicle type you are entering the garage:
<Car/Truck/Motorcyle>
");
                    VehicleType = Console.ReadLine();//get vehicle type
                    Vehicle.CheckUserInputForNewVehivle(VehicleType);
                    isValidInput = true;
                }
                catch(FormatException fex)
                {
                    Console.Write(fex.Message);
                    Console.WriteLine(Environment.NewLine);
                    Thread.Sleep(2500);
                }
            }           
            if(Logic.IsVehicleTruck(ref doesUseElectric, VehicleType) != true)
            {
                isValidInput = false;
                while (isValidInput != true)
                {
                    try
                    {
                        Console.WriteLine(@"Does your {0} has an electric engine?  <Y/N>", VehicleType);// check if the vehicle has an electric/gasoline engine
                        doesUseElectric = Console.ReadLine();
                        GaragesLogic.CheckerForYesNoQuestions(doesUseElectric);
                        isValidInput = true;
                    }
                    catch (FormatException fex)
                    {
                        Console.WriteLine(fex.Message);
                        Thread.Sleep(2500);
                    }
                }
            }     
            Logic.CreateNewVehicleAndInsertToGarageInputChecker(VehicleType, ref isElectric, doesUseElectric);
            questionsForUser = NewObjectCreator.GetQuestionsForUser(VehicleType, isElectric);               //get questions to ask the user 
            VehicleFutureDataMembers = GetDataMembers(questionsForUser, isElectric, VehicleType);                                    //get data members of client from the user
            newClient = new Client(NewObjectCreator.MakeNewVehicle(VehicleFutureDataMembers, VehicleType, isElectric), VehicleFutureDataMembers);//create a new client + create a new vehicle
            newClient.CalcEnergyPrecent();
            Logic.AddClient(newClient.clientVehicle.licenseNumber, newClient);
        }
      
        public static List<string> GetDataMembers(List<string> io_questions, bool i_isElectric, string i_VehicleType)
        {
            List<string> resDataMembersList = new List<string>();
            string input;
            int index = 1;
            bool isInputLegal = false;

            foreach(string str in io_questions)
            {
                while(isInputLegal == false)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine(str);
                        input = Console.ReadLine();
                        Checker.InputChecker(index, i_isElectric, i_VehicleType, input);
                        isInputLegal = true;
                        resDataMembersList.Add(input);
                    }
                    catch(FormatException fex)
                    {
                        Console.WriteLine(fex.Message);
                        isInputLegal = false;
                        Thread.Sleep(2000);
                    }
                    catch(ArgumentException aex)
                    {
                        Console.WriteLine(aex.Message);
                        isInputLegal = false;
                        Thread.Sleep(2000);
                    }
                    catch(ValueOutOfRangeExeption vex)
                    {
                        Console.WriteLine(vex.Message);
                        isInputLegal = false;
                        Thread.Sleep(2000);
                    }
                }
                isInputLegal = false;
                index++;                
            }
            return resDataMembersList;
        }

        public void ShowMainMenu()
        {
            char inputAsChar;
            int inputAsInt;
            string inputAsString;

            Console.Clear();
            Console.Write(@"
                                     WELCOME TO TAL AND GAL'S GARAGE
                                   ___________________________________

              1. To enter a new vehicle to the garage press (1)

              2. If you whould like to see a list of all the license number of 
                 the vehicel's in the garage press (2)

              3. To change a vehicle's status press (3)

              4. To inflate all the wheels of a vehicle to the max press (4)

              5. If you whould like to fuel a vehicle press (5)

              6. If yo whould like to charge an electric vehicle press (6)

              7. To view all the data of a specific vehicle press (7)

              8. To exit press (8)");
            Console.WriteLine(Environment.NewLine);
            inputAsChar = Console.ReadKey().KeyChar;
            try
            {
                inputAsString = char.ToString(inputAsChar);
                inputAsInt = int.Parse(inputAsString);
                SendInputToFunctionSwitch(inputAsInt);
                //ShowMainMenu();
            }
            catch(FormatException fex)
            {
                Console.Write(fex.Message);
                Thread.Sleep(1000);
                Console.Clear();
                ShowMainMenu();
            }
        }
        public void SendInputToFunctionSwitch(int i_input)
        {
            switch(i_input)
            {
                case 1:
                    {
                        Console.Clear();
                        UserChoseToINsertVehicleToGarage();
                        ShowMainMenu();
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        UserChoseToSeeLicenseList();
                        ShowMainMenu();
                        break;
                    }
                case 3:
                    {
                        Console.Clear();
                        UserChoseToChangeVehicleStatus();
                        ShowMainMenu();
                        break;
                    }
                case 4:
                    {
                        Console.Clear();
                        UserChoseToInflateWheels();
                        ShowMainMenu();
                        break;
                    }
                case 5:
                    {
                        Console.Clear();
                        UserChoseToFuelVehicle();
                        ShowMainMenu();
                        break;
                    }
                case 6:
                    {
                        Console.Clear();
                        UserChoseToChargeVehicle();
                        ShowMainMenu();
                        break;
                    }
                case 7:
                    {
                        Console.Clear();
                        UserChoseToSeeVehicleData();
                        ShowMainMenu();
                        break;
                    }
                case 8:
                    {
                        Console.Clear();
                        UserChoseToExit();
                        break;
                    }
                default:
                    {
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Wrong input ...   Try again");
                        Thread.Sleep(1000);
                        ShowMainMenu();
                        break;
                    }
            }          
        }

        public void UserChoseToInflateWheels()
        {
            string plateNumber;

            Console.WriteLine("Please enter your plate number");
            plateNumber = Console.ReadLine();

            try
            {
                Logic.TryToInflate(plateNumber);
            }
            catch(ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
                Thread.Sleep(4000);
                ShowMainMenu();                  
            }
        }

        public void UserChoseToExit()
        {
            Console.WriteLine("Goodbye...");
            Thread.Sleep(2500);
        }
        public void UserChoseToINsertVehicleToGarage()
        {   
            InsertVehicleToGarage();
        }
        public void UserChoseToChangeVehicleStatus()
        {
            Client.eVehicleStatus status=new Client.eVehicleStatus();
            string plateNumber;            
            bool isInputLegal=false;

            Console.WriteLine("Please enter your vehicels license plate number: ");
            plateNumber = Console.ReadLine();
            while (isInputLegal == false)
            {
                try
                {
                    Console.WriteLine("Whats your vehicels new status? <Payed/Fixed/Inprocess>");
                    status=Client.GetVehicleStatus(ref isInputLegal);
                }
                catch(FormatException fex)
                {
                    Console.WriteLine(fex.Message);
                }
            }
            if(Logic.ChangeVehicelsStatus(plateNumber, status) == true)
            {
                Console.Clear();
                Console.WriteLine("Status changed Successfully.");
                Console.WriteLine(@"Plate number: {0} is now in status: {1}.", plateNumber, status);
                Thread.Sleep(4000);
            }
            else
            {
                Console.Clear();
                Console.WriteLine(@"Plate number: {0} was not found in the system... ",plateNumber);
                Thread.Sleep(4000);               
            }          
        }      
        public void UserChoseToSeeVehicleData()
        {
            string vehicleLicensePlate;
            List<string> vehicelsData;
            Client client;
            bool isSearchSuccsesful=new bool();
            char tmp;
            try
            {
                Console.WriteLine("Please enter the license number of the vehicle you wish to view all the data: ");
                vehicleLicensePlate = Console.ReadLine();
                  Vehicle.CheckVehiclePlate(vehicleLicensePlate);

                client = Logic.TryGetClient(vehicleLicensePlate, ref isSearchSuccsesful);
                if(isSearchSuccsesful==true)
                {
                    vehicelsData = client.GetVehicelsDataByUserChoise();
                    ShowAllVehiclesData(vehicelsData);
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Press any key to return to menu");
                    tmp = Console.ReadKey().KeyChar;
                }
                else
                {
                    Console.WriteLine(@"License plate number: {0) was not found.", vehicleLicensePlate);
                    Thread.Sleep(40000);
                    Console.Clear();
                }
            }
            catch(FormatException fex)
            {
                Console.WriteLine(fex.Message);
                Thread.Sleep(4000);
                Console.Clear();
            }
        }
        public static void ShowAllVehiclesData(List<string> i_data)
        {
            Console.Clear();
            Console.Write(@"
         Vehicle Data
         _____________");
            Console.WriteLine(Environment.NewLine);
            foreach(string data in i_data)
            {
                Console.WriteLine("     " + data);
            }
        }

        public void UserChoseToSeeLicenseList()
        {
            char usersChoise;

            List<Client.eVehicleStatus> statusList = new List<Client.eVehicleStatus>();
            List<string> resLicenseNumber = new List<string>();

            Console.WriteLine("Whould you like to filter some of the parameters ? <Y/N>");
            usersChoise = Console.ReadKey().KeyChar;
            try
            {
               if(Logic.CheckIfUserWantsFilter(usersChoise,ref statusList)==true)
                {                  
                    FilterParameters(ref statusList);   
                }
            }
            catch(ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
                Thread.Sleep(5000);
                ShowMainMenu();
            }

            resLicenseNumber = Logic.GetLicenseNumbers(statusList);
            PrintResLicenseNumber(resLicenseNumber);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press any key to return to main menu");
            usersChoise = Console.ReadKey().KeyChar;
        }
        public void FilterParameters(ref List<Client.eVehicleStatus> io_statusList)
        {
            FilterInprocess(ref io_statusList);
            FilterPayed(ref io_statusList);
            FilterFixed(ref io_statusList);
        }
        public void PrintResLicenseNumber(List<string> i_LicenseList)
        {
            Console.WriteLine(Environment.NewLine);
            if(i_LicenseList.Capacity == 0)
            {
                Console.WriteLine("There are no Vehicels matches the search.");
            }
            else
            {
                foreach (string plateNumber in i_LicenseList)
                {
                    Console.WriteLine(plateNumber);
                }
            }
        }

        public void FilterPayed(ref List<Client.eVehicleStatus> io_statusList)
        {
            char usersChoise;
            bool isInputLegal = false;

            while(isInputLegal == false)
            {
                Console.WriteLine("Whould you like to see 'Payed' status vehicels ? <Y/N> ");
                usersChoise = Console.ReadKey().KeyChar;
                try
                {
                    Logic.CheckIfUserWantsToFilterPayed(usersChoise,ref io_statusList);
                    isInputLegal = true;
                }
                catch(ArgumentException argEx)
                {
                    Console.WriteLine(argEx.Message);
                    FilterPayed(ref io_statusList);
                }
            }
        }
        public void FilterFixed(ref List<Client.eVehicleStatus> io_statusList)
        {
            char usersChoise;
            bool isInputLegal = false;

            while(isInputLegal == false)
            {
                Console.WriteLine("Whould you like to see 'Fixed' status vehicels ? <Y/N> ");
                usersChoise = Console.ReadKey().KeyChar;
                try
                {
                    Logic.CheckIfUserWantsToFilterFixed(usersChoise, ref io_statusList);
                    isInputLegal = true;
                }
                catch (ArgumentException argEx)
                {
                    Console.WriteLine(argEx.Message);
                    FilterPayed(ref io_statusList);
                }
            }
        }
        public void FilterInprocess(ref List<Client.eVehicleStatus> io_statusList)
        {
            char usersChoise;
            bool isInputLegal = false;

            while(isInputLegal == false)
            {
                Console.WriteLine("Whould you like to see 'Inprocess' status vehicels ? <Y/N> ");
                usersChoise = Console.ReadKey().KeyChar;
                try
                {
                    Logic.CheckIfUserWantsToFilterInprocess(usersChoise, ref io_statusList);
                    isInputLegal = true;
                }
                catch(ArgumentException argEx)
                {
                    Console.WriteLine(argEx.Message);
                    FilterPayed(ref io_statusList);
                }
            }
        }

        public void UserChoseToChargeVehicle()
        {
            string licenseNumber;
            Client client;
            bool isSearchSuccsesful = new bool();

            Console.WriteLine("What is the license number of the vehicle you whould like to charge?");
            licenseNumber = Console.ReadLine();
            client = Logic.TryGetClient(licenseNumber, ref isSearchSuccsesful);

            if (isSearchSuccsesful == false)
            {
                LicensePlateDoesNotExistChargeEngine();
            }
            else
            {
                LicensePlateExistChargeEngine(ref client);
            }
        }
        public void LicensePlateDoesNotExistChargeEngine()
        {
            char input;

            Console.WriteLine("The vehicle does not exist in the garage.");
            Console.WriteLine("Press any key to return to main menu.");
            input = Console.ReadKey().KeyChar;
        }
        public void LicensePlateExistChargeEngine(ref Client i_client)
        {
            char input;
            float minutesToCharge = new float();
            bool isMinutesLegal = false;

            while (isMinutesLegal == false)
            {
                Console.WriteLine("How many minutes would you like to charge your vehicle");
                try
                {
                    minutesToCharge = float.Parse(Console.ReadLine());
                    isMinutesLegal = true;
                }
                catch (FormatException fex)
                {
                    Console.WriteLine(fex.Message);
                }
            }
            try
            {
                i_client.CheckIfEngineIsElectricAndCharge(minutesToCharge);
            }
            catch (ArgumentException arex)
            {
                Console.WriteLine(arex.Message);
            }
            catch (ValueOutOfRangeExeption voorex)
            {
                Console.WriteLine(voorex.Message);
            }
            Console.WriteLine("Press any key to return to main menu.");
            input = Console.ReadKey().KeyChar;
        }

        public void UserChoseToFuelVehicle()
        {
            string licenseNumber;
            Client client;
            bool isSearchSuccsesful = new bool();

            Console.WriteLine("What is the license number of the vehicle you whould like to fuel?");
            licenseNumber = Console.ReadLine();
            client = Logic.TryGetClient(licenseNumber, ref isSearchSuccsesful);
            if(isSearchSuccsesful == false)
            {
                LicensePlateDoesNotExistFuelEngine();
            }
            else
            {
                LicensePlateExistFuelEngine(ref client);
            }
        }
        public void LicensePlateDoesNotExistFuelEngine()
        {
            char input;

            Console.WriteLine("The vehicle does not exist in the garage.");
            Console.WriteLine("Press any key to return to main menu.");
            input = Console.ReadKey().KeyChar;
        }

        public void LicensePlateExistFuelEngine(ref Client i_client)
        {
            char input;
            float litersToFuel = new float();
            bool isMinutesLegal = false;
            string gasType;
            bool isFuelKindLegal = false;
            GasolineEngine ClientEngine;
            while(isFuelKindLegal == false)
            {
                try
                {
                    Console.Write(@"Please enter the fuel type of your vehicle:
<Soler/Octan95/Octan96/Octan98>
");
                    gasType = Console.ReadLine();
                    ClientEngine = i_client.clientVehicle.engine as GasolineEngine;
                    ClientEngine.CheckFuelType(gasType);
                    isFuelKindLegal = true;
                }
                catch (FormatException fex)
                {
                    Console.Write(fex.Message);
                }
                catch (ArgumentException aex)
                {
                    Console.Write(aex.Message);
                }
            }            
            while (isMinutesLegal == false)
            {
                Console.WriteLine("How many liters would you like to fuel your vehicels engine");
                try
                {
                    litersToFuel = float.Parse(Console.ReadLine());
                    isMinutesLegal = true;
                }
                catch (FormatException fex)
                {
                    Console.WriteLine(fex.Message);
                }
            }
            try
            {
                i_client.CheckIfEngineIsGasolineAndFuel(litersToFuel);
            }
            catch (ArgumentException arex)
            {
                Console.WriteLine(arex.Message);
            }
            catch (ValueOutOfRangeExeption voorex)
            {
                Console.WriteLine(voorex.Message);
            }
            Console.WriteLine("Press any key to return to main menu.");
            input = Console.ReadKey().KeyChar;
        }
    }
}
