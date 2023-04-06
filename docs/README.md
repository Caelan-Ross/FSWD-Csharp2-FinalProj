# EXSM3944: .NET Core MVC - 03

This ASP.net MVC application allows users to manage their vehicles through an easy-to-use interface. Users can create, read, update, and delete (C.R.U.D) their vehicle information using different pages within the application. With the help of a Database to store that information.

Getting Started

Before using the Vehicle Management System, ensure that you have the following installed:

    Visual Studio 2019 or later
    .NET Framework 7.0 or later

To install the application, follow these steps:

    Clone the repository to your local machine.
    Open the solution file in Visual Studio.
    Build the application to restore NuGet packages and dependencies.
    Launch XAMP and start Apache and MySQL.
    In the nuget package console, enter "dotnet ef database update --context ApplicationDbContext" and enter
    Then enter "dotnet ef database update --context VehicleContext" and enter
    You now can launch the application and test it out.

Usage

The Vehicle Management System provides the following features:

    User authentication: Users must log in to view and manage their vehicles.
    Create vehicle: Users can add new vehicles to their account by providing details such as make, model, year, and color.
    Read vehicle: Users can view all their vehicles in a summary page that displays key information such as make, model, and year, and view them individually in the details page.
    Update vehicle: Users can edit vehicle information such as make, model, year, and color.
    Delete vehicle: Users can remove vehicles from their account.