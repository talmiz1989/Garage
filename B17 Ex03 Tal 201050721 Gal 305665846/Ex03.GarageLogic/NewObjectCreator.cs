using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
   public class NewObjectCreator  //a class that can create any vehicle object
    {       
        public static List<string> GetQuestionsForUser(string i_vehicleName,bool i_isElectric)// this method gets the questions needed to ask the user about his vehicle
        {
            List<string> question=new List<string>();                           //create the questions list

            Client.AddClientQuestions(ref question);                            //add client questions to the list
            Vehicle.AddVehicleQuestions(ref question);                          //add vehicle questions to the list

            if (i_vehicleName.CompareTo("Truck")==0)                            //if the vehicle is a truck
            {
                Truck.AddTruckQuestions(ref question);                          //add truck questions to the list
            }
            else if(i_vehicleName.CompareTo("Car") == 0)                        //if the vehicle is a car
            {
                Car.AddCarQuestions(ref question);                              //add car questions to the list
            }
            else if(i_vehicleName.CompareTo("Motorcycle") == 0)                //if the vehicle is a motorcycle
            {
                Motorcycle.AddMotorcycleQuestions(ref question);                //add motorcycle questions to the list
            }
            Wheel.AddWheelQuestions(ref question);                              //add wheels questions to the list
            Engine.AddEngineQuestions(ref question, i_isElectric);              //add engine questions to the list

            return question;
        }
        public static Vehicle MakeNewVehicle(List<string> io_vehicleDataMembers,string i_vehicleName,bool i_isElectric)//this method creates a new vehicle and returns it
        {
            Vehicle newVehicle;

            if(i_vehicleName.CompareTo("Car")==0)       //if the vhicle is a car
            {
                newVehicle = new Car(io_vehicleDataMembers, i_isElectric,i_vehicleName);        //create a new car
            }
            else if(i_vehicleName.CompareTo("Truck") == 0)  //if the vhicle is a truck
            {
                newVehicle = new Truck(io_vehicleDataMembers, i_isElectric, i_vehicleName);     //create a new truck
            }
            else//if the vhicle is a motorcycle
            {
                newVehicle= new Motorcycle(io_vehicleDataMembers, i_isElectric, i_vehicleName);     //create a new motorcycle
            }
            return newVehicle;
        }
    }
}
