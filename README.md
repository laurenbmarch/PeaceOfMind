![headerphoto](bb-header-for-readme.png)
# :small_blue_diamond: Blue Badge Group API Project 

The Blue Badge API Project is a group project designed by Eleven Fifty Academy to evaluate students' progress in the Software Development program. The assignment was to build a .NET Framework API Web Application using n-tier architecture covering a topic of our group's choosing. The API needed to include at least one custom table per group member, with at least one of the tables implementing Foreign Key relationships.

Our group created an application called "Peace of Mind". "Peace of Mind" is a tool that allows a user to access a database of mental health experts and therapists. They can correspond with a therapist to schedule time to meet in-person or virtually. Users can also search through the application to locate local experts in the field. The target audience is for adults who are looking for professional assistance with their mental health.
 
We chose this project because there is an increasing awareness about the subject mental health in our day to day lives. Individuals are prioritizing their mental health more than ever before and this app would be a tool to help individuals access mental health professionals in a convenient and private way.

## :computer: Step One: Building the Database
We started the project by each creating a table for the database:
* Therapist Table: Lauren
* Therapist Rating Table: Jacob
* Therapist Office Location Table: Austin

Each therapist in the database has the following attributes:
* ID Number (not visible to the user)
* First and Last Name
* Gender
* Licensure/Degree Information
* Areas of Specialty
* Physical Office Location(s)
* Average "Star" Rating from Users

Each therapist's average rating comes from the following attributes in the therapist rating table:
* Professionalism
* Availability
* Communication
* Effectiveness

Each therapist can be connected to a table with details of their physical office location(s):
* Location ID (not visible to the user)
* Name of the Office
* Street Number
* Street Name
* City
* State
* Zip Code
* Country

After completing the data models, we each completed our respective services and controllers to finish out the API.

## :microscope: Testing the API
We used Postman to test our project and identify areas in need of debugging. We used common HTTP methods to create and manipulate our data: GET, POST, PUT, and DELETE. 

Our biggest challenge included finding a way to connect individual therapsits to one or more office locations. We eventually created another table called "Office Location Therapists" that used two foreign keys (the Therapist ID number and the Office Location ID) to help bridge the data together. 

## :bulb: What Did We Learn?
This project allowed for multiple areas of growth for us, especially in the following areas:
* **Building a Code-First Database** - Practice makes perfect! This project gave us more exposure to creating APIs from start to finish, and gave us more familiarity with all of the many pieces and steps involved in the process. 
* **GitHub** - GitHub is a wonderfully useful tool, but it certainly has its own learning curve, especially when working as a team. We made it through challenges with pull/push requests on various branches, and by the end of the project our commits and pushes were running very smoothly!
* **Collaborating as a Team** - We learned a lot about each other throughout this project, including about our strengths. We learned how to make sure everyone was "on the same page" as well as how to divvy up resposibilites based on eachother's strengths and experience. We worked together to create a realistic timeline for the project that worked well for our busy work schedules and families. Working "solo" certainly had its place in the project, but effective communication and collaboration is what made the ultimately project successful. 

## :smiley: What's Next?
We had several stretch goals for our project, but here are the two main ones that we would like to accomplish:
* **1. Creating a Simple Console App** - We want to create a console app in C# that will give the user a better idea of how the different data tables are connected as well as how the application as a whole can be a very userful resource. 
* **2. Create Additional Search Functionality** - We want the user to be able to see a list of therapists by their area of specialty. For example, if a user specifically wants to see all therpaists who specialise in anxiety, we want the user to easily be able to access that list.

### :computer: Eleven Fifty Academy
Our group is grateful to Eleven Fifty Academy and leading us through our Coding journey. To learn more about Eleven Fifty Academy click here: [Eleven Fifty Academy](https://elevenfifty.org/).
