# Build .NET Core Blazor app to retrieve market price report using LSEG Dat Library for .NET

## Introduction 

This article is a consecutive series of use cases to demonstrate usage of the LSEG Data Library for. NET(LD.NET). It will apply the core implementation from the previous article, which uses LD.NET to displaying a Real-Time Stock price update on the Web.

This article will provide an example of a web app created using the ASP.NET Core Blazor framework and the LD.NET to send a bulk request to get current stock prices from the Real-Time – Optimized or local web socket server running on Real-Time Advanced Distribution Server. Users can use the app to send a bulk snapshot request to request data from the RIC list or Chain RIC and customize fields from the web UI and then leverage the functionality of LD.NET to send snapshots with dynamic view request to get the data from the server. There is an option for a user to export the retrieved data to CSV files. The use case should be useful when users want to import the data to their system for reporting purposes or updating the price on their system or website.

To achieve the requirement to support Chain RIC, we will create a separate .NET library to expand Chain RIC based on the implementation of Example Websocket Chain Expander, which we have created earlier. We will migrate the Websocket connection manager inside the app to use the implementation of LD.NET instead to support the connection to Real-Time – Optimized. We can then call the method from the library to expand Chain and get a list of underlying RICs in the new web app.

For the full article, please refer to [this link](https://developers.lseg.com/en/article-catalog/article/build-dotnet-core-blazor-app-to-retrieve-marketprice-report-using-ld-library).
