## Travely: A Trip Planning Application

This project is a .NET Multi-platform App UI (MAUI) application built using .NET Core and SQLite database with Entity Framework Core (EF Core) ORM.

## Projects:
- Travely.Client: This project leverages .NET Multi-platform App UI (MAUI) framework to provide a modern and intuitive user interface across various platforms.
- Travely.Domain: The heart of the application, Travely.Domain handles everything related to the database. It includes the application context, migrations, and database tables defined using Entity Framework Core. The SQLite database ensures efficient data storage and retrieval, empowering users to store their trip details securely.
- Travely.BusinessLogic: Acting as the bridge between the client and domain layers, Travely.BusinessLogic implements the core business logic of the application. It orchestrates interactions between the client UI and the database, handling trip planning, itinerary generation, and other essential functionalities.

## Features:

## 1.Destination Exploration: 
Discover new destinations, attractions, and accommodations, with rich multimedia content.In this section, we utilized the Destination API to display popular destinations, popular seaside destinations, and popular mountain destinations
By utilizing these API endpoints, we were able to create a comprehensive and engaging section for users to explore popular destinations, seaside getaways, and mountain retreats, enhancing their travel planning experience.

![image](https://github.com/adelinamfatu/Travely/assets/115553717/5a8d786e-2b7e-4dc8-9423-987bf23757cf)
![image](https://github.com/adelinamfatu/Travely/assets/115553717/42d77f31-e0c1-40d7-b16f-43241c3972a7)



## 2.Trip Planning: Seamlessly plan your trips by creating detailed itineraries, setting destinations, dates, and activities.
In this section, we have integrated multiple features to enhance your trip planning experience. Using a comprehensive API, we provide the following functionalities:

- Travel History and Filtering

1. Users can view all their past trips, including the dates and destinations of each trip. This feature also allows users to filter their travel history based on date, making it easy to review past adventures and plan future trips accordingly.
2. Users can perform CRUD (Create, Read, Update, Delete) operations on countries. This feature allows users to add new countries, delete existing ones, and modify details such as dates and other relevant information.

![image](https://github.com/adelinamfatu/Travely/assets/115553717/3bb7f7e3-2f95-47f7-a1af-06f600b3a342)


- Plan a Trip

Users can plan a trip by selecting the period, country, and other details. This feature allows for detailed trip planning, ensuring that all aspects of the trip are covered, from the itinerary to accommodations and activities.

![image](https://github.com/adelinamfatu/Travely/assets/115553717/5a5e5aa1-fc9c-45da-828b-a70b67b7cc50)


- Packing Items Section

To ensure travelers are well-prepared for their journey, a packing items section is incorporated. This feature allows users to create and manage a checklist of items they need to pack. From clothing to travel essentials, this checklist ensures nothing important is forgotten for the trip.

![image](https://github.com/adelinamfatu/Travely/assets/115553717/a23917fe-a8b6-4e19-9bb1-c99f3a2a6f0e)

- Edit Trip Section

1. Flight Information

Users can add their flight details, and with the help of an API, the platform displays information about the flight, including the departure and destination airports. This feature helps users keep track of their flight schedules and ensures they have all the necessary information for their journey.

2. Add Notes for the Trip

In the Edit Trip Page, users can set notes for their trip. This allows users to jot down important information or reminders related to their travel plans.

  ![image](https://github.com/adelinamfatu/Travely/assets/115553717/99f83793-bf8a-4b03-9395-7b43938de4a1)
  ![image](https://github.com/adelinamfatu/Travely/assets/115553717/23974a37-323a-4823-ab90-ffb06fd48983)

- Itinerary Page

1. Retain Itinerary by Days

The itinerary page retains the itinerary for the trip on a day-by-day basis. For each day, users can select locations on the map within the chosen country, set the budget, and view the weather conditions.

2. Select Destination on the Map

With the help of an integrated map API, users can easily select their desired destination directly on the map. This interactive feature allows travelers to visually explore different locations and choose their next travel destination with ease.

3. Budget Planning

Managing finances is a crucial part of trip planning. The platform includes a budget planning tool that helps users estimate and manage their travel expenses. This feature enables users to set a budget for various aspects of the trip, such as accommodation, food, transportation, and activities, ensuring a financially stress-free travel experience.

4. Weather Information

Using a weather API, the platform displays current weather conditions for the selected destinations. This feature helps users plan their trips better by providing real-time weather updates, ensuring they are prepared for any weather conditions during their travels.

![image](https://github.com/adelinamfatu/Travely/assets/115553717/7849dd62-1a81-4681-9524-5af5adac1aa5)
![image](https://github.com/adelinamfatu/Travely/assets/115553717/c64a8e98-453f-488d-9f7b-73ce229e3047)
![image](https://github.com/adelinamfatu/Travely/assets/115553717/b6a12c95-f193-4a1e-b5eb-00a3bbf5aa1a)




- Profile Section with Analytics

The profile section provides users with insightful analytics and visualizations. Users can view graphs and charts that show the most popular destinations, the most visited cities, and other travel trends. This information helps users make informed decisions about where to travel next by understanding current travel patterns and preferences.

![image](https://github.com/adelinamfatu/Travely/assets/115553717/e8734cd8-c007-43ee-9ab3-6db55235c6ae)
![image](https://github.com/adelinamfatu/Travely/assets/115553717/36318e23-0bd0-4f43-98a0-ee1df2df5e93)


## 3. Offline Support: 
Enjoy uninterrupted access to trip details, even without an internet connection, thanks to offline storage capabilities provided by SQLite database.

## 4. Cross-Platform Compatibility:
Experience Travely on your preferred device, whether it's a smartphone, tablet, or desktop, with consistent user interface and functionality across platforms.

## 5. APIs Used:
- Countries API for Popular European Countries: Used to display popular european destination.
- Countries API for Popular Seaside and Mountain Side: Used to display popular seaside and mountain side destinations.
- Flight Information API: Used to display flight details including departure and destination airports.
- Weather API: Used to display current weather conditions for selected destinations.
- GeocodeCoordinatesAPI : Used to display the map
- FlagAPI: Used to display the flag for each country the user plan to visit
