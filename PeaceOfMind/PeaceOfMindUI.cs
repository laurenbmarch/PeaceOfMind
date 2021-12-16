using Newtonsoft.Json;
using PeaceOfMind.Data;
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
        private HttpClient _client = new HttpClient();
        public void Run()
        {
            var accesstoken = Login();
            MainMenu(accesstoken);
        }
        private string Login()
        {
            bool loggingIn = true;
            while (loggingIn)
            {
                Console.WriteLine("Welcome to Peace Of Mind.\n"
                    + "Do you already have an account? ( y / n )");
                string hasAccount = Console.ReadLine().ToLower();
                Console.Clear();
                if (hasAccount == "y")
                {
                    var user = FindExistingUser();
                    var token = GetUserToken(user);
                    if (token is null)
                    {
                        Console.WriteLine("That login is incorrect.");
                        PressAnyKey();
                    }
                    else
                    {
                        loggingIn = false;
                        return token.AccessToken;
                    }
                }
                if (hasAccount == "n")
                {
                    var user = CreateApplicationUser();
                    var token = GetUserToken(user);
                    if (token is null)
                    {
                        Console.WriteLine("Your logging could not be created.");
                        PressAnyKey();
                    }
                    else
                    {
                        loggingIn = false;
                        return token.AccessToken;
                    }
                }
            }
            return null;
        }
        private void MainMenu(string accessToken)
        {
            Console.Clear();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
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
                        DisplayAllOfficeLocations();
                        PressAnyKey();
                        break;
                    case 3:
                        GetASpecificOfficeLocation();
                        PressAnyKey();
                        break;
                    case 4:
                        UpdateAnOfficeLocation();
                        PressAnyKey();
                        break;
                    case 5:
                        PressAnyKey();
                        break;
                    case 6:
                        PressAnyKey();
                        break;
                    case 7:
                        CreateTherapist();
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
        private AppUserModel FindExistingUser()
        {
            Console.WriteLine("Please enter your email:");
            string email = Console.ReadLine();
            Console.WriteLine("Please enter your password:");
            string password = Console.ReadLine();
            var user = new AppUserModel();
            user.Email = email;
            user.Password = password;
            user.GrantType = "password";
            return user;
        }
        private AppUserModel CreateApplicationUser()
        {
            Console.WriteLine("Please enter your email.");
            string email = Console.ReadLine();
            Console.WriteLine("Please enter your password.");
            string password = Console.ReadLine();
            Console.WriteLine("Please re-enter your password.");
            string confirmPassword = Console.ReadLine();
            Console.Clear();
            var registerUserPairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("confirmpassword", confirmPassword)
            };
            var userContent = new FormUrlEncodedContent(registerUserPairs);
            var model = new AppUserModel();
            model.Email = email;
            model.Password = password;
            model.ConfirmPassword = confirmPassword;
            model.GrantType = "password";
            var result = _client.PostAsync("https://localhost:44301/api/Account/Register", userContent).Result;
            return model;
        }
        private UserTokenModel GetUserToken(AppUserModel appUser)
        {
            var tokenSubmit =
            new GetTokenModel
            {
                UserName = appUser.Email,
                Password = appUser.Password,
                GrantType = appUser.GrantType
            };
            // string outgoingJson = JsonConvert.SerializeObject(tokenSubmit);
            // var serializeJson = new StringContent(outgoingJson, Encoding.UTF8, "application/x-www-form-urlencoded");
            var tokenPostPairs = new List<KeyValuePair<string, string>>
           {
               new KeyValuePair<string, string>("username", tokenSubmit.UserName),
               new KeyValuePair<string, string>("password", tokenSubmit.Password),
               new KeyValuePair<string, string>("grant_type", tokenSubmit.GrantType)
           };
            var tokenContent = new FormUrlEncodedContent(tokenPostPairs);
            HttpResponseMessage response = _client.PostAsync("https://localhost:44301/token", tokenContent).Result;
            if (response.IsSuccessStatusCode)
            {
                var tokenJson = response.Content.ReadAsStringAsync().Result;
                UserTokenModel token = JsonConvert.DeserializeObject<UserTokenModel>(tokenJson);
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
            var createdOfficePairs = CollectOfficeKeyPairs();
            var officeContent = new FormUrlEncodedContent(createdOfficePairs);
            var createdOffice = _client.PostAsync($"https://localhost:44301/api/OfficeLocation", (officeContent)).Result;
            if (createdOffice.IsSuccessStatusCode)
            {
                Console.WriteLine("The office location was created.");
            }
            else
            {
                Console.WriteLine("The office location could not be created.");
            }
        }
        private List<KeyValuePair<string, string>> CollectOfficeKeyPairs()
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
            var serializedJson = JsonConvert.SerializeObject(model);
            var createOfficePairs = new List<KeyValuePair<string, string>>
           {
               new KeyValuePair<string, string>("AddressNumber", model.AddressNumber.ToString()),
               new KeyValuePair<string, string>("StreetName", model.StreetName),
               new KeyValuePair<string, string>("City", model.City),
               new KeyValuePair<string, string>("State", model.State),
               new KeyValuePair<string, string>("Zipcode", model.ZipCode),
               new KeyValuePair<string, string>("Country", model.Country)
           };
            return createOfficePairs;
        }
        private void DisplayAllOfficeLocations()
        {
            Console.Clear();
            var offices = _client.GetAsync("https://localhost:44301/api/OfficeLocation").Result;
            //var offices = _oLController.Get();
            if (offices.IsSuccessStatusCode)
            {
                var officeJson = offices.Content.ReadAsStringAsync().Result;
                List<OfficeLocationGetItem> officesList = JsonConvert.DeserializeObject<List<OfficeLocationGetItem>>(officeJson);
                foreach (OfficeLocationGetItem office in officesList)
                {
                    Console.WriteLine($" OfficeId: {office.OfficeLocationId}\n"
                        + $" {office.AddressNumber} {office.StreetName}, {office.City}, {office.State} \n"
                        + $" Therapists at this location: {office.TherapistCount}\n");
                }
            }
        }
        private void GetASpecificOfficeLocation()
        {
            Console.WriteLine("Do you know the Office Id that you would like to view? ( y / n)");
            string userResponse = Console.ReadLine().ToLower();
            if (userResponse == "y")
            {
                Console.Clear();
                DisplayOfficeLocation();
            }
            if (userResponse == "n")
            {
                DisplayAllOfficeLocations();
                PressAnyKey();
                DisplayOfficeLocation();
            }
        }
        private OfficeLocationModel GetOfficeLocationById(int officeId)
        {
            var office = _client.GetAsync($"https://localhost:44301/api/OfficeLocation/{officeId}").Result;
            //var office = _oLController.GetById(officeId);
            if (office.IsSuccessStatusCode)
            {
                var officeJson = office.Content.ReadAsStringAsync().Result;
                var officeObject = JsonConvert.DeserializeObject<OfficeLocationModel>(officeJson);
                return officeObject;
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
        private void UpdateAnOfficeLocation()
        {
            Console.Clear();
            Console.WriteLine("Do you know the id of the office location that you want to update? ( y / n )");
            string userInput = Console.ReadLine().ToLower();
            Console.Clear();
            if (userInput == "y")
            {
                Console.WriteLine("Please enter the office Id of the office you want to update:");
                int officeId = int.Parse(Console.ReadLine());
                var foundOffice = GetOfficeLocationById(officeId);
                if (foundOffice != null)
                {
                    var updateOfficePairs = CollectOfficeKeyPairs();
                    var updateOfficeContent = new FormUrlEncodedContent(updateOfficePairs);
                    var response = _client.PutAsync($"https://localhost:44301/api/OfficeLocation/{officeId}", updateOfficeContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.Clear();
                        Console.WriteLine("The office location was updated.");
                        PressAnyKey();

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("The office could not be updated.");
                        PressAnyKey();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("That office location does not exist.");
                    PressAnyKey();
                }
            }
            if (userInput == "n")
            {
                Console.Clear();
                DisplayAllOfficeLocations();
                Console.WriteLine("\n\nPlease enter the office Id of the office you want to update:");
                int officeId = int.Parse(Console.ReadLine());
                var foundOffice = GetOfficeLocationById(officeId);
                if (foundOffice != null)
                {
                    var updateOfficePairs = CollectOfficeKeyPairs();
                    var updateOfficeContent = new FormUrlEncodedContent(updateOfficePairs);
                    var response = _client.PutAsync($"https://localhost:44301/api/OfficeLocation/{officeId}", updateOfficeContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.Clear();
                        Console.WriteLine("The office location was updated.");
                        PressAnyKey();

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("The office could not be updated.");
                        PressAnyKey();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("That office location does not exist.");
                    PressAnyKey();
                }

            }
        }

        private List<KeyValuePair<string, string>> CollectTherapistKeyPairs()
        {
            var model = new TherapistModel();
            Console.WriteLine("Please enter the Last Name");
            model.LastName = Console.ReadLine();
            Console.WriteLine("Please enter the First Name");
            model.FirstName = Console.ReadLine();
            Console.WriteLine("Please enter the Gender");
            model.Gender = Console.ReadLine();
            Console.WriteLine("Please enter the License or Degree");
            model.LicenseOrDegree = Console.ReadLine();
            Console.WriteLine("Please enter the area of specialty this therapist is in (1-9)\n" 
                + "1, Psychotherapy\n"
                + "2, Anxiety\n"
                + "3, Depression\n" 
                + "4, Family\n"
                + "5, Marriage\n"
                + "6, Young_Adult\n"
                + "7, Substance_Abuse\n"
                + "8, Grief\n"
                + "9, Trauma");
            var response = int.Parse(Console.ReadLine());
            switch (response)
            {
                case 1:
                //model.AreaOfSpecialty
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
            }

            Console.Clear();
            var serializedJson = JsonConvert.SerializeObject(model);
            var createTherapistPairs = new List<KeyValuePair<string, string>>
           {
               new KeyValuePair<string, string>("LastName", model.LastName),
               new KeyValuePair<string, string>("FirstName", model.FirstName),
               new KeyValuePair<string, string>("Gender", model.Gender),
               new KeyValuePair<string, string>("LicenseOrDegree", model.LicenseOrDegree),
             //  new KeyValuePair<string, string>("AreaOfSpecialty", model.AreaOfSpecialty)
           };
            return createTherapistPairs;
        }
        private void CreateTherapist()
        {
            var createdTherapistPairs = CollectTherapistKeyPairs();
            var therapistContent = new FormUrlEncodedContent(createdTherapistPairs);
            var createdTherapist = _client.PostAsync($"https://localhost:44301/api/Therapist", (therapistContent)).Result;
            if (createdTherapist.IsSuccessStatusCode)
            {
                Console.WriteLine("The therapist was created.");
            }
            else
            {
                Console.WriteLine("The therapist could not be created.");
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



