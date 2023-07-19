# SCADA_SNUS
Project for "Software of supervisory management systems" university course.

The project is a general `SCADA management system`, composed of four `main components`:
- RTU client - C# console app `simulating field sensors`, sends new tag values every 1000ms to the core
- SCADA core (back) - the heart of the backend, a `multi-threaded` ASP.NET application that runs the scan simulation in the infinite while loop + HTTP/ws endpoints
- front - simple and efficient Angular `UI`
- SQLite database - for storage and backup

The `frontend` is further broken into three semantic clients:
- Trending - displays real-time updates of the tag values and alarm notifications, both arriving from the core through web-socket connections
- Database Manager - interface for CRUD operations on input and output tags
- Report Manager - displays detailed reports on chosen criteria

The app is not heavy on authorization and authentication (not the focus of the project), still, there are two `user roles`:
- Admin - has access to all UI components and functionalities, as well as the CRUD operations on all entities
- User - can only monitor tags through the Trending app upon login

All above makes for a well-crafted simulation of general principles behind SCADA system management.

## Terminology
- Tag - represents a single physical quantity (`analog tags` - temperature, humidity...) or state (`digital tags` - off/on), and can be used to read the value (`input tags`) or to set it (`output tags`)
- Scan and scan time - determines how frequent is the reading of the new values of the input tags (`scan time`) and if the values in the frontend/db should be updated at all (`scan boolean`)
- Driver - specifies whether the new input tag values are provided by the RTU_Client simulation (`Real-time driver`) or by applying a function to the current timestamp (`Simulation driver`)
- `Alarm` - triggers when the tag value is greater/lower than the critical value, the priority of the alarm maps on the intensity of the notification in the Trending app

## Technology stack
- ASP.NET
- SignalR
- EF
- SQLite database
- Angular
- Material Design


## Team members
- Tina Mihajlović
- Srdjan Stjepanović
- Nemanja Vujadinović

## Running the project
To run the project:
1. clone git repo
2. run RTU_Client solution to start the simulation of the sensors
3. run snus_back solution to start the backend core
4. run front Angular app to start the frontend

*Registration of new users is not supported through the UI, you can add them directly through a database management software of your choice - after that you can log in and enjoy the app :)


