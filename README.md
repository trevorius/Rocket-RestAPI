# REST API
## WEEK 14 Mobile App

In order to create the technicians mobile app a new end point had to be implemented and can be accessed as a ***GET*** request here:
https://rocket-elevators-status.azurewebsites.net/api/employees for a list of all employees

or : https://rocket-elevators-status.azurewebsites.net/api/employees/email/{EMAIL} to verrifie if EMAIL is an employee E-mail if so the request will return the boolean :  `true` 
If not the request will return a 404 not found.

## WEEK 11 Understanding the .NET Framework

For the implementation of this weeks task : the customer portal in c#

new end points have been implemented : 

new **GET** : 

get the customer information form their email address (used for registry and  product information page)
https://rocket-elevators-status.azurewebsites.net/api/Customers/email/{email}

the following endpoints extract the information reletaive to their parent element .

 - https://rocket-elevators-status.azurewebsites.net/api/buildings/for-customer-{id}
  - https://rocket-elevators-status.azurewebsites.net/api/batteries/for-building-{id}
  - https://rocket-elevators-status.azurewebsites.net/api/columns/for-battery-{id}
  - https://rocket-elevators-status.azurewebsites.net/api/elevators/for-column-{id}

new **PUT**
to change the customer information has been updated to only be able to change the information taht a customer should be able to change (they can not change creation date, user id, or tecnician id): 
https://rocket-elevators-status.azurewebsites.net/api/Customers/5

new **POST**
to submit the constomer filled intervention form.
https://rocket-elevators-status.azurewebsites.net/api/interventions/## Rocket Elevators REST API

### Week 9 Odyssey CONSOLIDATION

Acquired last weeks project and saved it to a private repo deployed it to azure services and set the CORS so it can be called. : new api-URL :  https://rocket-elevators-status.azurewebsites.net

The deploy is directly connected to AZURE any push to deploy branch will automatically be deployed to the remote api.

-The new Endpoints for interventions have been implemented as follows :
a  **GET**  request to  [https://rocket-elevators-status.azurewebsites.net/api/interventions](https://rocket-elevators-status.azurewebsites.net/api/interventions)  will return all interventions

a  **GET**  request to  [https://rocket-elevators-status.azurewebsites.net/api/interventions/pending-interventions](https://rocket-elevators-status.azurewebsites.net/api/interventions/pending-interventions)  will return all pending interventions that haven't begun.

a  **PUT**  request to.  [https://rocket-elevators-status.azurewebsites.net/api/interventions/change-status-to-in-progress/{id}](https://rocket-elevators-status.azurewebsites.net/api/interventions/change-status-to-in-progress/%7Bid%7D)  will change the status to inprogress and time stame the start date. you have to include in the body the id wich has to correspond with the URL id, other fields are not necessary and will be ignored.

a  **PUT**  request to  [https://rocket-elevators-status.azurewebsites.net/api/interventions/change-status-to-completed/{id}](https://rocket-elevators-status.azurewebsites.net/api/interventions/change-status-to-completed/%7Bid%7D)  will change the status to Completed and time stamp the end date. you have to include in the body the id which has to correspond with the URL id, other fields are not necessary and will be ignored. the API complete collection can be found here :

[![Run in Postman](https://camo.githubusercontent.com/16a903fe0c8e857e22585b47d674a11dc7fd16a2d4ef6a2d0e932e70a62cb0d6/68747470733a2f2f72756e2e7073746d6e2e696f2f627574746f6e2e737667)](https://app.getpostman.com/run-collection/47f22848ca3c199cba2f)


## 
##

.

### FALL-2020-TEAM-API-2 - Week 8 Odyssey 

#### TEAM MEMBERS:
- VIET-NGA DAO "Team Leader"
- TREVOR KITCHEN "Member"
- EMMANUELLA DERILUS "Member"
- ANDRE DE SANTANA "Member"
- JULIEN DUPONT "Member"

#### This week we were asked to create a Rest Api for Rocket Elevators.
Using C# and .NET Core.

The different models connect to the preexisting MySQL database established in the rails app from previous weeks.

The app is deployed on Azure services and can be accessed through this URL: 
https://rocketelevatorsstatus-restapi.azurewebsites.net/

The different models and Controllers allow us to access and modify in some cases specific information.

## 

 **The different end points can also be tested on an application such as postman (See the information below):**

### Batteries **(GET)**
* To retrieve the list of all Batteries:
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/batteries
* To retrieve all information of a specific Battery
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/batteries/8
* To retrieve the current status of a specific Battery
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/batteries/status/8

### Columns **(GET)**
* To retrieve the list of all Columns:
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/columns  
* To retrieve all information of a specific Column
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/columns/8
* To retrieve the current status of a specific Column
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/columns/status/8

### Elevators **(GET)**
* To retrieve the list of all Elevators:
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/elevators
* To retrieve all information of a specific Elevator
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/elevators/8
* To retrieve the current status of a specific Elevator
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/elevators/status/8

### **(PUT) Requests** - Not possible in the browser, you will need to copy the links and test It inside an app like Postman:
* Changing the Id number to the desired elevator
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/elevators/8/status
* Changing the Id number to the desired column
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/columns/8/status
* Changing the Id number to the desired battery
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/batteries/8/status


### Specific Requests **(GET)** 
* Retrieving a list of Elevators that are not in operation at the time of the request  
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/elevators/not-operating

* Retrieving a list of Buildings that contain at least one battery, column or elevator requiring intervention
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/buildings/needing-intervention

* Retrieving a list of Leads created in the last 30 days who have not yet become customers.
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/leads/open-leads

### Testing with Postman 
* Clicking on the button will send you to the postman collection (Rocket-Elevator-RestAPi). Inside Postman you can click on the button "Runner" which will execute a sequence, retrieving and changing the information before restoring for further tests. (Supplied in the Codeboxx deliverable)

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/47f22848ca3c199cba2f)


## Extra End Points

the customer comtroller allows you to **GET** all the customers here : 
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/customers

you can Also **GET** individual customer information by ID here (change id to desired customer )
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/customers/50

You can count recent customers in recent days  (less than 100) with a **GET** request
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/customers/count-in-last-70-days

You can count Customers in a specific time frame (year-month-day format) with a **GET** request 
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/customers/count-in-between-1983-10-15-and-2010-03-20

You can count the products owned by a customer here : 
**GET** https://rocketelevatorsstatus-restapi.azurewebsites.net/api/customers/customer-30-pruducts
the list of these products can be found using the graphQL API from this repo : 

https://github.com/week7VietEmanuellaJulienTrevor/GRAPHQL_API.git

