This is a school assignment for Cloud Databases.

The assignment is as follows:
```
Scenario:

Widget and Co product widget - have decided to transfer their website from a local ISP hosting company to the cloud. The site has become very popular during the past few years and can experience heavy loads at peak hours. It is suggested that the move to Azure could alleviate some of the occurring problems.
During peak hours a huge amount of traffic is generated by online users who place their orders. In the past the order placing process has been the main culprit in the sluggish performance of the website.
The orderdate is stored; after an order is shipped the shipping date is again stored. these are used to calculate orderprocessed metric. 
As Widget and Co is part of the conglomerate Wiley and Co the user info is shared with another department.
The product specification is kept in a traditional SQL database, Images of the product are also stored. 
The site also runs a forum part where users can post reviews of the products (anonymously). At a later stage the marketing department would use the reviews to profile a new range of products. 

Your task:

The Online web store will be hosted (and run) in a cloud environment (Azure). Design a proof of concept (in C# Web API with at least 2 Azure Functions) of the application where special attention is paid to the design of the Cloud Database architecture. 
The proof of concept does not require a frontend!
```

This project consists of 2 azure function projects. One for the API and another to pocess the orders. 2 queues are used, one to place orders and another to process orders. A blob storage is used to store the images of products. All data is stored in an Azure SQL database.
A swagger specification can be found [here](https://cdb648680.azurewebsites.net/api/swagger/ui).