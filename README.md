# **The Walking Dawg App**


Welcome to our Project. The Walking Dawg is an application designed to help connect dog owners with dog walkers. With this app, they would be able to easily set up an appointment to have their dog(s) walked. 


 You might be wondering what led us to create The Walking Dawg app. Well, we are a team of three that are enrolled in a Software Development bootcamp through the [Eleven Fifty Academy](https://www.elevenfifty.org/). Our group project requirement was to come up with an idea for a .NET Framework API web application using n-tier architecture where each member of the team  had at least one custom data table that they were responsible for. This application is a complilation of all that we have learned in 10 weeks of this course.  Enjoy! 

## Technologies & Tools
Project was created using:

- C#
- .NET SDK
- Azure Data Studio
- Docker
- Git
- Postman
- Visual Studio Code

## Features
- Lets users set an appointment to have their dog(s) walked
- The user has the ablility to choose from a list of available dog walkers
- The site has an option for users to be able to leave a rating for the walker
- Can view walk status for dog(s)
- Dog walkers can create a walk
- Dog walkers have ability to update walk status and show when finished
- Dog Walkers can view past walks

**Flow-Chart of Endpoints**

![The Walking Dawg Endpoints](./Assets/twdEndPoints.png)


## Getting Started
*You will need to make sure you have .Net SDK installed -> [.NET SDK](https://dotnet.microsoft.com/download)*


1. Clone The Walking Dawg App repository, use this link ->

```shell
git clone https://github.com/jacksonlee1/TheWalkingDawg.git
```
2. cd into the project directory

```shell
cd TheWalkingDawg
```

3. Set up Docker

```shell
docker pull mcr.microsoft.com/azure-sql-edge
```
```shell
docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=[YOUR PASSWORD]" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name TheWalkingDawg mcr.microsoft.com/azure-sql-edge
```
4. Create TheWalkingDawg Database
    ```Sql
    Create Database TheWalkingDawg
    ```
5. Create Tables Using Create Tables.sql

6. Initialize user secrets
```shell
dotnet user-secrets init --project WebAPI
```
7. Add connection string to user secrets
```shell
dotnet user-secrets set "ConnectionStrings:DefaultConnection": "[Your Connection String]" --project WebAPI
``` 
8. Add jwt key to user secrets 
     ```shell
        dotnet user-secrets set "Jwt:Key" "[YourSecretKey]",
        dotnet user-secrets set "Jwt:Issuer":"localhostServer",
        dotnet user-secrets set "Jwt:Audience": "localhostClient"
      ```
9. Run the project
```shell
dotnet run --project WebAPI
``` 




## Usage
(Here we will add some screen shots and provide further instruction. Users can reference this so know what can expect)

## Credits 

AND here are the names of those responsible for creating The Walking Dawg application along with links to their personal github pages:


 - **Garrett Alderink**: https://github.com/galderink
 
 - **Jackson Lee**: https://github.com/jacksonlee1
 
 - **Marla Laystrom**: https://github.com/mlaystrom
 