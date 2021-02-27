Please contact info@vanmarcke-software.be for additional info.

It's not maintained anymore by me.

# OmnisoftOnline.DotNet.Api.Examples
An example of API usage for Omnisoft Online


Request free access to the nuget repository for downloading the Api Client @ nico@vanmarcke-software.be
Please state the usage for further followup

Usable example for now is the MVC example.
The Windows Forms example is not ready yet, but will be soon.

# MVC example
- List of clients
- List of orders
- List of articles
- Create a client
- Create an order

# Usage

- Request your Api Key for connecting to your database
- Import the Nuget Packages, from the supplied Nuget Repository by Vanmarcke Software
- Change url depending the usage case ( dev / beta / live ), the corresonding url's are:
 - dev-api.omnisoftonline.be ( dev.omnisoftonline.be )
 - beta-api.omnisoftonline.be ( beta.omnisoftonline.be )
 - api.omnisoftonline.be ( live : www.omnisoftonline.be )
 
# Todo
 - [ ] Finish the Windows Forms Client

# Info
This repo is part of the partner program of Vanmarcke Software for their software: Omnisoft Online.
Which is a online web application for invoice management - ERP

Currently in transitioning from an old design to a new responsive design. Which well be the default soon. New customers always receive the new responsive design.

Documentation of the api can be found on http://api.omnisoftonline.be/
Example of api calls can be called on http://api.omnisoftonline.be/docs/index.html (wait a while to load, the documentation is live generated)

# Short Info on Api
We use odata webapi, which uses Web Api but can handle odata-query like strings. Eg. https://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/supporting-odata-query-options
 - Eg. paging would be //{url}/{version}/{Entity}?$skip=200&$take=100
 - Eg. filter on name would be //{url}/{version}/{Entity}?$$filter=Name eq 'Nico Sap'

We support xml and json through a url parameter ( &format=xml or &format=json ) , the default is json. Please notice some differences handling ComplexTypes like Address .
 
More info on http://api.omnisoftonline.be/ which handles all the concepts about the api.

Currently we have a .Net Api Client for your disposal.
