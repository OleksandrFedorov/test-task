# Test task

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
### Technical data
The application based on [ASP.NET Core app with React](https://learn.microsoft.com/en-us/visualstudio/javascript/tutorial-asp-net-core-with-react?view=vs-2022) template. 
 - .NET 8.0
 - Vite + React + TypeScript
 - Database - Sqlite
 - NuGet Packages:
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore.Sqlite
    - Microsoft.EntityFrameworkCore.Tools
    - Microsoft.Extensions.Options.ConfigurationExtensions
    - Microsoft.AspNetCore.Http.Features
    - Newtonsoft.Json
    - PdfLibCore.ImageSharp
 - React packages: 
     - Bootstrap
     - Bootstrap.Icons
     - React Dropzone
     - React Router

-------------
###Guides
[React Tutorial for Beginners
](https://youtu.be/SqcY0GlETPk?si=mdsCS7kjdR3EmzUi)
[React File Upload Tutorial with Drag-n-Drop and ProgressBar
](https://youtu.be/MAw0lQKqjRA?si=ZWOlraVNioU6aSKs)
[Drag and dropping files in React using react-dropzone](https://youtu.be/eGVC8UUqCBE?si=5bzcPtIRuvYLEtR5)

-------------
###End