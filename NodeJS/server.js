var websocket = require('ws');

var websocketServer = new websocket.Server({port:25500}, ()=>{
	console.log("Server is running.");
});

var wsList = [];
var userNameList = [];
var userName;
var userNameCheck = 0;

websocketServer.on("connection",(ws, rq)=>{

	console.log("Client connected.");
	wsList.push(ws);
	userNameCheck = 0;

	ws.on("message", (data)=>{
		if(userNameCheck == 0)
		{
			console.log(userNameCheck);
			console.log("Username sent from client: " + data);
			userName = data;
			userNameList.push(data);
			userNameCheck = 1;
		}
		else if (userNameCheck == 1)
		{
			userName = CheckUser(ws);
			console.log(userName + " / " + userNameCheck);
			console.log("Sent from client: " + data);
			Boardcast(data,userName);
		}
	});
	ws.on("close", ()=>{

		userName = CheckUser(ws);
		wsList = ArrayRemove(wsList, ws);

		console.log(userName + " OUT");
		
		userNameList = ArrayRemove(userNameList,userName);

		console.log("Current user: " + userNameList);
		console.log("Client disconnected.")
	});
});
function ArrayRemove(arr, value)
{
	return arr.filter((element)=>{
		return element != value;
	})
}
function Boardcast(data,name)
{
	for(var i = 0; i < wsList.length; i++)
	{
		if(userNameList[i] == userName)
		{
			wsList[i].send(name + ": " + data + "-=-0-=[]p[]p=p=0-=0");
		}
		else
		{
			wsList[i].send(name + ": " + data);
		}
	}
}
function CheckUser(data)
{
	for(var i = 0; i < wsList.length; i++)
	{
		if(wsList[i] == data)
		{
			return userNameList[i];
		}
	}
}
