## Travely: A Trip Planning Application

This project is a .NET Multi-platform App UI (MAUI) application built using .NET Core and SQLite database with Entity Framework Core (EF Core) ORM.

## Projects:
- Travely.Client: This project leverages .NET Multi-platform App UI (MAUI) framework to provide a modern and intuitive user interface across various platforms.
- Travely.Domain: The heart of the application, Travely.Domain handles everything related to the database. It includes the application context, migrations, and database tables defined using Entity Framework Core. The SQLite database ensures efficient data storage and retrieval, empowering users to store their trip details securely.
- Travely.BusinessLogic: Acting as the bridge between the client and domain layers, Travely.BusinessLogic implements the core business logic of the application. It orchestrates interactions between the client UI and the database, handling trip planning, itinerary generation, and other essential functionalities.

## Features:

## 1.Trip Planning: Seamlessly plan your trips by creating detailed itineraries, setting destinations, dates, and activities.
In this section, we have integrated multiple features to enhance your trip planning experience. Using a comprehensive API, we provide the following functionalities:

- Select Destination on the Map

With the help of an integrated map API, users can easily select their desired destination directly on the map. This interactive feature allows travelers to visually explore different locations and choose their next travel destination with ease.

- Budget Planning

Managing finances is a crucial part of trip planning. The platform includes a budget planning tool that helps users estimate and manage their travel expenses. This feature enables users to set a budget for various aspects of the trip, such as accommodation, food, transportation, and activities, ensuring a financially stress-free travel experience.

- Packing Items Section

To ensure travelers are well-prepared for their journey, a packing items section is incorporated. This feature allows users to create and manage a checklist of items they need to pack. From clothing to travel essentials, this checklist ensures nothing important is forgotten for the trip.

- Travel History and Filtering

Users can view all their past trips, including the dates and destinations of each trip. This feature also allows users to filter their travel history based on date, making it easy to review past adventures and plan future trips accordingly.

- Flight Information

Users can add their flight details, and with the help of an API, the platform displays information about the flight, including the departure and destination airports. This feature helps users keep track of their flight schedules and ensures they have all the necessary information for their journey.

- Profile Section with Analytics

The profile section provides users with insightful analytics and visualizations. Users can view graphs and charts that show the most popular destinations, the most visited cities, and other travel trends. This information helps users make informed decisions about where to travel next by understanding current travel patterns and preferences.

## 2.Destination Exploration: 
Discover new destinations, attractions, and accommodations, with rich multimedia content.In this section, we utilized the Destination API to display popular destinations, popular seaside destinations, and popular mountain destinations (https://documenter.getpostman.com/view/1134062/T1LJjU52#abee09ea-aeb9-479c-b970-81d909c2a58c). 
By utilizing these API endpoints, we were able to create a comprehensive and engaging section for users to explore popular destinations, seaside getaways, and mountain retreats, enhancing their travel planning experience.
    
## 3. Offline Support: 
Enjoy uninterrupted access to trip details, even without an internet connection, thanks to offline storage capabilities provided by SQLite database.

## 4. Cross-Platform Compatibility:
Experience Travely on your preferred device, whether it's a smartphone, tablet, or desktop, with consistent user interface and functionality across platforms.
