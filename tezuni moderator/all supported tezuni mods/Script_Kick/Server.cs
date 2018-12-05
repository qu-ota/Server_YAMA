//////////////////
//Kick Command//////
//////////////////
//Author: Tezuni//
//////////////////

function servercmdK(%client,%Player)
{
	%player = findClientByName(%player);

	if(%client.isAdmin)
	{
		if(%player.isAdmin || %player.isSuperAdmin)
			return messageClient(%client, '', "\c2You cannot kick other admins with this command.");

        if(!isObject(%player))
        	return messageClient(%client, '', "\c2Player not found.");
        	
        messageAll('MsgAdminForce', '\c2[\c3Admin\c2] \c3%1 \c2kicked \c3%2\c2(ID: %3)', %client.name, %player.name, %player.bl_id);
	    %player.delete("You were kicked by server staff.");
		return;	
	}

	if(%client.isModerator)
	{
		if(%client.ModeratorCommandSpam + 5 > $Sim::Time)
			return;
		
		%client.ModeratorCommandSpam = $Sim::Time;
		
		if(%player.isModerator || %player.isAdmin)
			return messageClient(%client, '', "\c6You cannot kick other server staff with this command.");

        if(!isObject(%player))
        	return messageClient(%client, '', "\c6Player not found.");

        messageAll('MsgAdminForce', '\c6[\c4MOD\c6] \c4%1 \c6kicked \c4%2\c6(ID: %3)', %client.name, %player.name, %player.bl_id);
		%player.delete("You were kicked by server staff.");
		return;	
	}
	
	commandToClient(%client, 'centerPrint', "\c3/K \c2is for server staff only.", 4);
}

