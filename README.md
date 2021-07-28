# Basic-Chess-Server
Basic Chess Server Written in C#

Upon learning the Socket Programming in Computer Networking class. I wanted to apply what I learned to building an efficent online chess server in C#. I choose C# because in client-side I used Unity game engine to make it easier to serialize data. In the building process of the server I learned a lot about networking and understanding the server side.

Project written in C# allows you and your friends play Chess 
- Uses TCP-IP connections.
- Uses threads for each connection.
- Has a match making system.
- Chess Engine will validate if its a valid move if it is It will update the match and send the match information. 
- For serializing the data Ceras-Serializer is used.
