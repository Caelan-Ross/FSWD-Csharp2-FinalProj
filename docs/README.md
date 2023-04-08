# EXSM3944: .NET Core MVC - 03

This ASP.net MVC application allows users to manage their vehicles through an easy-to-use interface. Users can create, read, update, and delete (C.R.U.D) their vehicle information using different pages within the application. With the help of a Database to store that information.

## Getting Started:

### Before using the Vehicle Management System, ensure that you have the following installed:

    Visual Studio 2019 or later
    .NET Framework 7.0 or later

### To install the application, follow these steps:

    Clone the repository to your local machine.
    Open the solution file in Visual Studio.
    Build the application to restore NuGet packages and dependencies.
    Launch XAMP and start Apache and MySQL.
    In the nuget package console, enter "dotnet ef database update --context ApplicationDbContext" and enter
    Then enter "dotnet ef database update --context VehicleContext" and enter
    You now can launch the application and test it out.

## Usage:

### The Vehicle Management System provides the following features:

    User authentication: Users must log in to view and manage their vehicles.

    Create vehicle: Users can add new vehicles to their account by providing details such as make, model, year, and color.
    Read vehicle: Users can view all their vehicles in a summary page that displays key information such as make, model, and year, and view them individually in the details page.
    Update vehicle: Users can edit vehicle information such as make, model, year, and color.
    Delete vehicle: Users can remove vehicles from their account.

    Create vehicle models: Users can add new vehicle models to their account by providing details such as name, and manufacturer.
    Read vehicle models: Users can view all their vehicle models in a summary page that displays key information such as name, manufacturer id, and view them individually in the details page.
    Update vehicle models: Users can edit vehicle models information such as name and manufacturer id.
    Delete vehicle models: Users can remove vehicle models from their account.

    Create manufacturers: Users can add new manufacturers to their account by providing details such as name.
    Read manufacturers: Users can view all their manufacturers in a summary page that displays key information such as name and view them individually in the details page.
    Update manufacturers: Users can edit manufacturers information such as name.
    Delete manufacturers: Users can remove manufacturers from their account.

    There is also a custom create page that allows users to create new manufacturer, vehicle model, and vehicle that will all be attached to eachother. 
    As well, there is a custom view page that shows the user the corresponding manufacturer name and vehicle model name alongside the vehicles information rather than  the id values.