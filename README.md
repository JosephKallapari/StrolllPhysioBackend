# StrolllPhysioBackend

Overview
The API is backend service designed to manage exercise routines and session results for physiotherapy patients.It provides endpoints to prescribe exercise routines and fetch session results.The API is build using ASP.Net Core and targets .NET 8.

**Features**

.Prescribe Exercise Routines:Add or Update exercise routines for patients.

.Retrieve Session Results:Fetch session results for a patient

Technologies Used

.Asp.Net Core
. .Net 8
. Swagger for API documentation

**Installation**

1.Clone the repo 
  git clone <repo url>

2.cd <project directory>
3.Restore dependencies 
dotnet restore
4.Run the application
dotnet run
5.Access the Swagger UI or run the frontend to hit endpoints

**API Endpoints**

**1.Prescribe Exercise Routine **

POST /api/Physio/prescribe

.Description: Adds a new exercise routine or update an existing one for a patient
.Request Body:
{
"Id":1,
"PatentId":"123",
"Age":30,
"prescribedExercises":[
  {
   "exercise":"Quad Sets",
   "repetitons":10,
   "sets":3  
  }
]
}

Response
 200 OK:Routine added successfully

 **2.Get Session Results by patient ID**

 GET /api/Physio/physiosessionresults/{patientid}

 .Description:Retrieves Session results for a specific patient.no session results if not exist
 .Response:
  . 200 OK:List of session results.
  . 404 Not found : No session results 
 
