## Travely: A Trip Planning Application

This project is a .NET Multi-platform App UI (MAUI) application built using .NET Core and SQLite database with Entity Framework Core (EF Core) ORM.

## Projects:
- Travely.Client: This project leverages .NET Multi-platform App UI (MAUI) framework to provide a modern and intuitive user interface across various platforms.
- Travely.Domain: The heart of the application, Travely.Domain handles everything related to the database. It includes the application context, migrations, and database tables defined using Entity Framework Core. The SQLite database ensures efficient data storage and retrieval, empowering users to store their trip details securely.
- Travely.BusinessLogic: Acting as the bridge between the client and domain layers, Travely.BusinessLogic implements the core business logic of the application. It orchestrates interactions between the client UI and the database, handling trip planning, itinerary generation, and other essential functionalities.

## Features:
- Trip Planning: Seamlessly plan your trips by creating detailed itineraries, setting destinations, dates, and activities.
In this section, we have integrated multiple features to enhance your trip planning experience. Using a comprehensive API, we provide the following functionalities:

    ##Select Destination on the Map
With the help of our integrated map API, users can easily select their desired destination directly on the map. This interactive feature allows travelers to visually explore different locations and choose their next travel destination with ease.

    ##Budget Planning
We understand that managing finances is a crucial part of trip planning. Our platform includes a budget planning tool that helps users estimate and manage their travel expenses. This feature enables you to set a budget for various aspects of your trip, such as accommodation, food, transportation, and activities, ensuring a financially stress-free travel experience.

    ##Packing Items Section
To ensure you are well-prepared for your journey, we have incorporated a packing items section. This feature allows users to create and manage a checklist of items they need to pack. From clothing to travel essentials, this checklist ensures you don't forget anything important for your trip.

- Destination Exploration: Discover new destinations, attractions, and accommodations, with rich multimedia content.In this section, we utilized the Destination API to display popular destinations, popular seaside destinations, and popular mountain destinations (https://documenter.getpostman.com/view/1134062/T1LJjU52#abee09ea-aeb9-479c-b970-81d909c2a58c). By utilizing these API endpoints, we were able to create a comprehensive and engaging section for users to explore popular destinations, seaside getaways, and mountain retreats, enhancing their travel planning experience.
- Offline Support: Enjoy uninterrupted access to trip details, even without an internet connection, thanks to offline storage capabilities provided by SQLite database.
- Cross-Platform Compatibility: Experience Travely on your preferred device, whether it's a smartphone, tablet, or desktop, with consistent user interface and functionality across platforms.
