# LibMan

## Setup

### Dependencies:
* .Net 9

### Steps to run:
1. Clone the repository
    `git clone https://github.com/AbdelrahmanMahmoudDev/LibMan .`

2. Open the Visual studio solution file (.sln)

3. Run the app using F5 in debug mode, or CTRL + F5 to launch the server and expose a port

## Project Brief
* The project is built with C# and ASP.NET CORE MVC
* An N-Tier Architecture was used (Data Access, Domains, Services, Presentation)
    * The decision to add a domains layer was to avoid cyclic dependencies between projects
* The project has 2 areas: the root and the admin areas
    * admin area: /Admin

## Regarding the design_files folder
This is a folder I created to keep the files and notes I kept regarding the project
* design_decisions.txt: Why certain decisions were made, some assumptions and considerations
* Erd.drawio.png: The Entity-Relationship diagram (ERD) I extracted from the requirement sheet
* Library Management System.pdf: a copy of the project's requirement sheet
* Schema.drawio.png: The relational schema I extracted from the ERD
tasks.xlsx: The tasks I extracted after reading the requirement sheet, which helped me tackle the project quite a lot