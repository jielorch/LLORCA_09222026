================================
	SETUP
================================
 1. Run the FileChecker.slnx
 2. Open the Package Manager Console or View > Other Windows > Package Manager Console
 3. In Default Project dropdown select App.Infrastructure
 4. run Update-Database
 
 
================================
	TEST THE SERVICE, SUCCESS PART
================================
 1. Run the project
 2. Click the Authorize button on the right hand side
 3. Enter the API Key for testing purposes I provide the key here "Your-Super-Secret-Local-Development-Key-12345"
 4. Click Authorize then close
 5. Click the POST /api/File/upload end point and click Try it out
 6. Select the Profit.csv and click execute
 
 you should see 
 	
	Response body
	Download
	{
	  "message": "File processed successfully."
	}
	
 To check the record
 1. Click the GET /api/File/record and click Try it out
 2. Click Execute
 
 you should see the data of the processed file
 	
Response body
Download
[
  {
    "publicId": "58c6f0ee-bdbe-4548-884e-18ef0aee5487",
    "fileName": "Profit.csv",
    "average": 27.5,
    "processingTime": "17:01:29.4500000"
  }
]

================================
	TEST THE SERVICE, FAILED PART
================================
 1. Click the GET api/File/record without API Key, same with POST api/file/upload
 2. Click Execute
 
 You should see status 401 and a message "API Key was not provided"
 
 
 *** Wrong API Key ***
 1. Click the Authorize button on the right hand side
 2. Input a wrong api key
 3. Click Authorize then close
 4. Test the endpoints either GET or POST
 
 You should see status 401 and a message "Unauthorized client"