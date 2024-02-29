# Test task

The application based on [ASP.NET Core app with React](https://learn.microsoft.com/en-us/visualstudio/javascript/tutorial-asp-net-core-with-react?view=vs-2022) template. 
 - .NET 8.0
 - Vite + React + TypeScript
 - Database - Sqlite
 - NuGet Packages:
    - Microsoft.AspNetCore.Http.Features
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore.Sqlite
    - Microsoft.EntityFrameworkCore.Tools
    - Microsoft.Extensions.Options.ConfigurationExtensions
    - Newtonsoft.Json
    - PdfLibCore.ImageSharp
 - React packages: 
     - [Bootstrap](https://getbootstrap.com/)
     - [Bootstrap Icons](https://icons.getbootstrap.com/)
     - [React Dropzone](https://react-dropzone.js.org/)
     - [React Router](https://reactrouter.com/en/main)

Reasons for choice: 
 - ASP .NET Core: my familiarity with it and non-functional requirements;
 - React: my interest  in trying it and non-functional requirements;
 - Sqlite: lightweight and easy-to-deploy database for small projects.
 - Local file storage: to imitate the self-hosted server

-------------
###How to run
1.  Clone the project with Git or [Download Zip](https://github.com/OleksandrFedorov/test-task/archive/refs/heads/main.zip)
2. Open `TestTask.sln` with Microsoft Visual Studio 2022 ("ASP.NET and web development" and "Node.js development" workloads from Visual Studio Installer are required)
3. Configure the startup project:
    3.1. Go to "Project > Configure Startup Projects.."
    3.2. Select "Multiple startup projects"
    3.3. Set `TestTask.Server` and `testtask.client` actions to "Start"
    3.4. Name the startup profile as you want and click "Ok"
    3.5. You can specify the File service destination folder in `TestTask.Server/appsettings.json` file ("C:\Temp\TestTask\Files" by default)
4. Run the project
    4.1. Click "Start Without Debugging" or "Debug > Start Without Debugging" or "Ctrl+F5"
    4.3. If you have "There were deployment errors. Continue?" simply ignore it and click "Yes"
5. Test the app
    5.1. Open https://localhost:5173/
    5.2. If it first launches  navigate to the "Register" page and create a new account.
    5.3. Login into the service using your credentials
    5.4. Drag & drop into upload area or click to select files to upload
    5.5. Remove files that you want to upload by clicking "Remove" or remove them all by clicking "Clear all"
    5.6. Upload files to the File service by clicking the "Upload" button
    5.7. Now you can review your files in the "All Files" area
    5.8. Download, Share or Delete the file by clicking on the corresponding button. You can share and delete only your files. 
    5.9. To share the file click on the "Share" button on the file card
    5.10. Specify the duration of the share by Days/Hours/Minutes from now and click "Add"
    5.11. Now you see the shares dropdown with adding form and list of shares
    5.12. Expired  shares are highlighted in yellow. 
    5.13. You can delete share or copy their link by clicking on the corresponding buttons.
    5.14. Open the copied link of the file share to check whether  it works.

That`s basically  it.

-------------
###What can be impoved? 

**Tech part:**
- Add unit tests
- Add DOC and XLS preview 
- Add session validation on requests (or close access to backend from outside)
- Deploy to Docker container


**User part:**
- Add edit files
- Share to specific user
- Folders, groups, sorting
-------------
###Challenge Details

The application is a document library intended to give its users a web based solution to store and share their documents with others.

All the following requirements are what we provide. Make your assumptions when needed.

###Functional Requirements:

**Design and implement the application’s UI / API based on the requirements:**

- available document types for uploading
    - PDF / Excel / Word/ txt / pictures documents
- display / get a list of available documents
    - name of the document
    - icon based on its type
    - a preview image of its content 1st page content
    - date and time of upload
    - number of downloads
- download / upload a document
- download / upload several documents
- a document can be shared with other users via a generated link which is publicly available for the specified time period (e.g.: 1 hour, 1 day, etc.)

**Non-functional Requirements:**

- Desired frontend framework is React. But any other can be used as well.
-  Desired backend framework is ASP NET Core. But any other can be used as well. 
- Any 3rd party library or framework can be used. If a license or key is required for running the application it has to be provided.

**Deliverables:**

- Code and other assets access - zip archive, github / gitlab repository or any other comfortable for you way 
- Instructions how to run & test the application
- Description of main architecture and design decisions
- Ideas and proposals how to improve the application from a user or technical perspective

-------------
###Used guides
[React Tutorial for Beginners](https://youtu.be/SqcY0GlETPk?si=mdsCS7kjdR3EmzUi)
[React File Upload Tutorial with Drag-n-Drop and ProgressBar](https://youtu.be/MAw0lQKqjRA?si=ZWOlraVNioU6aSKs)
[Drag and dropping files in React using react-dropzone](https://youtu.be/eGVC8UUqCBE?si=5bzcPtIRuvYLEtR5)

-------------
###End