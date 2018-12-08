//Revive player
//By Shappeh & Tezuni

function serverCmdResurrect(%client, %target)
{
	%target = FindClientByName(%target);

	if(!%client.isAdmin && !%client.isModerator)
	{
		return;
	}
	
	if(!isObject(%target))
	{
		messageClient(%client,"","\c6Player not found!");
		return;
	}


	if(!isObject(%target.player) && %target.hasSpawnedOnce)
	{
		%target.spawnPlayer();
		%target.addLives(1);
		%target.setDead(0);
		%target.spawnPlayer();
		announce("\c3" SPC %target.name SPC "\c6was \c2resurrected \c6by\c3" SPC %client.name);
		messageClient(%target,"","\c6You have been resurrected by \c3" @ %client.name @ "\c6.");
		return;
	}

	else if(isObject(%target.player))
	{
		messageClient(%client,"","\c3"@ %target.name @" \c6is still alive!");
		return;
	}
	
}

function serverCmdRes(%client, %target)
{
	serverCmdResurrect(%client, %target);
}

function serverCmdR(%client, %target)
{
	serverCmdResurrect(%client, %target);
}
