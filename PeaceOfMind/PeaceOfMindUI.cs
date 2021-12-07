using Newtonsoft.Json;
using PeaceOfMind.Models;
using PeaceOfMind.WebApi.Controllers;
using PeaceOfMind.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.ConsoleApp
{
    class PeaceOfMindUI
    {
        private AccountController _aController = new AccountController();
        private OfficeLocationController _oLController = new OfficeLocationController();
        private TherapistController _tController = new TherapistController();
        private HttpClient _client = new HttpClient();

        public void Run()
        {
            Login();
            MainMenu();
        }
         private async Task Login()
          {              
              Console.WriteLine("Welcome to Peace Of Mind.\n"
                  + "Do you already have an account? ( y / n )");
              string hasAccount = Console.ReadLine().ToLower();
              Console.Clear();
              if (hasAccount == "y")
              {
              }
              if (hasAccount == "n")
              {
               await  CreateApplicationUser();
              }
          }
        private void MainMenu()
        {           
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Welcome to Peace Of Mind.\n"
                    + "What would you like to do?\n\n"
                    + "1. Create an Office Location            7. Create a Therapist\n"
                    + "2. See all Office Locations             8. See all Therapists\n"
                    + "3. Find a specific Office Location      9. Find a specific Therapist\n"
                    + "4. Update an Office Location            10. Update a Therapist\n"
                    + "5. Delete an Office Location            11. Delete a Therapist\n"
                    + "6. Add a therapist to Office Location   12. Add a rating to a Therapist\n\n"
                    + "13. Exit the program"
                    );
                int mainResponse = int.Parse(Console.ReadLine());
                switch (mainResponse)
                {
                    case 1:
                        CreateOfficeLocation();
                        PressAnyKey();
                        break;
                    case 2:
                        GetAllOfficeLocations();
                        PressAnyKey();
                        break;
                    case 3:
                        GetASpecificOfficeLocation();
                        PressAnyKey();
                        break;
                    case 4:
                        PressAnyKey();
                        break;
                    case 5:
                        PressAnyKey();
                        break;
                    case 6:
                        PressAnyKey();
                        break;
                    case 7:
                        PressAnyKey();
                        break;
                    case 8:
                        PressAnyKey();
                        break;
                    case 9:
                        PressAnyKey();
                        break;
                    case 10:
                        PressAnyKey();
                        break;
                    case 11:
                        PressAnyKey();
                        break;
                    case 12:
                        PressAnyKey();
                        break;
                    case 13:
                        Console.WriteLine("Goodbye!");
                        System.Threading.Thread.Sleep(1500);
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid option...");
                        PressAnyKey();
                        break;
                }
            }
        }
        private async Task<AppUserModel> CreateApplicationUser()
        {
            var model = new AppUserModel();
            Console.WriteLine("Please enter your email.");
            model.Email = Console.ReadLine();
            Console.WriteLine("Please enter your password.");
            model.Password = Console.ReadLine();
            Console.WriteLine("Please re-enter your password.");
            model.ConfirmPassword = Console.ReadLine();
            model.GrantType = "password";
            await _aController.Register(ConvertToBindingModel(model));
            UserTokenModel  token = await GetUserToken(model);
            model.Token = token.AccessToken;             
            return model;
        }
       private async Task<UserTokenModel> GetUserToken(AppUserModel appUser)
        {
            var tokenSubmit =
                  new GetTokenModel
                  {
                      UserName = appUser.Email,
                      Password = appUser.Password,
                      GrantType = appUser.GrantType
                  };
            string outgoingJson = JsonConvert.SerializeObject(tokenSubmit);
            var serializeJson = new StringContent(outgoingJson, Encoding.UTF8, "application/x-www-form-urlencoded");
            HttpResponseMessage response = await _client.PostAsync("https://localhost:44301/token", serializeJson);
            if (response.IsSuccessStatusCode)
            {
                var incomingJson = response.ToString();
                UserTokenModel token = JsonConvert.DeserializeObject<UserTokenModel>(incomingJson);
                return token;
            }
            return null;
        }
        private RegisterBindingModel ConvertToBindingModel(AppUserModel userModel)
        {
            RegisterBindingModel model =
                new RegisterBindingModel
                {
                    Email = userModel.Email,
                    Password = userModel.Password,
                    ConfirmPassword = userModel.ConfirmPassword
                };
            return model;
        }
        private void CreateOfficeLocation()
        {
            var model = new OfficeLocationModel();
            Console.WriteLine("Please enter the Address number of the Office Location:");
            model.AddressNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the street name of the office location:");
            model.StreetName = Console.ReadLine();
            Console.WriteLine("Plesae the the city that the office is located in:");
            model.City = Console.ReadLine();
            Console.WriteLine("Please enter the state that the Office is located in:");
            model.State = Console.ReadLine();
            Console.WriteLine("Please enter the Zip code for the office location:");
            model.ZipCode = Console.ReadLine();
            Console.WriteLine("Please enter the Country that the office is located in:");
            model.Country = Console.ReadLine();
            Console.Clear();
            _oLController.Post(model);
        }
        private void GetAllOfficeLocations()
        {
            // var offices =   _client.GetAsync("https://localhost:44301/api/OfficeLocation");
            // offices.Wait();
            var offices = _oLController.Get();
            //if (offices.IsCompleted)
            // {
            // var officeTask = offices.Result;                
            // if (officeTask.IsSuccessStatusCode)
            // {
            var officeJson = offices.ToString();
            List<OfficeLocationGetItem> officesList = JsonConvert.DeserializeObject<dynamic>(officeJson);

            foreach (OfficeLocationGetItem office in officesList)
            {
                Console.WriteLine($" {office.OfficeLocationId}"
                    + $"{office.AddressNumber} {office.StreetName}, {office.City}, {office.State}"
                    + $"{office.TherapistCount}");
            }
            //  }
            //  }
        }
        private void GetASpecificOfficeLocation()
        {
            Console.WriteLine("Do you know the Office Id that you would like to view? ( y / n)");
            string userResponse = Console.ReadLine().ToLower();
            if(userResponse == "y")
            {
                Console.Clear();
                DisplayOfficeLocation();
            }
            if(userResponse == "n")
            {
                GetAllOfficeLocations();
                PressAnyKey();
                DisplayOfficeLocation();
            }
        }
        private OfficeLocationModel GetOfficeLocationById(int officeId)
        {
            var office = _client.GetAsync($"https://localhost:44301/api/OfficeLocation/{officeId}");
            office.Wait();
            //var office = _oLController.GetById(officeId);
            if (office.IsCompleted)
            {
                var officeTask = office.Result;
                if (officeTask.IsSuccessStatusCode)
                {
                    var officeJson = office.ToString();
                    var officeObject = JsonConvert.DeserializeObject<OfficeLocationModel>(officeJson);
                    return officeObject;
                }
            }
            return null;
        }     
        private void DisplayOfficeLocation()
        {
            Console.WriteLine("Please enter the Office Location Id:");
            var officeId = int.Parse(Console.ReadLine());
            OfficeLocationModel office = GetOfficeLocationById(officeId);
            Console.Clear();
            Console.WriteLine($"{office.AddressNumber} {office.StreetName}, {office.City}, {office.State}\n");
            foreach (TherapistModel t in office.Therapists)
            {
                Console.WriteLine($"Name: {t.FirstName} {t.LastName}\n"
                    + $"License and/or Degrees: {t.LicenseOrDegree}\n"
                    + $"Areas of specialty: {t.AreaOfSpecialty}");
            }
        }
        public void PressAnyKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

