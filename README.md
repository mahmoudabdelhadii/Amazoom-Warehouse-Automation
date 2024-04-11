# Amazoom Automated Warehouse

admin username: admin@amazoom.com  
admin password: Admin12345*

## Introduction
Amazoom is exploring automation solutions to enhance efficiency and reduce operational costs across its variously sized warehouses, which have diverse dimensions and randomly distributed inventory. To address the complexities of differing warehouse layouts and inventory organization, a versatile approach is necessary.

## Proposed Solution
To effectively address these challenges without significant initial expenditure, I propose the development of a computer simulation. This simulation will allow me to assess the feasibility and effectiveness of potential solutions in a controlled environment.

## Interaction Diagram
![image](https://github.com/mahmoudabdelhadii/Amazoom-Warehouse-Automation/assets/56850296/08bdaf80-e733-48e2-a6c2-f8dd6e40d192)

## Warehouse Simulation Details
The simulation will represent Amazoom warehouses as customizable grids that reflect the actual dimensions of our physical warehouses. In this simulated environment, an administrator can configure the placement and details of inventory items. Users can view aggregated inventory across all locations, with items categorized by name for streamlined browsing and inclusion in shopping carts. Additionally, each simulated warehouse includes a loading bay to facilitate the interaction between robots and delivery trucks.

## Use Case Diagram
<img width="474" alt="image" src="https://github.com/mahmoudabdelhadii/Amazoom-Warehouse-Automation/assets/56850296/3c19532c-fb92-4a17-bf95-72a0fc7aa801">

## Inventory and Order Management
The simulation is designed to track the location and quantity of each product in real-time, alerting managers when stock levels are low and facilitating timely reordering. It also allows managers to monitor the status of all ongoing orders, ensuring prompt identification and resolution of any issues.

## Robotics and Automation
To ensure efficient operations, the simulation incorporates thread-safe array maps to prevent robot overlap within warehouses. Robots operate on a predetermined clockwise path to avoid collisions, with a semaphore system in place to regulate their numbers in any given area. Task chaining ensures sequential item retrieval, enhancing procedural efficiency.

## System Efficiency
The entire simulation leverages multithreading to optimize the retrieval process, minimizing the time required for item collection. I validate the simulation through extensive testing, including console output checks and dynamic tests with randomized data to ensure robust performance across varying scenarios.

## Final Thoughts
My simulation provides a scalable and adaptable solution that positions Amazoom at the forefront of warehouse automation technology. By continuously refining my simulation, I can develop the most effective strategies for real-world application without significant resource expenditure.

## Technology Stack
For the frontend, I selected ASP.NET with an MVC architecture to maintain a clear separation between user interface and backend logic, enabling concurrent development and integration of both components. The backend is developed using C#, chosen for its relevance to my educational background and its efficacy in handling complex backend functionalities.




## Sequence Diagram 
![image](https://github.com/mahmoudabdelhadii/Amazoom-Warehouse-Automation/assets/56850296/265455bf-ef03-462a-b29e-d7cc54eee04b)


## Class Diagram

![image](https://github.com/mahmoudabdelhadii/Amazoom-Warehouse-Automation/assets/56850296/05ec542a-5662-4934-b657-55d8809dcf7f)



