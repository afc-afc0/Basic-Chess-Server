# Basic-Chess-Server
Basic Chess Server Written in C#

Upon learning the Socket Programming in Computer Networking class. I wanted to apply what I learned to building an online chess server to play chess with my friends so I built this server. I used C# because it would be easier to serialize the data and send to Unity and deserialize. 

Project written in C# allows you and your friends play Chess 
- Uses TCP-IP connections.
- Uses threads for each connection.
- Has a match making system.
- Chess Engine will validate if its a valid move if it is It will update the match and send the match information. 
- For serializing the data Ceras-Serializer is used.
