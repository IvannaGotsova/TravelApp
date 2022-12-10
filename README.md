# TravelApp
Brief summary of the TravelApp
There are three levels of access to the information:
Guest - has the right to access the following information:
* sees all journeys with brief information about them;
* sees all countries with brief information about them;
* sees all towns with brief information about them;
Registered User - has the right to access the following information (everything guest can do and additional rights):
* sees all journeys with brief information about them:
   - can see details about them;
   - can make requests about particular journey;
* sees all countries with brief information about them:
   - can see details about them;
   - can see all journeys for this country;
* sees all towns with brief information about them:
   - can see details about them;
   - can see all journeys for this town;
* sees all requests made from them:
   - sees their approval status;
   - sees their management status;
   - sees the final price for this journey according to number of people they put in the request form;
   - from here it can be done request for particular journey;
* sees his/her trips with brief information about them (trips are added by the user based on the journeys in Journey Information):
   - can see details about his/her trips;
   - can add, edit and delete trips;
   - can see all post about this trip;
   - from here it can be done trip for particular journey;
   - other users cannot access trips not added by them;
   - can access the particular journey;
* sees all posts with brief information about them:
   - can see details about all the posts;
   - if the login user is the author of the post, there is possibility to edit or delete this post;
   - from here it can be done post for particular journey;
   - can see all the comments about this post;
   - can access the particular journey;
* sees all comments with brief information about them and can access details of every comment;
Admin - has the right to access the following information (everything guest and user can do, and additional rights):
* Become User - the rights user have;
* Become Admin – additional rights:
   - can add, edit and delete Journeys;
   - can add, edit and delete Countries;
   - can add, edit and delete Towns; 
   - can see and manage(approve and decline) all the requests made by users;  
   - can see and manage (add and delete VIP Status, status that gives them a 10% discount) all the users;

The project consists of:
TravelApp
TravelApp.Core – Business logic;
TravelApp.Data – Data;
TravelApp.Tests – Tests;
