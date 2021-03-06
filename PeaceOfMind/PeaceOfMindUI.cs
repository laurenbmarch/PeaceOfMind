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
                    var user = FindExsistingUser();
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
                        DeleteAnOfficeLocation();
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
        private AppUserModel FindExsistingUser()
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
            // var serializedJson = JsonConvert.SerializeObject(model);
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
                    Console.WriteLine("That office location does not exsist.");
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
                    Console.WriteLine("That office location does not exsist.");
                    PressAnyKey();
                }

            }
        }
        private void DeleteAnOfficeLocation()
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
                    var response = _client.DeleteAsync($"https://localhost:44301/api/OfficeLocation/{officeId}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.Clear();
                        Console.WriteLine("The office location was deleted.");
                        PressAnyKey();

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("The office could not be deleted.");
                        PressAnyKey();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("That office location does not exsist.");
                    PressAnyKey();
                }
            }
            if (userInput == "n")
            {
                Console.Clear();
                DisplayAllOfficeLocations();
                Console.WriteLine("\n\nPlease enter the office Id of the office you want to delete:");
                int officeId = int.Parse(Console.ReadLine());
                var foundOffice = GetOfficeLocationById(officeId);
                if (foundOffice != null)
                {

                    var response = _client.DeleteAsync($"https://localhost:44301/api/OfficeLocation/{officeId}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.Clear();
                        Console.WriteLine("The office location was deleted.");
                        PressAnyKey();

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("The office could not be deleted.");
                        PressAnyKey();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("That office location does not exsist.");
                    PressAnyKey();
                }
            }
        }
        private List<KeyValuePair<string, string>> CollectTherapistKeyPairs()
        {
            Console.Clear();
            Console.WriteLine("Please enter the first name of the therapist you wouldf like to add:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Please emter the last name of therapist: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Please enter the therapists gender: ");
            string gender = Console.ReadLine();
            // Could impliment all of the therapists credentials
            Console.WriteLine("Please enter one of the therapists Licenses or degrees");
            string licenseOrDegrees = Console.ReadLine();
            var areaOfSpecialities = CollectAreaOfSpecialities();
            var therapistKeyValuePairs = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("LastName", lastName),
                new KeyValuePair<string, string>("FirstName", firstName),
                new KeyValuePair<string, string>("Gender", gender),
                new KeyValuePair<string, string>("LicenseOrDegree", licenseOrDegrees),
                new KeyValuePair<string, string>("AreaOfSpecialty", areaOfSpecialities.ToString()),
            };
            return therapistKeyValuePairs;
        }
        public void CreateTherapist()
        {
            var therapistKeyValuePairs = CollectTherapistKeyPairs();
            var therapistContent = new FormUrlEncodedContent(therapistKeyValuePairs);
            var createdOffice = _client.PostAsync($"https://localhost:44301/api/Therapist", (therapistContent)).Result;
            if (createdOffice.IsSuccessStatusCode)
            {
                Console.WriteLine("The therapist was created.");
            }
            else
            {
                Console.WriteLine("The therapist could not be created.");
            }
        }
        public List<string> CollectAreaOfSpecialities()
        {
            Console.Clear();
            var listOfSpecialities = new List<string>();
            Console.WriteLine("Please enter the number of the speciality you would like to add: \n"
            + "1 = Psychotherapy,\n"
            + "2 = Anxiety,\n"
            + "3 = Depression,\n"
            + "4 = Family,\n"
            + "5 = Marriage,\n"
            + "6 = Young_Adult,\n"
            + "7 = Substance_Abuse\n "
            + "8 = Grief\n"
            + "9 = Trauma");
            string areaOfSpecialty = Console.ReadLine().ToLower();
            if (areaOfSpecialty == "psychotherapy")
                listOfSpecialities.Add(areaOfSpecialty);
            if (areaOfSpecialty == "anxiety")
                listOfSpecialities.Add(areaOfSpecialty);
            if (areaOfSpecialty == "depression")
                listOfSpecialities.Add(areaOfSpecialty);
            if (areaOfSpecialty == "family")
                listOfSpecialities.Add(areaOfSpecialty);
            if (areaOfSpecialty == "young adult")
                listOfSpecialities.Add(areaOfSpecialty);
            if (areaOfSpecialty == "substance abuse")
                listOfSpecialities.Add(areaOfSpecialty);
            if (areaOfSpecialty == "marriage")
                listOfSpecialities.Add(areaOfSpecialty);
            if (areaOfSpecialty == "grief")
                listOfSpecialities.Add(areaOfSpecialty);
            if (areaOfSpecialty == "trauma")
                listOfSpecialities.Add(areaOfSpecialty);
        Console.Clear();
            bool whileAdding = true;
            while (whileAdding)
            {
                Console.Clear();
                Console.WriteLine("Would you like to add another Area of speciality? ( y / n)");
                string userResponse = Console.ReadLine().ToLower();
                if (userResponse == "y")
                {
                    {
                        Console.WriteLine("Please type the speciality you would like to add: \n"
                     + "Psychotherapy,\n"
                     + "Anxiety,\n"
                     + "Depression,\n"
                     + "Family,\n"
                     + "Marriage,\n"
                     + "Young_Adult,\n"
                     + "Substance_Abuse\n"
                     + "Grief\n"
                     + "Trauma");
                        string specialty = Console.ReadLine().ToLower();
                        if (specialty == "psychotherapy")
                            listOfSpecialities.Add(specialty);
                        if (specialty == "anxiety")
                            listOfSpecialities.Add(specialty);
                        if (specialty == "depression")
                            listOfSpecialities.Add(specialty);
                        if (specialty == "family")
                            listOfSpecialities.Add(specialty);
                        if (specialty == "young adult")
                            listOfSpecialities.Add(specialty);
                        if (specialty == "substance abuse")
                            listOfSpecialities.Add(specialty);
                        if (specialty == "marriage")
                            listOfSpecialities.Add(specialty);
                        if (specialty == "grief")
                            listOfSpecialities.Add(specialty);
                        if (specialty == "trauma")
                            listOfSpecialities.Add(specialty);
                    }
                }
                if (userResponse == "n")
                {
                    Console.Clear();
                    Console.WriteLine("Your list of specialities has been created");
                    whileAdding = false;
                    return listOfSpecialities;
                }
            }
            return null;
        }
        /*public List<AreaOfSpecialty> CollectAreaOfSpecialities()
        {
            Console.Clear();
            var listOfSpecialities = new List<AreaOfSpecialty>();
            Console.WriteLine("Please enter the number of the speciality you would like to add: \n"
            + "1 = Psychotherapy,\n"
            + "2 = Anxiety,\n"
            + "3 = Depression,\n"
            + "4 = Family,\n"
            + "5 = Marriage,\n"
            + "6 = Young_Adult,\n"
            + "7 = Substance_Abuse\n "
            + "8 = Grief\n"
            + "9 = Trauma");
            int areaOfSpeciality = int.Parse(Console.ReadLine());
            switch (areaOfSpeciality)
            {
                case 1:
                    listOfSpecialities.Add(Data.AreaOfSpecialty.Psychotherapy);
                    break;
                case 2:
                    listOfSpecialities.Add(Data.AreaOfSpecialty.Depression);
                    break;
                case 3:
                    listOfSpecialities.Add(Data.AreaOfSpecialty.Anxiety);
                    break;
                case 4:
                    listOfSpecialities.Add(Data.AreaOfSpecialty.Family);
                    break;
                case 5:
                    listOfSpecialities.Add(Data.AreaOfSpecialty.Marriage);
                    break;
                case 6:
                    listOfSpecialities.Add(Data.AreaOfSpecialty.Young_Adult);
                    break;
                case 7:
                    listOfSpecialities.Add(Data.AreaOfSpecialty.Substance_Abuse);
                    break;
                case 8:
                    listOfSpecialities.Add(Data.AreaOfSpecialty.Grief);
                    break;
                case 9:
                    listOfSpecialities.Add(Data.AreaOfSpecialty.Trauma);
                    break;
                default:
                    Console.WriteLine("Please enter a valid option...");
                    PressAnyKey();
                    break;
            }
            Console.Clear();
            bool whileAdding = true;
            while (whileAdding)
            {
                Console.Clear();
                Console.WriteLine("Would you like to add another Area of speciality? ( y / n)");
                string userResponse = Console.ReadLine().ToLower();
                if (userResponse == "y")
                {
                    {
                        Console.WriteLine("Please enter the number of the speciality you would like to add: \n"
                     + "1 = Psychotherapy,\n"
                     + "2 = Anxiety,\n"
                     + "3 = Depression,\n"
                     + "4 = Family,\n"
                     + "5 = Marriage,\n"
                     + "6 = Young_Adult,\n"
                     + "7 = Substance_Abuse\n"
                     + "8 = Grief\n"
                     + "9 = Trauma");
                        int speciality = int.Parse(Console.ReadLine());
                        switch (speciality)
                        {
                            case 1:
                                listOfSpecialities.Add(Data.AreaOfSpecialty.Psychotherapy);
                                break;
                            case 2:
                                listOfSpecialities.Add(Data.AreaOfSpecialty.Depression);
                                break;
                            case 3:
                                listOfSpecialities.Add(Data.AreaOfSpecialty.Anxiety);
                                break;
                            case 4:
                                listOfSpecialities.Add(Data.AreaOfSpecialty.Family);
                                break;
                            case 5:
                                listOfSpecialities.Add(Data.AreaOfSpecialty.Marriage);
                                break;
                            case 6:
                                listOfSpecialities.Add(Data.AreaOfSpecialty.Young_Adult);
                                break;
                            case 7:
                                listOfSpecialities.Add(Data.AreaOfSpecialty.Substance_Abuse);
                                break;
                            case 8:
                                listOfSpecialities.Add(Data.AreaOfSpecialty.Grief);
                                break;
                            case 9:
                                listOfSpecialities.Add(Data.AreaOfSpecialty.Trauma);
                                break;
                            default:
                                Console.WriteLine("Please enter a valid option...");
                                PressAnyKey();
                                break;
                        }       
                    }
                }
                        if (userResponse == "n")
                        {
                            Console.Clear();
                            Console.WriteLine("Your list of specialities has been created");
                            whileAdding = false;
                            return listOfSpecialities;
                        }
            }
            return null;
        }*/
        public void PressAnyKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

